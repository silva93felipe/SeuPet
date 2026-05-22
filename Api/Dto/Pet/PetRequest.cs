
using SeuPet.Domain.Enums;

namespace SeuPet.Api.Dto.Pet
{
    public record PetRequest(string Nome, DateTime DataNascimento, SexoEnum Sexo, TipoPetEnum Tipo, IFormFile? Foto, TipoSanguineoEnum TipoSanguineo);    
}