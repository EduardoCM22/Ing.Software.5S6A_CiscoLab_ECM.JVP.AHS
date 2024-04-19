<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagAdmin.aspx.cs" Inherits="CiscoLab.pages.PagAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>ADMINISTRADOR</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <link href="../styles/PagAdmin.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200" />
</head>

<body>
    <form id="form1" runat="server">


        <nav class="navbar navbar-expand-lg bg-body-tertiary" data-bs-theme="dark">
            <div class="container-fluid">
                <a class="navbar-brand" id="lblCiscoLab">CiscoLab</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                        <li class="nav-item">
                            <asp:LinkButton ID="lnkbtnUsuarios" runat="server" CssClass="nav-link" href="#lblUsuarios">Usuarios</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="lnkbtnReservaciones" runat="server" CssClass="nav-link" href="#lblReservaciones">Reservaciones</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="lnkbtnDiaInhabil" runat="server" CssClass="nav-link" href="#lblDiaInhabil">Dia Inhabil</asp:LinkButton>
                        </li>
                    </ul>
                    <div class="d-flex" role="search" style="align-items: center;">
                        <span class="material-symbols-outlined" style="font-size: 30px; color: white">person</span>
                        <asp:Label class="navbar-brand" ID="lblNombre" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesion" CssClass="btn btn-sm btn-outline-light" OnClick="btnCerrarSesion_Click" />
                    </div>
                </div>
            </div>
        </nav>


        <div class="cuerpo">

            <div class="contenido">
                <asp:Label ID="lblUsuarios" runat="server" Text="Usuarios"></asp:Label>
                <div class="opciones">

                    <asp:Button runat="server" Text="Agregar Usuario" CssClass="btn btn-sm btn-success" OnClick="AgregarUsuario_Click" />

                </div>
                <div class="tabla" style="max-height: 380px; overflow-y: auto;">
                    <asp:GridView ID="gvUsuarios" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Username" HeaderText="No. Control" />
                            <asp:TemplateField HeaderText="Nombre Completo">
                                <ItemTemplate>
                                    <%# Eval("Nombre") + " " + Eval("Apellidos") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Email" HeaderText="Correo" />
                            <asp:BoundField DataField="Password" HeaderText="Contraseña" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CommandArgument='<%# Eval("ID") %>'
                                        OnClick="EditarUsuario_Click" CssClass="btn btn-sm btn-primary">Editar</asp:LinkButton>
                                    <asp:LinkButton runat="server" CommandArgument='<%# Eval("ID") %>'
                                        OnClick="EliminarUsuario_Click" CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('¿Deseas eliminar el usuario?')">Eliminar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>

            <div class="contenido">
                <asp:Label ID="lblReservaciones" runat="server" Text="Reservaciones"></asp:Label>
                <div class="opciones">
                    <asp:Button ID="btnAgregarReservacion" runat="server" Text="Agregar Reservacion" CssClass="btn btn-success" />
                </div>
                <div class="tabla">
                    <asp:Table ID="tblReservaciones" runat="server"></asp:Table>
                </div>
            </div>

            <div class="contenido">
                <asp:Label ID="lblDiaInhabil" runat="server" Text="Día Inhábil"></asp:Label>
                <asp:Button runat="server" Text="Inhabilitar" OnClick="InhabilitarDia_Click"/>
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Calendar ID="cldrDiaInhabil" runat="server"></asp:Calendar>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>        

    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>

</html>
