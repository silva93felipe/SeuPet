using SeuPet.Domain.Enums;

namespace SeuPet.Api.Dto.Dashboard;

public class AdocaoResponse
{
       
}

public class QuantidadePorTipo
{
    public int Quantidade { get; set; }
    public TipoPetEnum Tipo { get; set; }
}

public class QuantidadePorSexo
{
    public int Quantidade { get; set; }
    public SexoEnum Sexo { get; set; }
}