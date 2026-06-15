using System.ComponentModel.DataAnnotations;

namespace CyberForgeMS.Business.Stock;

public class Fornecedor{
    public string Id {get; set;} // Nome da empresa
    public string? Contacto {get; set;}
    public string? Email {get; set;}
    public string? Morada {get; set;}
}