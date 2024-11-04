using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeuPet.Enums;

namespace SeuPet.Dto
{
    public class PetRequest
    {
        public string Nome { get; set; }        
        public DateTime DataNascimento { get; set; }
        public SexoEnum Sexo { get; set; }
        public TipoPetEnum Tipo { get; set; }
        public string Foto {get; set;}
        public TipoSanguineoEnum TipoSanguineo { get; set; }
    }
}