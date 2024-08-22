using CrowdSisters.Conections;
using CrowdSisters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace CrowdSisters.DAL
{
    public class DALSubcategoria
    {
        private readonly Connection _connection;

        public DALSubcategoria(Connection connection)
        {
            _connection = connection;
        }

        // Crear
        public async Task<bool> CreateAsync(Subcategoria subcategoria)
        {
            const string query = @"
                INSERT INTO Subcategoria (Nombre,FKCategoria)
                VALUES (@Nombre,@FKCategoria)";
            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@Nombre", subcategoria.Nombre);
                    command.Parameters.AddWithValue("@FKCategoria", subcategoria.FKCategoria);


                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Leer
        public async Task<IEnumerable<Subcategoria>> GetAllAsync()
        {
            List<Subcategoria> subcategorias = new List<Subcategoria>();
            try
            {

                const string query = @"SELECT * FROM Subcategoria;";
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            subcategorias.Add(new Subcategoria
                            {
                                IDSubcategoria = reader.GetInt32(reader.GetOrdinal("IDSubcategoria")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                FKCategoria = reader.GetInt32(reader.GetOrdinal("FKCategoria"))
                            });

                        }
                    }
                }
                return subcategorias;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<Subcategoria> GetByIdAsync(int id)
        {
            const string query = @"
                SELECT * FROM Subcategoria
                WHERE IDSubcategoria = @IDSubcategoria";
            try
            {

                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDSubcategoria", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Subcategoria
                            {
                                IDSubcategoria = reader.GetInt32(reader.GetOrdinal("IDSubcategoria")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                                FKCategoria = reader.GetInt32(reader.GetOrdinal("FKCategoria"))
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


        public async Task<JsonResult> GetSubcategorias(int idCategoria)
        {
            List<Subcategoria> subcategorias = new List<Subcategoria>();

            const string query = @"SELECT IDSubcategoria, Nombre 
                           FROM Subcategoria
                           WHERE FKCategoria = @IdCategoria;";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    command.Parameters.AddWithValue("@IdCategoria", idCategoria);
                    sqlConn.Open();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            subcategorias.Add(new Subcategoria
                            {
                                IDSubcategoria = reader.GetInt32(reader.GetOrdinal("IDSubcategoria")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
                            });
                        }
                    }
                }

                return new JsonResult(subcategorias);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al obtener subcategorías: {ex.Message}");
                return new JsonResult(new { error = "Error al obtener subcategorías" });
            }
        }


        // Actualizar
        public async Task<bool> UpdateAsync(Subcategoria subcategoria)
        {
            const string query = @"
                UPDATE Subcategoria
                SET Nombre = @Nombre,
                WHERE IDSubcategoria = @IDSubcategoria";
            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDSubcateogira", subcategoria.IDSubcategoria);
                    command.Parameters.AddWithValue("@Nombre", subcategoria.Nombre);

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
            const string query = @"
                DELETE FROM Subcategoria
                WHERE IDSubcategoria = @IDSubcategoria";
            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    sqlConn.Open(); // Asegúrate de abrir la conexión
                    command.Parameters.AddWithValue("@IDSubcategoria", id);

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
