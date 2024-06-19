using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen___Bernardo_Gonzalez_Erramuspe__Ignacio_Gomez
{
    public class Prestamo
    {
        public int CodigoEjemplar { get; set; }
        public DateTime Fecha { get; set; }
        public bool Devuelto { get; set; } = false;
        public DateTime FechaDevolucion { get; set; }
        public string Socio { get; set; }
    }
}
