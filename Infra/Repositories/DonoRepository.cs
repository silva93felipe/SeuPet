using Domain.Interfaces.Repositories;
using Domain.Models;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class DonoRepository : IDonoRepository
    {
        private readonly PetContext _dbContext;

        public DonoRepository(PetContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Create(Dono input)
        {
            _dbContext.Dono.AddAsync(input);
            _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Delete(int id)
        {
            Dono donoDb = await _dbContext.Dono.FirstOrDefaultAsync(d => d.Id == id);

            if(donoDb == null)
                return false;
            
            donoDb.Ativo = false;
            donoDb.UpdateAt = DateTime.UtcNow.AddHours(-3);
            _dbContext.SaveChanges();

            return true;
        }

        public IEnumerable<Dono> GetAll()
        {
            return  _dbContext.Dono;
        }

        public Task<Dono> GetByCpf(string cpf)
        {
            return  _dbContext.Dono.Where(d => d.Cpf == cpf).FirstOrDefaultAsync();
        }

        public Task<Dono> GetById(int id)
        {
             return _dbContext.Dono.Where(d => d.Id == id).FirstOrDefaultAsync();
        }

        public async void Update(Dono input)
        {

            _dbContext.Dono.Update(input);
            await _dbContext.SaveChangesAsync();
        }
    }
}