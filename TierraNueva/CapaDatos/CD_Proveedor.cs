using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace CapaDatos
{
    public class CD_Proveedor
    {

        public List<Proveedor> Listar()
        {
            List<Proveedor> lista = new List<Proveedor>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select IdProveedor, Nombre, Correo, Cbu, Telefono, Direccion, Estado from Proveedor";


                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;


                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Proveedor
                            {
                                IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                                Nombre = dr["Nombre"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Cbu = Convert.ToInt32(dr["Cbu"]),
                                Telefono = Convert.ToInt32(dr["Telefono"]),
                               Direccion = dr["Direccion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),


                            }
                            );
                        }

                    }
                }
            }

            catch
            {
                lista = new List<Proveedor>();
            }

            return lista;
        }


        public int Registrar(Proveedor obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_CrearProveedor", oconexion);


                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("Cbu", obj.Cbu);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;

            }
            return idautogenerado;
        }




        public bool Editar(Proveedor obj, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarProveedor", oconexion);
                    cmd.Parameters.AddWithValue("IdProveedor", obj.IdProveedor);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Correo", obj.Correo);
                    cmd.Parameters.AddWithValue("Telefono", obj.Telefono);
                    cmd.Parameters.AddWithValue("Cbu", obj.Cbu);
                    cmd.Parameters.AddWithValue("Direccion", obj.Direccion);
                    cmd.Parameters.AddWithValue("Estado", obj.Estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;

            }
            return resultado;
        }



        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarProveedor", oconexion);
                    cmd.Parameters.AddWithValue("IdProveedor", id);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;

            }
            return resultado;
        }


        public List<Proveedor> ListarProveedorPorCategoria(int idcategoria)
        {
            List<Proveedor> lista = new List<Proveedor>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                   StringBuilder sb = new StringBuilder();

                    sb.AppendLine("select distinct prov.IdProveedor, prov.Nombre from Producto prod");
                    sb.AppendLine("inner join Categoria c on c.IdCategoria = prod.IdCategoria");
                    sb.AppendLine("inner join Proveedor prov on prov.IdProveedor = prod.IdProveedor");
                    sb.AppendLine("where c.IdCategoria = iif(@idcategoria = 0, c.IdCategoria, @idcategoria)");


                    //                    select* from Producto prod
                    //inner join Categoria c on c.IdCategoria = prod.IdCategoria
                    //inner join Proveedor prov on prov.IdProveedor = prod.IdProveedor
                    //where c.IdCategoria = iif(@idcategoria = 0, c.IdCategoria, @idcategoria)


                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idcategoria", idcategoria);
                    cmd.CommandType = CommandType.Text;


                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Proveedor
                            {
                                IdProveedor = Convert.ToInt32(dr["IdProveedor"]),
                                Nombre = dr["Nombre"].ToString(),
                                Correo = dr["Correo"].ToString(),
                                Telefono = Convert.ToInt32(dr["Telefono"]),
                                Direccion = dr["Direccion"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"]),


                            }
                            );
                        }

                    }
                }
            }

            catch
            {
                lista = new List<Proveedor>();
            }

            return lista;
        }

    }
}
