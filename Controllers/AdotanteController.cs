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
        public IActionResult GetAll()
        {
            return Ok(_context.Adotante.AsNoTracking().Where(e => e.Ativo).Select(e => e.ToAdotanteResponse()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var adotante = _context.Adotante.AsNoTracking().FirstOrDefault(e => e.Id == id && e.Ativo);
            if(adotante == null)
                return NotFound();
            return Ok(adotante.ToAdotanteResponse());
        }

        [HttpPost]
        public IActionResult Create(AdotanteRequest request)
        {
            _context.Adotante.Add(request.ToAdotante());
            _context.SaveChangesAsync();    
            return Created();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, AdotanteRequest request)
        { 
            var adotante = _context.Adotante.FirstOrDefault(e => e.Id == id && e.Ativo);
            if( adotante == null )
                return NotFound();
            
            var updateAdotante = request.ToAdotante();
            adotante = updateAdotante;
            //_context.Update(adotante);
            _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var adotante = _context.Adotante.FirstOrDefault(e => e.Id == id && e.Ativo);
            if( adotante == null )
                return NotFound();
            adotante.Inativar();
            _context.SaveChangesAsync();
            return NoContent();            
        }
    }
}