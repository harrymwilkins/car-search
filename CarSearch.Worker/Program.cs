using CarSearch.Worker;
using CarSearch.Worker.Data;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>();
builder.Services.AddDbContext<DataContext>();

var host = builder.Build();
host.Run();
