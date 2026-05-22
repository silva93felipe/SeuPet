using SeuPet.Api.Dto;
using SeuPet.Api.Dto.Usuario;
using SeuPet.Domain.Contracts;
using SeuPet.Domain.Entity;

namespace SeuPet.Domain.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }
    public async Task Create(UsuarioRequest request)
    {
        var salt = Guid.NewGuid().ToByteArray();
        string senha = Argon2Service.HashPassword(request.Senha, out salt );
        Usuario newUsuario = new Usuario(request.Nome, request.Email, request.Senha, salt);
        await _usuarioRepository.Create(newUsuario);
    }

    public async Task<string> Login(LoginRequest request)
    {
        var usuario = await _usuarioRepository.Login(request.Email);
        if( !Argon2Service.VerifyPassword(request.Senha, usuario.Hash, usuario.Salt) ) throw new ApplicationException("Credenciais inválidas");
        return usuario.Nome;
    }
}