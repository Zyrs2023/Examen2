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
        // atributos
        private static int Id;
        private static string Correo;
        private static string Clave;
        private static string Nombre;
        public static int rol;
        public static string nombrerol;


        // constructor
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


        // Getter =  funcion me devuelve un valor -- return
        // Setter = asignar un valor a un atributo  -- void -- son metodos

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



        public static List<Login> ObtenerUsuarios()
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            List<Login> Usuario = new List<Login>();
            try
            {

                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("getusuario ", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    retorno = cmd.ExecuteNonQuery();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Login usuario = new Login();
                            usuario.SetID(reader.GetInt32(0));
                            usuario.SetCorreo(reader.GetString(1));
                            usuario.SetClave(reader.GetString(2));
                            usuario.SetNombre(reader.GetString(3));
                            Usuario.Add(usuario);
                        }

                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                return Usuario;
            }
            finally
            {
                Conn.Close();
                Conn.Dispose();
            }

            return Usuario;
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

    }
}