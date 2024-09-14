namespace BarberBoss.Infraestructure.DataAcess
{
    public interface IUnitOfWork
    {

        Task Commit();
    }
}
