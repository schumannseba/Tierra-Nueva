using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Cliente
    {
        private CD_Cliente objCapaDato = new CD_Cliente();

        public List<Cliente> Listar()
        {
            return objCapaDato.Listar();
        }


        public int Registrar(Cliente obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del cliente no puede ser vacío";
            }

            else if (string.IsNullOrEmpty(obj.Apellido) || string.IsNullOrWhiteSpace(obj.Apellido))
            {
                Mensaje = "El Apellido del cliente no puede ser vacío";
            }

            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El Correo del cliente no puede ser vacío";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                string clave = CN_Recursos.GenerarClave();

                string asunto = "Creacion de cuenta";
                string mensaje_correo = "<h3>Su cuenta fue creada correctamente</h3><br><p>Su contraseña para acceder es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);

                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);

                if (respuesta)
                {

                    obj.Clave = CN_Recursos.ConvertirSha256(clave);

                    return objCapaDato.Registrar(obj, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }
            }
            else
            {
                return 0;
            }


        }


        public bool CambiarClave(int idCliente, string nuevaclave, out string Mensaje)
        {
            return objCapaDato.CambiarClave(idCliente, nuevaclave, out Mensaje);
        }



        public bool ReestablecerClave(int idCliente, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string nuevaclave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.ReestablecerClave(idCliente, CN_Recursos.ConvertirSha256(nuevaclave), out Mensaje);


            if (resultado)
            {
                string asunto = "Contraseña Reestablecida";
                string mensaje_correo = "<h3>Su cuenta fue reestablecida correctamente</h3></br><p>Su contraseña para acceder ahora es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);
                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);
                if (respuesta)
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }

            else
            {
                Mensaje = "No se pudo reestablecer la contraseña";
                return false;
            }

        }
    }


}

