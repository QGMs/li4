using CyberForgeMS.Business.Stock;

namespace CyberForgeMS.Data.Stock;

public interface IStockDAO {
    public Task ConsumeComponente(StockModel stock,ComponenteFornecedor componente,int quantidade);
    
    public Task SupplyComponente(StockModel stock,ComponenteFornecedor componente,int quantidade);

    public Task<StockModel> GetStockById(int id);

    public Task<List<int>> GetStockIds();

    public Task InsertOnStock(int idStock,ComponenteFornecedor componenteFornecedor);
}