using SeuPet.Domain.Entity;

namespace SeuPet.Domain.Contracts;

public interface IUsuarioRepository
{
    Task Create(Usuario request);
    Task<Usuario?> Login(string email);
}