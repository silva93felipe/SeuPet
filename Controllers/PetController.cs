using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeuPet.Dto;
using SeuPet.Enums;
using SeuPet.Mapping;
using SeuPet.Models;

namespace SeuPet.Controllers
{
    [ApiController]
    [Route("api/pets")]
    public class PetController : ControllerBase
    {
        private readonly string _pathServidor;
        private readonly SeuPetContext _context;
        public PetController(SeuPetContext context, IWebHostEnvironment env)
        {
            _context = context;
            _pathServidor = env.ContentRootPath;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _context.Pet
                            .Where(e => e.Status == StatusPetEnum.Espera && e.Ativo)
                            .Select( pet => pet)
                            .ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var pet = await _context.Pet.FirstOrDefaultAsync(p => p.Id == id && p.Status == StatusPetEnum.Espera && p.Ativo);
            if(pet == null)
                return NotFound();
            return Ok(pet.ToPetResponse());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(PetRequest pet)
        {
            await _context.Pet.AddAsync(new Pet(pet.Nome, pet.Sexo, pet.DataNascimento, pet.TipoSanguineo, pet.Tipo, pet.Foto));
            await _context.SaveChangesAsync();
            return Created();
        }

        [HttpPost("{id}/upload")]
        public async Task<IActionResult> Upload(int id, IFormFile request)
        {
            string path = _pathServidor + "/Imagens/pets/";
            string extensaoImagem = request.FileName.Split(".")[^1];
            string nameFoto = id + "." + extensaoImagem; 
            if( !Directory.Exists(path)){
                Directory.CreateDirectory(path);
            }
            
            using (var stream = System.IO.File.Create(path + nameFoto)){
                await request.CopyToAsync(stream);
            }

            return NoContent();
        }

        [HttpPost("{petId}/adotar")]
        // TODO - MELHORAR URL 
        public async Task<IActionResult> AdotarAsync(int petId, [FromBody]int adotanteId)
        {
            Pet pet = await _context.Pet.FirstOrDefaultAsync(p => p.Id == petId && p.Status == StatusPetEnum.Espera && p.Ativo);
            if(pet == null)
               return NotFound();
            Adotante adotante = await _context.Adotante.FirstOrDefaultAsync(p => p.Id == adotanteId && p.Ativo);
            if(adotante == null)
                return NotFound();
            pet.Adotar(adotante.Id);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, PetRequest pet)
        {
            var petDb = await _context.Pet.FirstOrDefaultAsync(p => p.Id == id && p.Status == StatusPetEnum.Espera && p.Ativo);
            if(petDb == null)
                return NotFound();
            petDb.Update(pet.Nome, pet.Sexo, pet.DataNascimento, pet.TipoSanguineo, pet.Tipo, pet.Foto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var petNoBanco = await _context.Pet.FirstOrDefaultAsync(p => p.Id == id && p.Status == StatusPetEnum.Espera && p.Ativo);
            if(petNoBanco == null)
                return NotFound();
            petNoBanco.Inativar();
            await _context.SaveChangesAsync();
            return NoContent();            
        }
    }
}