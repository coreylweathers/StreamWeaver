using Azure.Storage.Queues;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace StreamProcessorFx;

public class Function1
{
    private readonly ILogger<Function1> _logger;
    private readonly IConfiguration _configuration;

    public Function1(ILogger<Function1> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    // ...

    [Function("StreamProcessor")]
    public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequest req)
    {
        using var reader = new StreamReader(req.Body);
        string message = await reader.ReadToEndAsync();

        // TODO: Figure out how to get connection string from Aspire-injected configuration
        string? connectionString = _configuration.GetValue<string>("Aspire:Azure:Storage:Queues:streamprocessor:ConnectionString");

        if (string.IsNullOrEmpty(connectionString))
        {
            _logger.LogError("Connection string for 'streamprocessor' is null or empty.");
            return new BadRequestObjectResult("Configuration error: Connection string is missing.");
        }

        var queueClient = new QueueClient(connectionString, "streamprocessor");

        await queueClient.CreateIfNotExistsAsync();
        await queueClient.SendMessageAsync(message);

        _logger.LogInformation("Message added to queue: {Message}", message);
        return new OkObjectResult("Message added to queue.");
    }
}