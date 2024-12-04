using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using SeuPet.Dto;
using SeuPet.Enums;
using SeuPet.Mapping;
using SeuPet.Models;

namespace SeuPet.Controllers
{
    [ApiController]
    [Route("api/v1/pets")]
    public class PetsController : ControllerBase
    {
        private readonly string _pathServidor;
        private readonly string _pathDiretorioPet;
        private readonly SeuPetContext _context;
        private readonly string KEY_CACHE_PET = "PET";
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions distributedCacheEntryOptions;
        public PetsController(SeuPetContext context, IWebHostEnvironment env, IDistributedCache cache)
        {
            _context = context;
            _pathServidor = env.ContentRootPath;
            _pathDiretorioPet = _pathServidor + "/Imagens/pets/";
            _cache = cache;
            distributedCacheEntryOptions = new DistributedCacheEntryOptions(){
                SlidingExpiration = TimeSpan.FromSeconds(60),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
            };
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int limit = 5, int offset = 0)
        {
            var result = await _context.Pet
                            .Where(e => e.Status == StatusPetEnum.Espera && e.Ativo)
                            .Skip(offset)
                            .Take(limit)
                            .OrderByDescending(e => e.Id)
                            .Select( pet => pet.ToPetResponse())
                            .ToListAsync();
            
            return Ok(new ResponseHtttp(System.Net.HttpStatusCode.OK, true, result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var petCache = await _cache.GetStringAsync($"{KEY_CACHE_PET}_{id}");
            Pet pet;
            if( string.IsNullOrEmpty(petCache) ){
                pet = await _context.Pet.FirstOrDefaultAsync(p => p.Id == id && p.Status == StatusPetEnum.Espera && p.Ativo);
                if(pet == null)
                    return NotFound(new ResponseHtttp(System.Net.HttpStatusCode.NotFound, false, new List<string>(){"Pet não encontrado"}));
                
                await _cache.SetStringAsync($"{KEY_CACHE_PET}_{id}", JsonConvert.SerializeObject(pet), distributedCacheEntryOptions);
                return Ok(new ResponseHtttp(System.Net.HttpStatusCode.OK, true, pet.ToPetResponse()));
            }
            pet = JsonConvert.DeserializeObject<Pet>(petCache);
            return Ok(new ResponseHtttp(System.Net.HttpStatusCode.OK, true, pet.ToPetResponse()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]PetRequest request)
        {
            var newPet = new Pet(request.Nome, request.Sexo, request.DataNascimento, request.TipoSanguineo, request.Tipo);
            await _context.Pet.AddAsync(newPet);
            await _context.SaveChangesAsync();
            if(request.Foto?.Length > 0){
                newPet.UpdateImagem(await CreateDirectoryImagemPet(request.Foto, newPet.Id));
                await _context.SaveChangesAsync();
            }
            await _cache.SetStringAsync($"{KEY_CACHE_PET}_{newPet.Id}", JsonConvert.SerializeObject(newPet), distributedCacheEntryOptions);
            return Created(string.Empty, new ResponseHtttp(System.Net.HttpStatusCode.OK, true, newPet.ToPetResponse()));
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

        [HttpGet("{id}/imagem")]
        public async Task<IActionResult> GetImagemById(int id)
        {
            var pet = await _context.Pet.FindAsync(id);
            if(pet == null)
                return BadRequest(new ResponseHtttp(System.Net.HttpStatusCode.BadRequest, false, new List<string>(){"Pet não encontrado"}));
            if(pet.Foto == null)    
                return NotFound(new ResponseHtttp(System.Net.HttpStatusCode.NotFound, false, new List<string>(){"Pet não tem imagem"}));  
            var filePath = Path.Combine(_pathDiretorioPet, pet.Foto);
            if (!System.IO.File.Exists(filePath))
                return NotFound(new ResponseHtttp(System.Net.HttpStatusCode.NotFound, false, new List<string>(){"Pet não tem imagem"})); 
            var imageBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            var base64Image = Convert.ToBase64String(imageBytes);
            return Ok(new ResponseHtttp(System.Net.HttpStatusCode.OK, true, base64Image));
        }

        [HttpGet("{id}/upload")]
        public async Task<IActionResult> Upload(int id, IFormFile foto)
        {
            var pet = await _context.Pet.FindAsync(id);
            if(pet == null)
                return NotFound(new ResponseHtttp(System.Net.HttpStatusCode.NotFound, false, new List<string>(){"Pet não encontrado"}));
          
            string nameFile = await CreateDirectoryImagemPet(foto, pet.Id);
            pet.UpdateImagem(nameFile);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("{id}/adotar")]
        public async Task<IActionResult> AdotarAsync(int id, [FromBody]int adotanteId)
        {
            Pet pet = await _context.Pet.FirstOrDefaultAsync(p => p.Id == id && p.Status == StatusPetEnum.Espera && p.Ativo);
            if(pet == null)
                return NotFound(new ResponseHtttp(System.Net.HttpStatusCode.NotFound, false, new List<string>(){"Pet não encontrado"}));
            Adotante adotante = await _context.Adotante.FirstOrDefaultAsync(p => p.Id == adotanteId && p.Ativo);
            if(adotante == null)
                return NotFound(new ResponseHtttp(System.Net.HttpStatusCode.NotFound, false, new List<string>(){"Adotante não encontrado"}));
            pet.Adotar(adotante.Id);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm]PetRequest request)
        {
            var petDb = await _context.Pet.FirstOrDefaultAsync(p => p.Id == id && p.Status == StatusPetEnum.Espera && p.Ativo);
            if(petDb == null)
                return NotFound(new ResponseHtttp(System.Net.HttpStatusCode.NotFound, false, new List<string>(){"Pet não encontrado"}));
            petDb.Update(request.Nome, request.Sexo, request.DataNascimento, request.TipoSanguineo, request.Tipo);
            if(request.Foto?.Length > 0){
                petDb.UpdateImagem(await CreateDirectoryImagemPet(request.Foto, id));
            }
            await _context.SaveChangesAsync();
            await _cache.RemoveAsync($"{KEY_CACHE_PET}_{id}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var petNoBanco = await _context.Pet.FirstOrDefaultAsync(p => p.Id == id && p.Status == StatusPetEnum.Espera && p.Ativo);
            if(petNoBanco == null)
                return NotFound(new ResponseHtttp(System.Net.HttpStatusCode.NotFound, false, new List<string>(){"Pet não encontrado"}));
            petNoBanco.Inativar();
            await _context.SaveChangesAsync();
            await _cache.RemoveAsync($"{KEY_CACHE_PET}_{id}");
            return NoContent();            
        }
    }
}