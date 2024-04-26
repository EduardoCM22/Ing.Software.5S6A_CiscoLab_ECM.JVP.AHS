using CiscoLab.DataLayer;
using CiscoLab.EntityLayer;
using System;
using System.Web.UI;

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Inicio de sesión exitoso.'); window.location.href = 'PagUsuario.aspx';", true);
                Session["Usuario"] = user;
            }
            else
            {
                Administrador admin = new AdministradorDL().LoginAdministrador(txtUsuario.Text, txtContraseña.Text);
                if (admin != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Inicio de sesión exitoso.'); window.location.href = 'PagAdmin.aspx';", true);
                    Session["Administrador"] = admin;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error: Nombre de usuario o contraseña incorrectos.');", true);
                    txtContraseña.Focus();
                }
            }
        }
    }
}