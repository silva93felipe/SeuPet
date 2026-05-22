using System.Net;
using Microsoft.AspNetCore.Mvc;
using SeuPet.Api.Dto;
using SeuPet.Api.Dto.Usuario;
using SeuPet.Domain.Contracts;

namespace SeuPet.Api.Controllers
{
    [ApiController]
    [Route("api/v1/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(UsuarioRequest request)
        {
            await _usuarioService.Create(request);
            return Created(string.Empty, new ResponseHttp( HttpStatusCode.Created, true ));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest request)
        {
            var token = await _usuarioService.Login(request);
            return Ok(new ResponseHttp( HttpStatusCode.OK, true, token ));
        }
    }
}