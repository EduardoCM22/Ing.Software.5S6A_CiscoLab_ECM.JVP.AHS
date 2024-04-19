﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CiscoLab.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Login</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous"/>
    <link href="../styles/Login.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://fonts.googleapis.com/css2?family=Material+Symbols+Outlined:opsz,wght,FILL,GRAD@20..48,100..700,0..1,-50..200" />
</head>
<body>
    <form id="form1" runat="server">


        <div class="cuerpo">
            <%--<div class="arriva">
                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/images/CiscoLabLogoRecortado.png" />     
            </div>--%>

            <div class="centro">

                <h2 style="margin: 10px">"Bienvenido"</h2>

                <div class="datos">
                    <div>
                        <span class="material-symbols-outlined" style="font-size: 80px;">person</span>
                    </div>
                    <div class="inputs">
                        <asp:Label ID="lbUsuario" runat="server" Text="Usuario"></asp:Label>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>


                </div>

                <div class="datos">
                    <div>
                        <span class="material-symbols-outlined" style="font-size: 80px;">key</span>
                    </div>
                    <div class="inputs">
                        <asp:Label ID="lbContraseña" runat="server" Text="Contraseña"></asp:Label>
                        <asp:TextBox ID="txtContraseña" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                    </div>
                </div>

                <asp:Button ID="btnIngresar" runat="server" Text="Ingresar" CssClass="btn btn-outline-dark" Width="150px" OnClick="btnIngresar_Click" />

            </div>
        </div>

    </form>
</body>
</html>
