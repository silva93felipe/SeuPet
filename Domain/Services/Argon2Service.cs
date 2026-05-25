using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace SeuPet.Domain.Services;

public class Argon2Service
{
    public static byte[] HashPassword(string password, out byte[] salt)
    {
        // Gerando o salt aleatório (16 bytes é o recomendado)
        salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
            rng.GetBytes(salt);

        using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password)))
        {
            argon2.Salt = salt;
            argon2.DegreeOfParallelism = 4; // Quantidade de threads (CPU)
            argon2.MemorySize = 65536;      // Memória em KB (64 MB)
            argon2.Iterations = 4;         // Número de iterações (Tempo)

            // Gera um hash de 32 bytes (256 bits)
            return argon2.GetBytes(32);
        }
    }
    
    public static bool VerifyPassword(string password, byte[] storedHash, byte[] salt)
    {
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
        argon2.Salt = salt;
        argon2.DegreeOfParallelism = 4;
        argon2.MemorySize = 65536;
        argon2.Iterations = 4;
        var newHashBytes = argon2.GetBytes(32);
        return storedHash.Equals(newHashBytes);
    }
}