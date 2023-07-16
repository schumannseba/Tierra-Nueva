using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using CapaEntidad;
using CapaNegocio;
using System.IO;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;

namespace CapaPresentacionTienda.Controllers
{
    public class TiendaController : Controller
    {
        // GET: Tienda
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Carro()
        {
            return View();
        }


        public ActionResult DetalleProducto(int idproducto = 0)
        {
            Producto oProducto = new Producto();
            bool conversion;

            oProducto = new CN_Producto().Listar().Where(p => p.IdProducto == idproducto).FirstOrDefault();
            if (oProducto != null)
            {

                oProducto.Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oProducto.RutaImagen, oProducto.NombreImagen), out conversion);
                oProducto.Extension = Path.GetExtension(oProducto.NombreImagen);
            }

            return View(oProducto);
        }

        [HttpGet]

        public JsonResult ListaCategorias()
        {
            List<Categoria> lista = new List<Categoria>();

            lista = new CN_Categoria().Listar();

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaProveedorPorCategoria(int idcategoria)
        {
            List<Proveedor> lista = new List<Proveedor>();

            lista = new CN_Proveedor().ListarMarcaporCategoria(idcategoria);

            return Json(new { data = lista }, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult ListarProducto(int idcategoria)
        {
            List<Producto> lista = new List<Producto>();
            bool conversion;

            lista = new CN_Producto().Listar().Select(p => new Producto()
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                oProveedor = p.oProveedor,
                oCategoria = p.oCategoria,
                CostoProveedor = p.CostoProveedor,
                PorcentajeRecargo = p.PorcentajeRecargo,
                Stock = p.Stock,
                RutaImagen = p.RutaImagen,
                Base64 = CN_Recursos.ConvertirBase64(Path.Combine(p.RutaImagen, p.NombreImagen), out conversion),
                Extension = Path.GetExtension(p.NombreImagen),
                Estado = p.Estado

            }).Where(p =>
            p.oCategoria.IdCategoria == (idcategoria == 0 ? p.oCategoria.IdCategoria : idcategoria) &&
            p.Stock > 0 && p.Estado == true

            ).ToList();

            var jsonresult = Json(new { data = lista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;

            return jsonresult;
        }

        [HttpPost]
        public JsonResult AgregarCarrito(int idproducto)
        {
            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;


            bool existe = new CN_Carrito().ExisteCarrito(idcliente, idproducto);


            bool respuesta = false;
            string mensaje = string.Empty;


            if (existe)
            {
                mensaje = "El producto ya existe en el carrito";
            }

            else
            {
                respuesta = new CN_Carrito().OperacionCarrito(idcliente, idproducto, true, out mensaje);
            }

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]

        public JsonResult CantidadEnCarrito()
        {
            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;
            int cantidad = new CN_Carrito().CantidadEnCarrito(idcliente);
            return Json(new { cantidad = cantidad }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public JsonResult ListarProductosCarrito()
        {
            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;

            List<Carrito> oLista = new List<Carrito>();

            bool conversion;

            oLista = new CN_Carrito().ListarProducto(idcliente).Select(oc => new Carrito()
            {
                oProducto = new Producto()
                {
                    IdProducto = oc.oProducto.IdProducto,
                    Nombre = oc.oProducto.Nombre,
                    oCategoria = oc.oProducto.oCategoria,
                    CostoActualizado = oc.oProducto.CostoActualizado,
                    RutaImagen = oc.oProducto.RutaImagen,
                    Base64 = CN_Recursos.ConvertirBase64(Path.Combine(oc.oProducto.RutaImagen, oc.oProducto.NombreImagen), out conversion),
                    Extension = Path.GetExtension(oc.oProducto.NombreImagen)

                },
                Cantidad = oc.Cantidad

            }).ToList();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);


        }
        [HttpPost]
        public JsonResult OperacionCarrito(int idproducto, bool sumar)
        {
            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;


           


            bool respuesta = false;
            string mensaje = string.Empty;


            respuesta = new CN_Carrito().OperacionCarrito(idcliente, idproducto, true, out mensaje);

          

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]

        public JsonResult EliminarCarrito (int idproducto)
        {
            int idcliente = ((Cliente)Session["Cliente"]).IdCliente;


            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Carrito().EliminarCarrito(idcliente, idproducto);

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]

        public JsonResult ObtenerProvincia()
        {
            List<Provincia> oLista = new List<Provincia> ();

            oLista = new CN_Ubicacion().ObtenerProvincia();

          return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public JsonResult ObtenerCiudad(string idprovincia)
        {
            List<Ciudad> oLista = new List<Ciudad>();

            oLista = new CN_Ubicacion().ObtenerCiudad(idprovincia);

            return Json(new { lista = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<JsonResult>ProcesarPago(List<Carrito> oListaCarrito, Pedido oPedido)
        {
            decimal total = 0;

            DataTable detalle_pedido = new DataTable();
            detalle_pedido.Locale = new CultureInfo("es-AR");
            detalle_pedido.Columns.Add("IdProducto", typeof(string));
            detalle_pedido.Columns.Add("Cantidad", typeof(int));
            detalle_pedido.Columns.Add("Total", typeof(decimal));



            foreach (Carrito oCarrito in oListaCarrito)
            {
                decimal subtotal = Convert.ToDecimal(oCarrito.Cantidad.ToString()) * oCarrito.oProducto.CostoActualizado;

                total += subtotal;

                detalle_pedido.Rows.Add(new object[] {
                oCarrito.oProducto.IdProducto,
                oCarrito.Cantidad,
                subtotal
                });
            }
            oPedido.MontoTotal = total;
            oPedido.IdCliente = ((Cliente)Session["Cliente"]).IdCliente;

            TempData["Pedido"] = oPedido;
            TempData["DetallePedido"] = detalle_pedido;

            return Json(new { Status = true, Link = "/Tienda/PagoEfectuado?idTransaccion=code0001&status=true" }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> PagoEfectuado()
        {
            string idtransaccion = Request.QueryString["idTransaccion"];
            bool status = Convert.ToBoolean(Request.QueryString["status"]);

            ViewData["Status"] = status;


            if (status)
            {
                Pedido oPedido = (Pedido)TempData["Pedido"];

                DataTable detalle_pedido = (DataTable)TempData["DetallePedido"];

                oPedido.IdTransaccion = idtransaccion;

                string mensaje = string.Empty;

                bool respuesta = new CN_Pedido().Registrar(oPedido, detalle_pedido, out mensaje);

                ViewData["IdTransaccion"] = oPedido.IdTransaccion;

            }

            return View();
        }
    }
}