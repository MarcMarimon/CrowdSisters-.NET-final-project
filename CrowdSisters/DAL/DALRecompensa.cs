using CrowdSisters.Conections;
using CrowdSisters.Models;
using Microsoft.Data.SqlClient;
using System.Data.Common;


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
                INSERT INTO Recompensa (FKProyecto, Titulo, Descripcion, MontoMinimo, MontoMaximo, CantidadDisponible,URLImagenRecompensa)
                VALUES (@FKProyecto, @Titulo,@Descripcion,@MontoMinimo,@MontoMaximo,@CantidadDisponible,@URLImagenRecompensa)";

            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@FKProyecto", recompensa.FKProyecto);
                command.Parameters.AddWithValue("@Titulo", recompensa.Titulo);
                command.Parameters.AddWithValue("@Descripcion", recompensa.Descripcion);
                command.Parameters.AddWithValue("@MontoMinimo", recompensa.MontoMinimo);
                command.Parameters.AddWithValue("@MontoMaximo", recompensa.MontoMaximo);
                command.Parameters.AddWithValue("@CantidadDisponible", recompensa.CantidadDisponible);
                command.Parameters.AddWithValue("@URLImagenRecompensa", recompensa.URLImagenRecompensa);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        // Leer
        public async Task<List<Recompensa>> GetAllAsync()
        {
            List<Recompensa> recompensas = new List<Recompensa>();
            DALProyecto dal = new DALProyecto(_connection);

            const string query = @"SELECT * FROM Recompensa;";
            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        recompensas.Add(new Recompensa
                        {
                            IDRecompensa = reader.GetInt32(reader.GetOrdinal("IDCategoria")),
                            FKProyecto = reader.GetInt32(reader.GetOrdinal("FKProyecto")),
                            Proyecto = await dal.GetByIdAsync(reader.GetInt32(reader.GetOrdinal("FKProyecto"))),
                            Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                            MontoMinimo = reader.GetDecimal(reader.GetOrdinal("MontoMinimo")),
                            MontoMaximo = reader.GetDecimal(reader.GetOrdinal("MontoMaximo")),
                            CantidadDisponible = reader.GetInt32(reader.GetOrdinal("CantidadDisponible")),
                            URLImagenRecompensa = reader.GetString(reader.GetOrdinal("URLImagenRecompensa")),
                        });

                    }
                }
            }
            return recompensas;
        }
        public async Task<Recompensa> GetByIdAsync(int id)
        {
            DALProyecto dal = new DALProyecto(_connection);
            const string query = @"
                SELECT * FROM Recompensa
                WHERE IDRecompensa = @IDRecompensa";

            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@IDRecompensa", id);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Recompensa
                        {
                            IDRecompensa = reader.GetInt32(reader.GetOrdinal("IDRecompensa")),
                            FKProyecto = reader.GetInt32(reader.GetOrdinal("FKProyecto")),
                            Proyecto = await dal.GetByIdAsync(reader.GetInt32(reader.GetOrdinal("FKProyecto"))),
                            Titulo = reader.GetString(reader.GetOrdinal("Titulo")),
                            Descripcion = reader.GetString(reader.GetOrdinal("Descripcion")),
                            MontoMinimo = reader.GetDecimal(reader.GetOrdinal("MontoMinimo")),
                            MontoMaximo = reader.GetDecimal(reader.GetOrdinal("MontoMaximo")),
                            CantidadDisponible = reader.GetInt32(reader.GetOrdinal("CantidadDisponible")),
                            URLImagenRecompensa = reader.GetString(reader.GetOrdinal("URLImagenRecompensa"))
                        };
                    }

                    return null;
                }
            }
        }

        // Actualizar
        public async Task<bool> UpdateAsync(Recompensa recompensa)
        {
            const string query = @"
                UPDATE Recompensa
                SET Titulo = @Titulo,
                    Descripcion = @Descripcion,
                    MontoMinimo = @MontoMinimo,
                    MontoMaximo = @MontoMaximo,
                    CantidadDisponible = @CantidadDisponible,
                    URLImagenRecompensa = @URLImagenRecompensa
                WHERE IDRecompensa = @IDRecompensa";

            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@IDRecompensa", recompensa.IDRecompensa);
                command.Parameters.AddWithValue("@Titulo", recompensa.Titulo);
                command.Parameters.AddWithValue("@Descripcion", recompensa.Descripcion);
                command.Parameters.AddWithValue("@MontoMinimo", recompensa.MontoMinimo);
                command.Parameters.AddWithValue("@MontoMaximo", recompensa.MontoMaximo);
                command.Parameters.AddWithValue("@CantidadDisponible", recompensa.CantidadDisponible);
                command.Parameters.AddWithValue("@URLImagenRecompensa", recompensa.URLImagenRecompensa);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        // Eliminar
        public async Task<bool> DeleteAsync(int id)
        {
            const string query = @"
                DELETE FROM Recompensa
                WHERE IDRecompensa = @IDRecompensa";

            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@IDRecompensa", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
    }
}
