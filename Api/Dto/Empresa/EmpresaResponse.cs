namespace SeuPet.Api.Dto.Empresa;

public record EmpresaResponse(Guid Id, EnderecoRequest Endereco, string Nome, string Telefone);