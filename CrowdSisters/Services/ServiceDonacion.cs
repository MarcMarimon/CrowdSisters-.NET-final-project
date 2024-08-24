using CrowdSisters.DAL;
using CrowdSisters.Models;

namespace CrowdSisters.Services
{
    public class ServiceDonacion
    {

        private readonly DALDonacion _dalDonacion;

        public ServiceDonacion(DALDonacion dalDonacion)
        {
            _dalDonacion = dalDonacion;
        }

        public async Task<Boolean> CrearDonacionAsync(Donacion donacion, int IdUsuario)
        {
            try
            {
                return await _dalDonacion.CreateAsync(donacion, IdUsuario);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al crear el proyecto: {ex.Message}");
                return false;
            }
        }
    }
}
