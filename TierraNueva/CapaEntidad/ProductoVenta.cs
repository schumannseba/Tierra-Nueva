using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
   public class ProductoVenta
    {

        public int IdProductoVenta { get; set; }

        //public int IdPrecioVentaProducto { get; set; }

        public PrecioVentaProducto oPrecioVentaProducto { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        // public int IdCategoria { get; set; }

        public Categoria oCategoria { get; set; }

        public int Stock { get; set; }

        public string RutaImagen { get; set; }

        public string NombreImagen { get; set; }

        public bool Estado { get; set; }
    }
}
