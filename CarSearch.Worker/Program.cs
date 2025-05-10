using CarSearch.Worker.Data;
using CarSearch.Worker.Jobs;
using Quartz;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<DataContext>();
builder.Services.AddTransient<FetchLatestListingsJob>();

static void AddRepeatingQuartzJob<T>(IServiceCollectionQuartzConfigurator quartz, string cronSchedule) where T: IJob 
{
    JobKey jobKey = new(nameof(T));

    quartz.AddJob<T>(options => options.WithIdentity(jobKey));
    quartz.AddTrigger(options => options
        .ForJob(jobKey)
        .WithIdentity($"{nameof(T)}-Trigger")
        .WithCronSchedule(cronSchedule)
    );
}

builder.Services.AddQuartz(quartz =>
{
    AddRepeatingQuartzJob<FetchLatestListingsJob>(quartz, "0/5 * * * * ?");
}); 

builder.Services.AddQuartzHostedService((options) =>
{
    options.WaitForJobsToComplete = true;
});

var host = builder.Build();

host.Run();
