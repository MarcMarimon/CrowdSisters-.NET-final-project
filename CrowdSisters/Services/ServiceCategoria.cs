using CrowdSisters.DAL;
using CrowdSisters.Models;

namespace CrowdSisters.Services
{
    public class ServiceCategoria
    {
        private readonly DALCategoria _dalCategoria;

        public ServiceCategoria(DALCategoria dalCategoria)
        {
            _dalCategoria = dalCategoria;
        }

        // Obtener todos los Proyectos
        public async Task<IEnumerable<Categoria>> GetAllCategoriasAsync()
        {
            try
            {
                return await _dalCategoria.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al obtener todos las categorias: {ex.Message}");
                return null;
            }
        }

    }
}
