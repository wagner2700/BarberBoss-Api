using BarberBoss.Application.AutoMapper;
using BarberBoss.Application.UseCases.Bill;
using BarberBoss.Application.UseCases.Reports;
using BarberBoss.Application.UseCases.User;
using BarberBoss.Domain.Users;
using BarberBoss.Infraestructure.DataAcess;
using Microsoft.Extensions.DependencyInjection;


namespace BarberBoss.Application
{
    public static class DependencyInjectionExtension
    {
        
        public static void AddApplication(this IServiceCollection service)
        {
            
            AddUseCase(service);
            AutoMapper(service);

            
        }

        private static void AutoMapper( IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapping));
        }

        private static void AddUseCase(IServiceCollection service)
        {
            service.AddScoped<IRegisterBillUseCase, RegisterBillUseCase>();
            service.AddScoped<IDeleteBillUseCase, DeleteBillUseCase>();
            service.AddScoped<IUpdateBillUseCase, UpdateBillUseCase>();
            service.AddScoped<IBillGetByIdUseCase, BillGetByIdUseCase>();
            service.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            service.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
            service.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            service.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
            service.AddScoped<IDoLoginUseCase , DoLoginUseCase>();
            service.AddScoped< IGenerateBillReportUseCase ,GenerateBillReportUseCase >();
            service.AddScoped<IGenerateExpenseReportPdfUseCase, GenerateExpenseReportPdfUseCase>();
        
        }
    }
}
