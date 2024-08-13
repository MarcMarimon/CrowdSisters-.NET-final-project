using CrowdSisters.Models;
using System.Data.Common;

namespace CrowdSisters.DAL
{
    public class DALImagen
    {
        private DbConnection connection;

        public DALImagen()
        {
            this.connection = new DbConnection();
        }
        public void insertImagen(Imagen imagen)
        {
            connection.Open();

            string sql = @"
                INSERT INTO IMAGEN (FKProyecto, URLImagenProyecto) 
                VALUES (@FKProyecto, @URLImagenProyecto)";

            SqlCommand cmd = new SqlCommand(sql, connection.GetConnection());

            cmd.Parameters.AddWithValue("@FKProyecto", imagen.FKProyecto);
            cmd.Parameters.AddWithValue("@URLImagenProyecto", imagen.URLImagenProyecto);

            cmd.ExecuteNonQuery();

            connection.Close();
        }


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

    }

}
