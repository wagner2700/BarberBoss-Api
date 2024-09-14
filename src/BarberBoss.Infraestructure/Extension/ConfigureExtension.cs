using Microsoft.Extensions.Configuration;

namespace BarberBoss.Infraestructure.Extension
{
    public static class ConfigureExtension
    {

        public static bool IsTestEnviroment(this IConfiguration configuration)
        {
            return configuration.GetValue<bool>("inMemoryTest");
        }
    }
}
