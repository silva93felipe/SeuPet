using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
namespace Domain.Interfaces.Services
{
    public interface IDonoService : IBaseService<Dono>
    {
        bool Adotar(int donoId, int petId);
        Task<Dono> GetByCpf(string cpf);
    }
}