using CrowdSisters.Conections;
using CrowdSisters.Models;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace CrowdSisters.DAL
{
    public class DALCategoria
    {
        private readonly Connection _connection;

        public DALCategoria(Connection connection)
        {
            _connection = connection;
        }

        // Crear
        public async Task<bool> CreateAsync(Categoria categoria)
        {
            const string query = @"
                INSERT INTO Categoria (Nombre)
                VALUES (@Nombre)";
            try
            {

                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    command.Parameters.AddWithValue("@Nombre", categoria.Nombre);

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
        public async Task<IEnumerable<Categoria>> GetAllAsync()
        {
            List<Categoria> categorias = new List<Categoria>();
            try
            {

                const string query = @"SELECT * FROM Categoria;";
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            categorias.Add(new Categoria
                            {
                                IDCategoria = reader.GetInt32(reader.GetOrdinal("IDCategoria")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
                            });

                        }
                    }
                }
                return categorias;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<Categoria> GetByIdAsync(int id)
        {
            const string query = @"
                SELECT * FROM Categoria
                WHERE IDCategoria = @IDCategoria";
            try
            {

                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    command.Parameters.AddWithValue("@IDCategoria", id);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Categoria
                            {
                                IDCategoria = reader.GetInt32(reader.GetOrdinal("IDCategoria")),
                                Nombre = reader.GetString(reader.GetOrdinal("Nombre"))
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
        public async Task<bool> UpdateAsync(Categoria categoria)
        {
            const string query = @"
                UPDATE Categoria
                SET Nombre = @Nombre,
                WHERE IDCategoria = @IDCategoria";

            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    command.Parameters.AddWithValue("@IDCategoria", categoria.IDCategoria);
                    command.Parameters.AddWithValue("@Nombre", categoria.Nombre);

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
                DELETE FROM Categoria
                WHERE IDCategoria = @IDCategoria";
            try
            {
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    command.Parameters.AddWithValue("@IDCategoria", id);

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
