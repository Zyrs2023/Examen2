﻿using Examen2progra.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Examen2progra
{
    public partial class Principal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lusuario.Text = Clases.Login.GetNombre();
            Label2.Text = Clases.Login.Getnombrerol();

        }
    }
}