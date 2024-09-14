using BarberBoss.Communication.Request;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Security;
using BarberBoss.Domain.Token;
using BarberBoss.Domain.Users;
using BarberBoss.Exception;
using BarberBoss.Exception.Exceptions;

namespace BarberBoss.Application.UseCases.User
{
    public class DoLoginUseCase : IDoLoginUseCase
    {

        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IPasswordEncrypter _passwordEncrypter;
        private readonly ITokenGenerator _tokenGenerator;
        


        public DoLoginUseCase(IUserReadOnlyRepository readOnlyRepository, IPasswordEncrypter passwordEncrypter, ITokenGenerator tokenGenerator)
        {
            _passwordEncrypter = passwordEncrypter;
            _readOnlyRepository = readOnlyRepository;
            _tokenGenerator = tokenGenerator;
        }


        public async Task<ResponseUserJson> Login(RequestLoginJson request)
        {
            var user = await _readOnlyRepository.GetByEmail(request.Email);

            if(user is null)
            {
                throw new InvalidLoginException(ResourceErrorMessages.LoginNaoAutorizado);
            }

            var passwordMatch = _passwordEncrypter.VerifyPassword(request.Password, user.Password);

            if(passwordMatch == false)
            {
                throw new InvalidLoginException(ResourceErrorMessages.LoginNaoAutorizado);
            }

            return new ResponseUserJson
            {
                Email = request.Email,
                Name = user.Name,
                Token = _tokenGenerator.GenerateToken(user)
            };
        }
    }
}
