using BarberBoss.Domain.Entities;
using BarberBoss.Infraestructure.DataAcess;
using CommonTestsLibraries.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Tests.Resources;


namespace CommonTestsLibraries
{
    public class CustomWebApplicatioFactory : WebApplicationFactory<Program>
    {

        public UserIdentityManager User_Team_Member {  get; set; }
        public UserIdentityManager User_Admin {  get; set; }
        public BillIdentityManager bill {  get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test")
                .ConfigureServices(services =>
                {
                    var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                    services.AddDbContext<BarberBossDbContext>(config =>
                    {
                        config.UseInMemoryDatabase("InMemoryDbForTesting");
                        config.UseInternalServiceProvider(provider);
                    });
                    var scope = services.BuildServiceProvider().CreateScope();
                    var dbContext = scope.ServiceProvider.GetRequiredService<BarberBossDbContext>();
                }); 
        }

        private void StartDatabase(BarberBossDbContext dbContext)
        {
            var _user = UserBuilder.Build();
            dbContext.Add(_user);
            dbContext.SaveChanges();

        }
    }
}
