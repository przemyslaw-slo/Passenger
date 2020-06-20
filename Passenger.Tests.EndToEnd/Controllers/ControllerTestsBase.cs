using System.Net.Http;
using System.Text;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Passenger.Api;

namespace Passenger.Tests.EndToEnd.Controllers
{
    public abstract class ControllerTestsBase
    {
        protected readonly IHost Host;
        protected readonly HttpClient Client;

        protected ControllerTestsBase()
        {
            // .NET Core 2.x
            //var webHostBuilder = new WebHostBuilder()
            //    .UseStartup<Startup>();

            //_server = new TestServer(webHostBuilder);
            //_client = _server.CreateClient();

            // .Net Core 3.x
            var hostBuilder = new HostBuilder()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHost(webBuilder =>
                {
                    // Add TestServer
                    webBuilder.UseTestServer();
                    webBuilder.UseStartup<Startup>();
                });

            // Build and start the IHost
            Host = hostBuilder.Start();

            // Create an HttpClient to send requests to the TestServer
            Client = Host.GetTestClient();
        }

        protected static StringContent GetPayload(object data)
        {
            var json = JsonConvert.SerializeObject(data);

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
