using Aspire.Hosting.Azure;

var builder = DistributedApplication.CreateBuilder(args);

var storage = builder.AddAzureStorage("storage")
    .RunAsEmulator();

var signalR = builder.AddAzureSignalR("signalr", AzureSignalRServiceMode.Serverless)
                     .RunAsEmulator();

var streamProcessorQueue = storage.AddQueues("streamprocessor");

builder.AddAzureFunctionsProject<Projects.StreamProcessorFx>("streamprocessorfx")
    .WithExternalHttpEndpoints()
    .WithReference(streamProcessorQueue)
    .WaitFor(streamProcessorQueue);

builder.AddAzureFunctionsProject<Projects.ContentGeneratorFx>("contentgeneratorfx")
    .WithExternalHttpEndpoints();

builder.AddAzureFunctionsProject<Projects.NotificationFx>("notificationfx")
    .WithExternalHttpEndpoints()
    .WithReference(signalR)
    .WaitFor(signalR);

builder.AddProject<Projects.web>("web");


builder.Build().Run();
