using System;
using SeuPet.Enums;

namespace SeuPet.Models
{
    public class Pet : Base
    {
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public SexoEnum Sexo { get; private set; }
        public TipoSanguineoEnum TipoSanguineo { get; private set; }
        public StatusPetEnum Status { get; private set; }
        public TipoPetEnum Tipo { get; private set; } 
        public string Foto {get; private set;} 
        public DateTime DataAdocao {get; private set;}
        public ICollection<Adocao> Adocao { get; private set; }
        private Pet(){}
        public Pet(string nome, SexoEnum sexo, DateTime dataNascimento, TipoSanguineoEnum tipoSanguineo, TipoPetEnum tipo){
            Nome = nome;
            Sexo = sexo;
            DataNascimento = dataNascimento;
            TipoSanguineo = tipoSanguineo;
            Status = StatusPetEnum.Espera;
            Tipo = tipo;
            Foto = string.Empty;
        }

        public void Update(string nome, SexoEnum sexo, DateTime dataNascimento, TipoSanguineoEnum tipoSanguineo, TipoPetEnum tipo){
            Nome = nome;
            Sexo = sexo;
            DataNascimento = dataNascimento;
            TipoSanguineo = tipoSanguineo;
            Tipo = tipo;
            Foto = string.Empty;
            UpdateAt = DateTime.Now;
        }

        public void Adotar(){
            Status = StatusPetEnum.Adotado;
            UpdateAt = DateTime.Now;
        }

        public override void Inativar()
        {
            Ativo = false;
            UpdateAt = DateTime.Now;
        }
    }
}