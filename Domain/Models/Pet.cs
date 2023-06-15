using System;
using Domain.Enums;

namespace Domain.Models
{
    public class Pet : Base<int>
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public SexoEnum Sexo { get; set; }
        public TipoSanguineoEnum TipoSanguineo { get; set; }
        public StatusPetEnum Status { get; set; }
        public TipoPetEnum Tipo { get; set; } 
        public CondicaoPetEnum Condicao { get; set; }
        public string Foto {get; set;}
        public Dono Dono {get; set;}


        public Pet(){
            Sexo = SexoEnum.Masculino;
            TipoSanguineo = TipoSanguineoEnum.A;
            Status = StatusPetEnum.Espera;
            Tipo = TipoPetEnum.Cachorro;
            Condicao = CondicaoPetEnum.Filhote;
        }
    }
}