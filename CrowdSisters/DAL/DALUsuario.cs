using CrowdSisters.Conections;
using CrowdSisters.Models;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace CrowdSisters.DAL
{
    public class DALUsuario
    {
        private readonly Connection _connection;

        public DALUsuario(Connection connection)
        {
            _connection = connection;
        }


        // Crear
        public async Task<bool> CreateAsync(Usuario usuario)
        {
            const string query = @"
                INSERT INTO USUARIO (Nombre, Email, Contrasena, FechaRegistro, IsAdmin, PerfilPublico, 
                URLImagenUsuario, Monedero) 
                VALUES (@Nombre, @Email, @Contrasena, @FechaRegistro, @IsAdmin, @PerfilPublico, @URLImagenUsuario,
                @Monedero)";

            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                command.Parameters.AddWithValue("@FechaRegistro", usuario.FechaRegistro);
                command.Parameters.AddWithValue("@IsAdmin", usuario.IsAdmin);
                command.Parameters.AddWithValue("@PerfilPublico", usuario.PerfilPublico);
                command.Parameters.AddWithValue("@URLImagenUsuario", usuario.URLImagenUsuario);
                command.Parameters.AddWithValue("@Monedero", usuario.Monedero);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        // Leer
        public async Task<List<Usuario>> GetAllAsync()
        {

            List<Usuario> usuarios = new List<Usuario>();

            const string query = @"SELECT * FROM USUARIO;";
            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        usuarios.Add(new Usuario
                        {
                            IDUsuario = reader.GetInt32(reader.GetOrdinal("IDUsuario")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Contrasena = reader.GetString(reader.GetOrdinal("Contrasena")),
                            FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
                            IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                            PerfilPublico = reader.GetString(reader.GetOrdinal("PerfilPublico")),
                            URLImagenUsuario = reader.GetString(reader.GetOrdinal("URLImagenUsuario")),
                            Monedero = reader.GetDecimal(reader.GetOrdinal("Monedero"))
                        });

                    }
                }
            }
            return usuarios;
        }

       
        public async Task<Usuario> GetByIdAsync(int id)
        {
            const string query = @"SELECT * FROM USUARIO WHERE IDUsuario = @IDUsuario";

            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@IDUsuario", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Usuario
                        {
                            IDUsuario = reader.GetInt32(reader.GetOrdinal("IDUsuario")),
                            Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Contrasena = reader.GetString(reader.GetOrdinal("Contrasena")),
                            FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
                            IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                            PerfilPublico = reader.GetString(reader.GetOrdinal("PerfilPublico")),
                            URLImagenUsuario = reader.GetString(reader.GetOrdinal("URLImagenUsuario")),
                            Monedero = reader.GetDecimal(reader.GetOrdinal("Monedero"))
                        };
                    }
                    return null;
                }
            }
        }


        // Actualizar

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            const string query = @"UPDATE USUARIO 
                     SET Nombre = @Nombre,
                         Email = @Email,
                         Contrasena = @Contrasena,
                         FechaRegistro = @FechaRegistro,
                         IsAdmin = @IsAdmin,
                         PerfilPublico = @PerfilPublico,
                         URLImagenUsuario = @URLImagenUsuario,
                         Monedero = @Monedero
                     WHERE IDUsuario = @IDUsuario";

            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@IDUsuario", usuario.IDUsuario);
                command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                command.Parameters.AddWithValue("@Email", usuario.Email);
                command.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                command.Parameters.AddWithValue("@FechaRegistro", usuario.FechaRegistro);
                command.Parameters.AddWithValue("@IsAdmin", usuario.IsAdmin);
                command.Parameters.AddWithValue("@PerfilPublico", usuario.PerfilPublico);
                command.Parameters.AddWithValue("@URLImagenUsuario", usuario.URLImagenUsuario);
                command.Parameters.AddWithValue("@Monedero", usuario.Monedero);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }


        // Eliminar

        public async Task<bool> DeleteAsync(int id)
        {
            const string query = @"DELETE FROM USUARIO WHERE IDUsuario = @IDUsuario";
            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@IDUsuario", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

    }

}
