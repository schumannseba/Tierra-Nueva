﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
   public class DetallePedido
    {

        public int IdDetallePedido { get; set; }

        public int IdPedido { get; set; }

       // public int IdProductoVenta { get; set; }

        public ProductoVenta oProductoVenta { get; set; }

        public int Cantidad { get; set; }

        public decimal Total { get; set; }

        public string IdTransaccion { get; set; }
    }
}
