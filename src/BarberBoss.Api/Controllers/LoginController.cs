using BarberBoss.Application.UseCases.User;
using BarberBoss.Communication.Request;
using BarberBoss.Communication.Response;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace BarberBoss.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {



        [HttpPost]
        [ProducesResponseType(typeof(ResponseUserJson) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromServices]IDoLoginUseCase useCase , [FromBody] RequestLoginJson request)
        {
            var result = await useCase.Login(request);

            return Ok(result);  
        }
    }
}
