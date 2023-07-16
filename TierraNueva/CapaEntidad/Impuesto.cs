using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Impuesto
    {

        public int IdImpuesto { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; } = true;

        public DateTime FechaDesde { get; set; }

        public DateTime FechaHasta { get; set; }

        public int Valor { get; set; }
    }
}
