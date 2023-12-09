<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Rol.aspx.cs" Inherits="Examen2progra.Rol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" />
    <link href="CSS/Estilos1.css" rel="stylesheet" />
        <div class="container text-caenter">
        <h1>Rol</h1>
    </div>
    <div>
    <br />
    <br />
    <asp:GridView runat="server" ID="datagrid" PageSize="10" HorizontalAlign="Center"
            CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header"
            RowStyle-CssClass="rows" AllowPaging="True" />

    <br />
    <br />
    <br />
</div>
    <div class="container text-center">
        Filtro: <asp:TextBox ID="txtFiltro" class="form-control" runat="server"></asp:TextBox>
        Codigo: <asp:TextBox ID="tid" class="form-control" runat="server"></asp:TextBox>
        Nombre: <asp:TextBox ID="tnombre" class="form-control" runat="server"></asp:TextBox>
        
      
        
    </div>
     <div class="container text-center">
    <asp:Button ID="Button1" class="btn btn-primary btn-custom" runat="server" Text="Agregar" OnClick="Button1_Click" />
    <asp:Button ID="Button2" class="btn btn-primary btn-custom" runat="server" Text="Borrar" OnClick="Button2_Click" />
    <asp:Button ID="Button3" runat="server" class="btn btn-primary btn-custom" Text="Modificar" OnClick="Button3_Click" style="width: 120px" />
    <asp:Button ID="Bconsulta" runat="server" class="btn btn-primary btn-custom" Text="Consultar" OnClick="Button4_Click" />
</div>
   <footer class="footer">
        <div class="container">
            <p>Innovasolucionescr.com</p>
        </div>
    </footer>

    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</asp:Content>