using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeuPet.Api.Dto;

namespace SeuPet.Api.Controllers;

public class DashboardController : ControllerBase
{
    [Authorize]
    [ApiController]
    [Route("api/v1/dashboard")]
    public class AdotantesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Adocoes([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            return Ok(new ResponseHttp(HttpStatusCode.OK, true));
        }
    }
}