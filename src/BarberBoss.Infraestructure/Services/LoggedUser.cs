using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Users;
using BarberBoss.Infraestructure.DataAcess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BarberBoss.Infraestructure.Services
{
    public class LoggedUser : ILoggedUser 
    {
        private readonly BarberBossDbContext _dbContext;
        private readonly ITokenProvider _tokenProvider;   

        public LoggedUser(BarberBossDbContext dbContext, ITokenProvider tokenProvider)
        {
            _dbContext = dbContext;
            _tokenProvider = tokenProvider;
        }
        public async Task<User?> Get()
        {
            var token = _tokenProvider.TokenOnRequest();

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(token);

            var userIdentifier =  securityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

            return await _dbContext.User.AsNoTracking().FirstOrDefaultAsync(user => user.UserIdentifier == Guid.Parse(userIdentifier));

             
        }
    }
}
