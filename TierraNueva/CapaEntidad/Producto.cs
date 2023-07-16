using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{

//    IdProducto int primary key identity,
//Nombre varchar(500),
//Descripcion varchar(500),
//IdCategoria int references Categoria(IdCategoria),
//IdProveedor int references Proveedor(IdProveedor),
//CostoProveedor decimal (10,2),
//IdIPC_Mensual int references IPC_Mensual(IdIPC_Mensual),
//CostoActualizado decimal (10,2),
//RutaImagen varchar(100),
//NombreImagen varchar(100),
//Stock int,
//Estado bit default 1,
//FechaRegistro datetime default getdate()
    public class Producto
    {
        public int IdProducto { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

       // public int IdCategoria { get; set; }

        public Categoria oCategoria { get; set; }

        // public int IdProveedor { get; set; }

        public Proveedor oProveedor { get; set; }

        public decimal CostoProveedor { get; set; }

       public string CostoProveedorPrecio { get; set; }

        // public int IdIPC_Mensual { get; set; }

        public decimal PorcentajeRecargo { get; set; }

        public string PorcentajeRecargoValor { get; set; }

        public IPC_Mensual oIPC_Mensual { get; set; }

        public decimal CostoActualizado { get; set; }

        public string CostoActualizadoPrecio { get; set; }

        public string RutaImagen { get; set; }

        public string NombreImagen { get; set; }

        public int Stock { get; set; }

        public bool Estado { get; set; }


        public string Base64 { get; set; }

        public string Extension { get; set; }
    }
}
