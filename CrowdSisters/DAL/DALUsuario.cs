using CrowdSisters.Models;
using System.Data.Common;

namespace CrowdSisters.DAL
{
    public class DALUsuario
    {
        private DbConnection connection;

        public DALUsuario()
        {
            this.connection = new DbConnection();
        }

        public void insertUsuario(Usuario usuario)
        {
            connection.Open();

            string sql = @"
                INSERT INTO USUARIO (Nombre, Email, Contrasena, FechaRegistro, IsAdmin, PerfilPublico, 
                URLImagenUsuario, Monedero) 
                VALUES (@Nombre, @Email, @Contrasena, @FechaRegistro, @IsAdmin, @PerfilPublico, @URLImagenUsuario,
                @Monedero)";

            SqlCommand cmd = new SqlCommand(sql, connection.GetConnection());

            cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            cmd.Parameters.AddWithValue("@Email", usuario.Email);
            cmd.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
            cmd.Parameters.AddWithValue("@FechaRegistro", usuario.FechaRegistro);
            cmd.Parameters.AddWithValue("@IsAdmin", usuario.IsAdmin);
            cmd.Parameters.AddWithValue("@PerfilPublico", usuario.PerfilPublico);
            cmd.Parameters.AddWithValue("@URLImagenUsuario", usuario.URLImagenUsuario);
            cmd.Parameters.AddWithValue("@Monedero", usuario.Monedero);



            cmd.ExecuteNonQuery();

            connection.Close();
        }


        public List<Usuario> SelectAll()
        {
            connection.Open();

            List<Usuario> usuarios = new List<Usuario>();

            string query = "SELECT * FROM USUARIO";
            SqlCommand command = new SqlCommand(query, connection.GetConnection());

            SqlDataReader records = command.ExecuteReader();

            while (records.Read())
            {
                int idUsuario = records.GetInt32(records.GetOrdinal("IDUsuario"));
                string nombre = records.GetString(records.GetOrdinal("Nombre"));
                string email = records.GetString(records.GetOrdinal("Email"));
                string contrasena = records.GetString(records.GetOrdinal("Contrasena"));
                DateTime fechaRegistro = records.GetDateTime(records.GetOrdinal("FechaRegistro"));
                bool isAdmin = records.GetBoolean(records.GetOrdinal("IsAdmin"));
                string perfilPublico = records.GetString(records.GetOrdinal("PerfilPublico"));
                string urlImagenUsuario = records.GetString(records.GetOrdinal("URLImagenUsuario"));
                decimal monedero = records.GetDecimal(records.GetOrdinal("Monedero"));



                Usuario usuario = new Usuario();

                usuario.IDUsuario = idUsuario;
                usuario.Nombre = nombre;
                usuario.Email = email;
                usuario.Contrasena = contrasena;
                usuario.FechaRegistro = fechaRegistro;
                usuario.IsAdmin = isAdmin;
                usuario.PerfilPublico = perfilPublico;
                usuario.URLImagenUsuario = urlImagenUsuario;
                usuario.Monedero = monedero;


                usuarios.Add(usuario);
            }

            records.Close();
            connection.Close();
            return usuarios;

        }

        public Usuario SelectUsuarioById(int idUsuario)
        {
            connection.Open();

            Usuario usuario = null;

            string query = "SELECT * FROM USUARIO WHERE IDUsuario = @IDUsuario";
            SqlCommand command = new SqlCommand(query, connection.GetConnection());
            command.Parameters.AddWithValue("@IDUsuario", idUsuario);

            SqlDataReader records = command.ExecuteReader();

            if (records.Read())
            {
                // Asignación de los valores del registro a las propiedades del objeto Usuario
                usuario = new Usuario
                {
                    IDUsuario = records.GetInt32(records.GetOrdinal("IDUsuario")),
                    Nombre = records.GetString(records.GetOrdinal("Nombre")),
                    Email = records.GetString(records.GetOrdinal("Email")),
                    Contrasena = records.GetString(records.GetOrdinal("Contrasena")),
                    FechaRegistro = records.GetDateTime(records.GetOrdinal("FechaRegistro")),
                    IsAdmin = records.GetBoolean(records.GetOrdinal("IsAdmin")),
                    PerfilPublico = records.GetString(records.GetOrdinal("PerfilPublico")),
                    URLImagenUsuario = records.GetString(records.GetOrdinal("URLImagenUsuario")),
                    Monedero = records.GetDecimal(records.GetOrdinal("Monedero"))
                };
            }

            records.Close();
            connection.Close();

            return usuario;
        }

    }

}