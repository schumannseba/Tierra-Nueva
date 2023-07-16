using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_IPC_Mensual
    {
        private CD_IPC_Mensual objCapaDato = new CD_IPC_Mensual();


        public List<IPC_Mensual> Listar()
        {
            return objCapaDato.Listar();
        }


        public int Registrar(IPC_Mensual obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            //if (string.IsNullOrEmpty(obj.Valor) || string.IsNullOrWhiteSpace(obj.Valor))
            //{
            //    Mensaje = "La descripción no pueden ser vacía";
            //}



            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }


        }

        public bool Editar(IPC_Mensual obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            //if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            //{
            //    Mensaje = "La descripción de la categoría no puede ser vacía";
            //}


            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.Editar(obj, out Mensaje);
            }




            else
            {
                return false;
            }
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }


    }
}

