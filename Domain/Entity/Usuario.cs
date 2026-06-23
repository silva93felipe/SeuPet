using SeuPet.Domain.ValueObject;

namespace SeuPet.Domain.Entity
{
    public class Usuario : Base<int>
    {
        public Email Email { get; private set; }
        public byte[] Hash { get; private set; }
        public byte[] Salt  { get; private set; }
        private Usuario(){}
        public Usuario(string email, byte[] senha,  byte[] salt)
        {
            Email = (Email)email;
            Hash = senha;
            Salt = salt;
        }
        
        
    }
}