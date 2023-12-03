using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Web.UI;

namespace Examen2progra
{
    public partial class Reparacion : Page
    {
        protected void calFechaReparacion_SelectionChanged(object sender, EventArgs e)
        {
           
            if (calFechaReparacion.SelectedDate != null)
            {
                DateTime fechaSeleccionada = calFechaReparacion.SelectedDate;

                
                tFechaReparacion.Text = fechaSeleccionada.ToString("yyyy-MM-dd");
            }
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
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM REPARACIONES"))
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
            if (DateTime.TryParse(tFechaReparacion.Text, out DateTime fechaReparacion))
            {
                if (int.TryParse(tequipoID.Text, out int equipoID))
                {
                    
                    string estado = rblEstado.SelectedValue;

                    int resultado = Clases.Reparacion.AgregarReparacion(equipoID, fechaReparacion, estado);

                    if (resultado > 0)
                    {
                        alertas("Reparación ingresada con éxito");
                        tequipoID.Text = string.Empty;
                        tFechaReparacion.Text = string.Empty;

                        LlenarGrid();
                    }
                    else
                    {
                        alertas("Error al ingresar Reparación");
                    }
                }
                else
                {
                    alertas("Formato de equipoID incorrecto");
                }
            }
            else
            {
                alertas("Formato de fecha incorrecto");
            }
        }




        protected void Button2_Click(object sender, EventArgs e)
        {
            int resultado = Clases.Reparacion.BorrarReparacion(int.Parse(treparacionID.Text));

            if (resultado > 0)
            {
                alertas("Equipo borrado con éxito");
                treparacionID.Text = string.Empty;
                LlenarGrid();
            }
            else
            {
                alertas("Error al borrar Equipo");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (int.TryParse(treparacionID.Text, out int reparacionID) &&
            int.TryParse(tequipoID.Text, out int equipoID) &&
             DateTime.TryParse(tFechaReparacion.Text, out DateTime fechaSolicitud))
            {
                
                string estado = rblEstado.SelectedValue;

                int resultado = Clases.Reparacion.ModificarReparacion(reparacionID, equipoID, fechaSolicitud, estado);

                if (resultado > 0)
                {
                    alertas("Reparación actualizada con éxito");
                    treparacionID.Text = string.Empty;
                    tequipoID.Text = string.Empty;
                    tFechaReparacion.Text = string.Empty;

                    LlenarGrid();
                }
                else
                {
                    alertas("Error al actualizar Reparación");
                }
            }
            else
            {
                alertas("Ingrese valores válidos para ReparacionID, EquipoID o Fecha de Solicitud");
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            int EquipoID = int.Parse(txtFiltro.Text);
            string constr = ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM REPARACIONES WHERE EquipoID ='" + EquipoID + "'"))


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
