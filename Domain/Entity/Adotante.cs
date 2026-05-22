
using SeuPet.Domain.Enums;

namespace SeuPet.Domain.Entity
{
    public class Adotante : Base<int>
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Telefone { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public SexoEnum Sexo { get; private set; }
        public ICollection<Pet> Pets { get; private set; }
        private Adotante(){}
        public Adotante(string nome, string email, DateTime dataNascimento, SexoEnum sexo, string telefone)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Telefone = telefone;
        }

        public void Update(string nome, string email, DateTime dataNascimento, SexoEnum sexo, string telefone)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Sexo = sexo;
            Telefone = telefone;
        }
    }
}