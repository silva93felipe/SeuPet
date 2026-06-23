using Microsoft.OpenApi.Extensions;
using SeuPet.Api.Dto.Adotante;
using SeuPet.Domain.Entity;

namespace SeuPet.Api.Mapping
{
    public static class AdotanteMapping{

        public static AdotanteResponse ToAdotanteResponse(this Adotante adotante){
            return new AdotanteResponse(adotante.Id, adotante.Nome, adotante.DataNascimento.ToString("dd/MM/yyyy"), adotante.Sexo.GetDisplayName());
        }

        public static Adotante ToAdotante(this AdotanteRequest adotante){
            return new Adotante(adotante.Nome,  adotante.DataNascimento, adotante.Sexo, adotante.Telefone);
        }
    }
}