using System.Net.Http;
using Anshan.Test.Faker;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Anshan.Test
{
    public abstract class WebApplicationFactoryBase<TStartup> : IClassFixture<WebApplicationFactory<TStartup>>
        where TStartup : class
    {
        protected readonly ContextData ContextData = new();
        protected readonly HttpClient HttpClient;

        public WebApplicationFactoryBase(WebApplicationFactory<TStartup> factory)
        {
            //System.Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
            HttpClient = factory.CreateClient();
        }
    }
}