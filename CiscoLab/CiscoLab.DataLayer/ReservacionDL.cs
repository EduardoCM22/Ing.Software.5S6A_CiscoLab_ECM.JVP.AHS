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
        string strConexion = Conexion.strConexion;

        public List<Reservacion> ObtenerReservaciones(string fecha, bool bandera)
        {
            List<Reservacion> reservaciones = new List<Reservacion>();

            using (SqlConnection connection = new SqlConnection(strConexion))
            {
                try
                {
                    connection.Open();

                    string query = "";
                    if (bandera == true)
                    {
                        query = @"SELECT r.ID, CONVERT(varchar(5), r.Hora, 108) AS Hora, r.Fecha, 
                                CONCAT(u.Nombre, ' ', u.Apellidos) AS 'Nombre Completo', 
                                u.Username FROM Reservaciones r JOIN Usuarios u ON r.ID_Usuario = u.ID 
                                where Fecha >= @Fecha order by Fecha asc, Hora asc;";
                    }
                    else
                    {
                        query = @"SELECT r.ID, CONVERT(varchar(5), r.Hora, 108) AS Hora, r.Fecha, 
                                CONCAT(u.Nombre, ' ', u.Apellidos) AS 'Nombre Completo', 
                                u.Username FROM Reservaciones r JOIN Usuarios u ON r.ID_Usuario = u.ID 
                                where Fecha <= @Fecha order by Fecha desc, Hora asc;";
                    }
                    
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Fecha", fecha);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Reservacion reserv = new Reservacion();
                        reserv.ID = Convert.ToInt32(reader["ID"]);
                        reserv.Hora = reader["Hora"].ToString(); // Asegúrate de que esto sea un byte[]
                        //reserv.Fecha = reader["Fecha"].ToString();
                        reserv.Fecha = ((DateTime)reader["Fecha"]).ToString("dd-MM-yyyy");
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

        public List<Reservacion> ObtenerReservacionesUsuario(string fecha, string usuario)
        {
            List<Reservacion> reservaciones = new List<Reservacion>();

            using (SqlConnection connection = new SqlConnection(strConexion))
            {
                try
                {
                    connection.Open();

                    string query = @"SELECT r.ID, CONVERT(varchar(5), r.Hora, 108) AS Hora, r.Fecha
                                FROM Reservaciones r JOIN Usuarios u ON r.ID_Usuario = u.ID 
                                where Username = @Usuario and Fecha >= @Fecha order by Fecha asc, Hora asc;";


                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Fecha", fecha);
                    command.Parameters.AddWithValue("@Usuario", usuario);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Reservacion reserv = new Reservacion();
                        reserv.ID = Convert.ToInt32(reader["ID"]);
                        reserv.Hora = reader["Hora"].ToString(); // Asegúrate de que esto sea un byte[]
                        //reserv.Fecha = reader["Fecha"].ToString();
                        reserv.Fecha = ((DateTime)reader["Fecha"]).ToString("dd-MM-yyyy");
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





        public int GenerarReservacion(string Hora, string Fecha, string Username)
        {

            // Crear una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(strConexion))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();

                    int ID;

                    SqlCommand validar = new SqlCommand(@"select ID from Usuarios where Username = @Username", connection);
                    validar.Parameters.AddWithValue("@Username", Username);
                    SqlDataReader reader = validar.ExecuteReader();
                    if (!reader.Read()) { return 2; } else { ID = Convert.ToInt32(reader["ID"]); }
                    reader.Close();


                    // Crear una consulta SQL de inserción
                    string query = @"INSERT INTO Reservaciones (Hora, Fecha, ID_Usuario)
                                        VALUES (@Hora, @Fecha, @ID);";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Añadir parámetros a la consulta SQL
                    command.Parameters.AddWithValue("@Hora", Hora);
                    command.Parameters.AddWithValue("@Fecha", Fecha);
                    command.Parameters.AddWithValue("@ID", ID);

                    // Ejecutar la consulta SQL
                    int rowsAffected = command.ExecuteNonQuery();

                    // Verificar si se ha insertado alguna fila
                    return rowsAffected;

                }
                catch (Exception ex)
                {
                    return 0;

                }
            }
        }

        public int EliminarReservacion(int ID)
        {

            // Crear una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(strConexion))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear una consulta SQL de inserción
                    string query = @"delete from Reservaciones where ID = @ID;";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Añadir parámetros a la consulta SQL
                    command.Parameters.AddWithValue("@ID", ID);

                    // Ejecutar la consulta SQL
                    int rowsAffected = command.ExecuteNonQuery();

                    // Verificar si se ha insertado alguna fila
                    return rowsAffected;

                }
                catch (Exception ex)
                {
                    return 0;

                }
            }
        }




        public List<Reservacion> BuscarReservaciones(string fecha, string hora)
        {
            List<Reservacion> reservaciones = new List<Reservacion>();

            using (SqlConnection connection = new SqlConnection(strConexion))
            {
                try
                {
                    connection.Open();

                    string query = "select * from Reservaciones where Hora = @hora and Fecha = @fecha;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@hora", hora);
                    command.Parameters.AddWithValue("@fecha", fecha);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Reservacion reserv = new Reservacion();
                        reserv.ID = Convert.ToInt32(reader["ID"]);
                        reserv.Fecha = reader["Fecha"].ToString();
                        reserv.Hora = reader["Hora"].ToString();
                        reserv.Username = reader["ID_Usuario"].ToString();
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
