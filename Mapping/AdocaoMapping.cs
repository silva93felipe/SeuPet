using Microsoft.OpenApi.Extensions;
using SeuPet.Dto;
using SeuPet.Models;

namespace SeuPet.Mapping
{
    public static class AdocaoMapping{

        public static AdocaoResponse ToAdocaoResponse(this Adocao adocao){
            return new AdocaoResponse(adocao.Pet.Nome, adocao.Adotante.Nome,adocao.PetId, adocao.AdotanteId, adocao.CreateAt.ToString("dd/MM/yyyy"));
        }

        public static Adocao ToAdocao(this AdocaoRequest request){
            return new Adocao(request.PetId, request.AdotanteId);
        }
    }
}