using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Examen2progra.Clases;

namespace Examen2progra
{
    public partial class Rol : System.Web.UI.Page
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Roles"))
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
            int resultado = Clases.Rol.Agregar( tnombre.Text);
            if (resultado > 0)
            {
                alertas("Rol ha sido ingresado con exito");
                
                tnombre.Text = string.Empty;
                

                LlenarGrid();
            }
            else
            {
                alertas("Error al ingresar Rol");

            }
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            int resultado = Clases.Rol.BorrarRol(int.Parse(tid.Text));


            if (resultado > 0)
            {
                alertas("Rol borrado con éxito");
               
                tid.Text = string.Empty;
                LlenarGrid();
            }
            else
            {
                alertas("Error al borrar rol");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int Codigo = int.Parse(tid.Text);
            string Nombre = tnombre.Text;
           


            int resultado = Clases.Rol.Modificar(Codigo, Nombre);
            if (resultado > 0)
            {
                alertas("Rol ha sido actualizado con éxito");
                tid.Text = string.Empty;
                tnombre.Text = string.Empty;
               

                LlenarGrid();
            }
            else
            {
                alertas("Error al actualizar Rol");
            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            int Codigo = int.Parse(txtFiltro.Text);
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Roles WHERE Codigo ='" + Codigo + "'"))


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
}