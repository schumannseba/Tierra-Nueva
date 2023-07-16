using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_Pedido
    {

        private CD_Pedido objCapaDato = new CD_Pedido();

        public bool Registrar(Pedido obj, DataTable DetallePedido, out string Mensaje)
        {
            return objCapaDato.Registrar(obj, DetallePedido, out Mensaje);
        }

    }
}
