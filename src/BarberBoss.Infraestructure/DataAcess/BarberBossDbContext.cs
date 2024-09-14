using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BarberBoss.Infraestructure.DataAcess
{
    public class BarberBossDbContext : DbContext
    {

        public BarberBossDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Bill> Fatura {  get; set; }
        public DbSet<User> User {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Domain.Entities.Tag>().ToTable("Tags");
        }
    }
}
