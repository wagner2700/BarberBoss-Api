using BarberBoss.Infraestructure.Exceptions;
using System.Net;

namespace BarberBoss.Exception.Exceptions
{
    public class RegisterNotFoundEception : BarberBossException
    {

        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public RegisterNotFoundEception(string errorMessage) : base(errorMessage){}

        public override List<string> GetErrors()
        {
            return new List<string>() { Message};
        }
    }
}
