using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace Examen2progra.Clases
{
    public class Login
    {
        
        private static string correo;
        private static string clave;
        private static string Nombre;

       
        public Login(string Correo, string Clave, string nombre)
        {
            correo = Correo;
            clave = Clave;
            Nombre = nombre;
        }

        public Login(string Correo, string Clave)
        {
            correo = Correo;
            clave = Clave;

        }
        public Login()
        {

        }

        

        public static string Getcorreo()
        {
            return correo;
        }

        public static string Getnombre()
        {
            return Nombre;
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


        public static int ValidarLogin()
        {
            int retorno = 0;

            SqlConnection Conn = new SqlConnection();
            try
            {
                using (Conn = DBConn.obtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("validarusuario", Conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@Correo", correo));
                    cmd.Parameters.Add(new SqlParameter("@Clave", clave));

                    retorno = cmd.ExecuteNonQuery();
                  
                    using (SqlDataReader lectura = cmd.ExecuteReader())
                    {
                        if (lectura.Read())
                        {
                            retorno = 1;
                            Nombre = lectura[2].ToString();
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