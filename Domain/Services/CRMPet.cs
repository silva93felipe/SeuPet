namespace SeuPet.Domain.Services;

public interface ICRMPet
{
    Task<bool> CredenciasEmpresa(Guid empresaId);
}

public class CRMPet : ICRMPet
{
    private readonly string URL_CRMPET = "";
    public Task<bool> CredenciasEmpresa(Guid empresaId)
    {
        throw new NotImplementedException();
    }
}