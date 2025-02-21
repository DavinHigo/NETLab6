using Client.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");


            var apiBaseUrl = builder.Configuration["ApiBaseUrl"];
            var baseAddress = string.IsNullOrEmpty(apiBaseUrl) ? builder.HostEnvironment.BaseAddress : apiBaseUrl;
            if (!Uri.TryCreate(baseAddress, UriKind.Absolute, out var validUri))
            {
                throw new InvalidOperationException("Invalid BaseAddress. Please check your configuration.");
            }

            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = validUri
            });

            builder.Services.AddScoped<StudentService>();

            await builder.Build().RunAsync();
        }
    }
}