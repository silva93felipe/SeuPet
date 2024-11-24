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
    public class AdotanteController : ControllerBase
    {
        private readonly SeuPetContext _context;
        public AdotanteController(SeuPetContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _context.Adotante
                            .AsNoTracking()
                            .Where(e => e.Ativo)
                            .Select(e => e.ToAdotanteResponse())
                            .ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var adotante = await _context.Adotante
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(e => e.Id == id && e.Ativo);
            if(adotante == null)
                return NotFound();
            return Ok(adotante.ToAdotanteResponse());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AdotanteRequest request)
        {
            await _context.Adotante.AddAsync(request.ToAdotante());
            await _context.SaveChangesAsync();    
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, AdotanteRequest request)
        { 
            var adotante = await _context.Adotante.FirstOrDefaultAsync(e => e.Id == id && e.Ativo);
            if( adotante == null )
                return NotFound();
            var updateAdotante = request.ToAdotante();
            adotante.Update(updateAdotante.Nome, updateAdotante.Email, updateAdotante.DataNascimento, updateAdotante.Sexo);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var adotante = await _context.Adotante.FirstOrDefaultAsync(e => e.Id == id && e.Ativo);
            if( adotante == null )
                return NotFound();
            adotante.Inativar();
            await _context.SaveChangesAsync();
            return NoContent()            
        }
    }
}