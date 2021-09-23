using Microsoft.AspNetCore.Builder;

namespace Anshan.Api
{
    public static class ErrorWrappingMiddlewareExtensions
    {
        public static IApplicationBuilder UseManagementProblemDetails(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ManagementErrorHandlingMiddleware>();
        }
        
        public static IApplicationBuilder UseApiProblemDetails(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiErrorHandlingMiddleware>();
        }
    }
}