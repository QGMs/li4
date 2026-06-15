namespace CyberForgeMS.Business.Stock;

public class StockModel{
    public int Id {get; set;}
    public Dictionary<int,List<ComponenteFornecedor>>? componentes;
}