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
    public partial class RolEmpleado : System.Web.UI.Page
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM empleados_roles"))
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
            int resultado = Clases.RolesEmpleados.Agregar(int.Parse(tid.Text), int.Parse(trol.Text));
            if (resultado > 0)
            {
                alertas("Rol Empleado ha sido ingresado con exito");
                trol.Text = string.Empty;
                trol.Text = string.Empty;


                LlenarGrid();
            }
            else
            {
                alertas("Error al ingresar Rol Empleado");

            }
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            int resultado = Clases.RolesEmpleados.BorrarRolesEmpleados(int.Parse(tid.Text), int.Parse(trol.Text));
            

            if (resultado > 0)
            {
                alertas("Usuario borrado con éxito");
                trol.Text = string.Empty;
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
            int IdEmpleado = int.Parse(tid.Text);
            int IdRol = int.Parse(trol.Text);




            int resultado = Clases.RolesEmpleados.Modificar(IdEmpleado, IdRol);
            if (resultado > 0)
            {
                alertas("Rol Empleado ha sido actualizado con éxito");
                tid.Text = string.Empty;
                trol.Text = string.Empty;


                LlenarGrid();
            }
            else
            {
                alertas("Error al actualizar Rol Empleado");
            }

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            int IdEmpleado = int.Parse(txtFiltro.Text);
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM empleados_roles WHERE IdEmpleado ='" + IdEmpleado + "'"))


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