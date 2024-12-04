using Microsoft.AspNetCore.Mvc;
using SeuPet.Dto;
using SeuPet.Services;

namespace SeuPet.Controllers
{
    [ApiController]
    [Route("api/v1/adotantes")]
    public class AdotantesController : ControllerBase
    {
        private readonly IAdotanteService _adotanteService;
        public AdotantesController(IAdotanteService adotanteService)
        {
            _adotanteService = adotanteService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int limit = 5, int offset = 0)
        {
            var adotantes = await _adotanteService.GetAllAsync(limit, offset);        
            return Ok(new ResponseHtttp(System.Net.HttpStatusCode.OK, true, adotantes));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var adotante = await _adotanteService.GetByIdAsync(id);
            return Ok(new ResponseHtttp(System.Net.HttpStatusCode.OK, true, adotante));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AdotanteRequest request)
        {
            var newAdotante = await _adotanteService.CreateAsync(request);
            return Created(string.Empty, new ResponseHtttp(System.Net.HttpStatusCode.OK, true, newAdotante));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, AdotanteRequest request)
        { 
            await _adotanteService.UpdateAsync(id, request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _adotanteService.DeleteAsync(id);
            return NoContent();            
        }
    }
}