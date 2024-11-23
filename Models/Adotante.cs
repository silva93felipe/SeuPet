using SeuPet.Enums;

namespace SeuPet.Models
{
    public class Adotante : Base
    {
        public int Id { get; private set;}
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public SexoEnum Sexo { get; private set; }
        public ICollection<Pet> Pets { get; private set; }
        private Adotante(){}
        public Adotante(string nome, string email, DateTime dataNascimento, SexoEnum sexo)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Sexo = sexo;
        }

        public void Update(string nome, string email, DateTime dataNascimento, SexoEnum sexo)
        {
            JaInativo();
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Sexo = sexo;
        }

        public override void Inativar()
        {
            JaInativo();
            Ativo = false;
            UpdateAt = DateTime.UtcNow;
        }

        private void JaInativo(){
            if(Ativo == false)
                throw new ArgumentException("Adotante já inativo.");
        }
    }
}