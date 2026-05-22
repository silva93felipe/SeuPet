namespace  SeuPet.Infra.Exceptions;
public class PetNotFoundException : ApplicationException{
    public PetNotFoundException() : base("Pet não encontrado"){}
}