using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BarberBoss.Infraestructure.DataAcess.Repository
{
    public class UserRepository : IWriteRegisterUserRepository , IUserReadOnlyRepository
    {
        private readonly BarberBossDbContext _dbContext;

        public UserRepository(BarberBossDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> EmailExist(string email)
        {
           return await _dbContext.User.AnyAsync(user => user.Email.Equals(email));
        }

        public async Task Execute(User user)
        {
            await _dbContext.AddAsync(user);
        }

        public async Task<Domain.Entities.User?> GetByEmail(string email)
        {
            return await _dbContext.User.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
        }

        public async Task<User?> GetById(long id)
        {
            return await _dbContext.User.FirstOrDefaultAsync(user => user.Id == id);
        }

        public void Update(User user)
        {
             _dbContext.User.Update(user);
        }
    }
}
