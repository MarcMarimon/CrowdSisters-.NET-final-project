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

        public Imagen SelectImagenById(int idImagen)
        {
            connection.Open();

            Imagen imagen = null;

            string query = "SELECT * FROM IMAGEN WHERE IDImagen = @IDImagen";
            SqlCommand command = new SqlCommand(query, connection.GetConnection());
            command.Parameters.AddWithValue("@IDImagen", idImagen);

            SqlDataReader records = command.ExecuteReader();

            if (records.Read())
            {
                int fkProyecto = records.GetInt32(records.GetOrdinal("FKProyecto"));
                Proyecto proyecto = null;  
                string urlImagenProyecto = records.GetString(records.GetOrdinal("URLImagenProyecto"));

                imagen = new Imagen
                {
                    IDImagen = idImagen,
                    FKProyecto = fkProyecto,
                    Proyecto = proyecto,
                    URLImagenProyecto = urlImagenProyecto
                };
            }

            records.Close();
            connection.Close();

            return imagen;
        }

        public bool DeleteImagenById(int idImagen)
        {
            connection.Open();

            string query = "DELETE FROM IMAGEN WHERE IDImagen = @IDImagen";
            SqlCommand command = new SqlCommand(query, connection.GetConnection());
            command.Parameters.AddWithValue("@IDImagen", idImagen);

            int rowsAffected = command.ExecuteNonQuery();

            connection.Close();

            return rowsAffected > 0;
        }

        public bool UpdateImagen(Imagen imagen)
        {
            connection.Open();

            string query = @"UPDATE IMAGEN 
                     SET FKProyecto = @FKProyecto,
                         URLImagenProyecto = @URLImagenProyecto
                     WHERE IDImagen = @IDImagen";

            SqlCommand command = new SqlCommand(query, connection.GetConnection());
            command.Parameters.AddWithValue("@IDImagen", imagen.IDImagen);
            command.Parameters.AddWithValue("@FKProyecto", imagen.FKProyecto);
            command.Parameters.AddWithValue("@URLImagenProyecto", imagen.URLImagenProyecto);

            int rowsAffected = command.ExecuteNonQuery();

            connection.Close();

            // Devuelve true si se actualizó alguna fila, false si no se encontró la imagen
            return rowsAffected > 0;
        }




    }

}
