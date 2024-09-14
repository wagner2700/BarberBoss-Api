using AutoMapper;
using BarberBoss.Communication.Response;
using BarberBoss.Domain.Bills;
using BarberBoss.Exception;
using BarberBoss.Exception.Exceptions;

namespace BarberBoss.Application.UseCases.Bill
{
    public class BillGetByIdUseCase : IBillGetByIdUseCase
    {
        private readonly IBillReadOnlyRepository _onlyRepository;
        private readonly IMapper _mapper;

        public BillGetByIdUseCase(IBillReadOnlyRepository onlyRepository, IMapper mapper)
        {
            _onlyRepository = onlyRepository;  
            _mapper = mapper;
        }

        public async Task<ResponseBillJson> GetById(long id)
        {
            var result = await _onlyRepository.GetById(id);
            
            if(result is null)
            {
                throw new RegisterNotFoundEception(ResourceErrorMessages.RegistroNaoEncontrado);
            }
            return _mapper.Map<ResponseBillJson>(result);
        }
    }
}
