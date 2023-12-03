using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Examen2progra.Clases
{
    public class Asignacion
    {
        public int AsignacionID { get; set; }
        public int ReparacionID { get; set; }
        public int TecnicoID { get; set; }
        public DateTime FechaAsignacion { get; set; }

        public Asignacion(int asignacionID, int reparacionID, int tecnicoID, DateTime fechaAsignacion)
        {
            AsignacionID = asignacionID;
            ReparacionID = reparacionID;
            TecnicoID = tecnicoID;
            FechaAsignacion = fechaAsignacion;
        }

        public Asignacion() { }

        public static int AgregarAsignacion(int reparacionID, int tecnicoID, DateTime fechaAsignacion)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertAsignacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@ReparacionID", reparacionID));
                    cmd.Parameters.Add(new SqlParameter("@TecnicoID", tecnicoID));
                    cmd.Parameters.Add(new SqlParameter("@FechaAsignacion", fechaAsignacion));

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

        public static int BorrarAsignacion(int id)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteAsignacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@AsignacionID", id));

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

        public static int ModificarAsignacion(int asignacionID, int reparacionID, int tecnicoID, DateTime fechaAsignacion)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateAsignacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@AsignacionID", asignacionID));
                    cmd.Parameters.Add(new SqlParameter("@ReparacionID", reparacionID));
                    cmd.Parameters.Add(new SqlParameter("@TecnicoID", tecnicoID));
                    cmd.Parameters.Add(new SqlParameter("@FechaAsignacion", fechaAsignacion));

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

        public static List<Asignacion> ConsultarAsignacionConFiltro(int asignacionID)
        {
            List<Asignacion> asignaciones = new List<Asignacion>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_GetAsignacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@AsignacionID", asignacionID));

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Asignacion asignacion = new Asignacion
                            {
                                AsignacionID = reader.GetInt32(0),
                                ReparacionID = reader.GetInt32(1),
                                TecnicoID = reader.GetInt32(2),
                                FechaAsignacion = reader.GetDateTime(3)
                            };

                            asignaciones.Add(asignacion);
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    // Manejar la excepción según tus necesidades
                }
            }

            return asignaciones;
        }

        public static List<Asignacion> ObtenerAsignaciones()
        {
            List<Asignacion> asignaciones = new List<Asignacion>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_GetAsignacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Asignacion asignacion = new Asignacion
                            {
                                AsignacionID = reader.GetInt32(0),
                                ReparacionID = reader.GetInt32(1),
                                TecnicoID = reader.GetInt32(2),
                                FechaAsignacion = reader.GetDateTime(3)
                            };

                            asignaciones.Add(asignacion);
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    // Manejar la excepción según tus necesidades
                }
            }

            return asignaciones;
        }
    }
}
