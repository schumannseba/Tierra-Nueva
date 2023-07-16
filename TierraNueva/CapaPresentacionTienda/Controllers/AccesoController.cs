using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CapaPresentacionTienda.Controllers
{
    public class AccesoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registrar()
        {
            return View();
        }
        public ActionResult Reestablecer()
        {
            return View();
        }
        public ActionResult CambiarClave()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registrar(Cliente objeto)
        {
            int resultado;
            string mensaje = string.Empty;

            ViewData["Nombres"] = string.IsNullOrEmpty(objeto.Nombre) ? "" : objeto.Nombre;
            ViewData["Apellidos"] = string.IsNullOrEmpty(objeto.Apellido) ? "" : objeto.Apellido;
            ViewData["Correo"] = string.IsNullOrEmpty(objeto.Nombre) ? "" : objeto.Correo;

            if (objeto.Clave != objeto.ConfirmarClave)
            {
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            resultado = new CN_Cliente().Registrar(objeto, out mensaje);
            if (resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {

            Cliente oCliente = null;

            oCliente = new CN_Cliente().Listar().Where(item => item.Correo == correo && item.Clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();

            

            if (oCliente == null) {
                ViewBag.Error = "Correo o contraseña no son correctas";
                return View();
            }
            else
            {
                if (oCliente.Reestablecer) {
                    TempData["IdCliente"] = oCliente.IdCliente;
                    return RedirectToAction("CambiarClave", "Acceso");
                }
                else {

                    FormsAuthentication.SetAuthCookie(oCliente.Correo, false);

                    Session["Cliente"] = oCliente;

                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Tienda");
}
                }

          //  return View();
                }
        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            {
                Cliente oCliente = new Cliente();

                oCliente = new CN_Cliente().Listar().Where(item => item.Correo == correo).FirstOrDefault();

                if (oCliente == null)
                {
                    ViewBag.Error = "No se encontró un cliente relacionado a ese correo";
                    return View();
                }

                string mensaje = string.Empty;
                bool respuesta = new CN_Cliente().ReestablecerClave(oCliente.IdCliente, correo, out mensaje);

                if (respuesta)
                {
                    ViewBag.Error = null;
                    return RedirectToAction("Index", "Acceso");

                }

                else
                {
                    ViewBag.Error = mensaje;
                    return View();
                }
            }
        }
            [HttpPost]
            public ActionResult CambiarClave(string idcliente, string claveactual, string nuevaclave, string confirmarclave)

            {
                Cliente oCliente = new Cliente();

                oCliente = new CN_Cliente().Listar().Where(u => u.IdCliente == int.Parse(idcliente)).FirstOrDefault();





                if (oCliente.Clave != CN_Recursos.ConvertirSha256(claveactual))
                {
                    // El valor de idusuario no es un número entero válido
                    TempData["IdCliente"] = idcliente;
                    ViewData["vclave"] = "";
                    ViewBag.Error = "La contraseña actual no es correcta";
                    return View();
                }
                else if (nuevaclave != confirmarclave)
                {
                    TempData["IdCliente"] = idcliente;
                    ViewData["vclave"] = claveactual;
                    ViewBag.Error = "Las contraseñas no coinciden";
                    return View();
                }
                ViewData["vclave"] = "";

                nuevaclave = CN_Recursos.ConvertirSha256(nuevaclave);

                string mensaje = string.Empty;

                bool respuesta = new CN_Cliente().CambiarClave(int.Parse(idcliente), nuevaclave, out mensaje);

                    if (respuesta)
                    {
                        return RedirectToAction("Index");
                    }

                    else
                    {
                        TempData["IdCliente"] = idcliente;

                        ViewBag.Error = mensaje;
                        return View();
                    }

                    // return View();




                }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Acceso");
        }


    }

        }
    
