using CrowdSisters.Conections;
using CrowdSisters.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrowdSisters.DAL
{
    public class DALRecompensa
    {
        private readonly Connection _connection;

        public DALRecompensa(Connection connection)
        {
            _connection = connection;
        }

        // Crear
        public async Task<bool> CreateAsync(Recompensa recompensa)
        {
            const string query = @"
                INSERT INTO Recompensa (
                    Titulo, 
                    Descripcion, 
                    Monto, 
                    URLImagenRecompensa, 
                    FKProyecto
                ) VALUES (
                    @Titulo, 
                    @Descripcion, 
                    @Monto, 
                    @URLImagenRecompensa, 
                    @FKProyecto
                )";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@Titulo", recompensa.Titulo);
                    command.Parameters.AddWithValue("@Descripcion", recompensa.Descripcion);
                    command.Parameters.AddWithValue("@Monto", recompensa.Monto);
                    command.Parameters.AddWithValue("@URLImagenRecompensa", recompensa.URLImagenRecompensa);
                    command.Parameters.AddWithValue("@FKProyecto", recompensa.FKProyecto);

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
        public async Task<IEnumerable<Recompensa>> GetAllAsync()
        {
            List<Recompensa> recompensas = new List<Recompensa>();

            const string query = @"SELECT * FROM Recompensa;";

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
                            recompensas.Add(new Recompensa
                            {
                                IDRecompensa = reader.GetInt32(reader.GetOrdinal("IDRecompensa")),
                                Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                Monto = reader.GetDecimal(reader.GetOrdinal("Monto")),
                                URLImagenRecompensa = reader.GetString(reader.GetOrdinal("URLImagenRecompensa")),
                                FKProyecto = reader.GetInt32(reader.GetOrdinal("FKProyecto")),
                            });
                        }
                    }
                }
                return recompensas;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // Leer por ID
        public async Task<Recompensa> GetByIdAsync(int id)
        {
            const string query = @"SELECT * FROM Recompensa WHERE IDRecompensa = @IDRecompensa";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDRecompensa", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Recompensa
                            {
                                IDRecompensa = reader.GetInt32(reader.GetOrdinal("IDRecompensa")),
                                Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                                Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                                Monto = reader.GetDecimal(reader.GetOrdinal("Monto")),
                                URLImagenRecompensa = reader.GetString(reader.GetOrdinal("URLImagenRecompensa")),
                                FKProyecto = reader.GetInt32(reader.GetOrdinal("FKProyecto")),
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
        public async Task<bool> UpdateAsync(Recompensa recompensa)
        {
            const string query = @"
                UPDATE Recompensa 
                SET 
                    Titulo = @Titulo,
                    Descripcion = @Descripcion,
                    Monto = @Monto,
                    URLImagenRecompensa = @URLImagenRecompensa,
                    FKProyecto = @FKProyecto
                WHERE IDRecompensa = @IDRecompensa";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDRecompensa", recompensa.IDRecompensa);
                    command.Parameters.AddWithValue("@Titulo", recompensa.Titulo);
                    command.Parameters.AddWithValue("@Descripcion", recompensa.Descripcion);
                    command.Parameters.AddWithValue("@Monto", recompensa.Monto);
                    command.Parameters.AddWithValue("@URLImagenRecompensa", recompensa.URLImagenRecompensa);
                    command.Parameters.AddWithValue("@FKProyecto", recompensa.FKProyecto);

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
            const string query = @"DELETE FROM Recompensa WHERE IDRecompensa = @IDRecompensa";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDRecompensa", id);
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
