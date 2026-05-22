namespace  SeuPet.Infra.Exceptions;

public class AdotanteNotFoundException : ApplicationException{
    public AdotanteNotFoundException() : base("Adontate não encontrado"){}
}