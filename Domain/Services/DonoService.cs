using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Interfaces.Repositories;

namespace Domain.Services
{
    public class DonoService : IDonoService
    {
        private readonly IDonoRepository _donoRepository;

        public DonoService(IDonoRepository donoRepository)
        {
            _donoRepository = donoRepository;
        }

        public bool Adotar(int donoId, int petId)
        {
            return true;
        }

        public void Create(Dono dono)
        {
            
        }

        public Task<bool> Delete(int donoId)
        {
            return _donoRepository.Delete(donoId);
        }

        public IEnumerable<Dono> GetAll()
        {
            //return new List<Dono>();
            return _donoRepository.GetAll();
        }

        public Task<Dono> GetByCpf(string cpf)
        {
            return _donoRepository.GetByCpf(cpf);        
        
        }

        public Task<Dono> GetById(int id)
        {
            return _donoRepository.GetById(id);  
        }

        public void Update(Dono dono, int donoId)
        {
        
        }
    }
}