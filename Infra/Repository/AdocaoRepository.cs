using SeuPet.Domain;
using SeuPet.Domain.Contracts;
using SeuPet.Domain.Entity;

namespace SeuPet.Infra.Repository;

public class AdocaoRepository : IAdocaoRepository
{
    private readonly SeuPetContext _context;

    public AdocaoRepository(SeuPetContext context)
    {
        _context = context;
    }

    public async Task Create(Adocao adocao)
    {
        await _context.Adocao.AddAsync(adocao);
        await Save();
    }

    private async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}