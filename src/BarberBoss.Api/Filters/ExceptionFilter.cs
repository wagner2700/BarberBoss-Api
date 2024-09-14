using BarberBoss.Communication.Response;
using BarberBoss.Exception.Exceptions;
using BarberBoss.Infraestructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BarberBoss.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if(context.Exception is BarberBossException)
            {
                HandleProjectException(context);
            }
            else
            {
                ThrowUnknowError(context);
            }
        }

        private void HandleProjectException(ExceptionContext context)
        {
            var barberBoss = context.Exception as BarberBossException;
            var errorResponse = new ResponseErrorJson(barberBoss!.GetErrors());

            

            context.HttpContext.Response.StatusCode = barberBoss.StatusCode;
            context.Result = new ObjectResult(errorResponse);       
        }

        private void ThrowUnknowError(ExceptionContext context)
        {
            var responseError = new ResponseErrorJson("Erro desconhecido");
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            context.Result = new ObjectResult(responseError);


        }
    }

   
}
