using CrowdSisters.Models;
using System.Data.Common;

namespace CrowdSisters.DAL
{
    public class DALCategoria
    {
        private SqlConnection _connection { get; set; }

        public List<Categoria> SelectAllCategoria()
        {
            List<Categoria> categorias = new List<Categoria>();
            _connection.Open();

            string query = "SELECT * FROM Categoria;";
            SqlCommand cmd = new SqlCommand(query, _connection);

            SqlDataReader records = cmd.ExecuteReader();

            while (records.Read())
            {
                Categoria categoria = new Categoria();
                categoria.IDCategoria = records.GetInt32(records.GetOrdinal("IDCategoria"));
                categoria.Nombre = records.GetString(records.GetOrdinal("Nombre"));
                
                categorias.Add(categoria);
            }

            return categorias;
        }

        public static Categoria SelectCategoriaById(int id)
        {
            Categoria categoria = new Categoria();
            string query = $"SELECT * FROM Categoria WHERE IDCategoria = {id}";

            SqlCommand cmd = new SqlCommand(query, _connection);
            SqlDataReader records = cmd.ExecuteReader();
            while (records.Read())
            {
                categoria.IDCategoria = (Categoria)records.GetInt32(records.GetOrdinal("IDCategoria"));
                categoria.Nombre = records.GetString(records.GetOrdinal("Nombre"));
            }
            return categoria;
        }
    }
}
