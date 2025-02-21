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

            // Get the ApiBaseUrl from configuration
            var apiBaseUrl = builder.Configuration["ApiBaseUrl"];

            // Use the ApiBaseUrl if provided, otherwise fall back to the app's base address
            var baseAddress = string.IsNullOrEmpty(apiBaseUrl) ? builder.HostEnvironment.BaseAddress : apiBaseUrl;

            // Ensure the base address is a valid URI
            if (!Uri.TryCreate(baseAddress, UriKind.Absolute, out var validUri))
            {
                throw new InvalidOperationException("Invalid BaseAddress. Please check your configuration.");
            }

            // Add HttpClient with the resolved base address
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = validUri
            });

            builder.Services.AddScoped<StudentService>();

            await builder.Build().RunAsync();
        }
    }
}