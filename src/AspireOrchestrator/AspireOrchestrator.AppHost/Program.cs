using Aspire.Hosting.Azure;

var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("storage")
    .RunAsEmulator();

var signalR = builder.AddAzureSignalR("signalr", AzureSignalRServiceMode.Serverless)
                     .RunAsEmulator();

var streamProcessorQueue = storage.AddQueues("streamprocessorqueues");

builder.AddAzureFunctionsProject<Projects.StreamProcessorFx>("streamprocessorfx")
    .WithExternalHttpEndpoints()
    .WithReference(streamProcessorQueue)
    .WaitFor(streamProcessorQueue)
    .WithArgs("--host", "localhost", "--port", "7208");

builder.AddAzureFunctionsProject<Projects.ContentGeneratorFx>("contentgeneratorfx")
    .WithExternalHttpEndpoints()
    .WithArgs("--host", "localhost", "--port", "7110");

builder.AddAzureFunctionsProject<Projects.NotificationFx>("notificationfx")
    .WithExternalHttpEndpoints()
    .WithReference(signalR)
    .WaitFor(signalR)
    .WithArgs("--host", "localhost", "--port", "7031");

builder.AddProject<Projects.web>("web");

builder.Build().Run();
