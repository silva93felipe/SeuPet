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
    public class AdocaoController : ControllerBase
    {
        private readonly SeuPetContext _context;
        public AdocaoController(SeuPetContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _context.Adocao
                                       .AsNoTracking()
                                       .Select(e => e.ToAdocaoResponse())
                                       .ToListAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            // TODO
            var adocao = await _context.Adocao.AsNoTracking().FirstOrDefaultAsync();
            if(adocao == null)
                return NotFound();
            return Ok(adocao.ToAdocaoResponse());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AdocaoRequest request)
        {
            await _context.Adocao.AddAsync(request.ToAdocao());
            await _context.SaveChangesAsync();    
            return Created();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, AdocaoRequest request)
        { 
             // TODO
            var adocao = await _context.Adocao.FirstOrDefaultAsync();
            if( adocao == null )
                return NotFound();
            
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            // TODO
            var adocao = await _context.Adocao.FirstOrDefaultAsync();
            if( adocao == null )
                return NotFound();
            adocao.Inativar();
            await _context.SaveChangesAsync();
            return NoContent();            
        }
    }
}