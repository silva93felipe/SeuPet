using SeuPet.Api.Dto.Adotante;

namespace SeuPet.Domain.Contracts
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