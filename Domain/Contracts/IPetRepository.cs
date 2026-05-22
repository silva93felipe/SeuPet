using SeuPet.Domain.Entity;

namespace SeuPet.Domain.Contracts.Repository
{
    public interface IPetRepository
    {
        Task<List<Pet>> GetAllAsync(int limit, int offset);
        Task<Pet?> GetByIdAsync(int id);
        Task<Pet> CreateAsync(Pet request);
        Task Update(Pet request);
    }
}