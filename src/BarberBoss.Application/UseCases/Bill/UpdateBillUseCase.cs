using AutoMapper;
using BarberBoss.Application.UseCases.Bill.Validator;
using BarberBoss.Communication.Request;
using BarberBoss.Domain.Bills;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Users;
using BarberBoss.Exception;
using BarberBoss.Exception.Exceptions;
using BarberBoss.Infraestructure.DataAcess;
using BarberBoss.Infraestructure.Exceptions;

namespace BarberBoss.Application.UseCases.Bill
{
    internal class UpdateBillUseCase : IUpdateBillUseCase
    {
        private readonly IBillUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;

        public UpdateBillUseCase(IBillUpdateOnlyRepository repository, IUnitOfWork unitOfWork, IMapper mapper , ILoggedUser loggedUser)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggedUser = loggedUser;
        }


        public async Task Update(long id, RequestBillJson request)
        {
            Validate(request);
            var loggedUser = await _loggedUser.Get();
            var bill = await _repository.GetById(loggedUser, id);
           

            if (bill == null )
            {
                throw new RegisterNotFoundEception(ResourceErrorMessages.RegistroNaoEncontrado);
            }
            bill.Tags.Clear();
            _mapper.Map(request , bill);
            

            _repository.Update(bill);
            await _unitOfWork.Commit();
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
