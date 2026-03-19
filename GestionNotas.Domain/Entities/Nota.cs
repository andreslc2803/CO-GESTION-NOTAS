namespace GestionNotas.Api.Domain.Entities
{
    public class Nota
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }
        public int IdProfesor { get; private set; }
        public int IdEstudiante { get; private set; }
        public decimal Valor { get; private set; }

        public static Nota CreateNota(
            string nombre,
            int idProfesor,
            int idEstudiante,
            decimal valor)
        {
            return new Nota
            {
                Nombre = nombre,
                IdProfesor = idProfesor,
                IdEstudiante = idEstudiante,
                Valor = valor
            };
        }

        public Profesor Profesor { get; set; }
        public Estudiante Estudiante { get; set; }
    }
}
