using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeuPet.Api.Dto;
using SeuPet.Api.Dto.Pet;
using SeuPet.Domain.Contracts;

namespace SeuPet.Api.Controllers
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
            return Ok(new ResponseHttp(HttpStatusCode.OK, true, result));
        }
        
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var pet = await _petService.GetByIdAsync(id);
            return Ok(new ResponseHttp(HttpStatusCode.OK, true, pet));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]PetRequest request)
        {
            var newPet = await _petService.CreateAsync(request);
            return Created(string.Empty, new ResponseHttp(HttpStatusCode.OK, true, newPet));
        }

        // [Authorize]
        // [HttpGet("{id}/imagem")]
        // public async Task<IActionResult> GetImagemById(int id)
        // {
        //     var base64Image = await _petService.GetImagemById(id);
        //     return Ok(new ResponseHttp(System.Net.HttpStatusCode.OK, true, base64Image));
        // }
        //
        // [Authorize]
        // [HttpPost("{id}/upload")]
        // public async Task<IActionResult> Upload(int id, IFormFile foto)
        // {
        //     await _petService.Upload(id, foto);
        //     return NoContent();
        // }
        
        [Authorize]
        [HttpPost("{id}/adotar")]
        public async Task<IActionResult> AdotarAsync(int id, [FromBody]int adotanteId)
        {
            await _petService.AdotarAsync(id, adotanteId);
            return NoContent();
        }
        
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm]PetRequest request)
        {
            await _petService.UpdateAsync(id, request);
            return NoContent();
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _petService.DeleteAsync(id);
            return NoContent();            
        }
    }
}