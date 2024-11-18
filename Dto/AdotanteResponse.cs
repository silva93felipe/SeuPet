using SeuPet.Enums;

namespace SeuPet.Dto
{
    public record AdotanteResponse(int Id, string Nome, string Email, string DataNascimento, string SexoEnum);
}