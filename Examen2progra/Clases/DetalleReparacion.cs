using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Examen2progra.Clases
{
    public class DetalleReparacion
    {
        public int DetalleID { get; set; }
        public int ReparacionID { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public DetalleReparacion(int detalleID, int reparacionID, string descripcion, DateTime fechaInicio, DateTime fechaFin)
        {
            DetalleID = detalleID;
            ReparacionID = reparacionID;
            Descripcion = descripcion;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }

        public DetalleReparacion() { }

        public static int AgregarDetalleReparacion(int reparacionID, string descripcion, DateTime fechaInicio, DateTime fechaFin)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertDetalleReparacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@ReparacionID", reparacionID));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", descripcion));
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", fechaFin));

                    retorno = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    retorno = -1;
                    Console.WriteLine("Error SQL: " + ex.Message);
                }
            }

            return retorno;
        }

        public static int BorrarDetalleReparacion(int id)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteDetalleReparacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@DetalleID", id));

                    retorno = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    retorno = -1;
                    Console.WriteLine("Error SQL: " + ex.Message);
                }
            }

            return retorno;
        }

        public static int ModificarDetalleReparacion(int detalleID, int reparacionID, string descripcion, DateTime fechaInicio, DateTime fechaFin)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateDetalleReparacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@DetalleID", detalleID));
                    cmd.Parameters.Add(new SqlParameter("@ReparacionID", reparacionID));
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", descripcion));
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", fechaInicio));
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", fechaFin));

                    retorno = cmd.ExecuteNonQuery();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    retorno = -1;
                    Console.WriteLine("Error SQL: " + ex.Message);
                }
            }

            return retorno;
        }

        public static List<DetalleReparacion> ConsultarDetalleReparacionConFiltro(int detalleID)
        {
            List<DetalleReparacion> detalles = new List<DetalleReparacion>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_GetDetalleReparacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@DetalleID", detalleID));

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DetalleReparacion detalle = new DetalleReparacion
                            {
                                DetalleID = reader.GetInt32(0),
                                ReparacionID = reader.GetInt32(1),
                                Descripcion = reader.GetString(2),
                                FechaInicio = reader.GetDateTime(3),
                                FechaFin = reader.GetDateTime(4)
                            };

                            detalles.Add(detalle);
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    
                }
            }

            return detalles;
        }

        public static List<DetalleReparacion> ObtenerDetallesReparacion()
        {
            List<DetalleReparacion> detalles = new List<DetalleReparacion>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_GetDetalleReparacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DetalleReparacion detalle = new DetalleReparacion
                            {
                                DetalleID = reader.GetInt32(0),
                                ReparacionID = reader.GetInt32(1),
                                Descripcion = reader.GetString(2),
                                FechaInicio = reader.GetDateTime(3),
                                FechaFin = reader.GetDateTime(4)
                            };

                            detalles.Add(detalle);
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    
                }
            }

            return detalles;
        }
    }
}