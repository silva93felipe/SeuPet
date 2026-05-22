using SeuPet.Api.Dto.Adotante;
using SeuPet.Domain.Contracts;

namespace SeuPet.Domain.Contracts.Services
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