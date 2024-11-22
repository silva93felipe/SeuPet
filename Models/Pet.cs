using System;
using SeuPet.Enums;

namespace SeuPet.Models
{
    public class Pet : Base
    {
        public int Id { get; private set;}
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public SexoEnum Sexo { get; private set; }
        public TipoSanguineoEnum TipoSanguineo { get; private set; }
        public StatusPetEnum Status { get; private set; }
        public TipoPetEnum Tipo { get; private set; } 
        public string Foto {get; private set;}         
        public ICollection<Adocao> Adocao { get; private set; }
        private Pet(){}
        public Pet(string nome, SexoEnum sexo, DateTime dataNascimento, TipoSanguineoEnum tipoSanguineo, TipoPetEnum tipo, string foto){
            IsValidNome(nome);
            Nome = nome;
            Sexo = sexo;
            DataNascimento = dataNascimento;
            TipoSanguineo = tipoSanguineo;
            Status = StatusPetEnum.Espera;
            Tipo = tipo;
            Foto = foto;
        }

        private void IsValidNome(string nome){
            if(string.IsNullOrEmpty(nome)){
                throw new ArgumentException("Nome é obrigatório.");
            }
        }

        public void Update(string nome, SexoEnum sexo, DateTime dataNascimento, TipoSanguineoEnum tipoSanguineo, TipoPetEnum tipo, string foto){
            IsValidNome(nome);
            Nome = nome;
            Sexo = sexo;
            DataNascimento = dataNascimento;
            TipoSanguineo = tipoSanguineo;
            Tipo = tipo;
            Foto = foto;
            UpdateAt = DateTime.UtcNow;
        }

        public void Adotar(){
            if(Status == StatusPetEnum.Adotado){
                throw new ArgumentException("Pet já adotado.");
            }
            Status = StatusPetEnum.Adotado;
            UpdateAt = DateTime.UtcNow;
        }

        public override void Inativar()
        {
             if(Ativo == false){
                throw new ArgumentException("Pet já inativo.");
            }
            Ativo = false;
            UpdateAt = DateTime.UtcNow;
        }
    }
}