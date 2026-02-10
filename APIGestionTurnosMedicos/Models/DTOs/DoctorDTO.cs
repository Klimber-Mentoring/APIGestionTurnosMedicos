namespace APIGestionTurnosMedicos.Models.DTOs
{
    public class DoctorDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Especialidad { get; set; }
    }
}
