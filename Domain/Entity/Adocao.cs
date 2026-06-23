namespace SeuPet.Domain.Entity;

public class Adocao : Base<int>
{
    public int AdotanteId { get; private set; }
    public virtual Adotante Adotante { get; private set; }
    public DateTime DataAdocao { get; private set; }
    public int PetId {get; private set;}
    public virtual Pet Pet { get; private set; }
    private Adocao(){}
    public Adocao(int adotanteId, int petId)
    {
        DataAdocao = DateTime.UtcNow;
        UpdateAt = DateTime.UtcNow;
        AdotanteId = adotanteId;
        PetId = petId;
    }
}