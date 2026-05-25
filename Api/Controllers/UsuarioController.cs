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
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,   // Bloqueia acesso via JavaScript (Protege contra XSS)
                Secure = true,     // Exige HTTPS
                SameSite = SameSiteMode.Strict, // Protege contra CSRF
                Expires = DateTime.UtcNow.AddMinutes(10) // Mesmo tempo de expiração do JWT
            };

            // Adiciona o cookie na resposta HTTP com o nome "X-Access-Token"
            Response.Cookies.Append("X-Access-Token", token.Item1, cookieOptions);
            Response.Cookies.Append("X-Refresh-Token", token.Item2, cookieOptions);
            return Ok(new ResponseHttp( HttpStatusCode.OK, true, string.Empty ));
        }
        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            // 1. Recupera o Refresh Token dos cookies
            if (!Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshTokenValue))
                return Unauthorized("Refresh Token ausente.");
            
            var response = await _usuarioService.RefreshToken(refreshTokenValue);
            if(string.IsNullOrEmpty(response.Item1)) return Unauthorized("Refresh Token inválido.");
            
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,   // Bloqueia acesso via JavaScript (Protege contra XSS)
                Secure = true,     // Exige HTTPS
                SameSite = SameSiteMode.Strict, // Protege contra CSRF
                Expires = DateTime.UtcNow.AddMinutes(10) // Mesmo tempo de expiração do JWT
            };

            // Adiciona o cookie na resposta HTTP com o nome "X-Access-Token"
            Response.Cookies.Append("X-Access-Token", response.Item1, cookieOptions);
            Response.Cookies.Append("X-Refresh-Token", response.Item2, cookieOptions);
            return Ok(new ResponseHttp( HttpStatusCode.OK, true, string.Empty ));
        }
    }
}