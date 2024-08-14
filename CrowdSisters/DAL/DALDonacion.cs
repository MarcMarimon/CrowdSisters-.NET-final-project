using CrowdSisters.Conections;
using CrowdSisters.Models;
using Microsoft.Data.SqlClient;

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
                INSERT INTO DONACION (FKProyecto, FKUsuario, Monto, FechaDonacion, MetodoPago) 
                VALUES (@FKProyecto, @FKUsuario, @Monto, @FechaDonacion, @MetodoPago)";

            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@FKProyecto", donacion.FKProyecto);
                command.Parameters.AddWithValue("@FKUsuario", donacion.FKUsuario);
                command.Parameters.AddWithValue("@Monto", donacion.Monto);
                command.Parameters.AddWithValue("@FechaDonacion", donacion.FechaDonacion);
                command.Parameters.AddWithValue("@MetodoPago", donacion.MetodoPago);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        /*
        public List<Donacion> SelectAll()
        {
            connection.Open();

            List<Donacion> donaciones = new List<Donacion>();

            string query = "SELECT * FROM DONACION";
            SqlCommand command = new SqlCommand(query, connection.GetConnection());

            SqlDataReader records = command.ExecuteReader();

            while (records.Read())
            {
                int idDonacion = records.GetInt32(records.GetOrdinal("IDDonacion"));
                int fkProyecto = records.GetInt32(records.GetOrdinal("FKProyecto"));
                Proyecto proyecto = null;
                int fkUsuario = records.GetInt32(records.GetOrdinal("FKUsuario"));
                Usuario usuario = dalUsuario.SelectUsuarioById(fkUsuario);
                decimal monto = records.GetDecimal(records.GetOrdinal("Monto"));
                DateTime fechaDonacion = records.GetDateTime(records.GetOrdinal("FechaDonacion"));
                int metodoPago = records.GetInt32(records.GetOrdinal("MetodoPago"));

                
                Donacion donacion = new Donacion();

                donacion.IDDonacion = idDonacion;
                donacion.FKProyecto = fkProyecto;
                donacion.Proyecto = proyecto;
                donacion.FKUsuario = fkUsuario;
                donacion.Usuario = usuario;
                donacion.Monto = monto;
                donacion.FechaDonacion = fechaDonacion;
                donacion.MetodoPago = metodoPago;

                
                donaciones.Add(donacion);
            }

            records.Close();
            connection.Close();
            return donaciones;

        }
        */


        // Leer
        public async Task<Donacion> GetByIdAsync(int id)
        {
          DALProyecto dalProyecto = new DALProyecto(_connection);

          DALUsuario dalUsuario = new DALUsuario(_connection);

        const string query = @"SELECT * FROM DONACION WHERE IDDonacion = @IDDonacion";

            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@IDDonacion", id);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new Donacion
                        {
                            IDDonacion = reader.GetInt32(reader.GetOrdinal("IDDonacion")),
                            FKProyecto = reader.GetInt32(reader.GetOrdinal("FKProyecto")),
                            Proyecto = await dalProyecto.GetByIdAsync(reader.GetInt32(reader.GetOrdinal("FKProyecto"))),
                            FKUsuario = reader.GetInt32(reader.GetOrdinal("FKUsuario")),
                            Usuario = await dalUsuario.GetByIdAsync(reader.GetInt32(reader.GetOrdinal("FKUsuario"))),
                            Monto = reader.GetDecimal(reader.GetOrdinal("Monto")),
                            FechaDonacion = reader.GetDateTime(reader.GetOrdinal("FechaDonacion")),
                            MetodoPago = reader.GetInt32(reader.GetOrdinal("MetodoPago"))
                        };
                    }
                    return null;
                }
            }
        }




        // Actualizar
        public async Task<bool> UpdateAsync(Donacion donacion)
        {
            const string query = @"UPDATE DONACION 
                     SET FKProyecto = @FKProyecto,
                         FKUsuario = @FKUsuario,
                         Monto = @Monto,
                         FechaDonacion = @FechaDonacion,
                         MetodoPago = @MetodoPago
                     WHERE IDDonacion = @IDDonacion";

            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@IDDonacion", donacion.IDDonacion);
                command.Parameters.AddWithValue("@FKProyecto", donacion.FKProyecto);
                command.Parameters.AddWithValue("@FKUsuario", donacion.FKUsuario);
                command.Parameters.AddWithValue("@Monto", donacion.Monto);
                command.Parameters.AddWithValue("@FechaDonacion", donacion.FechaDonacion);
                command.Parameters.AddWithValue("@MetodoPago", donacion.MetodoPago);
                return await command.ExecuteNonQueryAsync() > 0;
            }
        }



        // Eliminar

        public async Task<bool> DeleteAsync(int id)
        {
            const string query = @"DELETE FROM DONACION WHERE IDDonacion = @IDDonacion";
            using (var sqlConn = _connection.GetSqlConn())
            using (var command = new SqlCommand(query, sqlConn))
            {
                command.Parameters.AddWithValue("@IDDonacion", id);

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

    }

}

