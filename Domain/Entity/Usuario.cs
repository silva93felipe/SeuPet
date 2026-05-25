namespace SeuPet.Domain.Entity
{
    public class Usuario : Base<int>
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public byte[] Hash { get; private set; }
        public byte[] Salt  { get; private set; }
        private Usuario(){}
        public Usuario(string nome, string email, byte[] senha,  byte[] salt)
        {
            Validar(nome);
            Validar(email);
            Nome = nome;
            Email = email;
            Hash = senha;
            Salt = salt;
        }

        private static void Validar(string parametro){
            if(string.IsNullOrEmpty(parametro) || string.IsNullOrWhiteSpace(parametro))
                throw new ArgumentException("Campo inválido.");
        }
    }
}