using AutoMapper;
using BarberBoss.Application.UseCases.Bill.Validator;
using BarberBoss.Communication.Request;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Bills;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Users;
using BarberBoss.Infraestructure.DataAcess;
using BarberBoss.Infraestructure.Exceptions;

namespace BarberBoss.Application.UseCases.Bill
{
    public class RegisterBillUseCase : IRegisterBillUseCase
    {
        private readonly IBillWriteOnlyRepository _repositoryWriteOnly;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUser _loggedUser;


        public RegisterBillUseCase(IBillWriteOnlyRepository repositoryWriteOnly, IMapper mapper, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
        {
            _repositoryWriteOnly = repositoryWriteOnly;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _loggedUser = loggedUser;

        }
        public async Task<ResponseBillJson> Registrar(RequestBillJson request)
        {
            Validate(request);
            var fatura = _mapper.Map<Domain.Entities.Bill>(request);
            var loggedUser = await _loggedUser.Get();
            fatura.UserId = loggedUser!.Id;
            await _repositoryWriteOnly.RegisterBill(fatura);
            await _unitOfWork.Commit();
            return _mapper.Map<ResponseBillJson>(fatura);

        }


        private void Validate(RequestBillJson request)
        {
            var validate = new BillValidator();

            var result = validate.Validate(request);
            // Pegar mensagem
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            if (result.IsValid == false)
            {
                throw new ErrorOnValidatorException(errorMessages);
            }
        }
    }
}
