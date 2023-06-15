
namespace Domain.Models
{
    public class Dono: Base<int>
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }
        //public Endereco Endereco {get; set;}
        //public List<Pet> Pets {get; set;}

        /* public Dono(){
            Endereco = new Endereco();
            Pets = new List<Pet>();
        } */

    }
}