using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace CapaNegocio
{
    public class CN_Proveedor
    {
        private CD_Proveedor objCapaDato = new CD_Proveedor();


        public List<Proveedor> Listar()
        {
            return objCapaDato.Listar();
        }


        public int Registrar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del proveedor no puede ser vacío";
            }

            else if (string.IsNullOrEmpty(obj.Direccion) || string.IsNullOrWhiteSpace(obj.Direccion))
            {
                Mensaje = "La Direccion del proveedor no puede ser vacío";
            }

            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El Correo del proveedor no puede ser vacío";
            }



            if (string.IsNullOrEmpty(Mensaje))
            {

                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }


        }

        public bool Editar(Proveedor obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El nombre del proveedor no puede ser vacío";
            }

            else if (string.IsNullOrEmpty(obj.Direccion) || string.IsNullOrWhiteSpace(obj.Direccion))
            {
                Mensaje = "La Direccion del proveedor no puede ser vacío";
            }

            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El Correo del proveedor no puede ser vacío";
            }


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

        public List<Proveedor> ListarMarcaporCategoria(int idcategoria)
        {
            return objCapaDato.ListarProveedorPorCategoria(idcategoria);
        }
    }
}
