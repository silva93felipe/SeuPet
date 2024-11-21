using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeuPet.Dto;
using SeuPet.Enums;
using SeuPet.Mapping;
using SeuPet.Models;

namespace SeuPet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly SeuPetContext _context;
        public PetController(SeuPetContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _context.Pet
                            .Where(e => e.Status == StatusPetEnum.Espera && e.Ativo)
                            .Select( pet => pet.ToPetResponse())
                            .ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var pet = await _context.Pet.FirstOrDefaultAsync(p => p.Id == id && p.Status == StatusPetEnum.Espera);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, PetRequest pet)
        {
            var petDb = await _context.Pet.FirstOrDefaultAsync(p => p.Id == id);
            if(petDb == null)
                return NotFound();
            petDb.Update(pet.Nome, pet.Sexo, pet.DataNascimento, pet.TipoSanguineo, pet.Tipo, pet.Foto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var petNoBanco = await _context.Pet.FirstOrDefaultAsync(p => p.Id == id);
            if(petNoBanco == null)
                return NotFound();
            petNoBanco.Inativar();
            await _context.SaveChangesAsync();
            return NoContent();            
        }
    }
}