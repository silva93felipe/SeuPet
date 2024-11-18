using Microsoft.OpenApi.Extensions;
using SeuPet.Dto;
using SeuPet.Models;

namespace SeuPet.Mapping
{
    public static class AdotanteMapping{

        public static AdotanteResponse ToAdotanteResponse(this Adotante adotante){
            return new AdotanteResponse(adotante.Id, adotante.Nome, adotante.Email, adotante.DataNascimento.ToString("dd/MM/yyyy"), adotante.Sexo.GetDisplayName());
        }

        public static Adotante ToAdotante(this AdotanteRequest adotante){
            return new Adotante(adotante.Nome, adotante.Email, adotante.DataNascimento, adotante.Sexo);
        }
    }
}