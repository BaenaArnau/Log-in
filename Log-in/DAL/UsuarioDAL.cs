using Log_in.Models;
using System.Data.SqlClient;

namespace Log_in.DAL
{
    public class UsuarioDAL
    {
        SqlConnectionStringBuilder constructorCadenaConexion = new SqlConnectionStringBuilder
        {
            DataSource = "85.208.21.117,54321",
            InitialCatalog = "ArnauLogin",
            UserID = "sa",
            Password = "Sql#123456789"
        };

        public Usuario GetUsuarioLogin(string userName, string pwd)
        {
            using (var connection = new SqlConnection(constructorCadenaConexion.ToString()))
            {
                var command = new SqlCommand(@"
            SELECT IdUsuario, UserName, PasswordHash, PasswordSalt,
            Apellido, Email, FechaNacimiento, Telefono, Direccion,
            Ciudad, Estado, CodigoPostal, FechaRegistro, Activo
            FROM Usuario
            WHERE UserName = @UserName", connection);
                command.Parameters.AddWithValue("@UserName", userName);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var passwordHash = (byte[])reader["PasswordHash"];
                        var passwordSalt = (byte[])reader["PasswordSalt"];

                        // Verificar la contraseña
                        if (PasswordHelper.VerifyPasswordHash(pwd, passwordHash, passwordSalt))
                        {
                            return new Usuario
                            {
                                IdUsuario = (int)reader["IdUsuario"],
                                UserName = reader["UserName"] != DBNull.Value ? (string)reader["UserName"] : null,
                                PasswordHash = reader["PasswordHash"] != DBNull.Value ? (byte[])reader["PasswordHash"] : null,
                                PasswordSalt = reader["PasswordSalt"] != DBNull.Value ? (byte[])reader["PasswordSalt"] : null,
                                Apellido = reader["Apellido"] != DBNull.Value ? (string)reader["Apellido"] : null,
                                Email = reader["Email"] != DBNull.Value ? (string)reader["Email"] : null,
                                FechaNacimiento = reader["FechaNacimiento"] != DBNull.Value ? (DateTime?)reader["FechaNacimiento"] : null,
                                Telefono = reader["Telefono"] != DBNull.Value ? (string)reader["Telefono"] : null,
                                Direccion = reader["Direccion"] != DBNull.Value ? (string)reader["Direccion"] : null,
                                Ciudad = reader["Ciudad"] != DBNull.Value ? (string)reader["Ciudad"] : null,
                                Estado = reader["Estado"] != DBNull.Value ? (string)reader["Estado"] : null,
                                CodigoPostal = reader["CodigoPostal"] != DBNull.Value ? (string)reader["CodigoPostal"] : null,
                                FechaRegistro = reader["FechaRegistro"] != DBNull.Value ? (DateTime)reader["FechaRegistro"] : DateTime.MinValue,
                                Activo = reader["Activo"] != DBNull.Value ? (bool)reader["Activo"] : false
                            };

                        }
                    }
                }
            }
            return null;
        }

        public void CreateUsuario(Usuario usuario, string password)
        {
            // Generar el hash y el salt para la contraseña
            PasswordHelper.CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            using (var connection = new SqlConnection(constructorCadenaConexion.ToString()))
            {
                var command = new SqlCommand(@"
            INSERT INTO Usuario
            (UserName, PasswordHash, PasswordSalt)
            VALUES (@UserName, @PasswordHash, @PasswordSalt)",
                    connection);

                command.Parameters.AddWithValue("@UserName", usuario.UserName);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                command.Parameters.AddWithValue("@PasswordSalt", passwordSalt);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
