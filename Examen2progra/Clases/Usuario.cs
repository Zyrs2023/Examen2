﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Examen2progra.Clases;

namespace Examen2progra.Clases
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string Nombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }

        // Constructor
        public Usuario(int usuarioID, string nombre, string correoElectronico, string telefono)
        {
            UsuarioID = usuarioID;
            Nombre = nombre;
            CorreoElectronico = correoElectronico;
            Telefono = telefono;
        }

        // Constructor vacío
        public Usuario() { }

        public static int Agregar(string Nombre, string CorreoElectronico, string Telefono )
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
        public static int Modificar(int UsuarioID, string Nombre, string CorreoElectronico, string Telefono)
        {
            int retorno = 0;

            using (SqlConnection conn = DBConn.obtenerConexion())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("ModificarUsuario", conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.Add(new SqlParameter("@UsuarioID", UsuarioID));
                    cmd.Parameters.Add(new SqlParameter("@Nombre", Nombre));
                    cmd.Parameters.Add(new SqlParameter("@CorreoElectronico", CorreoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@Telefono", Telefono));



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

        public static List<Usuario> ConsultarEquipoConFiltro(int UsuarioID)
        {
            List<Usuario> tipos = new List<Usuario>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            using (SqlCommand cmd = new SqlCommand("ConsultarEquipoConFiltro", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UsuarioID", SqlDbType.Int).Value = UsuarioID;

                try
                {
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
                catch (SqlException ex)
                {
                    Console.WriteLine("Error SQL: " + ex.Message);
                }
            }

            return tipos;
        }

        public static List<Usuario> ObtenerTipos()
        {
            List<Usuario> tipos = new List<Usuario>();

            using (SqlConnection conn = DBConn.obtenerConexion())
            using (SqlCommand cmd = new SqlCommand("ConsultarEquipoConFiltro", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
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
                catch (SqlException ex)
                {
                    Console.WriteLine("Error SQL: " + ex.Message);
                }
            }

            return tipos;
        }

       
    }
}







