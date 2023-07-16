using CapaEntidad;
using CapaNegocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacionAdmin.Controllers
{
    [Authorize]
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Categoria()
        {
            return View();
        }

        public ActionResult Proveedor()
        {
            return View();
        }


        public ActionResult Producto()
        {
            return View();
        }


        public ActionResult IPC_Mensual()
        {
            return View();
        }

        // +++++++++++CATEGORIA

        #region CATEGORIA

        [HttpGet]
        public ActionResult ListarCategorias()
        {

            List<Categoria> oLista = new List<Categoria>();
            oLista = new CN_Categoria().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]

        public JsonResult GuardarCategoria(Categoria objeto)
        {

            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
            {


                resultado = new CN_Categoria().Registrar(objeto, out mensaje);
            }

            else
            {
                resultado = new CN_Categoria().Editar(objeto, out mensaje);
            }





            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(id, out mensaje);


            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        // +++++++++++CATEGORIA

        #region PROVEEDOR
        [HttpGet]
        public ActionResult ListarProveedores()
        {

            List<Proveedor> oLista = new List<Proveedor>();
            oLista = new CN_Proveedor().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]

        public JsonResult GuardarProveedor(Proveedor objeto)
        {

            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdProveedor == 0)
            {


                resultado = new CN_Proveedor().Registrar(objeto, out mensaje);
            }

            else
            {
                resultado = new CN_Proveedor().Editar(objeto, out mensaje);
            }





            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarProveedor(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Proveedor().Eliminar(id, out mensaje);


            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        // +++++++++++IPC_Mensual

        #region IPC_MENSUAL
        [HttpGet]
        public ActionResult ListarIPC_Mensuales()
        {

            List<IPC_Mensual> oLista = new List<IPC_Mensual>();
            oLista = new CN_IPC_Mensual().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]

        public JsonResult GuardarIPC_Mensual(IPC_Mensual objeto)
        {

            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdIPC_Mensual == 0)
            {


                resultado = new CN_IPC_Mensual().Registrar(objeto, out mensaje);
            }

            else
            {
                resultado = new CN_IPC_Mensual().Editar(objeto, out mensaje);
            }





            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarIPC_Mensual(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_IPC_Mensual().Eliminar(id, out mensaje);


            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        // +++++++++++Proveedor
        #region PRODUCTO
        [HttpGet]
        public ActionResult ListarProducto()
        {

            List<Producto> oLista = new List<Producto>();
            oLista = new CN_Producto().Listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GuardarProducto(string objeto, HttpPostedFileBase archivoImagen)
        {

            object resultado;
            string mensaje = string.Empty;
            bool operacion_exitosa = true;
            bool guardar_imagen_exito = true;

            Producto oProducto = new Producto();
            oProducto = JsonConvert.DeserializeObject<Producto>(objeto);


            decimal costoProveedor;

            if (decimal.TryParse(oProducto.CostoProveedorPrecio,NumberStyles.AllowDecimalPoint, new CultureInfo("es-AR"), out costoProveedor))
            {
                oProducto.CostoProveedor = costoProveedor;
            }
            else
            {
                return Json(new { operacionExitosa = false, mensaje = "El formato del costoProveedor debe ser ##.##" }, JsonRequestBehavior.AllowGet);
            }

          


            if (oProducto.IdProducto == 0)
            {


                int idProductoGenerado = new CN_Producto().Registrar(oProducto, out mensaje);

                if (idProductoGenerado != 0)
                {
                    oProducto.IdProducto = idProductoGenerado;
                }
                else
                {
                    operacion_exitosa = false;
                }
            }


            else
            {
                operacion_exitosa = new CN_Producto().Editar(oProducto, out mensaje);
            }

            if (operacion_exitosa)
            {
                if(archivoImagen != null)
                {
                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oProducto.IdProducto.ToString(), extension);

                    try
                    {
                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));
                    }
                    catch(Exception ex) { 
                    string msg = ex.Message;
                        guardar_imagen_exito = false;
                    }

                    if (guardar_imagen_exito)
                    {
                        oProducto.RutaImagen = ruta_guardar;
                        oProducto.NombreImagen = nombre_imagen;
                        bool rspta = new CN_Producto().GuardarDatosImagen(oProducto, out mensaje);
                    }
                    else
                    {
                        mensaje = "Se guardara el producto pero hubo problemas con la imagen";
                    }
                }
            }




            return Json(new { operacionExitosa = operacion_exitosa, idGenerado = oProducto.IdProducto, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public JsonResult ImagenProducto(int id)
        {
            bool conversion;
            Producto oproducto = new CN_Producto().Listar().Where(p => p.IdProducto == id).FirstOrDefault();

            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oproducto.RutaImagen, oproducto.NombreImagen), out conversion );


            return Json(new
            {
                conversion = conversion,
                textoBase64 = textoBase64,
                extension = Path.GetExtension(oproducto.NombreImagen)
            },
            JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Producto().Eliminar(id, out mensaje);


            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        #endregion
    }
}