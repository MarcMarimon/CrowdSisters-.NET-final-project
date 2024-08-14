using CrowdSisters.Conections;
using CrowdSisters.Models;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using static System.Net.Mime.MediaTypeNames;

namespace CrowdSisters.DAL
{
    public class DALImagen
    {
        private readonly Connection _connection;

        public DALImagen(Connection connection)
        {
            _connection = connection;
        }

        // Crear
        public async Task<bool> CreateAsync(Imagen imagen)
        {
            const string query = @"
                INSERT INTO IMAGEN (FKProyecto, URLImagenProyecto) 
                VALUES (@FKProyecto, @URLImagenProyecto)";

            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {

                command.Parameters.AddWithValue("@FKProyecto", imagen.FKProyecto);
                command.Parameters.AddWithValue("@URLImagenProyecto", imagen.URLImagenProyecto);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }


        // Leer

        public async Task<List<Imagen>> GetAllAsync()
          {
            DALProyecto dalProyecto = new DALProyecto(_connection);

            List<Imagen> imagenes = new List<Imagen>();

                const string query = @"SELECT * FROM IMAGEN;";
                using (var sqlConn = _connection.GetSqlConn())
                using (var command = new SqlCommand(query, sqlConn))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            imagenes.Add(new Imagen
                            {
                                IDImagen = reader.GetInt32(reader.GetOrdinal("IDImagen")),
                                FKProyecto = reader.GetInt32(reader.GetOrdinal("FKProyecto")),
                                Proyecto = await dalProyecto.GetByIdAsync(reader.GetInt32(reader.GetOrdinal("FKProyecto")));
                                URLImagenProyecto = reader.GetString(reader.GetOrdinal("URLImagenProyecto"))
                            });

                        }
                    }
                }
                return imagenes;
            }


        public async Task<Imagen> GetByIdAsync(int id)
        {
            DALProyecto dalProyecto = new DALProyecto(_connection);

            const string query = @"SELECT * FROM IMAGEN WHERE IDImagen = @IDImagen";

            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@IDImagen", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Imagen
                        {
                            IDImagen = reader.GetInt32(reader.GetOrdinal("IDImagen")),
                            FKProyecto = reader.GetInt32(reader.GetOrdinal("FKProyecto")),
                            Proyecto = await dalProyecto.GetByIdAsync(reader.GetInt32(reader.GetOrdinal("FKProyecto"))),
                            URLImagenProyecto = reader.GetString(reader.GetOrdinal("URLImagenProyecto"))
                        };
                    }

                    return null;
                }
            }
        }


        // Actualizar

        public async Task<bool> UpdateAsync(Imagen imagen)
        {
            const string query = @"UPDATE IMAGEN 
                     SET FKProyecto = @FKProyecto,
                         URLImagenProyecto = @URLImagenProyecto
                     WHERE IDImagen = @IDImagen";

            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@IDImagen", imagen.IDImagen);
                command.Parameters.AddWithValue("@FKProyecto", imagen.FKProyecto);
                command.Parameters.AddWithValue("@URLImagenProyecto", imagen.URLImagenProyecto);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        // Eliminar

        public async Task<bool> DeleteAsync(int id)
        {
            const string query = @"DELETE FROM IMAGEN WHERE IDImagen = @IDImagen";
            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@IDImagen", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

    }

}
