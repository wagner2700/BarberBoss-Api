using BarberBoss.Domain.Bills;
using BarberBoss.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infraestructure.DataAcess.Repository
{
    public class RegistrarFaturaRepository :  IBillReadOnlyRepository , IBillWriteOnlyRepository , IBillUpdateOnlyRepository
    {
        private readonly BarberBossDbContext _context;


        public RegistrarFaturaRepository(BarberBossDbContext context)
        {
            _context = context;
        }

        public async Task Delete( User user, long id)
        {
            var result = await _context.Fatura.FirstOrDefaultAsync(fatura => fatura.Id == id && fatura.UserId == user.Id);  
            if(result != null)
            {
                _context.Fatura.Remove(result);
            }
            
            
        }

        async Task<Bill?> IBillReadOnlyRepository.GetById(long id)
        {
            return await GetFullBill()
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        async Task<Bill?> IBillUpdateOnlyRepository.GetById(User user,long id)
        {
            return await _context.Fatura.FirstOrDefaultAsync(f => f.Id == id && f.UserId == user.Id);

             
        }

        public async Task RegisterBill(Bill fatura)
        {
            await _context.Fatura.AddAsync(fatura);
            
        }

        public void Update(Bill fatura)
        {
            _context.Fatura.Update(fatura);
        }

        private Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Bill, ICollection<Tag>> GetFullBill()
        {
            return _context.Fatura
                 .Include(bill => bill.Tags);
        }

        public async Task<List<Bill>> GetByMonth(DateOnly date)
        {
            var startDate = new DateTime(year: date.Year , month: date.Month, day: 1 ).Date;
            var daysInMonth = DateTime.DaysInMonth(year: date.Year , month: date.Month);
            var endDate = new DateTime(year: date.Year , month: date.Month, day: daysInMonth , hour: 23, minute: 59 , second: 59);
            

           return await _context.Fatura
                .AsNoTracking()
                .Where(bill => bill.Date >= startDate && bill.Date <= endDate)
                .OrderBy(bill => bill.Date)
                .ThenBy(bill => bill.Description)
                .ToListAsync();
        }
    }
}
