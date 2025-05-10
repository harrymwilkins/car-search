using CarSearch.Domain;
using CarSearch.Worker.Data;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace CarSearch.Worker.Jobs;

[DisallowConcurrentExecution]
public sealed class FetchLatestListingsJob: IJob 
{
    private readonly ILogger<FetchLatestListingsJob> _logger;
    private readonly DataContext _dataContext;

    public FetchLatestListingsJob(ILogger<FetchLatestListingsJob> logger, DataContext dataContext)
    {
        _logger = logger;
        _dataContext = dataContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation($"Executing {nameof(FetchLatestListingsJob)}");

        Listing? listing = await _dataContext.Listings
            .Where(l => l.Name == "testing")
            .SingleOrDefaultAsync();

        if (listing == null)
        {
            await _dataContext.Listings.AddAsync(new Listing { Name = "testing" });
            await _dataContext.SaveChangesAsync();

            _logger.LogInformation("Created and saved new listing");
        }
        else
        {
            _logger.LogInformation($"Listing found with ID {listing.ListingId}");
        }
    }
}