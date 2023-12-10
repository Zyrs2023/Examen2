using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Examen2progra.Clases;
using System.Security.Claims;

namespace Examen2progra.Clases
{
    public class Login
    {
        
        private static int Id;
        private static string Correo;
        private static string Clave;
        private static string Nombre;
        public static int rol;
        public static string nombrerol;


       
        public Login()
        {
        }

        public Login(int id, string correo, string clave, string nombre)
        {
            Id = id;
            Correo = correo;
            Clave = clave;
            Nombre = nombre;
        }

        public Login(string correo, string clave)
        {

            Correo = correo;
            Clave = clave;

        }


      

        public int GetId()
        {
            return Id;
        }

        public static string GetCorreo()
        {
            return Correo;
        }

        public static string GetNombre()
        {
            return Nombre;
        }
        public static string Getnombrerol()
        {
            return nombrerol;
        }

        public void SetID(int id)
        {
            Id = id;
        }

        public void SetNombre(string nombre)
        {
            Nombre = nombre;
        }

        public void SetClave(string clave)
        {
            Clave = clave;
        }

        public void SetCorreo(string correo)
        {
            Correo = correo;
        }




        public static int ValidarLogin()
        {
            int retorno = 0;
            int tipo = 0;
            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ValidarEmpleado", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@correo", Correo));
                    cmd.Parameters.Add(new SqlParameter("@clave", Clave));

                    retorno = cmd.ExecuteNonQuery();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            retorno = 1;
                            Nombre = rdr.GetString(0);
                          
                            nombrerol = rdr.GetString(1);

                        }
                        else
                        {
                            retorno = -1;
                        }

                    }


                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return retorno;
        }



        private static int ObtenerRolEmpleado(int empleadoID)
        {
            int rol = 0;
            SqlConnection conn = new SqlConnection();

            try
            {
                using (conn = DBConn.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("ObtenerRolEmpleado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmpleadoID", empleadoID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Supongamos que el rol está en la primera columna del resultado
                                rol = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar excepciones...
            }
            finally
            {
                conn.Close();
            }

            return rol;
        }
        public static List<Login> ObtenerInformacionEmpleado(int empleadoID)
        {
            List<Login> empleadoInfo = new List<Login>();
            SqlConnection conn = new SqlConnection();

            try
            {
                using (conn = DBConn.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("ObtenerInformacionEmpleado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmpleadoID", empleadoID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Login infoEmpleado = new Login();
                                infoEmpleado.SetID(reader.GetInt32(0));
                                infoEmpleado.SetCorreo(reader.GetString(1));
                                infoEmpleado.SetClave(reader.GetString(2));
                                infoEmpleado.SetNombre(reader.GetString(3));
                                

                                empleadoInfo.Add(infoEmpleado);
                            }
                        }
                    }

                    
                    Console.WriteLine($"Rol antes de obtener info específica: {rol}");

                   
                    rol = ObtenerRolEmpleado(empleadoID);

                  
                    Console.WriteLine($"Rol después de obtener info específica: {rol}");

                    if (rol == 5)
                    {
                        ObtenerInfoAdministrador(empleadoID, empleadoInfo);
                    }
                    else if (rol == 6) 
                    {
                        ObtenerInfoTecnico(empleadoID, empleadoInfo);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"Error en ObtenerInformacionEmpleado: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }

            return empleadoInfo;
        }



        private static void ObtenerInfoAdministrador(int empleadoID, List<Login> empleadoInfo)
        {
            SqlConnection conn = new SqlConnection();

            try
            {
                using (conn = DBConn.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("ObtenerInformacionEmpleado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmpleadoID", empleadoID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Login infoEmpleado = new Login();
                                infoEmpleado.SetID(reader.GetInt32(0));
                                infoEmpleado.SetCorreo(reader.GetString(1));
                                infoEmpleado.SetClave(reader.GetString(2));
                                infoEmpleado.SetNombre(reader.GetString(3));
                               

                                empleadoInfo.Add(infoEmpleado);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              
            }
            finally
            {
                conn.Close();
            }
        }

        private static void ObtenerInfoTecnico(int empleadoID, List<Login> empleadoInfo)
        {
            SqlConnection conn = new SqlConnection();

            try
            {
                using (conn = DBConn.obtenerConexion())
                {
                    using (SqlCommand cmd = new SqlCommand("ObtenerInformacionEmpleado", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmpleadoID", empleadoID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Login infoEmpleado = new Login();
                                infoEmpleado.SetID(reader.GetInt32(0));
                                infoEmpleado.SetCorreo(reader.GetString(1));
                                infoEmpleado.SetClave(reader.GetString(2));
                                infoEmpleado.SetNombre(reader.GetString(3));
                               

                                empleadoInfo.Add(infoEmpleado);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
        }

    }
}