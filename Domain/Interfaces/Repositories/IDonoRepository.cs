using Domain.Models;

namespace Domain.Interfaces.Repositories
{
    public interface IDonoRepository : IBaseRepository<Dono>
    {
        Task<Dono> GetByCpf(string cpf);
        
    }
}