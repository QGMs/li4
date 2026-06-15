using System.ComponentModel.DataAnnotations;

namespace CyberForgeMS.Business.Stock;

public class Componente{
    public int Id {get; set;}
    public string? Designacao {get; set;}
    public string? Tipo {get; set;}
    public int? TempoMontagem {get; set;}
    public float? PrecoVenda {get; set;}
}