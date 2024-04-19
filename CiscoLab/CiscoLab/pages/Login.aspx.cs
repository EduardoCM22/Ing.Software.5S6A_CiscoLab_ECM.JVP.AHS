using CiscoLab.DataLayer;
using CiscoLab.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CiscoLab
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            Usuario user = new UsuarioDL().LoginUsuario(txtUsuario.Text, txtContraseña.Text);
            if (user != null) 
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inicio de sesión exitoso.');", true);
                Session["Usuario"] = user;
                Response.Redirect("PagUsuario.aspx");
            }
            else 
            {
                Administrador admin = new AdministradorDL().LoginAdministrador(txtUsuario.Text, txtContraseña.Text);
                if (admin != null)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Inicio de sesión exitoso.');", true);
                    Session["Administrador"] = admin;
                    Response.Redirect("PagAdmin.aspx");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error: Nombre de usuario o contraseña incorrectos.');", true);
                }
            }
        }
    }
}