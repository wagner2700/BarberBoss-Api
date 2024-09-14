using System.Net;

namespace BarberBoss.Infraestructure.Exceptions
{
    public class ErrorOnValidatorException : BarberBossException
    {

        public  readonly List<string> _errors;

        public override int StatusCode => (int)HttpStatusCode.BadRequest;

       

        public ErrorOnValidatorException(List<string> errorMessges)  : base(string.Empty)
        {
            _errors = errorMessges;
            

        }

        public override List<string> GetErrors() { return _errors; }
    }
}
