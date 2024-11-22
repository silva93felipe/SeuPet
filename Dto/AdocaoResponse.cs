namespace SeuPet.Dto
{
    public record AdocaoResponse(string NomePet, string NomeAdotante, int PetId, int AdotanteId, string DataAdocao);
}