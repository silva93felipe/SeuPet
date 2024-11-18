using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult GetAll()
        {
            return Ok(_context.Pet.Where(e => e.Status == StatusPetEnum.Espera && e.Ativo)
                          .Select( pet => pet.ToPetResponse()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pet = _context.Pet.FirstOrDefault(p => p.Id == id && p.Status == StatusPetEnum.Espera);
            if(pet == null)
                return NotFound();
            return Ok(pet.ToPetResponse());
        }

        [HttpPost]
        public IActionResult Create(PetRequest pet)
        {
            _context.Pet.Add(new Pet(pet.Nome, pet.Sexo, pet.DataNascimento, pet.TipoSanguineo, pet.Tipo));
            _context.SaveChangesAsync();
            return Created();
        }

        /* [HttpPost("{id}/adotar")]
        public IActionResult Adotar(int id)
        {
            var petDb = _context.Pet.FirstOrDefault(p => p.Id == id && p.Status == StatusPetEnum.Espera);
            if(petDb == null)
                return NotFound();
            petDb.Adotar();

            return Ok();
        } */

        [HttpPut("{id}")]
        public IActionResult Update(int id, PetRequest pet)
        {
            var petDb = _context.Pet.FirstOrDefault(p => p.Id == id);
            if(petDb == null)
                return NotFound();
            petDb.Update(pet.Nome, pet.Sexo, pet.DataNascimento, pet.TipoSanguineo, pet.Tipo);
            _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var petNoBanco = _context.Pet.FirstOrDefault(p => p.Id == id);
            if(petNoBanco == null)
                return NotFound();
            petNoBanco.Inativar();
            _context.SaveChangesAsync();
            return NoContent();            
        }
    }
}