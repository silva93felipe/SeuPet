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
                pet.Tipo,
                pet.Foto

            );
        }

        public static PetResponse ToPetResponse(this Pet pet){
            return new PetResponse(){
                Id = pet.Id,
                Nome = pet.Nome,
                DataNascimento = pet.DataNascimento.ToString("dd/MM/yyyy"),
                Tipo = pet.Tipo.GetDisplayName(),
                Sexo = pet.Sexo.GetDisplayName(),
            };
        }
    }
}