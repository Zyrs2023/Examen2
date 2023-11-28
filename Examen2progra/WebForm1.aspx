<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Examen2progra.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login V2</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="CSS/Login.css" rel="stylesheet" />
    
</head>

<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2 class="login-title">Bienvenido</h2>
            <div class="login-form">
                <div class="mb-3">
                    <asp:TextBox ID="tlogin" class="form-control" aria-describedby="emailHelp" runat="server" placeholder="Correo"></asp:TextBox>
                </div>
                <div class="mb-3">
                    <asp:TextBox ID="tclave" class="form-control" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
                </div>
                <div class="mb-3 form-check">
                    <asp:CheckBox ID="chkRememberMe" class="form-check-input" runat="server" />
                    <label class="form-check-label" for="chkRememberMe">Recordar</label>
                </div>
                <asp:Button ID="Button1" class="btn btn-primary login-btn" runat="server" Text="Ingresar" OnClick="Button1_Click" />
                 <div id="Div1" class="error-message" runat="server"></div>
            </div>
            
            <asp:Label ID="lmensaje" runat="server" Text=""></asp:Label>
        </div>
    </form>

    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</body>
</html>
