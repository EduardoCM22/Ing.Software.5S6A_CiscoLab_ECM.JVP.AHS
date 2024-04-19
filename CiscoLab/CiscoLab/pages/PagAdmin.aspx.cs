using CiscoLab.DataLayer;
using CiscoLab.EntityLayer;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Administrador administrador = new Administrador();
            administrador.Nombre = "Eduardo";
            administrador.Apellidos = "Campos Manriquez";
            ////Administrador administrador = (Administrador)Session["Administrador"];
            lblNombre.Text = "  " + administrador.Nombre + " " + administrador.Apellidos;
            CargarUsuarios();
            TablaReservaciones();
            cldrDiaInhabil.DayRender += new DayRenderEventHandler(cldrDiaInhabil_DayRender);
        }

        protected void cldrDiaInhabil_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.IsOtherMonth)
            {
                e.Day.IsSelectable = false;
                e.Cell.CssClass = "otroMes";
            }
            else
            {
                // Verificar si el día es sábado o domingo
                if (e.Day.IsWeekend)
                {
                    // Puedes cambiar el estilo del día aquí si lo deseas
                    e.Cell.ForeColor = System.Drawing.Color.Red; // Cambiar el color del texto a rojo por ejemplo
                }
            }

            //// Verificar si el día es sábado o domingo
            //if (e.Day.Date.DayOfWeek == DayOfWeek.Saturday || e.Day.Date.DayOfWeek == DayOfWeek.Sunday)
            //{
            //    // Deshabilitar el día
            //    e.Day.IsSelectable = false;
            //    // Puedes cambiar el estilo del día aquí si lo deseas
            //    e.Cell.ForeColor = System.Drawing.Color.Red; // Cambiar el color del texto a rojo por ejemplo
            //}
        }

        private void CargarUsuarios()
        {
            List<Usuario> usuarios = new UsuarioDL().ObtenerUsuarios();

            gvUsuarios.DataSource = usuarios;
            gvUsuarios.DataBind();

        }

        protected string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()-_+=";
            Random random = new Random();
            StringBuilder stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return stringBuilder.ToString();
        }

        // Evento Click para el botón "Editar"
        protected void AgregarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("PagOpUs.aspx?ID=0");
        }

        // Evento Click para el botón "Eliminar"
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
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Error: No se pudo eliminar el usuario.');", true);

            }
        }

        private void TablaReservaciones()
        {
            List<Reservacion> reservaciones = new ReservacionDL().ObtenerReservaciones();

            // Crear la fila de encabezado
            TableHeaderRow tableHeaderRow = new TableHeaderRow();
            TableHeaderCell cell1 = new TableHeaderCell();
            TableHeaderCell cell2 = new TableHeaderCell();
            TableHeaderCell cell3 = new TableHeaderCell();
            TableHeaderCell cell4 = new TableHeaderCell();
            TableHeaderCell cell5 = new TableHeaderCell();
            TableHeaderCell cell6 = new TableHeaderCell();

            // Configurar los encabezados de columna
            cell1.Text = "No.reservacion";
            cell2.Text = "Hora";
            cell3.Text = "Fecha";
            cell4.Text = "Nombre Completo";
            cell5.Text = "No.Control";
            cell6.Text = "";

            // Agregar los encabezados a la fila de encabezado
            tableHeaderRow.Cells.Add(cell1);
            tableHeaderRow.Cells.Add(cell2);
            tableHeaderRow.Cells.Add(cell3);
            tableHeaderRow.Cells.Add(cell4);
            tableHeaderRow.Cells.Add(cell5);
            tableHeaderRow.Cells.Add(cell6);

            // Agregar la fila de encabezado a la tabla
            tblReservaciones.Rows.Add(tableHeaderRow);

            // Iterar sobre los usuarios y agregarlos a la tabla

            if (reservaciones != null) 
            {
                foreach (Reservacion item in reservaciones)
                {
                    // Crear una nueva fila de datos
                    TableRow tableRow = new TableRow();

                    // Crear celdas para cada atributo del usuario
                    TableCell cellI1 = new TableCell();
                    TableCell cellI2 = new TableCell();
                    TableCell cellI3 = new TableCell();
                    TableCell cellI4 = new TableCell();
                    TableCell cellI5 = new TableCell();
                    TableCell cellI6 = new TableCell();

                    // Configurar los datos en las celdas
                    cellI1.Text = Convert.ToInt32(item.ID).ToString();
                    cellI2.Text = item.Hora;
                    cellI3.Text = item.Fecha;
                    cellI4.Text = item.NombreCompleto;
                    cellI5.Text = item.Username;

                    // Crear botones "Editar" y "Eliminar"
                    //Button btnEditar = new Button();
                    //btnEditar.CssClass = "btn btn-sm btn-primary";
                    //btnEditar.Text = "Editar";
                    //btnEditar.CommandArgument = item.Username;
                    //btnEditar.Click += new EventHandler(btnEditar_Click);
                    //cellI4.Controls.Add(btnEditar);

                    Button btnEliminar = new Button();
                    btnEliminar.CssClass = "btn btn-sm btn-danger";
                    btnEliminar.Text = "Eliminar";
                    btnEliminar.CommandArgument = item.Username;
                    //btnEliminar.Click += new EventHandler(btnEliminar_Click);
                    cellI6.Controls.Add(btnEliminar);

                    // Agregar las celdas a la fila de datos
                    tableRow.Cells.Add(cellI1);
                    tableRow.Cells.Add(cellI2);
                    tableRow.Cells.Add(cellI3);
                    tableRow.Cells.Add(cellI4);
                    tableRow.Cells.Add(cellI5);
                    tableRow.Cells.Add(cellI6);

                    // Agregar la fila de datos a la tabla
                    tblReservaciones.Rows.Add(tableRow);
                }
            }
            
        }

        protected void modal_Click(object sender, EventArgs e)
        {
            string script = "$('#staticBackdrop').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string script = "$('#staticBackdrop').modal('show');";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", script, true);
        }

        protected void InhabilitarDia_Click(object sender, EventArgs e)
        {
            Console.WriteLine(cldrDiaInhabil.SelectedDate.ToString());
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/pages/Login.aspx");
        }
    }
}