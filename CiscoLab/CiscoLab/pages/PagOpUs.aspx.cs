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

namespace CiscoLab.pages
{
    public partial class PagOpUs : System.Web.UI.Page
    {

        private static int ID = 0;
        //DepartamentoBL departamentoBL = new DepartamentoBL();
        //EmpleadoBL empleadoBL = new EmpleadoBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["ID"] != null)
                {
                    ID = Convert.ToInt32(Request.QueryString["ID"].ToString());

                    if (ID != 0)
                    {
                        lblTitulo.Text = "Editar Usuario";
                        btnSubmit.Text = "Actualizar";

                        Usuario empleado = new UsuarioDL().ObtenerUsuario(ID);
                        txtNombre.Text = empleado.Nombre;
                        txtApellidos.Text = empleado.Apellidos;
                        //CargarDepartamentos(empleado.Departamento.IdDepartamento.ToString());
                        txtNoControl.Text = empleado.Username;
                        txtEmail.Text = empleado.Email;
                        txtPassword.Text = empleado.Password;
                        txtConfirmPassword.Text = empleado.Password;
                        //txtFechaContrato.Text = Convert.ToDateTime(empleado.FechaContrato, new CultureInfo("es-PE")).ToString("yyyy-MM-dd");
                    }
                    else
                    {
                        lblTitulo.Text = "Nuevo Usuario";
                        btnSubmit.Text = "Guardar";
                        //CargarDepartamentos();
                    }
                }
                else
                    Response.Redirect("~/Default.aspx");
            }
        }

        //private void CargarDepartamentos(string idDepartamento = "")
        //{
        //    List<Departamento> lista = departamentoBL.Lista();

        //    ddlDepartamento.DataTextField = "Nombre";
        //    ddlDepartamento.DataValueField = "IdDepartamento";

        //    ddlDepartamento.DataSource = lista;
        //    ddlDepartamento.DataBind();

        //    if (idDepartamento != "")
        //        ddlDepartamento.SelectedValue = idDepartamento;

        //}

        protected bool validado;
        protected void cvConfirmPassword_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (txtPassword.Text == txtConfirmPassword.Text);
            validado = args.IsValid;
        }

        //protected void btnGeneratePassword_Click(object sender, EventArgs e)
        //{
        //    int length = 8;
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_+=";
        //    Random random = new Random();
        //    StringBuilder stringBuilder = new StringBuilder(length);

        //    for (int i = 0; i < length; i++)
        //    {
        //        stringBuilder.Append(chars[random.Next(chars.Length)]);
        //    }

        //    txtPassword.Text=stringBuilder.ToString();
        //    txtConfirmPassword.Text = stringBuilder.ToString();
        //}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!validado) { return; }
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
                respuesta = new UsuarioDL().EditarUsuario(entidad);
            }
            else
            {
                respuesta = new UsuarioDL().AgregarUsuario(entidad);
            }

            if (respuesta == 1) 
            {
                Response.Redirect("~/pages/PagAdmin.aspx");
            }
            else if (respuesta == 2) 
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Error: Numero de Control duplicado')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('No se pudo realizar la operacion')", true);
            }
        }
    }
}