using BarberBoss.Infraestructure.DataAcess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infraestructure.Migrations
{
    public static class DataBaseMigration
    {
        public async static Task MigrateDatabase(IServiceProvider service)
        {
            var dbContext = service.GetRequiredService<BarberBossDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
