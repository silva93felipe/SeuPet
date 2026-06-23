using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SeuPet.Api.Dto;
using SeuPet.Api.Dto.Usuario;
using SeuPet.Domain.Contracts;
using SeuPet.Domain.Entity;

namespace SeuPet.Domain.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IConfiguration _configuration;

    public UsuarioService(IUsuarioRepository usuarioRepository,  IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _configuration = configuration;
    }

    private Task<bool> UsuarioJaExiste(string email)
        => _usuarioRepository.GetByEmail(email);
    public async Task Create(UsuarioRequest request)
    {
        if (await UsuarioJaExiste(request.Email))
            throw new ApplicationException("Usuário já existe.");
        
        var salt = Guid.NewGuid().ToByteArray();
        byte[] senha = Argon2Service.HashPassword(request.Senha, out salt );
        Usuario newUsuario = new Usuario(request.Email, senha, salt);
        await _usuarioRepository.Create(newUsuario);
    }

    public async Task<(string, string)> Login(LoginRequest request)
    {
        var usuario = await _usuarioRepository.Login(request.Email);
        if(usuario == null)
            throw new ApplicationException("Credenciais inválidas");
        if( !Argon2Service.VerifyPassword(request.Senha, usuario.Hash, usuario.Salt) ) throw new ApplicationException("Credenciais inválidas");
        return ( GerarToken(usuario), GerarRefreshToken(usuario) );
    }

    public async Task<(string, string)> RefreshToken(string token)
    {
       var result = await ValidarToken(token);
       if (result.Item1)
       {
          var usuario = await _usuarioRepository.GetById(result.Item2.Value);
          return (GerarRefreshToken(usuario), GerarRefreshToken(usuario));
          
       }
       return (null, null);
    }

    private string GerarToken(Usuario usuario)
    {
        var key = _configuration["Jwt:Key"];
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", usuario.Id.ToString()),
                new Claim("email", usuario.Email.Value),
            }),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpirationTimeInMinutes"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    private string GerarRefreshToken(Usuario usuario)
    {
        var key = _configuration["Jwt:Key"];
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id", usuario.Id.ToString()),
                new Claim("email", usuario.Email.Value),
            }),
            Expires = DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:RefreshExpirationTimeInMinutes"])),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"]
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private async Task<(bool, int?)> ValidarToken(string token)
    {
        var key = _configuration["Jwt:Key"];
        var paramets = TokenJwtService.GetTokenValidationParameters(_configuration);
        var result = await new JwtSecurityTokenHandler().ValidateTokenAsync(token, paramets);
        if (result.IsValid)
            return (true, int.Parse((string)result.Claims.FirstOrDefault(e => e.Key == "id").Value));

        return (false, null);
    }
}
