using SeuPet.Domain.Enums;

namespace SeuPet.Api.Dto.Adotante
{
    public record AdotanteRequest(string Nome, string Email, DateTime DataNascimento, string Telefone, SexoEnum Sexo);
}