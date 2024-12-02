using Microsoft.OpenApi.Extensions;
using SeuPet.Dto;
using SeuPet.Models;

namespace SeuPet.Mapping
{
    public static class PetMapping{
        public static Pet ToPet(this PetRequest pet){
            return new Pet(
                pet.Nome,
                pet.Sexo,
                pet.DataNascimento,
                pet.TipoSanguineo,
                pet.Tipo
            );
        }

        public static PetResponse ToPetResponse(this Pet pet){
            return new PetResponse(
                pet.Id,
                pet.Nome,
                pet.DataNascimento.ToString("dd/MM/yyyy"),
                pet.Sexo.GetDisplayName(),
                pet.Tipo.GetDisplayName(),
                pet.Foto
            );
        }
    }
}