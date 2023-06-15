using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Api.ModelView;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        public List<PetModelView> listaPet = new List<PetModelView>();
        public PetController(){}

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(listaPet);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var pet = listaPet.Where(p => p.Id == id).First();

            if(pet == null)
                return NotFound();

            return Ok(pet);
        }

        [HttpPost]
        public IActionResult Create(PetModelView pet)
        {

            listaPet.Add(pet);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PetModelView pet)
        {
            var petDb = listaPet.Where(p => p.Id == id).First();

            if(petDb == null)
                return NotFound();

            return NoContent();

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            bool deleted = listaPet.Remove(listaPet.Where(p => p.Id == id).First());

            if(deleted)
                return NoContent();

            
            return NotFound();
        }

    }
}