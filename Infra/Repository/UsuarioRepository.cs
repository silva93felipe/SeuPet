using Microsoft.EntityFrameworkCore;
using SeuPet.Api.Context;
using SeuPet.Domain.Contracts;
using SeuPet.Domain.Entity;

namespace SeuPet.Infra.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly SeuPetContext _context;
    public UsuarioRepository(SeuPetContext context)
    {
        _context = context;
    }
    public async Task Create(Usuario request)
    {
        await _context.Usuario.AddAsync(request);
        await Commit();
    }

    private async Task Commit()
        => await _context.SaveChangesAsync();
    
    public async Task<Usuario?> Login(string email)
        => await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email);
}