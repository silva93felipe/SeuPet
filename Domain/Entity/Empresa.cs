namespace SeuPet.Domain.Entity;

public class Empresa : Base<int>
{
    public string Nome { get; private set; }
    public Endereco Endereco { get; private set; }
    public string Telefone { get; private set; }
    private Empresa(){}

    public Empresa(string nome, Endereco endereco, string telefone)
    {
        Nome = nome;
        Endereco = endereco;
        Telefone = telefone;
    }

    public void Update(string nome, string telefone)
    {
        Nome = nome;
        Telefone = telefone;
    }

    public void UpdateEndereco(string logradoro, string bairro, string cidade, string estado, string numero)
    {
        Endereco.Update( logradoro,  bairro,  cidade,  estado,  numero);
    }
}

public class Endereco
{
    public Endereco(string logradoro, string bairro, string cidade, string estado, string numero)
    {
        Logradoro = logradoro;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        Numero = numero;
    }

    public void Update(string logradouro, string numero, string bairro, string cidade, string estado)
    {
        Logradoro = logradouro;
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
        Numero = numero;
    }
    public string Logradoro { get; private set; }
    public string Bairro { get; private set; }
    public string Cidade { get; private set; }
    public string Estado { get; private set; }
    public string Numero { get; private set; }
}