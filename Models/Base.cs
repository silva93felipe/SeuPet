using System;

namespace SeuPet.Models
{
    public abstract class Base
    {
        public bool Ativo {get; set;}
        public DateTime CreateAt { get; private set; }
        public DateTime UpdateAt {get; set;}
        public Base(){
            Ativo = true;
            CreateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }
        public abstract void Inativar();
    }
}