using CrowdSisters.DAL;
using CrowdSisters.Models;
using Firebase.Auth;
namespace CrowdSisters.Services
{
    public class ServiceCrearProyecto
    {
        private readonly DALProyecto _dalProyecto;
        private readonly DALUsuario _dalUsuario;

        public ServiceCrearProyecto(DALProyecto dalProyecto, DALUsuario dalUsuario)
        {
            _dalProyecto = dalProyecto;
            _dalUsuario = dalUsuario;
        }

        public async Task<Usuario> CrearProjecteView(int idUsuario)
        {
            try
            {
                return await _dalUsuario.GetByIdAsync(idUsuario);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al crear el proyecto: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateUsuarioCrearProyecto(Usuario usuario)
        {
            try
            {
                return await _dalUsuario.UpdateUsuarioCrearProyectoAsync(usuario);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al crear el proyecto: {ex.Message}");
                return false;
            }
        }

        // Crear Proyecto
        public async Task<Proyecto> CreateProyectoAsync(Proyecto proyecto)
        {
            try
            {
                return await _dalProyecto.CreateAsync(proyecto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al crear el proyecto: {ex.Message}");
                return null;
            }
        }
    }
}
