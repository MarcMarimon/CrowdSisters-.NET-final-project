using CrowdSisters.Conections;
using CrowdSisters.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrowdSisters.DAL
{
    public class DALDonacion
    {
        private readonly Connection _connection;

        public DALDonacion(Connection connection)
        {
            _connection = connection;
        }

        // Crear
        public async Task<bool> CreateAsync(Donacion donacion)
        {
            const string query = @"
                INSERT INTO Donacion (
                    FKProyecto, 
                    FKUsuario, 
                    FKRecompensa
                ) VALUES (
                    @FKProyecto, 
                    @FKUsuario, 
                    @FKRecompensa
                )";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@FKProyecto", donacion.FKProyecto);
                    command.Parameters.AddWithValue("@FKUsuario", donacion.FKUsuario);
                    command.Parameters.AddWithValue("@FKRecompensa", donacion.FKRecompensa);

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
        public async Task<IEnumerable<Donacion>> GetAllAsync()
        {
            List<Donacion> donaciones = new List<Donacion>();

            const string query = @"SELECT * FROM Donacion;";

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
                            donaciones.Add(new Donacion
                            {
                                IDDonacion = reader.GetInt32(reader.GetOrdinal("IDDonacion")),
                                FKProyecto = reader.GetInt32(reader.GetOrdinal("FKProyecto")),
                                FKUsuario = reader.GetInt32(reader.GetOrdinal("FKUsuario")),
                                FKRecompensa = reader.GetInt32(reader.GetOrdinal("FKRecompensa")),
                            });
                        }
                    }
                }
                return donaciones;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // Leer por ID
        public async Task<Donacion> GetByIdAsync(int id)
        {
            const string query = @"SELECT * FROM Donacion WHERE IDDonacion = @IDDonacion";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDDonacion", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Donacion
                            {
                                IDDonacion = reader.GetInt32(reader.GetOrdinal("IDDonacion")),
                                FKProyecto = reader.GetInt32(reader.GetOrdinal("FKProyecto")),
                                FKUsuario = reader.GetInt32(reader.GetOrdinal("FKUsuario")),
                                FKRecompensa = reader.GetInt32(reader.GetOrdinal("FKRecompensa")),
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
        public async Task<bool> UpdateAsync(Donacion donacion)
        {
            // El modelo actual no tiene campos actualizables, solo se podría usar este método si se añaden campos
            throw new NotImplementedException("El método UpdateAsync no está implementado ya que la tabla Donacion no tiene campos actualizables.");
        }

        // Eliminar
        public async Task<bool> DeleteAsync(int id)
        {
            const string query = @"DELETE FROM Donacion WHERE IDDonacion = @IDDonacion";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDDonacion", id);
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
