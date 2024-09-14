using BarberBoss.Infraestructure.Exceptions;
using System.Net;

namespace BarberBoss.Exception.Exceptions
{
    public class InvalidLoginException : BarberBossException
    {

        public override int StatusCode => (int)HttpStatusCode.Unauthorized;

        public InvalidLoginException(string errorMessage) : base(errorMessage){}

        public override List<string> GetErrors()
        {
            return new List<string>() { Message};
        }
    }
}
