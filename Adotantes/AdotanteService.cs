using Newtonsoft.Json;
using SeuPet.Dto;
using SeuPet.Mapping;
using SeuPet.Models;
using SeuPet.Repository;

namespace SeuPet.Services
{
    public class AdotanteService : IAdotanteService
    {
        private readonly string KEY_CACHE_ADOTANTE = "ADOTANTE";
        private readonly ICacheService _cache;
        private readonly IAdotanteRepository _adotanteRepository;
        public AdotanteService(ICacheService cache, IAdotanteRepository adotanteRepository)
        {
            _cache = cache;
            _adotanteRepository = adotanteRepository;
        }
        public async Task<AdotanteResponse> CreateAsync(AdotanteRequest request)
        {
            Adotante newAdotante = new Adotante(request.Nome, request.Email, request.DataNascimento, request.Sexo);
            var adotante = await _adotanteRepository.CreateAsync(newAdotante);
            await _cache.SetStringAsync(KEY_CACHE_ADOTANTE, JsonConvert.SerializeObject(adotante));
            return adotante.ToAdotanteResponse();
        }

        public async Task DeleteAsync(int id)
        {
            var adotante = await _adotanteRepository.GetByIdAsync(id);
            if( adotante == null )
                throw new AdotanteNotFoundException();
            adotante.Inativar();
            await _adotanteRepository.Save();
            await _cache.RemoveAsync($"{KEY_CACHE_ADOTANTE}_{id}");
        }

        public async Task<List<AdotanteResponse>> GetAllAsync(int limit, int offset){
            var adotantes = await _adotanteRepository.GetAllAsync(limit, offset);
            return adotantes.Select(e => e.ToAdotanteResponse())
                            .ToList();
        }

        public async Task<AdotanteResponse> GetByIdAsync(int id)
        {
            var adotanteCache = await _cache.GetStringAsync($"{KEY_CACHE_ADOTANTE}_{id}");
            Adotante adotante;
            if( string.IsNullOrEmpty(adotanteCache) ){
                adotante = await _adotanteRepository.GetByIdAsync(id);
                if(adotante == null){
                    throw new AdotanteNotFoundException();
                }
                await _cache.SetStringAsync($"{KEY_CACHE_ADOTANTE}_{id}", JsonConvert.SerializeObject(adotante));
                return adotante.ToAdotanteResponse();
            }
            adotante = JsonConvert.DeserializeObject<Adotante>(adotanteCache);
            return adotante.ToAdotanteResponse();
        }

        public async Task UpdateAsync(int id, AdotanteRequest request)
        {
            var adotante = await _adotanteRepository.GetByIdAsync(id);
            if(adotante == null){
                throw new AdotanteNotFoundException();
            }
            adotante.Update(request.Nome, request.Email, request.DataNascimento, request.Sexo);
            await _adotanteRepository.Save();
            await _cache.RemoveAsync($"{KEY_CACHE_ADOTANTE}_{id}");
        }
    }
}