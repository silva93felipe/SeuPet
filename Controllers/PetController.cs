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
            return Ok(pets.Where(e => e.Status == StatusPetEnum.Espera).Select( pet => pet.ToPetResponse()));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pet = pets.Find(p => p.Id == id && p.Status == StatusPetEnum.Espera);
            if(pet == null)
                return NotFound();
            return Ok(pet.ToPetResponse());
        }

        [HttpPost]
        public IActionResult Create(PetRequest pet)
        {
            pets.Add(new Pet(pet.Nome, pet.Sexo, pet.DataNascimento, pet.TipoSanguineo, pet.Tipo));
            return Ok();
        }

        [HttpPost]
        [Route("{id}/Adotar")]
        public IActionResult Adotar(int id)
        {
            var petDb = pets.Find(p => p.Id == id && p.Status == StatusPetEnum.Espera);
            if(petDb == null)
                return NotFound();
            petDb.Adotar();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PetRequest pet)
        {
            var petDb = pets.Find(p => p.Id == id);
            if(petDb == null)
                return NotFound();
            petDb.Update(pet.Nome, pet.Sexo, pet.DataNascimento, pet.TipoSanguineo, pet.Tipo);
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