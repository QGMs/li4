namespace CyberForgeMS.Business.Encomendas;

using CyberForgeMS.Business.Stock;

public class Produto{
    
    public int Id {get;set;}

    public float PrecoVenda {get; set;}

    public float PrecoFabrico {get; set;}

    public string? Nome {get; set;}

    // Mapa <idComponente,Quantidade>
    public List<ComponenteProduto>? Componentes {get; set;}

    public int TempoMontagem {get; set;} = 0;

}