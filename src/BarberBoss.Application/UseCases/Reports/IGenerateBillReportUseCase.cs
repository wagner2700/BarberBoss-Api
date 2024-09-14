namespace BarberBoss.Application.UseCases.Reports
{
    public interface IGenerateBillReportUseCase
    {
        Task<byte[]> Execute(DateOnly month);
    }
}
