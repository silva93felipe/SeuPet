using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SeuPet.Dto;
using SeuPet.Enums;
using SeuPet.Models;

namespace SeuPet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private static List<Pet> pets = new()
        {
            new Pet("chano", SexoEnum.Masculino, DateTime.Now.AddYears(-5), TipoSanguineoEnum.A, TipoPetEnum.Gato),
            new Pet("meow", SexoEnum.Masculino, DateTime.Now.AddYears(-2), TipoSanguineoEnum.B, TipoPetEnum.Gato),
            new Pet("princesa", SexoEnum.Feminino, DateTime.Now.AddYears(-3), TipoSanguineoEnum.AB, TipoPetEnum.Cachorro),
            new Pet("riana", SexoEnum.Feminino, DateTime.Now.AddYears(-1), TipoSanguineoEnum.O, TipoPetEnum.Cachorro),
        };
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(pets.Where(e => e.Status == StatusPetEnum.Espera));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pet = pets.Find(p => p.Id == id);
            if(pet == null)
                return NotFound();
            return Ok(pet);
        }

        [HttpPost]
        public IActionResult Create(PetRequest pet)
        {
            pets.Add(new Pet(pet.Nome, pet.Sexo, pet.DataNascimento, pet.TipoSanguineo, pet.Tipo));
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PetRequest pet)
        {
            var petDb = pets.Find(p => p.Id == id);
            if(petDb == null)
                return NotFound();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var petNoBanco = pets.Find(p => p.Id == id);
            if(petNoBanco == null)
                return NotFound();

            pets.Remove(petNoBanco);
            return NoContent();            
        }
    }
}