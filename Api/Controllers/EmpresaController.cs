using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeuPet.Api.Dto;
using SeuPet.Api.Dto.Empresa;
using SeuPet.Domain.Services;

namespace SeuPet.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/empresa")]
public class EmpresaController : ControllerBase
{
    private readonly IEmpresaService _empresaService ;

    public EmpresaController(IEmpresaService empresaService)
    {
        _empresaService = empresaService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmpresaRequest request)
    {
       var empresa = await _empresaService.Create(request);
       return Created(string.Empty, new ResponseHttp(HttpStatusCode.OK, true, empresa));
    }
    
    [HttpPost]
    public async Task<IActionResult> VerificarSeEmpresaPodeSerCriada(ValidarEmpresaRequest request)
    {
        
        return NoContent();
    }
}