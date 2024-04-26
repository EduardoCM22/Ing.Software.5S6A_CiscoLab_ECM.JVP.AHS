﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagUsuario.aspx.cs" Inherits="CiscoLab.pages.PagUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" id="Inicio">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>CiscoLab Usuario</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous"/>
    <link href="../styles/PagUsuario.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200" />
    <link rel="icon" href="~/images/Icon.png" type="image/x-icon"/>
</head>

<body>
    <form id="form1" runat="server">

        <nav class="navbar navbar-expand-lg bg-body-tertiary fixed-top" data-bs-theme="dark">
            <div class="container-fluid">
                <a class="navbar-brand" id="lblCiscoLab">CiscoLab</a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                        <li class="nav-item">
                            <asp:LinkButton ID="lnkbtnInicio" runat="server" CssClass="nav-link" href="#Inicio">Inicio</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="lnkbtnDisponibilidad" runat="server" CssClass="nav-link" href="#Disponibilidad">Disponibilidad</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="lnkbtnMisReservaciones" runat="server" CssClass="nav-link" href="#MisReservaciones">Mis Reservaciones</asp:LinkButton>
                        </li>
                    </ul>
                    <div class="d-flex" role="search" style="align-items: center;">
                        <span class="material-symbols-outlined" style="font-size: 40px; font-weight: lighter; color: white">person</span>
                        <asp:Label class="navbar-brand" ID="lblNombre" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar sesión" CssClass="btn btn-sm btn-outline-light" OnClick="btnCerrarSesion_Click" />
                    </div>
                </div>
            </div>
        </nav>



        <div class="cuerpo">
            <asp:ScriptManager ID="ScrptManager" runat="server"></asp:ScriptManager>



            <div class="contenido" id="Disponibilidad">

                <div>
                    <asp:Label ID="lblDisponibilidad" runat="server" Text="Disponibilidad"></asp:Label>
                </div>

                <div class="disponibilidad">

                    <asp:UpdatePanel ID="ActPanelDisponibilidad" runat="server">
                        <ContentTemplate>

                            <asp:Calendar ID="cldrReservaciones" runat="server" OnSelectionChanged="cldrReservaciones_SelectionChanged"
                                OnDayRender="cldrReservaciones_DayRender" CssClass="calendar-dark table table-dark"></asp:Calendar>

                            <div class="tabla" id="TablaCuposReservaciones">
                                <asp:GridView ID="gvHorasCupos" runat="server" CssClass="table table-dark table-bordered" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="Hora" HeaderText="Hora" />
                                        <asp:TemplateField HeaderText="Reservar">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lnkCupo" OnClick="GenerarReservacion_Click"
                                                    Width="80px"
                                                    Text='<%# GetTexto(Eval("InfoCupo")) %>'
                                                    Enabled='<%# GetHabilitada(Eval("InfoCupo")) %>'
                                                    CssClass='<%# GetColor(Eval("InfoCupo")) %>'
                                                    CommandArgument='<%# Eval("Hora") %>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>

            </div>



            <div class="contenido" id="MisReservaciones">

                <div>
                    <asp:Label ID="lblMisReservaciones" runat="server" Text="Mis Reservaciones"></asp:Label>
                </div>

                <div class="reservaciones">
                    <asp:UpdatePanel ID="ActPanelReservaciones" runat="server">
                        <ContentTemplate>

                            <div class="tabla" id="TablaReservaciones">
                                <asp:GridView ID="gvReservaciones" runat="server" CssClass="table table-dark table-bordered" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                        <asp:BoundField DataField="Hora" HeaderText="Hora" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" CommandArgument='<%# Eval("ID") %>'
                                                    OnClick="EliminarReservacion_Click" CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('¿Deseas cancelar la reservación?')">Cancelar</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

            </div>

        </div>

        <%--  --%>
    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
