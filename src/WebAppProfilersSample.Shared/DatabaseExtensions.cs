using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppProfilersSample.Shared.Data;
using WebAppProfilersSample.Shared.Data.Entities;

namespace WebAppProfilersSample.Shared
{
    public static class DatabaseExtensions
    {
        public static async Task EnsureDbReadyForTestsAsync(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ProfilingSampleDbContext>();
                await context.Database.EnsureCreatedAsync();
                if (!await context.Somes.AnyAsync())
                {
                    await context.Somes.AddRangeAsync(CreateSomeEntities());
                    await context.SaveChangesAsync();
                }
            }
        }

        private static IEnumerable<SomeEntity> CreateSomeEntities()
        {
            var entities = new List<SomeEntity>();
            for (var i = 0; i < 100; ++i)
            {
                var otherEntities = new List<SomeOtherEntity>();
                for (var j = 0; j < 1000; ++j)
                {
                    otherEntities.Add(new SomeOtherEntity { Description = $"{nameof(SomeOtherEntity)} {i} {j}" });
                }
                entities.Add(new SomeEntity { Description = $"{nameof(SomeEntity)} {i}", Others = otherEntities });
            }
            return entities;
        }
    }
}
