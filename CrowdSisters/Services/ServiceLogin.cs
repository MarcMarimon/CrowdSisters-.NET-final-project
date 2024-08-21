using CrowdSisters.DAL;
using CrowdSisters.Models;
using System.ComponentModel.DataAnnotations;

namespace CrowdSisters.Services
{
    public class ServiceLogin
    {
        private readonly DALUsuario _dalUsuario;

        public ServiceLogin(DALUsuario dalUsuario)
        {
            _dalUsuario = dalUsuario;
        }
        //Verify User

        public async Task<int> VerifyMailAsync(string email)
        {
            IEnumerable<Usuario> listUsuario = await _dalUsuario.GetAllAsync();

            foreach (Usuario usuario in listUsuario)
            {
                if(usuario.Email == email)
                    return usuario.IDUsuario;
            }
            return 0;
        }
        public async Task<bool> VerifyPasswordAsync(string password,int idUsuario)
        {
            Usuario usuario = await _dalUsuario.GetByIdAsync(idUsuario);
            if (usuario.Contrasena == password)
            {
                return true;
            }
            
            return false;
        }

        // Crear Proyecto
        public async Task<bool> CreateAsync(Usuario usuario)
        {
            try
            {
                return await _dalUsuario.CreateAsync(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al crear el usuario: {ex.Message}");
                return false;
            }
        }

        // Obtener Proyecto por ID
        public async Task<Usuario> GetByIdAsync(int id)
        {
            try
            {
                return await _dalUsuario.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al obtener el usuario por ID: {ex.Message}");
                return null;
            }
        }

        // Actualizar Proyecto
        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            try
            {
                return await _dalUsuario.UpdateAsync(usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al actualizar el usuario: {ex.Message}");
                return false;
            }
        }

        // Eliminar Proyecto
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _dalUsuario.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al eliminar el usuario: {ex.Message}");
                return false;
            }
        }

        // Obtener todos los Proyectos
        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            try
            {
                return await _dalUsuario.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al obtener todos los usuario: {ex.Message}");
                return null;
            }
        }
    }
}
