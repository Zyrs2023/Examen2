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
    public partial class Asignacion : System.Web.UI.Page
    {
        protected void calFechaAsignacion_SelectionChanged(object sender, EventArgs e)
        {
            tFechaAsignacion.Text = calFechaAsignacion.SelectedDate.ToShortDateString();
        }
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ASIGNACION"))
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
            if (int.TryParse(tReparacionID.Text, out int reparacionID) &&
                int.TryParse(tTecnicoID.Text, out int tecnicoID) &&
                DateTime.TryParse(tFechaAsignacion.Text, out DateTime fechaAsignacion))
            {
                int resultado = Clases.Asignacion.AgregarAsignacion(reparacionID, tecnicoID, fechaAsignacion);

                if (resultado > 0)
                {
                    alertas("Asignación ingresada con éxito");
                    tReparacionID.Text = string.Empty;
                    tTecnicoID.Text = string.Empty;
                    tFechaAsignacion.Text = string.Empty;

                    LlenarGrid();
                }
                else
                {
                    alertas("Error al ingresar Asignación");
                }
            }
            else
            {
                alertas("Ingrese valores válidos para ReparacionID, TecnicoID o Fecha de Asignación");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tAsignacionID.Text, out int asignacionID) &&
                int.TryParse(tReparacionID.Text, out int reparacionID) &&
                int.TryParse(tTecnicoID.Text, out int tecnicoID) &&
                DateTime.TryParse(tFechaAsignacion.Text, out DateTime fechaAsignacion))
            {
                int resultado = Clases.Asignacion.ModificarAsignacion(asignacionID, reparacionID, tecnicoID, fechaAsignacion);

                if (resultado > 0)
                {
                    alertas("Asignación ha sido actualizada con éxito");
                    tAsignacionID.Text = string.Empty;
                    tReparacionID.Text = string.Empty;
                    tTecnicoID.Text = string.Empty;
                    tFechaAsignacion.Text = string.Empty;

                    LlenarGrid();
                }
                else
                {
                    alertas("Error al actualizar Asignación");
                }
            }
            else
            {
                alertas("Ingrese valores válidos para AsignacionID, ReparacionID, TecnicoID o Fecha de Asignación");
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtFiltro.Text, out int asignacionID))
            {
                string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM ASIGNACION WHERE AsignacionID = @AsignacionID"))
                    {
                        cmd.Parameters.Add(new SqlParameter("@AsignacionID", asignacionID));

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
            else
            {
                alertas("Ingrese un valor válido para AsignacionID");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            int resultado = Clases.Asignacion.BorrarAsignacion(int.Parse(txtFiltro.Text));

            if (resultado > 0)
            {
                alertas("Asignacion borrado con éxito");
                txtFiltro.Text = string.Empty;
                LlenarGrid();
            }
            else
            {
                alertas("Error al borrar Asignacion");
            }
        }
    }
}
