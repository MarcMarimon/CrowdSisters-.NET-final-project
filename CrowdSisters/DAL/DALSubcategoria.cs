using CrowdSisters.Models;
using System.Data.Common;

namespace CrowdSisters.DAL
{
    public class DALSubcategoria
    {
        private SqlConnection _connection { get; set; }

        public List<Subcategoria> SelectAllSubcategoria()
        {
            List<Subcategoria> subcategorias = new List<Subcategoria>();
            _connection.Open();

            string query = "SELECT * FROM Subcategoria;";
            SqlCommand cmd = new SqlCommand(query, _connection);

            SqlDataReader records = cmd.ExecuteReader();

            while (records.Read())
            {
                Subcategoria subcategoria = new Subcategoria();
                subcategoria.IDSubcategoria = records.GetInt32(records.GetOrdinal("IDCategoria"));
                subcategoria.Nombre = records.GetString(records.GetOrdinal("Nombre"));
                subcategoria.FKCategoria = records.GetString(records.GetOrdinal("FKCategoria"));
                subcategoria.Categoria = DALCategoria.SelectCategoriaById(subcategoria.FKCategoria);


                subcategorias.Add(subcategoria);
            }

            return subcategorias;
        }

        public Subcategoria SelectSubcategoriaById(int id)
        {
            Subcategoria subcategoria = new Subcategoria();
            string query = $"SELECT * FROM Subcategoria WHERE IDSubcategoria = {id}";

            SqlCommand cmd = new SqlCommand(query, _connection);
            SqlDataReader records = cmd.ExecuteReader();
            while (records.Read())
            {
                subcategoria.IDSubcategoria = (Categoria)records.GetInt32(records.GetOrdinal("IDCategoria"));
                subcategoria.Nombre = records.GetString(records.GetOrdinal("Nombre"));
                subcategoria.FKCategoria = records.GetInt32(records.GetOrdinal("FKCategoria"));
                subcategoria.Categoria = DALCategoria.SelectCategoriaById(subcategoria.FKCategoria) ;

            }
            return subcategoria;
        }
    }
}
