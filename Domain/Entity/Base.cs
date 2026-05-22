
namespace SeuPet.Domain.Entity
{
    public abstract class Base<T>
    {
        public T Id { get; init;}
        public Guid IdExterno { get; private set; }
        public bool Ativo {get; set;}
        public DateTime CreateAt { get; private set; }
        public DateTime UpdateAt {get; set;}
        protected Base(){
            Ativo = true;
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
            IdExterno = new Guid();
        }

        public virtual void Inativar()
        {
            JaInativo();
            Ativo = false;
            UpdateAt = DateTime.UtcNow;
        }
        private void JaInativo(){
            if(!Ativo)
                throw new ArgumentException("Adotante já inativo.");
        }
    }
}