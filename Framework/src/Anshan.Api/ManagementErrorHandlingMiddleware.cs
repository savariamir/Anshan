using System;
using System.Threading.Tasks;
using Anshan.Application.Exceptions;
using Anshan.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Anshan.Api
{
    /// <summary>
    /// </summary>
    public class ManagementErrorHandlingMiddleware
    {
        private readonly ILogger<ManagementErrorHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;

        /// <summary>
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ManagementErrorHandlingMiddleware(RequestDelegate next, ILogger<ManagementErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var problemDetails = exception switch
            {
                CustomApplicationException applicationException => new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "One or more application errors occurred",
                    Status = applicationException.StatusCode,
                    Extensions =
                    {
                        ["errors"] = applicationException.Results
                    },
                    Detail = "Application error. More information can be found in the errors."
                },
                DomainException domainException => new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "One or more validation errors occurred",
                    Status = domainException.StatusCode,
                    Extensions =
                    {
                        ["errors"] = domainException.Message
                    },
                    Detail = "The request contains invalid parameters. More information can be found in the errors."
                },
                InfrastructureException infrastructureException => new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "One or more infrastructure errors occurred",
                    Status = infrastructureException.StatusCode,
                    Extensions =
                    {
                        ["errors"] = infrastructureException.Message
                    },
                    Detail = "Infrastructure error. More information can be found in the errors."
                },
                _ => new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = exception.Message,
                    Status = StatusCodes.Status500InternalServerError,
                    Extensions =
                    {
                        ["errors"] = exception.Data
                    }
                }
            };


            if (problemDetails.Status==StatusCodes.Status500InternalServerError)
            {
                _logger.LogError(exception.Message);
            }

            var result = JsonConvert.SerializeObject(problemDetails,
                new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore
                });

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = problemDetails.Status.Value;

            context.Response.GetTypedHeaders().CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };

            return context.Response.WriteAsync(result);
        }
    }
}