using CyberForgeMS.Business.Encomendas;

namespace CyberForgeMS.Data.Encomendas;


public interface IEncomendasDAO{
    public Task PutEncomenda(Encomenda encomenda);
 
    public Task<List<Encomenda>> GetEncomendas(int idUtilizador);

    public Task<List<Encomenda>> GetAllEncomendas();
    public Task<List<Encomenda>> GetEncomendasPorFazer();
    public Task<Encomenda?> GetEncomenda(int idEncomenda);
    
    public Task DeleteEncomenda(int idEncomenda);
    public Task Done(int idEncomenda);
}