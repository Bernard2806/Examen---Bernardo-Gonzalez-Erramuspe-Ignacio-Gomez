using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen___Bernardo_Gonzalez_Erramuspe__Ignacio_Gomez
{
    public class Biblioteca
    {
        public string Nombre { get; set; }
        public List<Ejemplar> Ejemplares { get; set; } = new List<Ejemplar>();
        public List<Prestamo> Prestamos { get; set; } = new List<Prestamo>();


        public void AniadirEjemplar(Ejemplar ejm) {
            Ejemplares.Add(ejm);
        }
        public void AniadirPrestamo(Prestamo pre)
        {
            Prestamos.Add(pre);
        }
    }
}
