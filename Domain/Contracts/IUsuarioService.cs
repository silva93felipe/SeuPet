using SeuPet.Api.Dto;
using SeuPet.Api.Dto.Usuario;

namespace SeuPet.Domain.Contracts;

public interface IUsuarioService
{
    Task Create(UsuarioRequest request);
    Task<(string, string)> Login(LoginRequest request);
    Task<(string, string)> RefreshToken(string token);
}