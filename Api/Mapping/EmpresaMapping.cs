using SeuPet.Api.Dto.Empresa;
using SeuPet.Domain.Entity;

namespace SeuPet.Api.Mapping;

public static class EmpresaMapping
{
    public static EmpresaResponse ToDto(this Empresa entity){
        return new EmpresaResponse(entity.IdExterno, entity.Endereco.ToDto(), entity.Nome, entity.Telefone);
    }

    public static Empresa ToEntity(this EmpresaRequest request)
    {
        var endereco = new Endereco(request.Endereco.Logradoro, request.Endereco.Bairro, request.Endereco.Cidade, request.Endereco.Estado, request.Endereco.Numero);
        return new Empresa(request.Nome, endereco, request.Telefone);
    }
    
    public static EnderecoRequest ToDto(this Endereco entity){
        return new EnderecoRequest(entity.Logradoro, entity.Bairro, entity.Cidade, entity.Estado,  entity.Numero);
    }

    public static Endereco ToEntity(this EnderecoRequest request){
        return new Endereco(request.Logradoro, request.Bairro, request.Cidade, request.Estado, request.Numero);
    }
}