using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Examen2progra.Clases
{
    public class Rol
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        
       
     

        // Constructor
        public Rol(int codigo, string nombre)
        {
            Codigo = codigo;
            Nombre = nombre;
           
        }

        // Constructor vacío
        public Rol() { }

        public static int Agregar(string Nombre)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_AgregarRol", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                   
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    


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

        public static int BorrarRol(int Codigo)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_BorrarRol", conn)
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
        public static int Modificar(int Codigo, string Nombre)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_ModificarRol", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Codigo", Codigo));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                 



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

        public static List<Rol> ConsultarEquipoConFiltro(int Codigo)
        {
            List<Rol> tipos = new List<Rol>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_ConsultarRolConFiltro", conn))
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
                            Rol tipo = new Rol
                            {
                                Codigo = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                
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

        public static List<Rol> ObtenerTipos()
        {
            List<Rol> tipos = new List<Rol>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_ConsultarRolConFiltro", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Rol tipo = new Rol
                            {
                                Codigo = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                
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