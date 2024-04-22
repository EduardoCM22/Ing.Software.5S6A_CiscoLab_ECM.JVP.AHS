using CiscoLab.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiscoLab.DataLayer
{
    public class DiaInhabilDL
    {
        string strConexion = Conexion.strConexion;

        public int AgregarDiaInhabil(string fecha, string motivo)
        {

            // Crear una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(strConexion))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear una consulta SQL de inserción
                    string query = @"insert into DiasInhabiles (Fecha, Motivo) values(@Fecha, @Motivo);";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Añadir parámetros a la consulta SQL
                    command.Parameters.AddWithValue("@Fecha", fecha);
                    command.Parameters.AddWithValue("@Motivo", motivo);

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
    }
}
