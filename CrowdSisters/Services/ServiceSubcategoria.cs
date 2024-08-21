using CrowdSisters.DAL;
using CrowdSisters.Models;

namespace CrowdSisters.Services
{
    public class ServiceSubcategoria
    {
        private readonly DALSubcategoria _dalSubcategoria;

        public ServiceSubcategoria(DALSubcategoria dalSubcategoria)
        {
            _dalSubcategoria = dalSubcategoria;
        }

        // Obtener todos los Proyectos
        public async Task<IEnumerable<Subcategoria>> GetAllSubcategoriasAsync()
        {
            try
            {
                return await _dalSubcategoria.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al obtener todos las subcategorias: {ex.Message}");
                return null;
            }
        }

    }
}
