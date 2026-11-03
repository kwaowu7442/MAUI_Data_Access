var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.MAUI_Data_Acces_ApiService>("apiservice");

builder.AddProject<Projects.MAUI_Data_Acces_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
