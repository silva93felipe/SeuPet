

namespace Domain.Models
{
    public class Endereco : Base<int>
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Cep {get; set;}
        public string Cidade {get; set;}
    }
}