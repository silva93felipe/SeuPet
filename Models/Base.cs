using System;

namespace SeuPet.Models
{
    public abstract class Base
    {
        private static int IdAutoIncrement = 1;
        public bool Ativo {get; private set;}
        public int Id { get; private set; }
        public DateTime CreateAt { get; private set; }
        protected DateTime UpdateAt {get; set;}

        public Base(){
            Id = IdAutoIncrement++;
            Ativo = true;
            CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;
        }

        protected void Inativar(){
            Ativo = false;
            UpdateAt = DateTime.Now;
        }
    }
}