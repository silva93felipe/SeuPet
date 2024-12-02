using SeuPet.Enums;

namespace SeuPet.Dto
{
    public record PetRequest(string Nome, DateTime DataNascimento, SexoEnum Sexo, TipoPetEnum Tipo, IFormFile? Foto, TipoSanguineoEnum TipoSanguineo);    
}