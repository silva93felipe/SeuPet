using SeuPet.Models;

namespace SeuPet.Repository
{
    public interface IPetRepository
    {
        Task<List<Pet>> GetAllAsync(int limit, int offset);
        Task<Pet> GetByIdAsync(int id);
        Task<Pet> CreateAsync(Pet request);
        Task Save();
    }
}