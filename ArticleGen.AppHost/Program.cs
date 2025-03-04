var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ArticleGen_Web>("articlegen-web");

builder.Build().Run();
