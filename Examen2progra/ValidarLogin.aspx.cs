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

        // En tu archivo .aspx.cs (por ejemplo, Principal.aspx.cs)
        protected void Button1_Click(object sender, EventArgs e)
        {
            Clases.Login objusuario = new Clases.Login();
            objusuario.SetCorreo(tcorreo.Text);
            objusuario.SetClave(tclave.Text);
            if (Clases.Login.ValidarLogin() > 0)
            {
                Response.Redirect("Principal.aspx");
            }
            else {
                alertas("Correo o clave incorrecta");
            }

        }




    }
}