using APIGestionTurnosMedicos.Enums;

namespace APIGestionTurnosMedicos.Models.Entities
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public Especialidad Especialidad { get; set; }
    }
}
