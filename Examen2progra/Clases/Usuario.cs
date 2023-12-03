using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Claims;
using System.Web;
using Examen2progra.Clases;

namespace Examen2progra.Clases
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string Clave { get; set; }

        // Constructor
        public Usuario(int usuarioID, string nombre, string correoElectronico, string telefono, string clave)
        {
            UsuarioID = usuarioID;
            Nombre = nombre;
            CorreoElectronico = correoElectronico;
            Telefono = telefono;
            Clave = clave;
        }

        // Constructor vacío
        public Usuario() { }
        private static string correo;
        private static string clave;
        private static string nombre;

        // constructor -- inicializar los atributos 
        public Usuario(string Correo, string Clave, string Nombre)
        {
            correo = Correo;
            clave = Clave;
            nombre = Nombre;
        }

        public Usuario(string Correo, string Clave)
        {
            correo = Correo;
            clave = Clave;

        }
       

        // Getter = funcion (return) mostrar los valores de atributos
        // setter =  metodo (void) asignar valores a los atributos

        public static string Getcorreo()
        {
            return correo;
        }

        public static string Getnombre()
        {
            return nombre;
        }
        public static string Getclave()
        {
            return clave;
        }

        public void Setcorreo(string email)
        {
            correo = email;
        }

        public void Setclave(string contrasena)
        {
            clave = contrasena;
        }

        // Método para validar usuario
        public static int ValidarLogin()
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ValidarUsuario", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", correo));
                    cmd.Parameters.Add(new SqlParameter("@Clave", clave));

                    retorno = cmd.ExecuteNonQuery();
                    // reader = lector = lectura = rdr
                    using (SqlDataReader lectura = cmd.ExecuteReader())
                    {
                        if (lectura.Read())
                        {
                            retorno = 1;
                            nombre = lectura[0].ToString();
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




        public static int Agregar(string Nombre, string CorreoElectronico, string Telefono, string Clave)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("AgregarUsuario", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", CorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", Telefono));
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


        public static int BorrarUsuario(int id)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("BorrarUsuario", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", id));

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
        public static int Modificar(int UsuarioID, string Nombre, string CorreoElectronico, string Telefono,string Clave)
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("ModificarUsuario", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", UsuarioID));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", CorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", Telefono));
                    cmd.Parameters.Add(new SqlParameter("@Clave", Clave));

                    retorno = cmd.ExecuteNonQuery();
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                retorno = -1;
            }
            finally
            {
                Conn.Close();
            }

            return retorno;

        }
        public static List<Usuario> ConsultarEquipoConFiltro(int UsuarioID)
        {
            List<Usuario> tipos = new List<Usuario>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ConsultarEquipoConFiltro", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", UsuarioID));

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario tipo = new Usuario
                            {
                                UsuarioID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                CorreoElectronico = reader.GetString(2),
                                Telefono = reader.GetString(3),
                                
                            };

                            tipos.Add(tipo);
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    
                }
            }

            return tipos;
        }

        public static List<Usuario> ObtenerTipos()
        {
            List<Usuario> tipos = new List<Usuario>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ConsultarEquipoConFiltro", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Usuario tipo = new Usuario
                            {
                                UsuarioID = reader.GetInt32(0),
                                Nombre = reader.GetString(1),
                                CorreoElectronico = reader.GetString(2),
                                Telefono = reader.GetString(3),

                            };

                            tipos.Add(tipo);
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                   
                }
            }

            return tipos;
        }
       


    }
}




    


