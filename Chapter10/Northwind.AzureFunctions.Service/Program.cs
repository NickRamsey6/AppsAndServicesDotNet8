using System.Net.Http.Headers; // To use MediaTypeWithQualityHeaderValue
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection; // To use AddHttpClient()
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddHttpClient(name: "Amazon",
            configureClient: options =>
            {
                options.BaseAddress = new Uri("https://wwww.amazon.com");

                // Pretend to be Chrome with US English
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("text/html"));
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("image/avif"));
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("image/webp"));
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("image/apng"));
                options.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("*/*", 0.8));

                options.DefaultRequestHeaders.AcceptLanguage.Add(
                    new StringWithQualityHeaderValue("en-us"));
                options.DefaultRequestHeaders.AcceptLanguage.Add(
                    new StringWithQualityHeaderValue("en", 0.8));

                options.DefaultRequestHeaders.UserAgent.Add(
                    new(productName: "Chrome", productVersion: "114.0.5735.91"));
            });
    })
    .Build();
host.Run();
