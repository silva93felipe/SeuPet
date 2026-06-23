namespace SeuPet.Api.Dto.Empresa;

public class EmpresaRequest
{
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public EnderecoRequest Endereco { get; set; }
}

public record EnderecoRequest(string Logradoro, string Bairro, string Cidade, string Estado, string Numero);

public record ValidarEmpresaRequest(Guid empresaId);