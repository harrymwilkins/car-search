using CarSearch.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CarSearch.Worker.Data;

public class DataContext: DbContext
{
    public DataContext(DbContextOptions options): base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string? connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        if (connectionString == null)
        {
            throw new Exception("Cannot configure data context - CONNECTION_STRING not set");
        }

        options.UseNpgsql(connectionString);
    }

    public required DbSet<Listing> Listings { get; set; }
}