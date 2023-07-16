using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{

//    IdIPC_Mensual int primary key identity,
//Valor int,
//Fecha datetime default getdate()
    public class IPC_Mensual
    {

        public int IdIPC_Mensual { get; set; }

        public  int Valor { get; set; }
    }
}
