using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Examen2progra.Clases
{
    public class RolesEmpleados
    {
        public int IdEmpleados { get; set; }
        public int IdRol { get; set; }




        // Constructor
        public RolesEmpleados(int idempleados, int idrol)
        {
            IdEmpleados = idempleados;
            IdRol = idrol;

        }

        // Constructor vacío
        public RolesEmpleados() { }

        public static int Agregar(int IdEmpleado, int IdRol)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_AgregarEmpleadoConRol", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    cmd.Parameters.Add(new SqlParameter("@IdEmpleado", IdEmpleado));
                    cmd.Parameters.Add(new SqlParameter("@IdRol", IdRol));



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

        public static int BorrarRolesEmpleados(int IdEmpleado, int IdRol)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_BorrarEmpleadoConRol", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@IdEmpleado", IdEmpleado));
                    cmd.Parameters.Add(new SqlParameter("@IdRol", IdRol));

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
        public static int Modificar(int IdEmpleado, int IdRol)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarEmpleadoRol", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@IdEmpleado", IdEmpleado));
                    cmd.Parameters.Add(new SqlParameter("@IdRol", IdRol));




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

        public static List<RolesEmpleados> ConsultarEquipoConFiltro(int Codigo)
        {
            List<RolesEmpleados> tipos = new List<RolesEmpleados>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_ConsultarEmpleadoRolConFiltro", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Codigo", SqlDbType.Int).Value = Codigo;

                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RolesEmpleados tipo = new RolesEmpleados
                            {
                                IdEmpleados = reader.GetInt32(0),
                                IdRol = reader.GetInt32(1),

                            };

                            tipos.Add(tipo);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error SQL: " + ex.Message);
                }
            }

            return tipos;
        }

        public static List<RolesEmpleados> ObtenerTipos()
        {
            List<RolesEmpleados> tipos = new List<RolesEmpleados>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_ConsultarEmpleadoRolConFiltro", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            RolesEmpleados tipo = new RolesEmpleados
                            {
                                IdEmpleados = reader.GetInt32(0),
                                IdRol = reader.GetInt32(1),

                            };

                            tipos.Add(tipo);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error SQL: " + ex.Message);
                }
            }

            return tipos;
        }
    }
}