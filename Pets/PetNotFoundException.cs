public class PetNotFoundException : ApplicationException{
    public PetNotFoundException() : base("Pet não encontrado"){}
}