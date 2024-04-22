<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagAdmin.aspx.cs" Inherits="CiscoLab.pages.PagAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>CiscoLab Administrador</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
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
                            <asp:LinkButton ID="lnkbtnUsuarios" runat="server" CssClass="nav-link" href="#Usuarios">Usuarios</asp:LinkButton>
                        </li>
                        <li class="nav-item">
                            <asp:LinkButton ID="lnkbtnReservaciones" runat="server" CssClass="nav-link" href="#Reservaciones">Reservaciones</asp:LinkButton>
                        </li>

                    </ul>
                    <div class="d-flex" role="search" style="align-items: center;">
                        <span class="material-symbols-outlined" style="font-size: 40px; font-weight:lighter; color: white">shield_person</span>
                        <asp:Label class="navbar-brand" ID="lblNombre" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnCerrarSesion" runat="server" Text="Cerrar sesión" CssClass="btn btn-sm btn-outline-light" OnClick="btnCerrarSesion_Click" />
                    </div>
                </div>
            </div>
        </nav>


        <div class="cuerpo">
            <asp:ScriptManager ID="ScrptManager" runat="server"></asp:ScriptManager>


            <div class="contenido" id="Usuarios">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>


                        <asp:Label ID="lblUsuarios" runat="server" Text="Usuarios"></asp:Label>

                        <div class="opciones">
                            <asp:Button runat="server" Text="Agregar Usuario" CssClass="btn btn-sm btn-success" OnClick="AgregarUsuario_Click" />
                        </div>

                        <div class="tabla" id="TablaUsuarios">
                            <asp:GridView ID="gvUsuarios" runat="server" CssClass="table table-dark table-bordered" AutoGenerateColumns="false">
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
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%--  --%>
            </div>



            <div class="contenido" id="Reservaciones" style="height: 740px;">
                <div>
                    <asp:Label ID="lblReservaciones" runat="server" Text="Reservaciones"></asp:Label>
                </div>

                <div class="reservaciones">

                    <asp:UpdatePanel ID="ActPanelReservaciones" runat="server">
                        <ContentTemplate>

                            <div class="reservacionesSup">
                                <asp:Calendar ID="cldrReservaciones" runat="server" OnSelectionChanged="cldrReservaciones_SelectionChanged"
                                    OnDayRender="cldrReservaciones_DayRender" CssClass="calendar-dark table table-dark"></asp:Calendar>

                                <div class="tabla" id="TablaCuposReservaciones">
                                    <asp:GridView ID="gvHorasCupos" runat="server" CssClass="table table-dark table-bordered" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="Hora" HeaderText="Hora" />
                                            <asp:TemplateField HeaderText="Cupo">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkCupo" OnClick="GenerarReservacion_Click"
                                                        Enabled='<%# GetHabilitada(Eval("InfoCupo")) %>' Text="Reservar"
                                                        CssClass='<%# GetColor(Eval("InfoCupo")) %>'
                                                        CommandArgument='<%# Eval("Hora") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div class="datos">
                                    <asp:Label ID="lblNoControl" for="txtNoControl" runat="server" Visible="false">Número de Control</asp:Label>
                                    <asp:TextBox ID="txtNoControl" class="form-control" placeholder="Número de control del usuario que reserva"
                                        runat="server" Visible="false" Enabled="false" MaxLength="9"></asp:TextBox>
                                    <asp:Label ID="lblValidacionTxtNoControl" runat="server" CssClass="text-danger"
                                        Text="El número de control es obligatorio" Visible="false"></asp:Label>
                                </div>
                            </div>

                            <div class="divisor"></div>

                            <div class="tabla" id="TablaReservaciones">
                                <asp:GridView ID="gvReservaciones" runat="server" CssClass="table table-dark table-bordered" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                        <asp:BoundField DataField="Hora" HeaderText="Hora" />
                                        <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" />
                                        <asp:BoundField DataField="Username" HeaderText="No. Control" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" CommandArgument='<%# Eval("ID") %>'
                                                    OnClick="EliminarReservacioon_Click" CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('¿Deseas eliminar la reservación?')">Eliminar</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <%--  --%>
            </div>



        </div>

    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>

</html>
