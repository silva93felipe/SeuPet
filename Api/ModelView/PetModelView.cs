using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using pet_api.Enums;

namespace Api.ModelView
{
    public class PetModelView
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public DateTime DataNascimento { get; set; }
       /*  public SexoEnum Sexo { get; set; }
        public TipoSanguineoEnum TipoSanguineo { get; set; }
        public StatusPetEnum Status { get; set; }
        public TipoPetEnum Tipo { get; set; }
        public CondicaoPetEnum Condicao { get; set; } */
        public string Foto {get; set;}
    }
}