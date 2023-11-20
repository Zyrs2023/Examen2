using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;

namespace Examen2progra
{
    public partial class Equipo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LlenarGrid();
            }

        }

        public void alertas(string texto)
        {
            string message = texto;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(message);
            sb.Append("')};");
            sb.Append("</script>");
            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

        protected void LlenarGrid()
        {
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM EQUIPOS"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            datagrid.DataSource = dt;
                            datagrid.DataBind();
                        }
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int resultado = Clases.Equipo.AgregarEquipo(ttipoEquipo.Text, tModelo.Text,tusuarioid.Text);

            if (resultado > 0)
            {
                alertas("Equipo ingresado con éxito");
                ttipoEquipo.Text = string.Empty;
                tModelo.Text = string.Empty;
                tusuarioid.Text = string.Empty;

                LlenarGrid();
            }
            else
            {
                alertas("Error al ingresar Equipo");
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int resultado = Clases.Equipo.BorrarEquipo(int.Parse(tid.Text));

            if (resultado > 0)
            {
                alertas("Equipo borrado con éxito");
                tid.Text = string.Empty;
                LlenarGrid();
            }
            else
            {
                alertas("Error al borrar Equipo");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int EquipoID = int.Parse(tid.Text);
            string TipoEquipo = ttipoEquipo.Text;
            string Modelo = tModelo.Text;
            int UsuarioID = int.Parse(tusuarioid.Text);

            int resultado = Clases.Equipo.Modificar(EquipoID, TipoEquipo, Modelo, UsuarioID);
            if (resultado > 0)
            {
                alertas("Equipo ha sido actualizado con éxito");
                tid.Text = string.Empty;
                ttipoEquipo.Text = string.Empty;
                tModelo.Text = string.Empty;
                tusuarioid.Text = string.Empty;
                LlenarGrid();
            }
            else
            {
                alertas("Error al actualizar Equipo");
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            int EquipoID = int.Parse(txtFiltro.Text);
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM EQUIPOS WHERE EquipoID ='" + EquipoID + "'"))


                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    sda.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        sda.Fill(dt);
                        datagrid.DataSource = dt;
                        datagrid.DataBind();  // actualizar el grid view
                    }

                }

            }


        }
    }
}