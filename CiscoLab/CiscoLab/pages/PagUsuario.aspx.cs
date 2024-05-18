using CiscoLab.DataLayer;
using CiscoLab.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CiscoLab.pages
{
    public partial class PagUsuario : System.Web.UI.Page
    {
        private static Usuario usuario = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["Usuario"];
            try
            {
                usuario = user;
                lblNombre.Text = "  " + user.Nombre + " " + user.Apellidos;
            }
            catch (Exception ex)
            {
                Response.Redirect("~/pages/Login.aspx");
            }
            
            CargarReservaciones();
        }

        protected void cldrReservaciones_DayRender(object sender, DayRenderEventArgs e)
        {
            DateTime fecha = e.Day.Date;

            // Lista de días feriados
            List<DateTime> diasAsueto = new List<DateTime>
            {
                new DateTime(2024, 1, 1),  // 1 de enero de 2024
                new DateTime(2024, 2, 5),  // 5 de febrero de 2024
                new DateTime(2024, 3, 5),  // 5 de marzo de 2024
                new DateTime(2024, 3, 18), // 18 de marzo de 2024
                new DateTime(2024, 3, 28), // 28 de marzo de 2024
                new DateTime(2024, 3, 29), // 29 de marzo de 2024
                new DateTime(2024, 5, 1),  // 1 de mayo de 2024
                new DateTime(2024, 5, 10), // 10 de mayo de 2024
                new DateTime(2024, 5, 15)  // 15 de mayo de 2024
            };

            List<DateTime> vacaciones = new List<DateTime>
            {
                new DateTime(2024, 1, 1),  // 1 de enero de 2024
                new DateTime(2024, 1, 2),  // 2 de enero de 2024
                new DateTime(2024, 1, 3),  // 3 de enero de 2024
                new DateTime(2024, 1, 4),  // 4 de enero de 2024
                new DateTime(2024, 1, 5),  // 5 de enero de 2024
                new DateTime(2024, 3, 25), // 25 de marzo de 2024
                new DateTime(2024, 3, 26), // 26 de marzo de 2024
                new DateTime(2024, 3, 27), // 27 de marzo de 2024
                new DateTime(2024, 3, 28), // 28 de marzo de 2024
                new DateTime(2024, 3, 29), // 29 de marzo de 2024
                new DateTime(2024, 4, 1),  // 1 de abril de 2024
                new DateTime(2024, 4, 2),  // 2 de abril de 2024
                new DateTime(2024, 4, 3),  // 3 de abril de 2024
                new DateTime(2024, 4, 4),  // 4 de abril de 2024
                new DateTime(2024, 4, 5)   // 5 de abril de 2024
            };

            // Comprobamos si la fecha actual es un día feriado y le asignamos la clase "diaAsueto"
            if (diasAsueto.Contains(fecha))
            {
                e.Cell.ForeColor = System.Drawing.Color.DarkKhaki;
                e.Day.IsSelectable = false;
            }

            if (vacaciones.Contains(fecha))
            {
                e.Cell.BackColor = System.Drawing.Color.MediumSlateBlue;
                e.Day.IsSelectable = false;
            }

            DateTime fechaActual = DateTime.Today;

            // Deshabilitamos días pasados y días de otros meses
            if (fecha < fechaActual)
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
                // Resaltamos los fines de semana
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


        private void CargarReservaciones()
        {
            string fecha = DateTime.Today.ToString("yyyy-MM-dd");
            List<Reservacion> reservaciones = new ReservacionDL().ObtenerReservacionesUsuario(fecha, usuario.Username);

            gvReservaciones.DataSource = reservaciones;
            gvReservaciones.DataBind();
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



        protected void GenerarReservacion_Click(object sender, EventArgs e)
        {

            LinkButton btn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string hora = row.Cells[0].Text;
            string fecha = cldrReservaciones.SelectedDate.ToString("yyyy-MM-dd");

            int rowsAffected = new ReservacionDL().GenerarReservacion(hora, fecha, usuario.Username);
            if (rowsAffected == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Éxito: Se generó la reservación correctamente.');", true);
                CargarTablaHorasCupos();
                CargarReservaciones();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error: No se pudo generar la reservación, inténtelo más tarde.');", true);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Éxito: Se ha cancelado la reservación correctamente.');", true);
                CargarReservaciones();

                string x = cldrReservaciones.SelectedDate.ToString("dd-MM-yyyy");
                if (x != "01-01-0001")
                {
                    CargarTablaHorasCupos();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error: No se pudo cancelar la reservación, inténtelo más tarde.');", true);
                return;
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



        /***********************/
    }
}