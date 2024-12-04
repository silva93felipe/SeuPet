using Newtonsoft.Json;
using SeuPet.Dto;
using SeuPet.Mapping;
using SeuPet.Models;
using SeuPet.Repository;

namespace SeuPet.Services
{
    public class PetService : IPetService
    {
        private readonly string _pathServidor;
        private readonly string _pathDiretorioPet;
        private readonly IPetRepository _petRepository;
        private readonly IAdotanteService _adotanteService;
        private readonly string KEY_CACHE_PET = "PET";
        private readonly ICacheService _cache;
        public PetService(IWebHostEnvironment env, ICacheService cache, IPetRepository petRepository, IAdotanteService adotanteService)
        {
            _pathServidor = env.ContentRootPath;
            _pathDiretorioPet = _pathServidor + "/Imagens/pets/";
            _cache = cache;
            _petRepository = petRepository;
            _adotanteService = adotanteService;
        }
        public async Task<List<PetResponse>> GetAllAsync(int limit = 5, int offset = 0){
            var pets = await _petRepository.GetAllAsync(limit, offset);
            return pets.Select(p => p.ToPetResponse()).ToList();
        }
        public async Task<PetResponse> GetByIdAsync(int id)
        {
            var petCache = await _cache.GetStringAsync($"{KEY_CACHE_PET}_{id}");
            Pet pet;
            if( string.IsNullOrEmpty(petCache) ){
                pet = await _petRepository.GetByIdAsync(id);
                if(pet == null){
                    throw new PetNotFoundException();
                }
                await _cache.SetStringAsync($"{KEY_CACHE_PET}_{id}", JsonConvert.SerializeObject(pet));
                return pet.ToPetResponse();
            }
            pet = JsonConvert.DeserializeObject<Pet>(petCache);
            return pet.ToPetResponse();
        }

        public async Task<PetResponse> CreateAsync(PetRequest request)
        {
            var newPet = new Pet(request.Nome, request.Sexo, request.DataNascimento, request.TipoSanguineo, request.Tipo);
            await _petRepository.CreateAsync(newPet);            
            if(request.Foto?.Length > 0){
                newPet.UpdateImagem(await CreateDirectoryImagemPet(request.Foto, newPet.Id));
                await _petRepository.Save();
            }
            await _cache.SetStringAsync($"{KEY_CACHE_PET}_{newPet.Id}", JsonConvert.SerializeObject(newPet));
            return newPet.ToPetResponse();
        }

        private async Task<string> CreateDirectoryImagemPet(IFormFile request, int petId){
            string extensaoImagem = request.FileName.Split(".")[^1];
            string nameFoto = petId + "." + extensaoImagem; 
            if( !Directory.Exists(_pathDiretorioPet)){
                Directory.CreateDirectory(_pathDiretorioPet);
            }
            string pathImagem = _pathDiretorioPet + nameFoto;
            using var stream = System.IO.File.Create(pathImagem);
            await request.CopyToAsync(stream);
            return nameFoto;
        }

        public async Task<string> GetImagemById(int id)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            if(pet == null)
                throw new PetNotFoundException();
            if(pet.Foto == null)    
               throw new ImagemNotFoundException();
            var filePath = Path.Combine(_pathDiretorioPet, pet.Foto);
            if (!System.IO.File.Exists(filePath))
                throw new ImagemNotFoundException();
            var imageBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var base64Image = Convert.ToBase64String(imageBytes);
            return base64Image;
        }

        public async Task Upload(int id, IFormFile foto)
        {
            var pet = await _petRepository.GetByIdAsync(id);
            if(pet == null)
                throw new PetNotFoundException();
            string nameFile = await CreateDirectoryImagemPet(foto, pet.Id);
            pet.UpdateImagem(nameFile);
            await _petRepository.Save();
        }

        public async Task AdotarAsync(int id, int adotanteId)
        {
            Pet pet = await _petRepository.GetByIdAsync(id);
            if(pet == null)
                throw new PetNotFoundException();
            AdotanteResponse adotante = await _adotanteService.GetByIdAsync(adotanteId);
            if(adotante == null)
                throw new AdotanteNotFoundException();
            pet.Adotar(adotante.Id);
            await _petRepository.Save();
        }

        public async Task UpdateAsync(int id, PetRequest request)
        {
            var petDb = await _petRepository.GetByIdAsync(id);
            if(petDb == null)
                throw new PetNotFoundException();
            petDb.Update(request.Nome, request.Sexo, request.DataNascimento, request.TipoSanguineo, request.Tipo);
            if(request.Foto?.Length > 0){
                petDb.UpdateImagem(await CreateDirectoryImagemPet(request.Foto, id));
            }
            await _petRepository.Save();
            await _cache.RemoveAsync($"{KEY_CACHE_PET}_{id}");
        }

        public async Task DeleteAsync(int id)
        {
            var petDb = await _petRepository.GetByIdAsync(id);
            if(petDb == null)
                throw new PetNotFoundException();
            petDb.Inativar();
            await _petRepository.Save();
            await _cache.RemoveAsync($"{KEY_CACHE_PET}_{id}");      
        }
    }
}