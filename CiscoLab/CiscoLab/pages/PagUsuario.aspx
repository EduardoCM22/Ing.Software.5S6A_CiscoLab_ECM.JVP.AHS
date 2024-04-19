<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagUsuario.aspx.cs" Inherits="CiscoLab.pages.PagUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>USUARIO</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <link href="../styles/PagUsuario.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-expand-lg bg-body-tertiary" data-bs-theme="dark">
            <div class="container-fluid">
                <a class="navbar-brand">CiscoLab</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                        <li class="nav-item">
                            <asp:LinkButton ID="lnkbtnDisponibilidad" runat="server" CssClass="nav-link" PostBackUrl="#">Disponibilidad</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="lnkbtnReservaciones" runat="server" CssClass="nav-link" PostBackUrl="#">Reservaciones</asp:LinkButton>
                        </li>
                    </ul>
                    <form class="d-flex" role="search">
                        <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar Sesion" CssClass="btn btn-sm btn-outline-light" />
                    </form>
                </div>
            </div>
        </nav>

        <div class="cuerpo">
            <div class="contenido">
                <asp:Calendar ID="cldrDiaInhabil" runat="server"></asp:Calendar>
                <table>
                    <thead>
                        <tr>
                            <th>Horario</th>
                            <th>Disponibilidad</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th>07:00</th>
                            <th></th>
                        </tr>
                        <tr>
                            <th>08:00</th>
                            <th></th>
                        </tr>
                    </tbody>
                </table>
            </div>

            <div class="contenido">
                <asp:Table ID="tblReservaciones" runat="server"></asp:Table>
            </div>
        </div>
    </form>
</body>
</html>
