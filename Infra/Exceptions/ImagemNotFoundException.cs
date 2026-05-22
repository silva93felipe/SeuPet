namespace  SeuPet.Infra.Exceptions;
public class ImagemNotFoundException : ApplicationException{
    public ImagemNotFoundException() : base("Imagem não encontrada"){}
}