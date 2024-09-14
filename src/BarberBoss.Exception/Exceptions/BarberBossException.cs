namespace BarberBoss.Infraestructure.Exceptions
{
    public abstract class BarberBossException : SystemException
    {
        public abstract int StatusCode { get; }
        public abstract List<string> GetErrors();
        protected BarberBossException(string message) : base(message) 
        {
            
        }
    }
}
