using Microsoft.AspNetCore.Mvc;
using SeuPet.Api.Dto.Empresa;

namespace SeuPet.Api.Controllers;

[ApiController]
[Route("api/v1/empresa")]
public class EmpresaController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(EmpresaRequest request)
    {
        return NoContent();
    }
}