using CrowdSisters.Models;
using CrowdSisters.Repository;

namespace CrowdSisters.Services
{
    public class ServiceProyecto
    {
        private readonly RepoProyecto _repoProyecto;

        public ServiceProyecto(RepoProyecto repoProyecto)
        {
            _repoProyecto = repoProyecto;
        }

        public async Task<IEnumerable<Proyecto>> GetAllProyectosAsync()
        {
            return await _repoProyecto.GetAllProyectosAsync();
        }
    }
}
