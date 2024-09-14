using BarberBoss.Application.UseCases.User.Validator;
using BarberBoss.Communication.Request;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Security;
using BarberBoss.Domain.Users;
using BarberBoss.Exception;
using BarberBoss.Infraestructure.DataAcess;
using BarberBoss.Infraestructure.Exceptions;

namespace BarberBoss.Application.UseCases.User
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IPasswordEncrypter _passwordEncrypter;
        private readonly IUserReadOnlyRepository _userReadOnlyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWriteRegisterUserRepository _writeRegisterUserRepository;


        public ChangePasswordUseCase(ILoggedUser loggedUser, IPasswordEncrypter passwordEncrypter , IUserReadOnlyRepository userReadOnlyRepository , IUnitOfWork unitOfWork, IWriteRegisterUserRepository writeRegisterUserRepository)
        {
            _loggedUser = loggedUser;
            _passwordEncrypter = passwordEncrypter;
            _userReadOnlyRepository = userReadOnlyRepository;
            _unitOfWork = unitOfWork;
            _writeRegisterUserRepository = writeRegisterUserRepository;
        }

        public async Task Execute(ChangePasswordRequest request)
        {
            var loggedUser = await _loggedUser.Get();

            
            Validate(request , loggedUser);

            var user = await _userReadOnlyRepository.GetById(loggedUser.Id);
            user.Password = request.NewPassword;
            _writeRegisterUserRepository.Update(user);
            await _unitOfWork.Commit();




        }

        private void Validate(ChangePasswordRequest request , BarberBoss.Domain.Entities.User user)
        {
            var validator = new ChangePasswordValidator();

            var result = validator.Validate(request);

            var passwordMatches = _passwordEncrypter.VerifyPassword(request.Password , user.Password);

            if(passwordMatches == false)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure( string.Empty , ResourceErrorMessages.SenhaDeveSerIgual));
            }

            if(result.IsValid == false)
            {
                var errors = result.Errors.Select(e => e.ErrorMessage).ToList();

                throw new ErrorOnValidatorException(errors);
            }
        }
    }
}
