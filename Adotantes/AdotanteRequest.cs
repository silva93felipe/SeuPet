using SeuPet.Enums;

namespace SeuPet.Dto
{
    public record AdotanteRequest(string Nome, string Email, DateTime DataNascimento, SexoEnum Sexo);
}