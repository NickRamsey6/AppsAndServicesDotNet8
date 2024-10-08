using Humanizer; // To use ToWords extension method
using Microsoft.Azure.Functions.Worker.Http; // To use HttpResponseData
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker; //  To use [HttpTrigger] 
using Microsoft.Extensions.Logging; // To use ILogger

namespace Northwind.AzureFunctions.Service;

public class NumbersToWordsFunction
{
    private readonly ILogger _logger;

    public NumbersToWordsFunction(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<NumbersToWordsFunction>();
    }

    [Function(nameof(NumbersToWordsFunction))]
    public HttpResponseData Run(
        [HttpTrigger(AuthorizationLevel.Anonymous,
        "get", "post", Route = null)] HttpRequestData req)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        string? amount = req.Query["amount"];

        HttpResponseData response;

        if (long.TryParse(amount, out long number))
        {
            response = req.CreateResponse(System.Net.HttpStatusCode.OK);
            //response.WriteString(number.ToWords());
            response.WriteStringAsync(number.ToWords());
        }
        else
        {
            response = req.CreateResponse(System.Net.HttpStatusCode.BadRequest);
            //response.WriteString($"Failed to parse: {amount}");
            response.WriteStringAsync($"Failed to parse: {amount}");
        }

        return response;
    }
}
