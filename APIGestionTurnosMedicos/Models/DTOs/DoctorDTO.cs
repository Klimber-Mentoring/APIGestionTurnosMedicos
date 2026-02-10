namespace APIGestionTurnosMedicos.Models.DTOs
{
    public class DoctorDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Dni { get; set; }
        public string Especialidad { get; set; }

        public DoctorDTO(Guid id, string nombre, string apellido, string dni, string especialidad) 
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Dni = dni;
            Especialidad = especialidad;        
        }
    }
}
