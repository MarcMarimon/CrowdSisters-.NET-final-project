using CrowdSisters.DAL;
using CrowdSisters.Models;

namespace CrowdSisters.Services
{
    public class ServiceUsuario
    {
        private readonly DALUsuario _dalUsuario;

        public ServiceUsuario(DALUsuario dalUsuario)
        {
            _dalUsuario = dalUsuario;
        }


        public async Task<Boolean> RestarMonederoUsuarioAsync(decimal resta, int idUsuario)
        {
            try
            {
                return await _dalUsuario.UpdateRestarMonederoUsuarioAsync(resta, idUsuario);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al crear el proyecto: {ex.Message}");
                return false;
            }
        }

        public async Task<Boolean> SumarMonederoUsuarioAsync(decimal suma, int idUsuario)
        {
            try
            {
                return await _dalUsuario.UpdateSumarMonederoUsuarioAsync(suma, idUsuario);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al crear el proyecto: {ex.Message}");
                return false;
            }
        }

        public async Task<Usuario> GetUsuarioById(int idUsuario)
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


        public async Task<int> GetMonto(int id)
        {
            Usuario usuario = await _dalUsuario.GetByIdAsync(id);
            return (int)usuario.Monedero;
        }




    }
}
