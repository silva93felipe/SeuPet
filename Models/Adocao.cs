namespace SeuPet.Models
{
    public class Adocao : Base
    {
        public int PetId { get; private set; }
        public virtual Pet Pet{ get; private set; }
        public int AdotanteId { get; private set; }
        public virtual Adotante Adotante{ get; private set; }
        public Adocao(int petId, int adotanteId){
            PetId = petId;
            AdotanteId = adotanteId;
        }
        private Adocao(){}
        public override void Inativar()
        {
            Ativo = false;
            UpdateAt = DateTime.UtcNow;
        }
    }
}