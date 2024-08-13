using CrowdSisters.Conections;
using CrowdSisters.Models;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace CrowdSisters.DAL
{
    public class DALImagen
    {
        private readonly Connection _connection;
        private DALProyecto _dalProyecto;

        public DALImagen(Connection connection)
        {
            _connection = connection;
            _dalProyecto = new DALProyecto();
        }

        // Método para obtener una conexión abierta
        private SqlConnection GetOpenConnection()
        {
            var connection = _connection.GetConnection();
            connection.Open();
            return connection;
        }

        // Crear
        public async Task<bool> CreateAsync(Imagen imagen)
        {
            const string query = @"
                INSERT INTO IMAGEN (FKProyecto, URLImagenProyecto) 
                VALUES (@FKProyecto, @URLImagenProyecto)";
            using (var connection = GetOpenConnection())
            using (var command = new SqlCommand(query, connection))
            {

                command.Parameters.AddWithValue("@FKProyecto", imagen.FKProyecto);
                command.Parameters.AddWithValue("@URLImagenProyecto", imagen.URLImagenProyecto);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }
        /*
                public List<Imagen> SelectAll()
                {
                    connection.Open();

                    List<Imagen> imagenes = new List<Imagen>();

                    string query = "SELECT * FROM IMAGEN";
                    SqlCommand command = new SqlCommand(query, connection.GetConnection());

                    SqlDataReader records = command.ExecuteReader();

                    while (records.Read())
                    {
                        int idImagen = records.GetInt32(records.GetOrdinal("IDImagen"));
                        int fkProyecto = records.GetInt32(records.GetOrdinal("FKProyecto"));
                        Proyecto proyecto = null;
                        string urlImagenProyecto = records.GetString(records.GetOrdinal("URLImagenProyecto"));

                        Imagen imagen = new Imagen();

                        imagen.IDImagen = idImagen;
                        imagen.FKProyecto = fkProyecto;
                        imagen.Proyecto = proyecto;
                        imagen.URLImagenProyecto = urlImagenProyecto;


                        imagenes.Add(imagen);
                    }

                    records.Close();
                    connection.Close();
                    return imagenes;

                }
        */

        // Leer
        public async Task<Imagen> GetByIdAsync(int id)
        {
            const string query = "SELECT * FROM IMAGEN WHERE IDImagen = @IDImagen";

            using (var connection = GetOpenConnection())
            using (var command = new SqlCommand(query, connection))
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
                            Proyecto = await _dalProyecto.GetByIdAsync(reader.GetInt32(reader.GetOrdinal("FKProyecto"))),
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

            using (var connection = GetOpenConnection())
            using (var command = new SqlCommand(query, connection))
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
            const string query = "DELETE FROM IMAGEN WHERE IDImagen = @IDImagen";
            using (var connection = GetOpenConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IDImagen", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

    }

}
