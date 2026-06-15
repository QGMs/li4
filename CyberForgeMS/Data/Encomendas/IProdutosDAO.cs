using CyberForgeMS.Business.Encomendas;

namespace CyberForgeMS.Data.Encomendas;

public interface IProdutosDAO{
    public Task<List<Produto>> GetAllProdutos();

    public Task<Produto?> GetProdutoById(int id);

    public Task PutProduto(Produto produto);

    public Task<List<int>> GetLinhasMontagem();

    public Task<int> GetTempoMontagem(int idProduto);
}