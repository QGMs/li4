using CyberForgeMS.Business.Stock;

namespace CyberForgeMS.Business.Encomendas;

public class Encomenda{
    public int Id { get; set; }
    public bool Estado { get; set; } = false;

    public int IdUtilizador {get; set;}

    public int IdLinhaMontagem {get; set;}
    public Dictionary<Produto,int> Produtos { get; set; }

    public int TempoTotal {get; set;} = 0;

    public float PrecoVenda {get; set;} = 0;
}