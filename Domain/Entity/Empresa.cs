namespace SeuPet.Domain.Entity;

public class Empresa : Base<int>
{
    public string Nome { get; set; }
    public Endereco Endereco { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
}

public class Endereco
{
    public string Logradoro { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Numero { get; set; }
}