using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CiscoLab.EntityLayer;
using CiscoLab.DataLayer;
using System.Globalization;
using System.Web.Management;
using System.Text;
using System.Runtime.InteropServices.WindowsRuntime;

namespace CiscoLab.pages
{
    public partial class PagOpUs : System.Web.UI.Page
    {

        protected static int ID;
        protected static string OriginNoControl;
        protected static string OriginEmail;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    ID = Convert.ToInt32(Request.QueryString["ID"].ToString());

                    if (ID != 0)
                    {
                        tlt.Text = "Editar Usuario";
                        lblTitulo.Text = "Editar Usuario";
                        lblPassw.Text = "Contraseña";
                        lblConfmPassw.Text = "Confirmar contraseña";
                        btnSubmit.Text = "Actualizar";

                        Usuario empleado = new UsuarioDL().ObtenerUsuario(ID);

                        txtNoControl.Text = empleado.Username;
                        OriginNoControl = empleado.Username;

                        txtNombre.Text = empleado.Nombre;
                        txtApellidos.Text = empleado.Apellidos;

                        txtEmail.Text = empleado.Email;
                        OriginEmail = empleado.Email;

                        //txtPassword.Text = "";
                        //rfvPassword.Enabled = false;
                        //revPassword.Enabled = false;

                        //txtConfirmPassword.Text = "";
                        //rfvConfirmPassword.Enabled = false;
                        //cvConfirmPassword.Enabled = false;

                        //rfvPassword.Enabled = true;
                        rfvPassword.Text = "La contraseña es opcional";
                        rfvPassword.CssClass = "text-warning";

                        //revPassword.Enabled = true;
                        revPassword.CssClass = "text-warning";

                        //rfvConfirmPassword.Enabled = true;
                        rfvConfirmPassword.Text = "Sí ingresó una contraseña confírmela";
                        rfvConfirmPassword.CssClass = "text-warning";

                        //cvConfirmPassword.Enabled = true;
                        cvConfirmPassword.CssClass = "text-warning";

                    }
                    else
                    {
                        lblTitulo.Text = "Nuevo Usuario";
                        tlt.Text = "Nuevo Usuario";
                        btnSubmit.Text = "Agregar";
                    }
                }
                else
                    Response.Redirect("~/pages/PagAdmin.aspx");
            }
            else
            {
                if (ID != 0)
                {
                    if (txtPassword.Text.Trim().Length >= 1 || txtConfirmPassword.Text.Trim().Length >= 1)
                    {
                        //rfvPassword.Enabled = true;
                        //rfvPassword.Text = "La contraseña es opcional";
                        //rfvPassword.CssClass = "text-warning";

                        //revPassword.Enabled = true;
                        //revPassword.CssClass = "text-warning";

                        //rfvConfirmPassword.Enabled = true;
                        //rfvConfirmPassword.Text = "Sí ingresó una contraseña confírmela";
                        //rfvConfirmPassword.CssClass = "text-warning";

                        //cvConfirmPassword.Enabled = true;
                        //cvConfirmPassword.CssClass = "text-warning";

                        editarPassword = true;
                    }
                    else
                    {
                        //rfvPassword.Enabled = false;
                        //revPassword.Enabled = false;
                        //rfvConfirmPassword.Enabled = false;
                        //cvConfirmPassword.Enabled = true;
                        editarPassword = false;
                        validado = true;
                    }
                }          
            }
        }

        protected bool validado;
        protected void cvConfirmPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (txtPassword.Text == txtConfirmPassword.Text);
            validado = args.IsValid;
        }

        bool editarPassword;
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!validado) { return; }

            if (!revPassword.IsValid) { return; }

            txtNoControl.Text = txtNoControl.Text.Trim();
            txtNombre.Text = txtNombre.Text.Trim();
            txtApellidos.Text = txtApellidos.Text.Trim();
            txtEmail.Text = txtEmail.Text.Trim();
            txtPassword.Text = txtPassword.Text.Trim();
            txtConfirmPassword.Text = txtConfirmPassword.Text.Trim();
            

            if (!rfvNombre.IsValid || !revNombre.IsValid || !rfvNoControl.IsValid || !revNoControl.IsValid ||
                !rfvApellidos.IsValid || !revApellidos.IsValid || !rfvEmail.IsValid || !revEmail.IsValid)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error: Complete correctamente los campos obligatorios.')", true);
                return;
            }

            Usuario entidad = new Usuario();
            entidad.ID = ID;
            entidad.Nombre = txtNombre.Text.Trim();
            entidad.Apellidos = txtApellidos.Text.Trim();
            entidad.Username = txtNoControl.Text.Trim().ToUpper();
            entidad.Email = txtEmail.Text.Trim().ToLower();
            entidad.Password = txtPassword.Text.Trim();
            

            int respuesta;   

            if (ID != 0)
            {
                if (OriginNoControl != entidad.Username)
                {
                    int validar = new UsuarioDL().Validar(entidad, 2);
                    if (validar == 2)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error: Número de control ya registrado.')", true);
                        return;
                    }
                    else if (validar == 0) 
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error: Validación de datos.')", true);
                        return;
                    }
                }
                if (OriginEmail != entidad.Email)
                {
                    int validar = new UsuarioDL().Validar(entidad, 3);
                    if (validar == 3)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error: Correo electrónico ya registrado.')", true);
                        return;
                    }
                    else if (validar == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error: Validación de datos.')", true);
                        return;
                    }
                }
                

                respuesta = new UsuarioDL().EditarUsuario(entidad, editarPassword);

            }
            else
            {
                respuesta = new UsuarioDL().AgregarUsuario(entidad);
            }

            if (respuesta == 1)
            {
                if (ID != 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Éxito: Usuario actualizado correctamente.'); setTimeout(function(){ window.location.href = 'PagAdmin.aspx'; }, 0);", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Éxito: Usuario agregado correctamente.')", true);
                    txtNoControl.Text = string.Empty;
                    txtNombre.Text = string.Empty;
                    txtApellidos.Text = string.Empty;
                    txtEmail.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtConfirmPassword.Text = string.Empty;
                }
            }
            else if (respuesta == 2)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error: Número de control ya registrado.')", true);
            }
            else if (respuesta == 3)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error: Correo electrónico ya registrado.')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error: No se pudo realizar la operacion.')", true);
            }
        }
    }
}