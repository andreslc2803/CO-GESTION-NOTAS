using System;
using System.Collections.Generic;
using System.Text;

namespace GestionNotas.Api.Domain.Entities
{
    public class Estudiante
    {
        public int Id { get; private set; }
        public string Nombre { get; private set; }

        public ICollection<Nota> Notas { get; set; } = new List<Nota>();

        private Estudiante() { }

        public Estudiante(string nombre)
        {
            Nombre = nombre;
        }

        public void Update(string nombre)
        {
            Nombre = nombre;
        }
    }
}
