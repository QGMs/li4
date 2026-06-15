namespace CyberForgeMS.Data.Encomendas;

using CyberForgeMS.Business.Encomendas;
using CyberForgeMS.Business.Stock;

public class ProdutosDAO(ISqlDataAccess _db) : IProdutosDAO{
    public async Task<List<Produto>> GetAllProdutos(){
        string sql = @"SELECT * FROM Produto;";
        List<Produto> lista = await _db.LoadData<Produto,dynamic>(sql,new {});
        if(lista != null && lista.Count != 0){
            foreach(Produto p in lista){
                sql = @"SELECT Sum(Componente.tempoMontagem) FROM Componente JOIN
                ComponenteProduto ON ComponenteProduto.idComponente = Componente.id
                WHERE idProduto = @id";
                p.TempoMontagem = (await _db.LoadData<int,dynamic>(sql,new {p.Id})).FirstOrDefault();
                p.Componentes = await GetComponentesProduto(p.Id);
            }
        }
        return lista;
    }

    public async Task<Produto?> GetProdutoById(int id){
        string sql = @"SELECT ComponenteProduto.idComponente,ComponenteProduto.quantidade,Componente.tempoMontagem FROM
        ComponenteProduto JOIN Componente ON ComponenteProduto.idComponente = Componente.id
        WHERE ComponenteProduto.idProduto = @id;";
        var componentes = await _db.LoadData<ComponenteProduto,dynamic>(sql,new{id});

        sql = @"SELECT * FROM Produto WHERE id = @id;";
        Produto? produto = (await _db.LoadData<Produto,dynamic>(sql,new {id})).FirstOrDefault();

        produto.Componentes = componentes;

        foreach(ComponenteProduto c in componentes){
            produto.TempoMontagem += c.Quantidade * c.TempoMontagem;
        }

        return produto;
    }

    public async Task PutProduto(Produto produto){
        string sql = @"SELECT id FROM Produto WHERE id = @id;";
        var exist1 = await _db.LoadData<int,dynamic>(sql,new {produto.Id});
        if(exist1 != null && exist1.Count == 0){
            sql = @"INSERT INTO Produto (precoVenda,precoFabrico,nome)
            VALUES (@precoVenda,@precoFabrico,@nome);";
            await _db.SaveData(sql,new{produto.PrecoVenda,produto.PrecoFabrico,produto.Nome});
        }
        sql = @"SELECT Count(*) FROM ComponenteProduto WHERE idProduto = @id;";
        var exist2 = (await _db.LoadData<int,dynamic>(sql,new {produto.Id})).FirstOrDefault();
        if(produto.Componentes != null){
            if(exist2 != produto.Componentes.Count){
                sql = @"INSERT INTO ComponenteProduto (idProduto,idComponente,quantidade)
                VALUES ";
                List<string> valuesList = [];

                foreach(ComponenteProduto c in produto.Componentes)
                {
                    valuesList.Add($"({produto.Id}, {c.IdComponente}, {c.Quantidade})");
                }

                sql += string.Join(", ", valuesList) + ";";
                await _db.SaveData(sql,new {});
            }
        }
    }

    public async Task<List<ComponenteProduto>> GetComponentesProduto(int idProduto){
        string sql = @"
        SELECT ComponenteProduto.idComponente,ComponenteProduto.quantidade,Componente.tempoMontagem
        FROM ComponenteProduto JOIN Componente
        ON ComponenteProduto.idComponente = Componente.id 
        WHERE ComponenteProduto.idProduto = @idProduto;";
        return await _db.LoadData<ComponenteProduto,dynamic>(sql,new {idProduto});
    }

    public async Task<List<int>> GetLinhasMontagem(){
        string sql = @"SELECT idLinha FROM LinhaMontagem;";
        return await _db.LoadData<int,dynamic>(sql, new{  });
    } 
    
    public async Task<int> GetTempoMontagem(int produtoId)
{
    string sql = @"
        SELECT SUM(c.tempoMontagem * cp.quantidade) as tempoMontagemTotal
        FROM Produto p
        JOIN ComponenteProduto cp ON p.id = cp.idProduto
        JOIN Componente c ON cp.idComponente = c.id
        WHERE p.id = @produtoId
        GROUP BY p.id";

    return (await _db.LoadData<int,dynamic>(sql, new { produtoId })).FirstOrDefault();
}
}