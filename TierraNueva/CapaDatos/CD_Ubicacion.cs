using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;
using System.Data.SqlClient;
using System.Data;


namespace CapaDatos
{
    public class CD_Ubicacion
    {
        public List<Provincia> ObtenerProvincia()
        {
            List<Provincia> lista = new List<Provincia>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select * from Provincia";


                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;


                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Provincia()
                            {
                                IdProvincia = dr["IdProvincia"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                             


                            }
                            );
                        }

                    }
                }
            }

            catch
            {
                lista = new List<Provincia>();
            }

            return lista;
        }

        public List<Ciudad> ObtenerCiudad(string idprovincia)
        {
            List<Ciudad> lista = new List<Ciudad>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select * from ciudad where IdProvincia = @idprovincia";


                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@idprovincia", idprovincia);
                    cmd.CommandType = CommandType.Text;


                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Ciudad
                            {
                                IdCiudad = dr["IdCiudad"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),



                            }
                            );
                        }

                    }
                }
            }

            catch
            {
                lista = new List<Ciudad>();
            }

            return lista;
        }




    }
}
