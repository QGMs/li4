

using CyberForgeMS.Business.Stock;

namespace CyberForgeMS.Data.Stock;

public interface IComponenteDAO{
    public Task PutComponente(Componente componente);
    public Task RemoveComponente(Componente componente);
    public Task<List<Componente>> GetAllComponentes();
    public Task<Componente?> GetComponenteById(int id);
    public Task<Fornecedor?> GetFornecedor(string id);
    public Task<List<Fornecedor>> GetFornecedores();
    public Task PutFornecedor(Fornecedor fornecedor);
}