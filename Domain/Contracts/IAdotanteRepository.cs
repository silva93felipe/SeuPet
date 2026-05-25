
using SeuPet.Domain.Entity;

namespace SeuPet.Domain.Contracts
{
    public interface IAdotanteRepository
    {
        Task<List<Adotante>> GetAllAsync(int limit, int offset);
        Task<Adotante?> GetByIdAsync(int id);
        Task<Adotante> CreateAsync(Adotante request);
        Task Update(Adotante request);
    }
}