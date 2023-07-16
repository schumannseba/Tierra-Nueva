using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{

//    Create table PrecioVentaProducto(
//IdPrecioVentaProducto int primary key identity,
//PorcentajeRecargo numeric(18,2),
//Descripcion varchar(500),
//IdProducto int references Producto(IdProducto),
//FragmentacionProducto varchar(100),
//CostoPonderadoActual decimal (10,2),
//FechaRegistro datetime default getdate()
//)
    public class PrecioVentaProducto
    {

        public int IdPrecioVentaProducto { get; set; }

        public decimal PorcentajeRecargo { get; set; }

        public string Descripcion { get; set; }

        // public int IdProducto { get; set; }

        public Producto oProducto { get; set; }

        public string FragmentacionProducto { get; set; }

        public decimal CostoPonderadoActual { get; set; }
    }
}
