using SeuPet.Domain.Entity;

namespace SeuPet.Domain.Contracts;

public interface IAdocaoRepository
{
    public Task Create(Adocao adocao);
}