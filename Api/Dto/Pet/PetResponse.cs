namespace SeuPet.Api.Dto.Pet
{
    public record PetResponse( int Id, string Nome, string DataNascimento, string Sexo, string Tipo, string Foto);
}