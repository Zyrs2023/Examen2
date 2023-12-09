using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Examen2progra.Clases
{
    public class Empleado
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string Rol { get; set; }
        public string NombreRol { get; set; }

        // Constructor
        public Empleado(int codigo, string nombre, string correo, string clave,string rol,string nombrerol)
        {
            Codigo = codigo;
            Nombre = nombre;
            Correo = correo;
            Clave = clave;
            Rol = rol;
            NombreRol = nombrerol;
        }

        // Constructor vacío
        public Empleado() { }



        public static int Agregar(string Nombre, string Correo, string Clave)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_AgregarEmpleado", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    cmd.Parameters.Add(new SqlParameter("@Correo", Correo));
                    cmd.Parameters.Add(new SqlParameter("@Clave", Clave));


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

        public static int BorrarEmpleado(int Codigo)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_BorrarEmpleado", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));

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
        public static int Modificar(int Codigo, string Nombre, string Correo, string Clave)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarEmpleado", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    cmd.Parameters.Add(new SqlParameter("@Correo", Correo));
                    cmd.Parameters.Add(new SqlParameter("@Clave", Clave));



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

        public static List<Empleado> ConsultarEquipoConFiltro(int CodigoID)
        {
            List<Empleado> tipos = new List<Empleado>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_ConsultarEmpleadoConFiltro", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CodigoID", SqlDbType.Int).Value = CodigoID;

                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Empleado tipo = new Empleado
                            {
                                Codigo = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Correo = reader.GetString(2),
                                Clave = reader.GetString(3),
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

        public static List<Empleado> ObtenerTipos()
        {
            List<Empleado> tipos = new List<Empleado>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_ConsultarEmpleadoConFiltro", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Empleado tipo = new Empleado
                            {
                                Codigo = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                Correo = reader.GetString(2),
                                Clave = reader.GetString(3),
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