
using BarberBoss.Application.UseCases.User.Validator;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Users;
using BarberBoss.Exception;
using BarberBoss.Infraestructure.DataAcess;
using BarberBoss.Infraestructure.Exceptions;

namespace BarberBoss.Application.UseCases.User
{
    public class UpdateUserUseCase : IUpdateUserUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserReadOnlyRepository _onlyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUserUseCase(ILoggedUser loggedUser, IUserReadOnlyRepository onlyRepository, IUnitOfWork unitOfWork)
        {
            _loggedUser = loggedUser;
            _onlyRepository = onlyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(RequestUpdateUserJson request)
        {
            var loggedUser = await _loggedUser.Get();

          

            await Validate(request, loggedUser!.Email);
            var user = await _onlyRepository.GetById(loggedUser.Id);

            user.Name  = request.Name;
            user.Email = request.Email;

            await _unitOfWork.Commit();

        }


        private async Task Validate(RequestUpdateUserJson request , string currentEmail)
        {
            var validator = new UpdateUserValidator();

            var result  =  validator.Validate(request);

            if(currentEmail.Equals(request.Email) == false)
            {
                var userExit = await _onlyRepository.GetByEmail(request.Email);
                if (userExit != null)
                {
                    result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceErrorMessages.EmailJaCadastrado));
                }
            }

            if(result.IsValid == false)
            {
                var errorMessage = result.Errors.Select(x => x.ErrorMessage).ToList();

                throw new ErrorOnValidatorException(errorMessage);
            }
        }
    }
}
