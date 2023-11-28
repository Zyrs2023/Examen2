<%@ Page Title="" Language="C#" MasterPageFile="~/Menu.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="Examen2progra.Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="CSS/Estilos1.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Image ID="Image1" runat="server" ImageUrl=<img src="Imagen/Imagen.jpg" /> />
    Bienvenido: <asp:Label ID="lusuario" runat="server" Text="Label"></asp:Label>
</asp:Content>
