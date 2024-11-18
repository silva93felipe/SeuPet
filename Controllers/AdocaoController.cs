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
        public IActionResult GetAll()
        {
            return Ok(_context.Adocao.AsNoTracking().Select(e => e.ToAdocaoResponse()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var adocao = _context.Adocao.AsNoTracking().FirstOrDefault(e => e.Id == id && e.Ativo);
            if(adocao == null)
                return NotFound();
            return Ok(adocao.ToAdocaoResponse());
        }

        [HttpPost]
        public IActionResult Create(AdocaoRequest request)
        {
            _context.Adocao.Add(request.ToAdocao());
            _context.SaveChangesAsync();    
            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, AdocaoRequest request)
        { 
            var adocao = _context.Adocao.FirstOrDefault(e => e.Id == id && e.Ativo);
            if( adocao == null )
                return NotFound();
            
            _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var adocao = _context.Adocao.FirstOrDefault(e => e.Id == id && e.Ativo);
            if( adocao == null )
                return NotFound();
            adocao.Inativar();
            _context.SaveChangesAsync();
            return NoContent();            
        }
    }
}