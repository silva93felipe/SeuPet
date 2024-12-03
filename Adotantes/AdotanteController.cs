using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SeuPet.Dto;
using SeuPet.Mapping;
using SeuPet.Models;

namespace SeuPet.Controllers
{
    [ApiController]
    [Route("api/v1/adotantes")]
    public class AdotantesController : ControllerBase
    {
        private readonly SeuPetContext _context;
        private readonly IDistributedCache _cache;
        private readonly string KEY_CACHE_ADOTANTE = "ADOTANTE";
        private readonly DistributedCacheEntryOptions distributedCacheEntryOptions;
        public AdotantesController(SeuPetContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
            distributedCacheEntryOptions = new DistributedCacheEntryOptions(){
                SlidingExpiration = TimeSpan.FromSeconds(60),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
            };
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int limit = 5, int offset = 0)
        {
            var adotantes = await _context.Adotante
                                .AsNoTracking()
                                .Where(e => e.Ativo)
                                .OrderByDescending(e => e.Id)
                                .Take(limit)
                                .Skip(offset)
                                .Select(e => e.ToAdotanteResponse())
                                .ToListAsync();            
            return Ok(new ResponseHtttp(System.Net.HttpStatusCode.OK, true, adotantes));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var adotanteCache = await _cache.GetStringAsync($"{KEY_CACHE_ADOTANTE}_{id}");
            Adotante adotante;
            if( string.IsNullOrEmpty(adotanteCache) ){
                adotante = await _context.Adotante
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(e => e.Id == id && e.Ativo);
                if(adotante == null)
                    return NotFound(new ResponseHtttp(System.Net.HttpStatusCode.NotFound, false, new List<string>(){ "Adotante não encontrado" }));
                
                await _cache.SetStringAsync($"{KEY_CACHE_ADOTANTE}_{id}", JsonConvert.SerializeObject(adotante), distributedCacheEntryOptions);
                return Ok(new ResponseHtttp(System.Net.HttpStatusCode.OK, true, adotante.ToAdotanteResponse()));
            }
            adotante = JsonConvert.DeserializeObject<Adotante>(adotanteCache);
            return Ok(new ResponseHtttp(System.Net.HttpStatusCode.OK, true, adotante.ToAdotanteResponse()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AdotanteRequest request)
        {
            Adotante newAdotante = new Adotante(request.Nome, request.Email, request.DataNascimento, request.Sexo);
            await _context.Adotante.AddAsync(newAdotante);
            await _context.SaveChangesAsync();
            await _cache.SetStringAsync(KEY_CACHE_ADOTANTE, JsonConvert.SerializeObject(newAdotante), distributedCacheEntryOptions);    
            return Created(string.Empty, new ResponseHtttp(System.Net.HttpStatusCode.OK, true, newAdotante.ToAdotanteResponse()));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, AdotanteRequest request)
        { 
            var adotante = await _context.Adotante.FirstOrDefaultAsync(e => e.Id == id && e.Ativo);
            if( adotante == null )
                return NotFound(new ResponseHtttp(System.Net.HttpStatusCode.NotFound, false, new List<string>(){ "Adotante não encontrado" }));
            adotante.Update(request.Nome, request.Email, request.DataNascimento, request.Sexo);
            await _context.SaveChangesAsync();
            await _cache.RemoveAsync($"{KEY_CACHE_ADOTANTE}_{id}");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var adotante = await _context.Adotante.FirstOrDefaultAsync(e => e.Id == id && e.Ativo);
            if( adotante == null )
                return NotFound(new ResponseHtttp(System.Net.HttpStatusCode.NotFound, false, new List<string>(){ "Adotante não encontrado" }));
            adotante.Inativar();
            await _context.SaveChangesAsync();
            await _cache.RemoveAsync($"{KEY_CACHE_ADOTANTE}_{id}");
            return NoContent();            
        }
    }
}