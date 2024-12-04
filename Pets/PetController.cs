using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeuPet.Dto;
using SeuPet.Enums;
using SeuPet.Services;

namespace SeuPet.Controllers
{
    [ApiController]
    [Route("api/v1/pets")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        public PetsController(IPetService petService)
        {
            _petService = petService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int limit = 5, int offset = 0)
        {
            var result = await _petService.GetAllAsync(limit, offset);
            return Ok(new ResponseHtttp(System.Net.HttpStatusCode.OK, true, result));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var pet = await _petService.GetByIdAsync(id);
            return Ok(new ResponseHtttp(System.Net.HttpStatusCode.OK, true, pet));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]PetRequest request)
        {
            var newPet = await _petService.CreateAsync(request);
            return Created(string.Empty, new ResponseHtttp(System.Net.HttpStatusCode.OK, true, newPet));
        }

        [HttpGet("{id}/imagem")]
        public async Task<IActionResult> GetImagemById(int id)
        {
            var base64Image = await _petService.GetImagemById(id);
            return Ok(new ResponseHtttp(System.Net.HttpStatusCode.OK, true, base64Image));
        }

        [HttpPost("{id}/upload")]
        public async Task<IActionResult> Upload(int id, IFormFile foto)
        {
            await _petService.Upload(id, foto);
            return NoContent();
        }

        [HttpPost("{id}/adotar")]
        public async Task<IActionResult> AdotarAsync(int id, [FromBody]int adotanteId)
        {
            await _petService.AdotarAsync(id, adotanteId);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm]PetRequest request)
        {
            await _petService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _petService.DeleteAsync(id);
            return NoContent();            
        }
    }
}