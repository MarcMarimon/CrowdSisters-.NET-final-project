using CrowdSisters.Models;
using System.Data.Common;

namespace CrowdSisters.DAL
{
    public class DALDonacion
    {
        private DbConnection connection;

        private DALUsuario dalUsuario;  

        public DALDonacion()
        {
            connection = new DbConnection();
            dalUsuario = new DALUsuario();
        }
        
        public void insertDonacion(Donacion donacion)
        {
            connection.Open();

            string sql = @"
                INSERT INTO DONACION (FKProyecto, FKUsuario, Monto, FechaDonacion, MetodoPago) 
                VALUES (@FKProyecto, @FKUsuario, @Monto, @FechaDonacion, @MetodoPago)";

            SqlCommand cmd = new SqlCommand(sql, connection.GetConnection());

            cmd.Parameters.AddWithValue("@FKProyecto", donacion.FKProyecto);
            cmd.Parameters.AddWithValue("@FKUsuario", donacion.FKUsuario);
            cmd.Parameters.AddWithValue("@Monto", donacion.Monto);
            cmd.Parameters.AddWithValue("@FechaDonacion", donacion.FechaDonacion);
            cmd.Parameters.AddWithValue("@MetodoPago", donacion.MetodoPago);

            cmd.ExecuteNonQuery();

            connection.Close();
        }
        

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

        public Donacion SelectDonacionById(int idDonacion)
        {
            connection.Open();

            Donacion donacion = null;

            string query = "SELECT * FROM DONACION WHERE IDDonacion = @IDDonacion";
            SqlCommand command = new SqlCommand(query, connection.GetConnection());
            command.Parameters.AddWithValue("@IDDonacion", idDonacion);

            SqlDataReader records = command.ExecuteReader();

            if (records.Read())
            {
                int fkProyecto = records.GetInt32(records.GetOrdinal("FKProyecto"));
                Proyecto proyecto = null;
                int fkUsuario = records.GetInt32(records.GetOrdinal("FKUsuario"));
                Usuario usuario = dalUsuario.SelectUsuarioById(fkUsuario); 
                decimal monto = records.GetDecimal(records.GetOrdinal("Monto"));
                DateTime fechaDonacion = records.GetDateTime(records.GetOrdinal("FechaDonacion"));
                int metodoPago = records.GetInt32(records.GetOrdinal("MetodoPago"));

                donacion = new Donacion
                {
                    IDDonacion = idDonacion,
                    FKProyecto = fkProyecto,
                    Proyecto = proyecto,
                    FKUsuario = fkUsuario,
                    Usuario = usuario,
                    Monto = monto,
                    FechaDonacion = fechaDonacion,
                    MetodoPago = metodoPago
                };
            }

            records.Close();
            connection.Close();

            return donacion;
        }

        public bool DeleteDonacionById(int idDonacion)
        {
            connection.Open();

            string query = "DELETE FROM DONACION WHERE IDDonacion = @IDDonacion";
            SqlCommand command = new SqlCommand(query, connection.GetConnection());
            command.Parameters.AddWithValue("@IDDonacion", idDonacion);

            int rowsAffected = command.ExecuteNonQuery();

            connection.Close();

            // Devuelve true si se eliminó alguna fila, false si no se encontró la donación
            return rowsAffected > 0;
        }

        public bool UpdateDonacion(Donacion donacion)
        {
            connection.Open();

            string query = @"UPDATE DONACION 
                     SET FKProyecto = @FKProyecto,
                         FKUsuario = @FKUsuario,
                         Monto = @Monto,
                         FechaDonacion = @FechaDonacion,
                         MetodoPago = @MetodoPago
                     WHERE IDDonacion = @IDDonacion";

            SqlCommand command = new SqlCommand(query, connection.GetConnection());
            command.Parameters.AddWithValue("@IDDonacion", donacion.IDDonacion);
            command.Parameters.AddWithValue("@FKProyecto", donacion.FKProyecto);
            command.Parameters.AddWithValue("@FKUsuario", donacion.FKUsuario);
            command.Parameters.AddWithValue("@Monto", donacion.Monto);
            command.Parameters.AddWithValue("@FechaDonacion", donacion.FechaDonacion);
            command.Parameters.AddWithValue("@MetodoPago", donacion.MetodoPago);

            int rowsAffected = command.ExecuteNonQuery();

            connection.Close();

            // Devuelve true si se actualizó alguna fila, false si no se encontró la donación
            return rowsAffected > 0;
        }



    }

}

