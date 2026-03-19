using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNotas.Api.Domain.Entities
{
    public class Profesor
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public ICollection<Nota> Notas { get; set; } = new List<Nota>();

        private Profesor() { }

        public Profesor(string nombre)
        {
            Nombre = nombre;
        }

        public void Update(string nombre)
        {
            Nombre = nombre;
        }
    }
}
