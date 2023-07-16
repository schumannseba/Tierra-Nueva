using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{//        Create table Cliente(
//IdCliente int primary key identity,
//Nombre varchar (100),
//Apellido varchar(100),
//Correo varchar(500),
//Clave varchar(150),
//Estado bit default 1,
//DNI int,
//Reestablecer bit default 0,
//FechaRegistro datetime default getdate()
//)
    public class Cliente
    {

        public int IdCliente { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Correo { get; set; }

        public string Clave { get; set; }

        public string ConfirmarClave { get; set; }

        public bool Estado { get; set; }

        public int DNI { get; set; }

        public bool Reestablecer { get; set; }

    }
}
