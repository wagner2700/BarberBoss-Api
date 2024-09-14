using AutoMapper;
using BarberBoss.Application.UseCases.User.Validator;
using BarberBoss.Communication.Request;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Security;
using BarberBoss.Domain.Token;
using BarberBoss.Domain.Users;
using BarberBoss.Exception;
using BarberBoss.Infraestructure.DataAcess;
using BarberBoss.Infraestructure.Exceptions;

namespace BarberBoss.Application.UseCases.User
{
    public class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IWriteRegisterUserRepository _repository;
        private readonly IUserReadOnlyRepository _readOnlyRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordEncrypter _passwordEncrypter;
        private readonly ITokenGenerator _tokenGenerator;

        public RegisterUserUseCase(IWriteRegisterUserRepository repository, 
            IMapper mapper, IUnitOfWork unitOfWork, 
            IPasswordEncrypter passwordEncrypter,
            IUserReadOnlyRepository readOnlyRepository,
            ITokenGenerator tokenGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _passwordEncrypter = passwordEncrypter;
            _readOnlyRepository = readOnlyRepository;
            _tokenGenerator = tokenGenerator;
        }


        public async Task<ResponseUserJson> Execute(UserRequestJson request)
        {
            await Validate(request);

            var user = _mapper.Map<BarberBoss.Domain.Entities.User>(request);

            var passwordEncrpter = _passwordEncrypter.Encrypt(request.Password);

            user.Password = passwordEncrpter;
            user.UserIdentifier = Guid.NewGuid();
            await _repository.Execute(user);
            await _unitOfWork.Commit();

            return new ResponseUserJson
            {
                Email = user.Email,
                Name = user.Name,
                Token = _tokenGenerator.GenerateToken(user)
            };     
        }


        private async Task Validate(UserRequestJson request)
        {
            var validate = new UserValidator();

            var result = validate.Validate(request);
            var emailExist = await _readOnlyRepository.EmailExist(request.Email);

            if (emailExist)
            {
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty,ResourceErrorMessages.EmailCadastrado));
            }

            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            if(result.IsValid is false)
            {
                throw new ErrorOnValidatorException(errorMessages);
            }
        }
    }
}
