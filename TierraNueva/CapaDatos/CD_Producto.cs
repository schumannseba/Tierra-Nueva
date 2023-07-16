using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Globalization;
using System.Collections;

namespace CapaDatos
{
    public class CD_Producto
    {

        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();


                   // sb.AppendLine("p.IdProducto, p.Nombre, p.Descripcion,");
                   // sb.AppendLine("pr.IdProveedor, pr.Nombre[ProvNombre],");
                   // sb.AppendLine("c.IdCategoria, c.Descripcion[DesCategoria],");
                   //// sb.AppendLine("ipc.IdIPC_Mensual, ipc.Valor,");
                   // sb.AppendLine("p.CostoProveedor, p.Stock, p.RutaImagen,");
                   // sb.AppendLine("p.NombreImagen, p.Estado");
                   // sb.AppendLine("from Producto p");
                   // sb.AppendLine("inner join Proveedor pr on pr.IdProveedor = p.IdProveedor");
                   // sb.AppendLine("inner join Categoria c on c.IdCategoria = p.IdCategoria");
                   // //  sb.AppendLine("inner join IPC_Mensual ipc on ipc.IdIPC_Mensual = p.IdIPC_Mensual");


                    sb.AppendLine("select p.IdProducto, p.Nombre, p.Descripcion, p.Estado, pr.IdProveedor, pr.Nombre[ProvNombre], p.CostoProveedor, p.PorcentajeRecargo, p.CostoActualizado, p.Stock, p.RutaImagen, p.NombreImagen, ipc.IdIPC_Mensual, ipc.Valor, c.IdCategoria, c.Descripcion[DesCategoria] from Producto p");
                    sb.AppendLine("inner join Proveedor pr on pr.IdProveedor = p.IdProveedor");
                    sb.AppendLine("inner join IPC_Mensual ipc on ipc.IdIPC_Mensual = p.IdIPC_Mensual");
                    sb.AppendLine("inner join Categoria c on c.IdCategoria = p.IdCategoria");
                 



                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;


                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Producto producto = new Producto();
                            lista.Add(new Producto()
                            {
                                IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                Nombre = dr["Nombre"].ToString(),
                                Descripcion = dr["Descripcion"].ToString(),
                                oProveedor = new Proveedor() { IdProveedor = Convert.ToInt32(dr["IdProveedor"]), Nombre = dr["ProvNombre"].ToString() },
                                oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Descripcion = dr["DesCategoria"].ToString() },
                                oIPC_Mensual = new IPC_Mensual() { IdIPC_Mensual = Convert.ToInt32(dr["IdIPC_Mensual"]), Valor = Convert.ToInt32(dr["Valor"]) },
                                CostoProveedor = Convert.ToDecimal(dr["CostoProveedor"], new CultureInfo("es-AR")),
                                PorcentajeRecargo = Convert.ToDecimal(dr["PorcentajeRecargo"], new CultureInfo("es-AR")),
                               CostoActualizado = Convert.ToDecimal(dr["CostoActualizado"], new CultureInfo("es-AR")),
                                Stock = Convert.ToInt32(dr["Stock"]),
                                RutaImagen = dr["RutaImagen"].ToString(),
                                NombreImagen = dr["NombreImagen"].ToString(),
                                Estado = Convert.ToBoolean(dr["Estado"])
                           


                        }




                            




                            );

                         



                        }


                    }
                }
            }

            catch
            {
                lista = new List<Producto>();
            }

            return lista;
        }


        //public List<Producto> Listar()
        //{
        //    List<Producto> lista = new List<Producto>();

        //    try
        //    {
        //        using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
        //        {
        //            string query = "select IdProducto, Descripcion, Estado from Producto";


        //            SqlCommand cmd = new SqlCommand(query, oconexion);
        //            cmd.CommandType = CommandType.Text;


        //            oconexion.Open();

        //            using (SqlDataReader dr = cmd.ExecuteReader())
        //            {
        //                while (dr.Read())
        //                {
        //                    lista.Add(new Producto
        //                    {
        //                        IdProducto = Convert.ToInt32(dr["IdProducto"]),
        //                        Descripcion = dr["Descripcion"].ToString(),
        //                        Estado = Convert.ToBoolean(dr["Estado"]),


        //                    }
        //                    );
        //                }

        //            }
        //        }
        //    }

        //    catch
        //    {
        //        lista = new List<Producto>();
        //    }

        //    return lista;
        //}

        public int Registrar(Producto obj, out string Mensaje)
        {
            int idautogenerado = 0;

            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", oconexion);



//                    @Nombre varchar(100), 
//@Descripcion varchar(100),
//@Stock int,
//@CostoProveedor decimal(10, 2),
//@Estado bit,
//@IdCategoria varchar(100),
//@IdProveedor varchar(100),
//@IdIPC_Mensual varchar(100),
//@CostoActualizado decimal(10, 2),
//@Mensaje varchar(500) output,
//@Resultado int output



                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("CostoProveedor", obj.CostoProveedor);
                    cmd.Parameters.AddWithValue("PorcentajeRecargo", obj.PorcentajeRecargo);
                    cmd.Parameters.AddWithValue("CostoActualizado", obj.CostoActualizado);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("IdProveedor", obj.oProveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("IdIPC_Mensual", obj.oIPC_Mensual.IdIPC_Mensual);
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

        public bool Editar(Producto obj, out string Mensaje)
        {
            bool resultado = false;

            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", obj.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", obj.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", obj.Descripcion);
                    cmd.Parameters.AddWithValue("Stock", obj.Stock);
                    cmd.Parameters.AddWithValue("CostoProveedor", obj.CostoProveedor);
                    cmd.Parameters.AddWithValue("PorcentajeRecargo", obj.PorcentajeRecargo);
                    cmd.Parameters.AddWithValue("CostoActualizado", obj.CostoActualizado);
                    cmd.Parameters.AddWithValue("IdCategoria", obj.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("IdProveedor", obj.oProveedor.IdProveedor);
                    cmd.Parameters.AddWithValue("IdIPC_Mensual", obj.oIPC_Mensual.IdIPC_Mensual);
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



        public bool GuardarDatosImagen (Producto obj, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {

                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {

                    string query = "update producto set RutaImagen = @rutaImagen, NombreImagen = @nombreImagen where IdProducto = @idproducto ";


                    SqlCommand cmd = new SqlCommand(query, oconexion);

                    cmd.Parameters.AddWithValue("rutaImagen", obj.RutaImagen);
                    cmd.Parameters.AddWithValue("nombreImagen", obj.NombreImagen);
                    cmd.Parameters.AddWithValue("idproducto", obj.IdProducto);
                  cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                   if( cmd.ExecuteNonQuery() > 0)
                    {
                        resultado = true;
                    }
                   else
                    {
                        Mensaje = "No se pudo actualizar imagen";
                    }
                }
            }
            catch (Exception ex ) { 
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
                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", id);
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
    }
}
