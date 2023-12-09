using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Examen2progra
{
    public partial class Empleado : System.Web.UI.Page
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM empleados"))
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
            int resultado = Clases.Empleado.Agregar(tnombre.Text, tcorreo.Text, tclave.Text);
            if (resultado > 0)
            {
                alertas("Empleado ha sido ingresado con exito");
                tnombre.Text = string.Empty;
                tcorreo.Text = string.Empty;
                tclave.Text = string.Empty;

                LlenarGrid();
            }
            else
            {
                alertas("Error al ingresar Empleado");

            }
        }


        protected void Button2_Click(object sender, EventArgs e)
        {

            int resultado = Clases.Empleado.BorrarEmpleado(int.Parse(tid.Text));

            if (resultado > 0)
            {
                alertas("Usuario borrado con éxito");
                tid.Text = string.Empty;
                LlenarGrid();
            }
            else
            {
                alertas("Error al borrar usuario");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int CodigoID = int.Parse(tid.Text);
            string Nombre = tnombre.Text;
            string Correo = tcorreo.Text;
            string Clave = tclave.Text;


            int resultado = Clases.Empleado.Modificar(CodigoID, Nombre, Correo, Clave);
            if (resultado > 0)
            {
                alertas("Empleado ha sido actualizado con éxito");
                tid.Text = string.Empty;
                tnombre.Text = string.Empty;
                tcorreo.Text = string.Empty;
                tclave.Text = string.Empty;

                LlenarGrid();
            }
            else
            {
                alertas("Error al actualizar Empleado");
            }


        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            int Codigo = int.Parse(txtFiltro.Text);
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Empleados WHERE codigo ='" + Codigo + "'"))


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