<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Equipo.aspx.cs" Inherits="Examen2progra.Equipo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" />
    <link href="CSS/Estilos1.css" rel="stylesheet" />
    <div class="container text-caenter">
        <h1>Equipo</h1>
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
        Filtro: <asp:TextBox ID="txtFiltro" CssClass="form-control" runat="server"></asp:TextBox>
        EquipoID: <asp:TextBox ID="tid" CssClass="form-control" runat="server"></asp:TextBox>
        TipoEquipo: <asp:TextBox ID="ttipoEquipo" CssClass="form-control" runat="server"></asp:TextBox>
        Modelo: <asp:TextBox ID="tModelo" CssClass="form-control" runat="server"></asp:TextBox>
        UsuarioID: <asp:TextBox ID="tusuarioid" CssClass="form-control" runat="server"></asp:TextBox>
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

 
</asp:Content>
