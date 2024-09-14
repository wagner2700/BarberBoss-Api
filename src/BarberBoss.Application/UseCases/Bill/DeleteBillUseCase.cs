
using BarberBoss.Domain.Bills;
using BarberBoss.Domain.Users;
using BarberBoss.Exception;
using BarberBoss.Exception.Exceptions;
using BarberBoss.Infraestructure.DataAcess;

namespace BarberBoss.Application.UseCases.Bill
{
    public class DeleteBillUseCase : IDeleteBillUseCase
    {
        private readonly IBillWriteOnlyRepository _repositoy;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggedUser _loggedUser;


        public DeleteBillUseCase(IBillWriteOnlyRepository repositoy, IUnitOfWork unitOfWork, ILoggedUser loggedUser)
        {
            _repositoy = repositoy;
            _unitOfWork = unitOfWork;
            _loggedUser = loggedUser;
        }
        public async Task Execute(long id)
        {
            var loggedUser = await _loggedUser.Get(); 


            await _repositoy.Delete(loggedUser!, id);

            await _unitOfWork.Commit();
        }
    }
}
