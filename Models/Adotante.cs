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
        public ICollection<Adocao> Adocao { get; private set; }
        public Adotante(string nome, string email, DateTime dataNascimento, SexoEnum sexo)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Sexo = sexo;
        }
        private Adotante(){}

        public override void Inativar()
        {
            Ativo = false;
            UpdateAt = DateTime.UtcNow;
        }
    }
}