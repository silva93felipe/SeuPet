using SeuPet.Dto;

namespace SeuPet.Services
{
    public interface IAdotanteService
    {
        Task<List<AdotanteResponse>> GetAllAsync(int limit, int offset);
        Task<AdotanteResponse> GetByIdAsync(int id);
        Task<AdotanteResponse> CreateAsync(AdotanteRequest request);
        Task UpdateAsync(int id, AdotanteRequest request);
        Task DeleteAsync(int id);
    }
}