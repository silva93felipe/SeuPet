using System;

namespace SeuPet.Models
{
    public abstract class Base
    {
        //private static int IdAutoIncrement = 1;
        public bool Ativo {get; set;}
        public int Id { get; private set; }
        public DateTime CreateAt { get; private set; }
        public DateTime UpdateAt {get; set;}

        public Base(){
            //Id = IdAutoIncrement++;
            Ativo = true;
            CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;
        }

        public abstract void Inativar();
    }
}