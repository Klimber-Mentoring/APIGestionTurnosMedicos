namespace APIGestionTurnosMedicos.Models.Entities
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
