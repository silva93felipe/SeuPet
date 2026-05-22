using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace  SeuPet.Domain.Enums
{
    public enum TipoPetEnum
    {
        [Display(Name = "Cachorro")]
        Cachorro,
        [Display(Name = "Gato")]
        Gato,
        [Display(Name = "Pássaro")]
        Passaro
    }
}