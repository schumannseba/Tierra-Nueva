using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
namespace CapaNegocio
{
    public class CN_Ubicacion
    {
        private CD_Ubicacion objCapaDato = new CD_Ubicacion();


        public List<Provincia> ObtenerProvincia()
        {
            return objCapaDato.ObtenerProvincia();
        }

        public List<Ciudad> ObtenerCiudad(string idprovincia)
        {
            return objCapaDato.ObtenerCiudad(idprovincia);
        }


     
    }
}
