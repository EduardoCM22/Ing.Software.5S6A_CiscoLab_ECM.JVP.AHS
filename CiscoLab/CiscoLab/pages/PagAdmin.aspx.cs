using CiscoLab.DataLayer;
using CiscoLab.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace CiscoLab.pages
{
    public partial class PagAdmin : System.Web.UI.Page
    {
        private int recarga;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Administrador administrador = new Administrador();
            //administrador.Nombre = "Eduardo";
            //administrador.Apellidos = "Campos Manriquez";
            Administrador administrador = (Administrador)Session["Administrador"];
            lblNombre.Text = "  " + administrador.Nombre + " " + administrador.Apellidos;
            CargarUsuarios();
            CargarReservaciones();
            //cldrDiaInhabil.DayRender += new DayRenderEventHandler(cldrDiaInhabil_DayRender);
            //cldrReservaciones.DayRender += new DayRenderEventHandler(cldrReservaciones_DayRender);

        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/Login.aspx");
        }

        private void CargarUsuarios()
        {
            List<Usuario> usuarios = new UsuarioDL().ObtenerUsuarios();

            gvUsuarios.DataSource = usuarios;
            gvUsuarios.DataBind();

        }

        private void CargarReservaciones()
        {
            List<Reservacion> reservaciones = new ReservacionDL().ObtenerReservaciones();

            gvReservaciones.DataSource = reservaciones;
            gvReservaciones.DataBind();
        }

        protected void cldrReservaciones_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsOtherMonth)
            {
                e.Day.IsSelectable = false;
                e.Cell.CssClass = "otroMes";
            }
            else
            {
                if (e.Day.IsWeekend)
                {
                    e.Cell.ForeColor = System.Drawing.Color.Red;
                    e.Day.IsSelectable = false;
                }
                else
                {
                    e.Cell.CssClass = "esteMes";
                }
            }
        }

        protected void AgregarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagOpUs.aspx?ID=0");
        }

        // Evento Click para el botón "Editar"
        protected void EditarUsuario_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string ID = btn.CommandArgument;
            Response.Redirect($"PagOpUs.aspx?ID={ID}");
        }

        // Evento Click para el botón "Eliminar"
        protected void EliminarUsuario_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string ID = btn.CommandArgument;

            int respuesta = new UsuarioDL().EliminarUsuario(Convert.ToInt32(ID));
            if (respuesta != 0)
            {
                CargarUsuarios();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error: No se pudo eliminar el usuario.');", true);
            }
        }

        protected void GenerarReservacion_Click(object sender, EventArgs e)
        {
            //LinkButton btn = (LinkButton)sender;
            //string Hora = btn.CommandArgument;
            //Response.Redirect($"PagOpRs.aspx?Hora={Hora}");

            if (txtNoControl.Text == string.Empty)
            {
                lblValidacionTxtNoControl.Text = "El número de control es obligatorio";
                lblValidacionTxtNoControl.Visible = true;
                return;
            }
            else if (txtNoControl.Text.Length != 9)
            {
                lblValidacionTxtNoControl.Text = "El número de control debe tener 9 caracteres";
                lblValidacionTxtNoControl.Visible = true;
                return;
            }

            lblValidacionTxtNoControl.Visible = false;


            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string hora = row.Cells[0].Text;
            string fecha = cldrReservaciones.SelectedDate.ToString("yyyy-MM-dd");

            int rowsAffected = new ReservacionDL().GenerarReservacion(hora, fecha, txtNoControl.Text);
            if (rowsAffected == 1)
            {
                txtNoControl.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Éxito: Se generó la reservación correctamente.');", true);
                CargarTablaHorasCupos();
                CargarReservaciones();
            }
            else if (rowsAffected == 2)
            {
                lblValidacionTxtNoControl.Text = "El usuario ingresado es incorrecto";
                lblValidacionTxtNoControl.Visible = true;
                return;
            }
            else
            {
                lblValidacionTxtNoControl.Text = "Ocurrió un error al realizar la operación, inténtelo más tarde";
                lblValidacionTxtNoControl.Visible = true;
                return;
            }

        }

        protected void EliminarReservacioon_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string ID = btn.CommandArgument;

            int respuesta = new ReservacionDL().EliminarReservacion(Convert.ToInt32(ID));
            if (respuesta != 0)
            {
                CargarReservaciones();

                string x = cldrReservaciones.SelectedDate.ToString("dd-MM-yyyy");
                if (x != "01-01-0001")
                {
                    CargarTablaHorasCupos();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error: No se pudo eliminar la reservación.');", true);

            }
        }

        protected void CargarTablaHorasCupos()
        {
            DataTable dtHorasCupos = new DataTable();
            dtHorasCupos.Columns.Add("Hora", typeof(string));
            dtHorasCupos.Columns.Add("InfoCupo", typeof(string)); // Una sola columna para combinar Color y Habilitada

            string fecha = cldrReservaciones.SelectedDate.ToString("yyyy-MM-dd");

            // Agrega las horas y cupos disponibles a la tabla
            for (int i = 7; i <= 15; i++)
            {
                string hora = i.ToString("00") + ":00";
                string infoCupo;
                string CssClass;
                bool habilitada;

                List<Reservacion> reservaciones = new ReservacionDL().BuscarReservaciones(fecha, hora);

                if (reservaciones.Count == 0)
                {
                    CssClass = "btn btn-sm btn-success";
                    habilitada = true;
                }
                else if (reservaciones.Count == 1)
                {
                    CssClass = "btn btn-sm btn-warning";
                    habilitada = true;
                }
                else
                {
                    CssClass = "btn btn-sm btn-danger disabled";
                    habilitada = false;
                }

                // Combina Color y Habilitada en una sola cadena y agrega al DataTable
                infoCupo = CssClass + "|" + habilitada.ToString();
                dtHorasCupos.Rows.Add(hora, infoCupo);
            }

            gvHorasCupos.DataSource = dtHorasCupos;
            gvHorasCupos.DataBind();

            lblNoControl.Visible = true;
            txtNoControl.Visible = true;
            txtNoControl.Enabled = true;
        }

        protected bool GetHabilitada(object infoCupo)
        {
            if (infoCupo != null)
            {
                string[] parts = infoCupo.ToString().Split('|');
                if (parts.Length == 2)
                {
                    return Convert.ToBoolean(parts[1]);
                }
            }
            return false; // Valor predeterminado si hay un problema
        }

        protected string GetColor(object infoCupo)
        {
            if (infoCupo != null)
            {
                string[] parts = infoCupo.ToString().Split('|');
                if (parts.Length == 2)
                {
                    return parts[0];
                }
            }
            return ""; // Valor predeterminado si hay un problema
        }

        protected void cldrReservaciones_SelectionChanged(object sender, EventArgs e)
        {
            cldrReservaciones.SelectedDayStyle.BackColor = System.Drawing.Color.SlateGray;
            CargarTablaHorasCupos();
        }

        ///////////////////////////
    }
}