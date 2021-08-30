using Microsoft.AspNetCore.Builder;

namespace Anshan.Api
{
    public static class ErrorWrappingMiddlewareExtensions
    {
        public static IApplicationBuilder UseProblemDetails(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}