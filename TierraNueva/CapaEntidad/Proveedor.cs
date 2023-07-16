using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{

//    Create table Proveedor(
//IdProveedor int primary key identity,
//Nombre varchar(100),
//Correo varchar(100),
//Telefono int,
//Direccion varchar(100),
//Estado bit default 1,
//FechaRegistro datetime default getdate()
//)
    public class Proveedor
    {

        public int IdProveedor { get; set; }

        public string Nombre { get; set; }

        public string Correo { get; set; }

        public int Cbu { get; set; }

        public int Telefono { get; set; }


        public string Direccion { get; set; }

        public bool Estado { get; set; }
    }
}
