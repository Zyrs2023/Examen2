using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace Examen2progra
{
    public partial class DetalleReparacion : System.Web.UI.Page
    {
        protected void calFechaInicio_SelectionChanged(object sender, EventArgs e)
        {
            DateTime fechaInicioSeleccionada = calFechaInicio.SelectedDate;
            tFechaInicio.Text = fechaInicioSeleccionada.ToString("yyyy-MM-dd");
        }
        protected void calFechaFin_SelectionChanged(object sender, EventArgs e)
        {
            DateTime fechaFinSeleccionada = calFechaFin.SelectedDate;
            tFechaFin.Text = fechaFinSeleccionada.ToString("yyyy-MM-dd");
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM DETALLESREPARACION"))
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
                !string.IsNullOrEmpty(tDescripcion.Text) &&
                DateTime.TryParse(tFechaInicio.Text, out DateTime fechaInicio) &&
                DateTime.TryParse(tFechaFin.Text, out DateTime fechaFin))
            {
                int resultado = Clases.DetalleReparacion.AgregarDetalleReparacion(reparacionID, tDescripcion.Text, fechaInicio, fechaFin);

                if (resultado > 0)
                {
                    alertas("Detalle de Reparación ingresado con éxito");
                    tReparacionID.Text = string.Empty;
                    tDescripcion.Text = string.Empty;
                    tFechaInicio.Text = string.Empty;
                    tFechaFin.Text = string.Empty;

                    LlenarGrid();
                }
                else
                {
                    alertas("Error al ingresar Detalle de Reparación");
                }
            }
            else
            {
                alertas("Ingrese valores válidos para ReparacionID, Descripción o Fechas");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tDetalleID.Text, out int detalleID))
            {
                int resultado = Clases.DetalleReparacion.BorrarDetalleReparacion(detalleID);

                if (resultado > 0)
                {
                    alertas("Detalle de Reparación borrado con éxito");
                    tDetalleID.Text = string.Empty;
                    LlenarGrid();
                }
                else
                {
                    alertas("Error al borrar Detalle de Reparación");
                }
            }
            else
            {
                alertas("Ingrese un valor válido para DetalleID");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (int.TryParse(tDetalleID.Text, out int detalleID) &&
                int.TryParse(tReparacionID.Text, out int reparacionID) &&
                !string.IsNullOrEmpty(tDescripcion.Text) &&
                DateTime.TryParse(tFechaInicio.Text, out DateTime fechaInicio) &&
                DateTime.TryParse(tFechaFin.Text, out DateTime fechaFin))
            {
                int resultado = Clases.DetalleReparacion.ModificarDetalleReparacion(detalleID, reparacionID, tDescripcion.Text, fechaInicio, fechaFin);

                if (resultado > 0)
                {
                    alertas("Detalle de Reparación ha sido actualizado con éxito");
                    tDetalleID.Text = string.Empty;
                    tReparacionID.Text = string.Empty;
                    tDescripcion.Text = string.Empty;
                    tFechaInicio.Text = string.Empty;
                    tFechaFin.Text = string.Empty;

                    LlenarGrid();
                }
                else
                {
                    alertas("Error al actualizar Detalle de Reparación");
                }
            }
            else
            {
                alertas("Ingrese valores válidos para DetalleID, ReparacionID, Descripción o Fechas");
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtFiltro.Text, out int detalleID))
            {
                string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM DETALLESREPARACION WHERE DetalleID = @DetalleID"))
                    {
                        cmd.Parameters.Add(new SqlParameter("@DetalleID", detalleID));

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
                alertas("Ingrese un valor válido para DetalleID");
            }
        }
    }
}