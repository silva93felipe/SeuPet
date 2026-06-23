using Microsoft.EntityFrameworkCore;
using SeuPet.Domain;
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
        => await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(e => e.Email.Value == email);

    public async Task<Usuario?> GetById(int id)
        => await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

    public Task<bool> GetByEmail(string email)
        => _context.Usuario.AsNoTracking().AnyAsync(e => e.Email.Value.Equals(email));
}