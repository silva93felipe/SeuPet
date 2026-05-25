using Microsoft.EntityFrameworkCore;
using SeuPet.Domain.Context;
using SeuPet.Domain.Contracts;
using SeuPet.Domain.Entity;

namespace SeuPet.Infra.Repository
{
    public class AdotanteRepository : IAdotanteRepository
    {
        private readonly SeuPetContext _context;

        public AdotanteRepository(SeuPetContext context)
        {
            _context = context;
        }

        public async Task<Adotante> CreateAsync(Adotante newAdotante)
        {
            await _context.Adotante.AddAsync(newAdotante);
            await Commit();
            return newAdotante;
        }

        public async Task Update(Adotante request)
        {
            _context.Adotante.Update(request); 
            await Commit();
        }

        public async Task<List<Adotante>> GetAllAsync(int limit, int offset)
            => await _context.Adotante
                            .AsNoTracking()
                            .Where(e => e.Ativo)
                            .OrderByDescending(e => e.Id)
                            .Take(limit)
                            .Skip(offset)
                            .ToListAsync();  

        public async Task<Adotante?> GetByIdAsync(int id)
            => await _context.Adotante.FirstOrDefaultAsync(e => e.Id == id && e.Ativo);

        private async Task Commit()
            => await _context.SaveChangesAsync();
    }
}