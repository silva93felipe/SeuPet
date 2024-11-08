using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeuPet.Dto
{
    public class PetResponse
    {
        public int Id { get; set; }
        public string Nome { get; set; }        
        public string DataNascimento { get; set; }
        public string Sexo { get; set; }
        public string Tipo { get; set; }
        public string Foto {get; set;}
    }
}