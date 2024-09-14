namespace BarberBoss.Application.UseCases.Reports
{
    public interface IGenerateExpenseReportPdfUseCase
    {
        Task<byte[]> Execute(DateOnly date);
    }
}
