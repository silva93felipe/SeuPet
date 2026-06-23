using Microsoft.EntityFrameworkCore;
using SeuPet.Domain;
using SeuPet.Domain.Entity;

namespace SeuPet.Infra.Repository;

public interface IEmpresaRepository
{
    public Task<Empresa> Create(Empresa request);
    public Task<Empresa?> GetById(Guid id);
    public Task Update(Empresa request);
    public Task Save();
}
public class EmpresaRepository : IEmpresaRepository
{
    private SeuPetContext _context;
    public EmpresaRepository(SeuPetContext context)
    {
        _context = context;
    }
    public async Task<Empresa> Create(Empresa entity)
    {
        await _context.Empresa.AddAsync(entity);
        await Save();
        return entity;
    }

    public async Task<Empresa?> GetById(Guid id) => await _context.Empresa.FirstOrDefaultAsync(e => e.IdExterno == id);

    public async Task Update(Empresa request)
    {
        _context.Empresa.Update(request);
        await Save();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}