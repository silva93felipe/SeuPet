using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SeuPet.Enums
{
    public enum TipoPetEnum
    {
        [Display(Name = "Cachorro")]
        Cachorro,
        [Display(Name = "Gato")]
        Gato,
        [Display(Name = "Passáro")]
        Passaro
    }
}