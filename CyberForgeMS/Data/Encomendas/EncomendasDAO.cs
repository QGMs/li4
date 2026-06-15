using CyberForgeMS.Business.Encomendas;
using CyberForgeMS.Business.Utilizadores;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace CyberForgeMS.Data.Encomendas;

public class EncomendasDAO(ISqlDataAccess _db,IProdutosDAO p_db) : IEncomendasDAO{
    public async Task PutEncomenda(Encomenda encomenda){
        string sql = @"INSERT INTO Encomenda (estado,idUtilizador,idLinhaMontagem)
        VALUES (@estado,@idUtilizador,@idLinhaMontagem);
        SELECT SCOPE_IDENTITY();";
        bool estado = false;
        int idEncomenda = (await _db.LoadData<int,dynamic>(sql,new {estado, encomenda.IdUtilizador,encomenda.IdLinhaMontagem})).FirstOrDefault();

        foreach(KeyValuePair<Produto,int> p in encomenda.Produtos){
            sql = @"INSERT INTO ProdutoEncomenda (idProduto,idEncomenda,quantidade) 
            VALUES (@id,@idEncomenda,@quantidade);";
            int quantidade = p.Value;
            await _db.SaveData(sql,new {p.Key.Id,idEncomenda,quantidade});
        }

    }
    public async Task<List<Encomenda>> GetEncomendas(int idUtilizador){
        string sql = @"SELECT * FROM Encomenda WHERE idUtilizador = @idUtilizador;";
        List<Encomenda> encomendas = await _db.LoadData<Encomenda,dynamic>(sql,new { idUtilizador });
        return encomendas;
    }

    public async Task<List<Encomenda>> GetAllEncomendas(){
        string sql = @"SELECT * FROM Encomenda;";
        List<Encomenda> encomendas = await _db.LoadData<Encomenda,dynamic>(sql,new { });
        foreach(Encomenda encomenda in encomendas){
            if(encomenda != null){
                sql = @"SELECT ProdutoEncomenda.idProduto,ProdutoEncomenda.quantidade,Produto.precoVenda
                FROM ProdutoEncomenda JOIN Produto ON ProdutoEncomenda.idProduto = Produto.id 
                WHERE ProdutoEncomenda.idEncomenda = @id;";
                List<(int,int,float)> produtos = await _db.LoadData<(int,int,float),dynamic>(sql,new {encomenda.Id});
                if(produtos != null && produtos.Count != 0){
                    foreach((int,int,float) a in produtos){
                        encomenda.TempoTotal += a.Item2 * await p_db.GetTempoMontagem(a.Item1);
                        encomenda.PrecoVenda += a.Item3 * a.Item2;
                    }
                }
            }
        }
        return encomendas;
    }

    public async Task<List<Encomenda>> GetEncomendasPorFazer(){
        string sql = @"SELECT * FROM Encomenda WHERE estado = 0;";
        List<Encomenda> encomendas = await _db.LoadData<Encomenda,dynamic>(sql,new { });
        foreach(Encomenda encomenda in encomendas){
            if(encomenda != null){
                encomenda.Produtos = [];
                sql = @"SELECT ProdutoEncomenda.idProduto,ProdutoEncomenda.quantidade,Produto.precoVenda
                FROM ProdutoEncomenda JOIN Produto ON ProdutoEncomenda.idProduto = Produto.id 
                WHERE ProdutoEncomenda.idEncomenda = @id;";
                List<(int,int,float)> produtos = await _db.LoadData<(int,int,float),dynamic>(sql,new {encomenda.Id});
                if(produtos != null && produtos.Count != 0){
                    foreach((int,int,float) a in produtos){
                        encomenda.TempoTotal += a.Item2 * await p_db.GetTempoMontagem(a.Item1);
                        encomenda.PrecoVenda += a.Item3 * a.Item2;
                        Produto? p = await p_db.GetProdutoById(a.Item1);
                        if(p != null) encomenda.Produtos.Add(p,a.Item2);
                    }
                }            
            }
        }
        return encomendas;
    }

    public async Task<Encomenda?> GetEncomenda(int idEncomenda){
        string sql = @"SELECT * FROM Encomenda WHERE id = @idEncomenda;";
        Encomenda? encomenda = (await _db.LoadData<Encomenda,dynamic>(sql,new {idEncomenda})).FirstOrDefault();
        if(encomenda != null){
            sql = @"SELECT ProdutoEncomenda.idProduto,ProdutoEncomenda.quantidade,Produto.precoVenda
            FROM ProdutoEncomenda JOIN Produto ON ProdutoEncomenda.idProduto = Produto.id 
            WHERE ProdutoEncomenda.idEncomenda = @idEncomenda;";
            List<(int,int,float)> produtos = await _db.LoadData<(int,int,float),dynamic>(sql,new {idEncomenda});
            if(produtos != null && produtos.Count != 0){
                foreach((int,int,float) a in produtos){
                    encomenda.TempoTotal += a.Item2 * await p_db.GetTempoMontagem(a.Item1);
                    encomenda.PrecoVenda += a.Item3 * a.Item2;
                }
            }
        }
        return encomenda;
    }

    public async Task DeleteEncomenda(int idEncomenda){
        string sql = @"
        DELETE FROM ProdutoEncomenda WHERE idEncomenda = @idEncomenda;
        DELETE FROM Encomenda WHERE id = @idEncomenda;";
        await _db.SaveData(sql,new {idEncomenda});
    }

    public async Task Done(int idEncomenda){
        string sql = @"UPDATE Encomenda
        SET estado = 1
        WHERE id = @idEncomenda;";
        await _db.SaveData(sql,new {idEncomenda});
    }

} 