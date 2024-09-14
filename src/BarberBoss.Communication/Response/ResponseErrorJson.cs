namespace BarberBoss.Communication.Response
{
    public class ResponseErrorJson
    {
        public List<string> ErrorMessages {  get; set; } 

        
        public ResponseErrorJson(List<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
        // Instanciar e incluir o erro caso receba apenas 1 erro no construtor
        public ResponseErrorJson(string errorMessage)
        {
            ErrorMessages = new List<string> { errorMessage };
        }
    }
}
