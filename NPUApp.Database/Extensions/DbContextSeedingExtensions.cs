using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NPUApp.Database.Context;
using NPUApp.Database.Seed;

namespace NPUApp.Database.Extensions
{
    public static class DbContextSeedingExtensions
    {
        public static void SeedDbWithData(this IServiceCollection services)
        {
            using (var dbContext = new NpuAppDbContext())
            {
                DbInitializer.Initialize(dbContext);
            }
        }
    }
}
