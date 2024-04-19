using CiscoLab.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiscoLab.DataLayer
{
    public class ReservacionDL
    {
        string strConexion = "Data Source=EDWARDCM;Initial Catalog=CiscoLab;User ID=sa;Password=root";

        public List<Reservacion> ObtenerReservaciones()
        {
            List<Reservacion> reservaciones = new List<Reservacion>();

            using (SqlConnection connection = new SqlConnection(strConexion))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT r.ID, CONVERT(varchar(5), r.Hora, 108) AS Hora, r.Fecha, CONCAT(u.Nombre, ' ', u.Apellidos) AS 'Nombre Completo', u.Username FROM Reservaciones r JOIN Usuarios u ON r.ID_Usuario = u.ID;";
                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        Reservacion reserv = new Reservacion();
                        reserv.ID = Convert.ToInt32(reader["ID"]);
                        reserv.Hora = reader["Hora"].ToString(); // Asegúrate de que esto sea un byte[]
                        reserv.Fecha = reader["Fecha"].ToString();
                        reserv.NombreCompleto = reader["Nombre Completo"].ToString();
                        reserv.Username = reader["Username"].ToString();
                        reservaciones.Add(reserv);
                    }
                    return reservaciones;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }
    }
}
