using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CiscoLab.EntityLayer;

namespace CiscoLab.DataLayer
{
    public class AdministradorDL
    {

        string strConexion = Conexion.strConexion;

        public Administrador LoginAdministrador(string user, string passw)
        {
            Administrador administrador = null;

            using (SqlConnection connection = new SqlConnection(strConexion))
            {
                try
                {
                    connection.Open();

                    // Hashear la contraseña proporcionada
                    //byte[] hashedPasswBytes = HashPassword(passw);

                    string query = "select * from Administradores where (Username=@user and Password=@passwHash);";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@passwHash", passw);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        administrador = new Administrador();
                        administrador.ID = Convert.ToInt32(reader["ID"]);
                        administrador.Password = reader["Password"].ToString(); // Asegúrate de que esto sea un byte[]
                        administrador.Username = reader["Username"].ToString();
                        administrador.Nombre = reader["Nombre"].ToString();
                        administrador.Apellidos = reader["Apellidos"].ToString();
                        administrador.Email = reader["Email"].ToString();
                    }
                    return administrador;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        //private byte[] HashPassword(string password)
        //{
        //    using (SHA256 sha256 = SHA256.Create())
        //    {
        //        return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        //    }
        //}


    }
}