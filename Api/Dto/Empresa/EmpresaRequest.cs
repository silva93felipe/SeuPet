namespace SeuPet.Api.Dto.Empresa;

public class EmpresaRequest
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public EnderecoRequest Endereco { get; set; }
}

public class EnderecoRequest
{
    public string Logradoro { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Estado { get; set; }
    public string Numero { get; set; }
}