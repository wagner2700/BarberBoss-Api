namespace BarberBoss.Infraestructure.DataAcess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BarberBossDbContext _context;

        public UnitOfWork(BarberBossDbContext context)
        {
            _context = context;
        }

        
        public async Task Commit()
        {
             await _context.SaveChangesAsync();
        }
    }
}
