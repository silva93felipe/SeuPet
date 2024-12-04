using SeuPet.Dto;

namespace SeuPet.Services
{
    public interface IPetService
    {
        Task<List<PetResponse>> GetAllAsync(int limit, int offset);
        Task<PetResponse> GetByIdAsync(int id);
        Task<PetResponse> CreateAsync(PetRequest request);
        Task UpdateAsync(int id, PetRequest request);
        Task DeleteAsync(int id);
        Task Upload(int id, IFormFile foto);
        Task<string> GetImagemById(int id);
        Task AdotarAsync(int id, int adotanteId);
    }
}