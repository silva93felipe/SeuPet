using SeuPet.Models;

namespace SeuPet.Repository
{
    public interface IAdotanteRepository
    {
        Task<List<Adotante>> GetAllAsync(int limit, int offset);
        Task<Adotante> GetByIdAsync(int id);
        Task<Adotante> CreateAsync(Adotante request);
        Task Save();
    }
}