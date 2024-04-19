<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagOpUs.aspx.cs" Inherits="CiscoLab.pages.PagOpUs" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="../styles/PagOpUs.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <asp:Label ID="lblTitulo" runat="server" CssClass="fs-4 fw-bold"></asp:Label>

        <div>
            <div>
                <label for="txtNoControl">Número de Control</label>
                <asp:TextBox ID="txtNoControl" class="form-control" placeholder="Ingresa el número de control del usuario"
                    runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNoControl" runat="server" ControlToValidate="txtNoControl"
                    ErrorMessage="El número de control es obligatorio" Display="Dynamic" CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="revNoControl" runat="server" ControlToValidate="txtNoControl"
                    ErrorMessage="El número de control debe tener 9 caracteres" ValidationExpression="^.{9}$"
                    Display="Dynamic" CssClass="text-danger" />
            </div>

            <div>
                <label for="txtNombre">Nombre</label>
                <asp:TextBox ID="txtNombre" class="form-control" placeholder="Ingresa el nombre del usuario"
                    runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre"
                    ErrorMessage="El nombre es obligatorio" Display="Dynamic" CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="revNombre" runat="server" ControlToValidate="txtNombre"
                    ErrorMessage="El nombre debe tener entre 2 y 50 caracteres" ValidationExpression="^.{2,50}$"
                    Display="Dynamic" CssClass="text-danger" />
            </div>

            <div>
                <label for="txtApellidos">Apellidos</label>
                <asp:TextBox ID="txtApellidos" class="form-control" placeholder="Ingresa los apellidos del usuario"
                    runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvApellidos" runat="server" ControlToValidate="txtApellidos"
                    ErrorMessage="Los apellidos son obligatorios" Display="Dynamic" CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="revApellidos" runat="server" ControlToValidate="txtApellidos"
                    ErrorMessage="Los apellidos deben tener entre 2 y 50 caracteres" ValidationExpression="^.{2,50}$"
                    Display="Dynamic" CssClass="text-danger" />
            </div>

            <div>
                <label for="txtEmail">Correo Electrónico</label>
                <asp:TextBox ID="txtEmail" class="form-control" placeholder="Ingresa el correo electrónico del usuario"
                    runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="El correo electrónico es obligatorio" Display="Dynamic" CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="El correo electrónico debe tener un formato válido y máximo 50 caracteres"
                    ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$" Display="Dynamic" CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="revEmailLength" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="El correo electrónico debe tener un máximo de 50 caracteres"
                    ValidationExpression="^.{1,50}$" Display="Dynamic" CssClass="text-danger" />
            </div>

            <div>
                <div>
                    <div>
                        <label for="txtPassword">Contraseña</label>
                    </div>
                    <asp:TextBox ID="txtPassword" class="form-control" placeholder="Ingresa la contraseña"
                         runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="La contraseña es obligatoria" Display="Dynamic" CssClass="text-danger" />
                    <asp:RegularExpressionValidator ID="revPassword" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="La contraseña debe tener entre 8 y 16 caracteres y puede incluir letras, números y caracteres especiales"
                        ValidationExpression="^[a-zA-Z0-9!@#$%^&*()_+-=]{8,16}$"
                        Display="Dynamic" CssClass="text-danger" />
                </div>
                <div>
                    <label for="txtConfirmPassword">Confirmar Contraseña</label>
                    <asp:TextBox ID="txtConfirmPassword" class="form-control" placeholder="Confirma la contraseña"
                         runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                        ErrorMessage="Por favor confirma la contraseña" Display="Dynamic" CssClass="text-danger" />
                    <asp:CustomValidator ID="cvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                        ErrorMessage="Las contraseñas no coinciden" OnServerValidate="cvConfirmPassword_ServerValidate"
                        Display="Dynamic" CssClass="text-danger" />
                </div>
            </div>
        </div>

        <div class="footer">
            <%--<a href="../pages/PagAdmin.aspx" class="btn btn-sm btn-warning">Volver</a>--%>
            <button type="button" onclick="window.location.href='../pages/PagAdmin.aspx'" class="btn btn-sm btn-warning">Volver</button>
            <%--<asp:LinkButton runat="server" PostBackUrl="../pages/PagAdmin.aspx" CssClass="btn btn-sm btn-warning">Volver</asp:LinkButton>--%>
            <asp:Button ID="btnSubmit" runat="server" Text="Agregar" CssClass="btn btn-sm btn-primary" OnClick="btnSubmit_Click" />
        </div>

    </form>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
</body>
</html>
