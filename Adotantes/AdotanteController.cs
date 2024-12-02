using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly string KEY_CACHE_PETS = "PETS";

        public AdotantesController(SeuPetContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int limit = 5, int offset = 0)
        {
            var petsCache = _cache.GetAsync(KEY_CACHE_PETS);
            if(petsCache == null){
                
            }
            var result = await _context.Adotante
                            .AsNoTracking()
                            .Where(e => e.Ativo)
                            .Take(limit)
                            .Skip(offset)
                            .OrderByDescending(e => e.Id)
                            .Select(e => e.ToAdotanteResponse())
                            .ToListAsync();
            return Ok(new ResponseHtttp(System.Net.HttpStatusCode.OK, true, result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var adotante = await _context.Adotante
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(e => e.Id == id && e.Ativo);
            if(adotante == null)
                return NotFound(new ResponseHtttp(System.Net.HttpStatusCode.NotFound, false, new List<string>(){ "Adotante não encontrado" }));
            return Ok(new ResponseHtttp(System.Net.HttpStatusCode.OK, true, adotante.ToAdotanteResponse()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AdotanteRequest request)
        {
            Adotante newAdotante = new Adotante(request.Nome, request.Email, request.DataNascimento, request.Sexo);
            await _context.Adotante.AddAsync(newAdotante);
            await _context.SaveChangesAsync();    
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
            return NoContent();            
        }
    }
}