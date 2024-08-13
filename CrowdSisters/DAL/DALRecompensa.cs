using CrowdSisters.Models;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace CrowdSisters.DAL
{
    public class DALRecompensa
    {
        public List<Recompensa> SelectAllRecompensa()
        {
            List<Recompensa> recompensas = new List<Recompensa>();
            _connection.Open();

            string query = "SELECT * FROM Recompensa;";
            SqlCommand cmd = new SqlCommand(query, _connection);

            SqlDataReader records = cmd.ExecuteReader();

            while (records.Read())
            {
                Recompensa recompensa = new Recompensa();
                recompensa.IDRecompensa = records.GetInt32(records.GetOrdinal("IDRecompensa"));
                recompensa.FKProyecto = records.GetInt32(records.GetOrdinal("FKProyecto"));
                recompensa.Proyecto = DALProyecto.SelectProyectoById(recompensa.FKProyecto);
                recompensa.Descripcion = records.GetString(records.GetOrdinal("Descripcion"));
                recompensa.MontoMinimo = records.GetDecimal(records.GetOrdinal("MontoMinimo"));
                recompensa.MontoMaximo = records.GetDecimal(records.GetOrdinal("MontoMaximo"));
                recompensa.CantidadDisponible = records.GetInt32(records.GetOrdinal("CantidadDisponible"));
                recompensa.URLImagenRecompensa = records.GetString(records.GetOrdinal("URLImagenRecompensa"));

                recompensas.Add(recompensa);
            }

            return recompensas;
        }

        public Recompensa SelectRecompensaById(int id)
        {
            Recompensa recompensa = new Recompensa();
            _connection.Open();

            string query = $"SELECT * FROM Recompensa WHERE IDRecompensa = {id};";
            SqlCommand cmd = new SqlCommand(query, _connection);

            SqlDataReader records = cmd.ExecuteReader();

            while (records.Read())
            {
                recompensa.IDRecompensa = records.GetInt32(records.GetOrdinal("IDRecompensa"));
                recompensa.FKProyecto = records.GetInt32(records.GetOrdinal("FKProyecto"));
                recompensa.Proyecto = DALProyecto.SelectProyectoById(recompensa.FKProyecto);
                recompensa.Descripcion = records.GetString(records.GetOrdinal("Descripcion"));
                recompensa.MontoMinimo = records.GetDecimal(records.GetOrdinal("MontoMinimo"));
                recompensa.MontoMaximo = records.GetDecimal(records.GetOrdinal("MontoMaximo"));
                recompensa.CantidadDisponible = records.GetInt32(records.GetOrdinal("CantidadDisponible"));
                recompensa.URLImagenRecompensa = records.GetString(records.GetOrdinal("URLImagenRecompensa"));
            }

            return recompensa;
        }
    }
}
