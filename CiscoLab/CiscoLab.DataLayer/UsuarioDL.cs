using CiscoLab.EntityLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CiscoLab.DataLayer
{
    public class UsuarioDL
    {
        string strConexion = "Data Source=EDWARDCM;Initial Catalog=CiscoLab;User ID=sa;Password=root";

        public Usuario LoginUsuario(string user, string passw)
        {
            Usuario usuario = null;

            using (SqlConnection connection = new SqlConnection(strConexion))
            {
                try
                {
                    connection.Open();

                    // Hashear la contraseña proporcionada
                    //byte[] hashedPasswBytes = HashPassword(passw);

                    string query = "select * from Usuarios where (Username=@user and Password=@passwHash);";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@user", user);
                    command.Parameters.AddWithValue("@passwHash", passw);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        usuario = new Usuario();
                        usuario.ID = Convert.ToInt32(reader["ID"]);
                        usuario.Password = reader["Password"].ToString(); // Asegúrate de que esto sea un byte[]
                        usuario.Username = reader["Username"].ToString();
                        usuario.Nombre = reader["Nombre"].ToString();
                        usuario.Apellidos = reader["Apellidos"].ToString();
                        usuario.Email = reader["Email"].ToString();
                    }
                    return usuario;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        private byte[] HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            using (SqlConnection connection = new SqlConnection(strConexion))
            {
                try
                {
                    connection.Open();

                    string query = "select * from Usuarios;";
                    SqlCommand command = new SqlCommand(query, connection);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Usuario user = new Usuario();
                        user.ID = Convert.ToInt32(reader["ID"]);
                        user.Password = reader["Password"].ToString(); // Asegúrate de que esto sea un byte[]
                        user.Username = reader["Username"].ToString();
                        user.Nombre = reader["Nombre"].ToString();
                        user.Apellidos = reader["Apellidos"].ToString();
                        user.Email = reader["Email"].ToString();
                        usuarios.Add(user);
                    }
                    return usuarios;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public Usuario ObtenerUsuario(int ID) 
        {

            Usuario user = null;

            using (SqlConnection connection = new SqlConnection(strConexion))
            {
                try
                {
                    connection.Open();

                    string query = "select * from Usuarios where ID = @ID;";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", ID);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        user = new Usuario();
                        user.ID = Convert.ToInt32(reader["ID"]);
                        user.Password = reader["Password"].ToString(); // Asegúrate de que esto sea un byte[]
                        user.Username = reader["Username"].ToString();
                        user.Nombre = reader["Nombre"].ToString();
                        user.Apellidos = reader["Apellidos"].ToString();
                        user.Email = reader["Email"].ToString();
                    }
                    return user;
                }
                catch (Exception e)
                {
                    return null;
                }
            }
        }

        public int AgregarUsuario(Usuario user)
        {

            // Crear una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(strConexion))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();

                    SqlCommand validar = new SqlCommand(@"select ID from Usuarios where Username = @Username", connection);
                    validar.Parameters.AddWithValue("@Username", user.Username);
                    SqlDataReader reader = validar.ExecuteReader();
                    if (reader.Read()) { return 2; }
                    reader.Close();


                    // Crear una consulta SQL de inserción
                    string query = @"INSERT INTO Usuarios (Username, Password, Nombre, Apellidos, Email) 
                                        VALUES (@NoControl, @Password, @Nombre, @Apellidos, @Email);";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Añadir parámetros a la consulta SQL
                    command.Parameters.AddWithValue("@NoControl", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Nombre", user.Nombre);
                    command.Parameters.AddWithValue("@Apellidos", user.Apellidos);
                    command.Parameters.AddWithValue("@Email", user.Email);

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

        public int EditarUsuario(Usuario user)
        {

            // Crear una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(strConexion))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear una consulta SQL de inserción
                    string query = @"update Usuarios set Username = @NoControl, Password = @Password, Nombre = @Nombre, 
                                        Apellidos = @Apellidos, Email = @Email where ID = @ID;";
                    SqlCommand command = new SqlCommand(query, connection);

                    // Añadir parámetros a la consulta SQL
                    command.Parameters.AddWithValue("@ID", user.ID);
                    command.Parameters.AddWithValue("@NoControl", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Nombre", user.Nombre);
                    command.Parameters.AddWithValue("@Apellidos", user.Apellidos);
                    command.Parameters.AddWithValue("@Email", user.Email);

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

        public int EliminarUsuario(int ID)
        {

            // Crear una conexión a la base de datos
            using (SqlConnection connection = new SqlConnection(strConexion))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear una consulta SQL de inserción
                    string query = @"delete from Usuarios where ID = @ID;";
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

    }
}
