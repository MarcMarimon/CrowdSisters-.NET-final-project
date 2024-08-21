using CrowdSisters.DAL;
using CrowdSisters.Models;

namespace CrowdSisters.Services
{
    public class ServiceRecompensa
    {
        private readonly DALRecompensa _dalRecompensa;

        public ServiceRecompensa(DALRecompensa dalRecompensa)
        {
            _dalRecompensa = dalRecompensa;
        }

        // Crear Proyecto
        public async Task<bool> CreateRecompensaAsync(Recompensa recompensa)
        {
            try
            {
                return await _dalRecompensa.CreateAsync(recompensa);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al crear el proyecto: {ex.Message}");
                return false;
            }
        }

    }
}
