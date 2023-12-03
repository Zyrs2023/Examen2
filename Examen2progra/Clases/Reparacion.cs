using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Examen2progra.Clases
{
    public class Reparacion
    {
        public int ReparacionID { get; set; }
        public int EquipoID { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Estado { get; set; }

        public Reparacion(int reparacionID, int equipoID, DateTime fechaSolicitud, string estado)
        {
            ReparacionID = reparacionID;
            EquipoID = equipoID;
            FechaSolicitud = fechaSolicitud;
            Estado = estado;
        }

        public Reparacion() { }

        public static int AgregarReparacion(int equipoID, DateTime fechaSolicitud, string estado)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_InsertReparacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@EquipoID", equipoID));
                    cmd.Parameters.Add(new SqlParameter("@FechaSolicitud", fechaSolicitud));
                    cmd.Parameters.Add(new SqlParameter("@Estado", estado));

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
        public static int BorrarReparacion(int id)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteReparacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@ReparacionID", id));

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
        public static int ModificarReparacion(int reparacionID, int equipoID, DateTime fechaSolicitud, string estado)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateReparacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@ReparacionID", reparacionID));
                    cmd.Parameters.Add(new SqlParameter("@EquipoID", equipoID));
                    cmd.Parameters.Add(new SqlParameter("@FechaSolicitud", fechaSolicitud));
                    cmd.Parameters.Add(new SqlParameter("@Estado", estado));

                    retorno = cmd.ExecuteNonQuery();
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    retorno = -1;
                }
            }

            return retorno;
        }
        public static List<Reparacion> ConsultarReparacionConFiltro(int reparacionID)
        {
            List<Reparacion> reparaciones = new List<Reparacion>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_GetReparacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@ReparacionID", reparacionID));

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Reparacion reparacion = new Reparacion
                            {
                                ReparacionID = reader.GetInt32(0),
                                EquipoID = reader.GetInt32(1),
                                FechaSolicitud = reader.GetDateTime(2),
                                Estado = reader.GetString(3)
                            };

                            reparaciones.Add(reparacion);
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    // Manejar la excepción según tus necesidades
                }
            }

            return reparaciones;
        }

        public static List<Reparacion> ObtenerReparaciones()
        {
            List<Reparacion> reparaciones = new List<Reparacion>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_GetReparacion", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Reparacion reparacion = new Reparacion
                            {
                                ReparacionID = reader.GetInt32(0),
                                EquipoID = reader.GetInt32(1),
                                FechaSolicitud = reader.GetDateTime(2),
                                Estado = reader.GetString(3)
                            };

                            reparaciones.Add(reparacion);
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    // Manejar la excepción según tus necesidades
                }
            }

            return reparaciones;
        }


    }


}
 
