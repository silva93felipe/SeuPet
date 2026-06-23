using SeuPet.Api.Dto.Empresa;
using SeuPet.Api.Mapping;
using SeuPet.Domain.Entity;
using SeuPet.Infra.Repository;

namespace SeuPet.Domain.Services;

public interface IEmpresaService
{
    public Task<EmpresaResponse> Create(EmpresaRequest request);
    public Task<Empresa?> GetById(Guid id);
    public Task Update(Guid id, EmpresaRequest request);
}

public class EmpresaService : IEmpresaService 
{
    private readonly IEmpresaRepository  _empresaRepository;

    public EmpresaService(IEmpresaRepository empresaRepository)
    {
        _empresaRepository = empresaRepository;
    }

    public async Task<EmpresaResponse> Create(EmpresaRequest request)
    {
        var endereco = new Endereco(request.Endereco.Logradoro, request.Endereco.Bairro, request.Endereco.Cidade, request.Endereco.Estado, request.Endereco.Numero);
        var newEmpresa = new Empresa(request.Nome, endereco, request.Telefone);
        var empresa = await _empresaRepository.Create(newEmpresa);
        return empresa.ToDto();
    }

    public async Task<Empresa?> GetById(Guid id)
    {
        var empresa = await _empresaRepository.GetById(id);
        if (empresa == null)
            throw new ApplicationException("Empresa not found");
        
        return empresa;
    }

    public async Task Update(Guid id, EmpresaRequest request)
    {
        var empresa = await _empresaRepository.GetById(id);
        if (empresa == null)
            throw new ApplicationException("Empresa not found");
        empresa.Update(request.Nome, request.Telefone);
        empresa.UpdateEndereco(request.Endereco.Logradoro, request.Endereco.Bairro, request.Endereco.Cidade,  request.Endereco.Estado, request.Endereco.Numero);
        await _empresaRepository.Update(empresa);
    }
}