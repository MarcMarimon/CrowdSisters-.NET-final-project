using CrowdSisters.Conections;
using CrowdSisters.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                URLImagenUsuario, Monedero, PrimerApellido, SegundoApellido, DNI, Direccion, CodigoPostal, Poblacion,
                Telofono, Pais, Nick)
                VALUES (@Nombre, @Email, @Contrasena, @FechaRegistro, @IsAdmin, @PerfilPublico, @URLImagenUsuario,
                @Monedero, @PrimerApellido, @SegundoApellido, @DNI, @Direccion, @CodigoPostal, @Poblacion,
                @Telofono, @Pais, @Nick)";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                    command.Parameters.AddWithValue("@FechaRegistro", usuario.FechaRegistro);
                    command.Parameters.AddWithValue("@IsAdmin", usuario.IsAdmin);
                    command.Parameters.AddWithValue("@PerfilPublico", usuario.PerfilPublico);
                    command.Parameters.AddWithValue("@URLImagenUsuario", usuario.URLImagenUsuario);
                    command.Parameters.AddWithValue("@Monedero", usuario.Monedero);
                    command.Parameters.AddWithValue("@PrimerApellido", usuario.PrimerApellido);
                    command.Parameters.AddWithValue("@SegundoApellido", usuario.SegundoApellido);
                    command.Parameters.AddWithValue("@DNI", usuario.DNI);
                    command.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                    command.Parameters.AddWithValue("@CodigoPostal", usuario.CodigoPostal);
                    command.Parameters.AddWithValue("@Poblacion", usuario.Poblacion);
                    command.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    command.Parameters.AddWithValue("@Pais", usuario.Pais);
                    command.Parameters.AddWithValue("@Nick", usuario.Nick);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Leer todos
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            List<Usuario> usuarios = new List<Usuario>();

            const string query = @"SELECT * FROM Usuario;";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {

                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            usuarios.Add(new Usuario
                            {
                                IDUsuario = reader.GetInt32(reader.GetOrdinal("IDUsuario")),
                                Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? null : reader.GetString(reader.GetOrdinal("Nombre")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Contrasena = reader.GetString(reader.GetOrdinal("Contrasena")),
                                FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
                                IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                                PerfilPublico = reader.IsDBNull(reader.GetOrdinal("PerfilPublico")) ? null : reader.GetString(reader.GetOrdinal("PerfilPublico")),
                                URLImagenUsuario = reader.IsDBNull(reader.GetOrdinal("URLImagenUsuario")) ? null : reader.GetString(reader.GetOrdinal("URLImagenUsuario")),
                                Monedero = reader.GetDecimal(reader.GetOrdinal("Monedero")),
                                PrimerApellido = reader.IsDBNull(reader.GetOrdinal("PrimerApellido")) ? null : reader.GetString(reader.GetOrdinal("PrimerApellido")),
                                SegundoApellido = reader.IsDBNull(reader.GetOrdinal("SegundoApellido")) ? null : reader.GetString(reader.GetOrdinal("SegundoApellido")),
                                DNI = reader.IsDBNull(reader.GetOrdinal("DNI")) ? null : reader.GetString(reader.GetOrdinal("DNI")),
                                Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion")),
                                CodigoPostal = reader.IsDBNull(reader.GetOrdinal("CodigoPostal")) ? null : reader.GetString(reader.GetOrdinal("CodigoPostal")),
                                Poblacion = reader.IsDBNull(reader.GetOrdinal("Poblacion")) ? null : reader.GetString(reader.GetOrdinal("Poblacion")),
                                Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                                Pais = reader.IsDBNull(reader.GetOrdinal("Pais")) ? null : reader.GetString(reader.GetOrdinal("Pais")),
                                Nick = reader.GetString(reader.GetOrdinal("Nick"))
                            });
                        }
                    }
                }
                return usuarios;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<Usuario> GetByIdAsync(int id)
        {
            const string query = @"SELECT * FROM Usuario WHERE IDUsuario = @IDUsuario";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {

                    sqlConn.Open(); // Asegúrate de abrir la conexión
 
                    command.Parameters.AddWithValue("@IDUsuario", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Usuario
                            {
                                IDUsuario = reader.GetInt32(reader.GetOrdinal("IDUsuario")),
                                Nombre = reader.IsDBNull(reader.GetOrdinal("Nombre")) ? null : reader.GetString(reader.GetOrdinal("Nombre")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                Contrasena = reader.GetString(reader.GetOrdinal("Contrasena")),
                                FechaRegistro = reader.GetDateTime(reader.GetOrdinal("FechaRegistro")),
                                IsAdmin = reader.GetBoolean(reader.GetOrdinal("IsAdmin")),
                                PerfilPublico = reader.IsDBNull(reader.GetOrdinal("PerfilPublico")) ? null : reader.GetString(reader.GetOrdinal("PerfilPublico")),
                                URLImagenUsuario = reader.IsDBNull(reader.GetOrdinal("URLImagenUsuario")) ? null : reader.GetString(reader.GetOrdinal("URLImagenUsuario")),
                                Monedero = reader.GetDecimal(reader.GetOrdinal("Monedero")),
                                PrimerApellido = reader.IsDBNull(reader.GetOrdinal("PrimerApellido")) ? null : reader.GetString(reader.GetOrdinal("PrimerApellido")),
                                SegundoApellido = reader.IsDBNull(reader.GetOrdinal("SegundoApellido")) ? null : reader.GetString(reader.GetOrdinal("SegundoApellido")),
                                DNI = reader.IsDBNull(reader.GetOrdinal("DNI")) ? null : reader.GetString(reader.GetOrdinal("DNI")),
                                Direccion = reader.IsDBNull(reader.GetOrdinal("Direccion")) ? null : reader.GetString(reader.GetOrdinal("Direccion")),
                                CodigoPostal = reader.IsDBNull(reader.GetOrdinal("CodigoPostal")) ? null : reader.GetString(reader.GetOrdinal("CodigoPostal")),
                                Poblacion = reader.IsDBNull(reader.GetOrdinal("Poblacion")) ? null : reader.GetString(reader.GetOrdinal("Poblacion")),
                                Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString(reader.GetOrdinal("Telefono")),
                                Pais = reader.IsDBNull(reader.GetOrdinal("Pais")) ? null : reader.GetString(reader.GetOrdinal("Pais")),
                                Nick = reader.GetString(reader.GetOrdinal("Nick"))
                            };
                        }
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
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
                         Monedero = @Monedero,
                         PrimerApellido = @PrimerApellido,
                         SegundoApellido = @SegundoApellido,
                         DNI = @DNI,
                         Direccion = @Direccion,
                         CodigoPostal = @CodigoPostal,
                         Poblacion = @Poblacion,
                         Telefono = @Telefono,
                         Pais = @Pais,
                         Nick = @Nick
                     WHERE IDUsuario = @IDUsuario";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDUsuario", usuario.IDUsuario);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@Email", usuario.Email);
                    command.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
                    command.Parameters.AddWithValue("@FechaRegistro", usuario.FechaRegistro);
                    command.Parameters.AddWithValue("@IsAdmin", usuario.IsAdmin);
                    command.Parameters.AddWithValue("@PerfilPublico", usuario.PerfilPublico);
                    command.Parameters.AddWithValue("@URLImagenUsuario", usuario.URLImagenUsuario);
                    command.Parameters.AddWithValue("@Monedero", usuario.Monedero);
                    command.Parameters.AddWithValue("@PrimerApellido", usuario.PrimerApellido);
                    command.Parameters.AddWithValue("@SegundoApellido", usuario.SegundoApellido);
                    command.Parameters.AddWithValue("@DNI", usuario.DNI);
                    command.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                    command.Parameters.AddWithValue("@CodigoPostal", usuario.CodigoPostal);
                    command.Parameters.AddWithValue("@Poblacion", usuario.Poblacion);
                    command.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    command.Parameters.AddWithValue("@Pais", usuario.Pais);
                    command.Parameters.AddWithValue("@Nick", usuario.Nick);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateUsuarioCrearProyectoAsync(Usuario usuario)
        {
            const string query = @"UPDATE Usuario 
                     SET Nombre = @Nombre,
                         PerfilPublico = @PerfilPublico,
                         URLImagenUsuario = @URLImagenUsuario,
                         PrimerApellido = @PrimerApellido,
                         SegundoApellido = @SegundoApellido,
                         DNI = @DNI,
                         Direccion = @Direccion,
                         CodigoPostal = @CodigoPostal,
                         Poblacion = @Poblacion,
                         Telefono = @Telefono,
                         Pais = @Pais
                     WHERE IDUsuario = @IDUsuario";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDUsuario", usuario.IDUsuario);
                    command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    command.Parameters.AddWithValue("@PerfilPublico", usuario.PerfilPublico);
                    command.Parameters.AddWithValue("@URLImagenUsuario", usuario.URLImagenUsuario);
                    command.Parameters.AddWithValue("@PrimerApellido", usuario.PrimerApellido);
                    command.Parameters.AddWithValue("@SegundoApellido", usuario.SegundoApellido);
                    command.Parameters.AddWithValue("@DNI", usuario.DNI);
                    command.Parameters.AddWithValue("@Direccion", usuario.Direccion);
                    command.Parameters.AddWithValue("@CodigoPostal", usuario.CodigoPostal);
                    command.Parameters.AddWithValue("@Poblacion", usuario.Poblacion);
                    command.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    command.Parameters.AddWithValue("@Pais", usuario.Pais);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Eliminar
        public async Task<bool> DeleteAsync(int id)
        {
            const string query = @"DELETE FROM USUARIO WHERE IDUsuario = @IDUsuario";
            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDUsuario", id);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
