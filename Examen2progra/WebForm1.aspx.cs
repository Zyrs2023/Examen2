﻿using Examen2progra.Clases;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Examen2progra
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void Button1_Click(object sender, EventArgs e)
        {
            Clases.Login objusuario = new Clases.Login(tlogin.Text, tclave.Text);  

            if (Clases.Login.ValidarLogin() > 0)
            {
                Response.Redirect("Principal.aspx");
            }
            else
            {
               
                string mensajeError = "Correo o clave incorrectos. Por favor, inténtalo de nuevo.";
                alertas(mensajeError);
            }

        }

        
    }
}