using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrowdSisters.DAL;
using CrowdSisters.Models;

namespace CrowdSisters.Services
{
    public class ServiceProyecto
    {
        private readonly DALProyecto _dalProyecto;

        public ServiceProyecto(DALProyecto dalProyecto)
        {
            _dalProyecto = dalProyecto;
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

        // Obtener Proyecto por ID
        public async Task<Proyecto> GetByIdAsync(int id)
        {
            try
            {
                return await _dalProyecto.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al obtener el proyecto por ID: {ex.Message}");
                return null;
            }
        }

        // Actualizar Proyecto
        public async Task<bool> UpdateAsync(Proyecto proyecto)
        {
            try
            {
                return await _dalProyecto.UpdateAsync(proyecto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al actualizar el proyecto: {ex.Message}");
                return false;
            }
        }

        // Eliminar Proyecto
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                return await _dalProyecto.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al eliminar el proyecto: {ex.Message}");
                return false;
            }
        }

        // Obtener todos los Proyectos
        public async Task<IEnumerable<Proyecto>> GetAllProyectosAsync()
        {
            try
            {
                return await _dalProyecto.GetAllAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en Service al obtener todos los proyectos: {ex.Message}");
                return null;
            }
        }
    }
}
