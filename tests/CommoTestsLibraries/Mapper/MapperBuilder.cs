using AutoMapper;
using BarberBoss.Application.AutoMapper;

namespace CommonTestsLibraries.Mapper
{
    public static class MapperBuilder
    {

        public static IMapper Build()
        {
            var config = new MapperConfiguration(config =>
            {
                config.AddProfile<AutoMapping>();
            });

            return config.CreateMapper();
        }
    }
}
