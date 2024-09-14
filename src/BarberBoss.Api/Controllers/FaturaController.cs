using BarberBoss.Application.UseCases.Bill;
using BarberBoss.Communication.Request;
using BarberBoss.Communication.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FaturaController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(typeof(ResponseBillJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult>   RegistrarFatura([FromBody]RequestBillJson request , [FromServices]IRegisterBillUseCase useCase)
        {
            var response = await useCase.Registrar(request);
            return Created(string.Empty , response); 
        }


        [HttpGet]
        [Route("id")]
        [ProducesResponseType(typeof(ResponseBillJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBillById(long id , [FromServices] IBillGetByIdUseCase useCase)
        {
            var response = await useCase.GetById(id);
            return Ok(response);
        }

        [HttpDelete]
        [Route("id")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(long id, [FromServices] IDeleteBillUseCase useCase)
        {
            await useCase.Execute(id);
            return NoContent();
        }

        [HttpPut]
        [Route("id")]
        [ProducesResponseType(typeof(ResponseBillJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(long id, [FromBody] RequestBillJson request , [FromServices] IUpdateBillUseCase useCase)
        {
            await useCase.Update(id, request);
            return NoContent();
        }



    }
}
