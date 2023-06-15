using System;

namespace Domain.Models
{
    public abstract class Base<T>
    {
        public bool Ativo {get; set;}
        public T Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt {get; set;}

        public Base(){
            Ativo = true;
            CreateAt = DateTime.Now;
            UpdateAt = DateTime.Now;
        }
    }
}