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

        protected void Page_Load(object sender, EventArgs e)
        {
            //Administrador administrador = new Administrador();
            //administrador.Nombre = "Eduardo";
            //administrador.Apellidos = "Campos Manriquez";
            Administrador administrador = (Administrador)Session["Administrador"];
            try 
            {
                lblNombre.Text = "  " + administrador.Nombre + " " + administrador.Apellidos;
            }
            catch(Exception ex) 
            {
                Response.Redirect("~/pages/Login.aspx");
            }
            CargarUsuarios();
            CargarReservaciones();
            CargarHistorialReservaciones();
            //cldrDiaInhabil.DayRender += new DayRenderEventHandler(cldrDiaInhabil_DayRender);
            //cldrReservaciones.DayRender += new DayRenderEventHandler(cldrReservaciones_DayRender);

        }

        protected void cldrReservaciones_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime a = e.Day.Date;
            DateTime fecha = DateTime.Today;

            if (a < fecha)
            {
                e.Day.IsSelectable = false;
            }

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
        protected void cldrReservaciones_SelectionChanged(object sender, EventArgs e)
        {
            cldrReservaciones.SelectedDayStyle.BackColor = System.Drawing.Color.SlateGray;
            CargarTablaHorasCupos();
        }


        private void CargarUsuarios()
        {
            List<Usuario> usuarios = new UsuarioDL().ObtenerUsuarios();

            gvUsuarios.DataSource = usuarios;
            gvUsuarios.DataBind();

        }

        private void CargarReservaciones()
        {
            string fecha = DateTime.Today.ToString("yyyy-MM-dd");
            List<Reservacion> reservaciones = new ReservacionDL().ObtenerReservaciones(fecha, true);

            gvReservaciones.DataSource = reservaciones;
            gvReservaciones.DataBind();
        }

        private void CargarHistorialReservaciones()
        {

            // Convertir la fecha anterior a formato de cadena (yyyy-MM-dd)
            string fecha = DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd");

            List<Reservacion> reservaciones = new ReservacionDL().ObtenerReservaciones(fecha, false);

            gvHistReserv.DataSource = reservaciones;
            gvHistReserv.DataBind();
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
                string texto;

                List<Reservacion> reservaciones = new ReservacionDL().BuscarReservaciones(fecha, hora);

                if (reservaciones.Count == 0)
                {
                    CssClass = "btn btn-sm btn-success";
                    habilitada = true;
                    texto = "2 Cupos";
                }
                else if (reservaciones.Count == 1)
                {
                    CssClass = "btn btn-sm btn-warning";
                    habilitada = true;
                    texto = "1 Cupo";
                }
                else
                {
                    CssClass = "btn btn-sm btn-danger disabled";
                    habilitada = false;
                    texto = "Ocupado";
                }

                // Combina Color y Habilitada en una sola cadena y agrega al DataTable
                infoCupo = CssClass + "|" + habilitada.ToString() + "|" + texto.ToString();
                dtHorasCupos.Rows.Add(hora, infoCupo);
            }

            gvHorasCupos.DataSource = dtHorasCupos;
            gvHorasCupos.DataBind();

            lblNoControl.Visible = true;
            txtNoControl.Visible = true;
            txtNoControl.Enabled = true;
        }



        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            // Limpiar la sesión actual
            Session.Clear();
            Session.Abandon();

            // Prevenir el almacenamiento en caché de la página actual y las páginas anteriores
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            Response.Redirect("~/pages/Login.aspx");
            Dispose();
        }



        protected void AgregarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagOpUs.aspx?ID=0");
        }

        protected void EditarUsuario_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string ID = btn.CommandArgument;
            Response.Redirect($"PagOpUs.aspx?ID={ID}");
        }

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

        protected void EliminarReservacion_Click(object sender, EventArgs e)
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



        protected bool GetHabilitada(object infoCupo)
        {
            if (infoCupo != null)
            {
                string[] parts = infoCupo.ToString().Split('|');
                if (parts.Length == 3)
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
                if (parts.Length == 3)
                {
                    return parts[0];
                }
            }
            return ""; // Valor predeterminado si hay un problema
        }

        protected string GetTexto(object infoCupo)
        {
            if (infoCupo != null)
            {
                string[] parts = infoCupo.ToString().Split('|');
                if (parts.Length == 3)
                {
                    return parts[2];
                }
            }
            return ""; // Valor predeterminado si hay un problema
        }

        ///////////////////////////
    }
}