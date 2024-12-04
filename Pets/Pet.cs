using System;
using SeuPet.Enums;

namespace SeuPet.Models
{
    public class Pet : Base
    {
        public int Id { get; set;}
        public string Nome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public SexoEnum Sexo { get; private set; }
        public TipoSanguineoEnum TipoSanguineo { get; private set; }
        public StatusPetEnum Status { get; private set; }
        public TipoPetEnum Tipo { get; private set; } 
        public string Foto {get; private set;}         
        public virtual Adotante Adotante { get; private set; }
        public int? AdotanteId { get; private set; }
        public DateTime? DataAdocao {get; private set;}
        private Pet(){}
        public Pet(string nome, SexoEnum sexo, DateTime dataNascimento, TipoSanguineoEnum tipoSanguineo, TipoPetEnum tipo){
            IsValidNome(nome);
            Nome = nome;
            Sexo = sexo;
            DataNascimento = dataNascimento;
            TipoSanguineo = tipoSanguineo;
            Status = StatusPetEnum.Espera;
            Tipo = tipo;
        }

        private void IsValidNome(string nome){
            if(string.IsNullOrEmpty(nome) || string.IsNullOrWhiteSpace(nome)){
                throw new ArgumentException("Nome é obrigatório.");
            }
        }

        public void Update(string nome, SexoEnum sexo, DateTime dataNascimento, TipoSanguineoEnum tipoSanguineo, TipoPetEnum tipo){
            JaAdotado();
            JaInativo();
            IsValidNome(nome);
            Nome = nome;
            Sexo = sexo;
            DataNascimento = dataNascimento;
            TipoSanguineo = tipoSanguineo;
            Tipo = tipo;
            UpdateAt = DateTime.UtcNow;
        }

        public void UpdateImagem(string path){
            Foto = path;
        }

        public void Adotar(int adotanteId){
            JaAdotado();
            JaInativo();
            Status = StatusPetEnum.Adotado;
            UpdateAt = DateTime.UtcNow;
            AdotanteId = adotanteId;
            DataAdocao = DateTime.UtcNow;
        }

        public bool IsAdotado(){
            return Status == StatusPetEnum.Adotado && AdotanteId != null;
        }

        private void JaAdotado(){
            if( IsAdotado())
                throw new ArgumentException("Pet já adotado.");
        }

        private void JaInativo(){
            if(Ativo == false)
                throw new ArgumentException("Pet já inativo.");
        }

        public override void Inativar()
        {
            JaAdotado();
            JaInativo();
            Ativo = false;
            UpdateAt = DateTime.UtcNow;
        }
    }
}