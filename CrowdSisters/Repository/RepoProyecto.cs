using System;
using System.Threading.Tasks;
using CrowdSisters.DAL;
using CrowdSisters.Models;

namespace CrowdSisters.Repository
{
    public class RepoProyecto
    {
        private readonly DALProyecto _dalProyecto;

        public RepoProyecto(DALProyecto dalProyecto)
        {
            _dalProyecto = dalProyecto;
        }

        public async Task<bool> CreateProyectoAsync(Proyecto proyecto)
        {
            try
            {
                return await _dalProyecto.CreateAsync(proyecto);
            }
            catch (Exception ex)
            {
                // Manejo de errores (log, rethrow, etc.)
                Console.WriteLine($"Error al crear proyecto: {ex.Message}");
                return false;
            }
        }

        public async Task<Proyecto> GetProyectoByIdAsync(int id)
        {
            try
            {
                return await _dalProyecto.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al obtener proyecto: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> UpdateProyectoAsync(Proyecto proyecto)
        {
            try
            {
                return await _dalProyecto.UpdateAsync(proyecto);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al actualizar proyecto: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteProyectoAsync(int id)
        {
            try
            {
                return await _dalProyecto.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Console.WriteLine($"Error al eliminar proyecto: {ex.Message}");
                return false;
            }
        }
    }
}
