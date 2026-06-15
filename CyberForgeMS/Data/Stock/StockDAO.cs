using CyberForgeMS.Business.Stock;

namespace CyberForgeMS.Data.Stock;

public class StockDAO(ISqlDataAccess db) : IStockDAO
{
    private readonly ISqlDataAccess _db = db;

    public async Task ConsumeComponente(StockModel stock,ComponenteFornecedor componente, int quantidade)
    {
        string sql = @"UPDATE [dbo].[StockComponente] 
        SET quantidade = quantidade - @quantidade
        WHERE idStock = @idStock 
        AND idComponente = @idComponente
        AND idFornecedor = @idFornecedor;";
        int idStock = stock.Id;
        int idComponente = componente.IdComponente;
        string idFornecedor = componente.IdFornecedor;
        await _db.SaveData(sql, new { quantidade , idStock , idComponente , idFornecedor });
    }

    public async Task SupplyComponente(StockModel stock,ComponenteFornecedor componente,int quantidade){
        string sql = @"UPDATE [dbo].[StockComponente]
        SET quantidade = quantidade + @quantidade
        WHERE idStock = @idStock
        AND idComponente = @idComponente
        AND idFornecedor = @idFornecedor;";
        int idStock = stock.Id;
        int idComponente = componente.IdComponente;
        string idFornecedor = componente.IdFornecedor;
        await _db.SaveData(sql, new { quantidade , idStock , idComponente , idFornecedor });
    }

    public async Task<StockModel> GetStockById(int id)
    {

        /*string sql = @"SELECT Componente.* FROM [dbo].[Componente]
        JOIN [dbo].[StockComponente]
        ON dbo.StockComponente.idComponente = dbo.Componente.id
        AND dbo.StockComponente.idStock = @idStock;";
        var componentes = await _db.LoadData<List<int>, dynamic>(sql, new { id });

        sql = @"SELECT Componente.* FROM [dbo].[Fornecedor]
        JOIN [dbo].[StockComponente]
        ON dbo.StockComponente.idFornecedor = dbo.Fornecedor.id
        AND dbo.StockComponente.idStock = @idStock;";
        var fornecedores = await _db.LoadData<List<int>, dynamic>(sql, new { id });*/

        string sql = @"SELECT DISTINCT StockComponente.idComponente,StockComponente.idFornecedor,ComponenteFornecedor.precoCompra, StockComponente.quantidade FROM [dbo].[ComponenteFornecedor]
        JOIN [dbo].[StockComponente]
        ON dbo.StockComponente.idComponente = dbo.ComponenteFornecedor.idComponente
        AND dbo.StockComponente.idStock = @id;";
        List<ComponenteFornecedor> componenteFornecedor = await _db.LoadData<ComponenteFornecedor, dynamic>(sql, new { id }); 

        sql = @"SELECT DISTINCT idComponente from [dbo].[StockComponente]
        WHERE dbo.StockComponente.idStock = @id;";
        var keys = await _db.LoadData<int,dynamic>(sql,new { id });

        Dictionary<int,List<ComponenteFornecedor>> final = keys.ToDictionary(
            key => key,
            key => componenteFornecedor.Where(obj => obj.IdComponente == key).ToList()
        );

        StockModel stock = new() { Id = id, componentes = final};

        return stock;
    }
    public async Task<List<int>> GetStockIds()
    {
        string sql = @"SELECT * FROM [dbo].[Stock]";
        return await _db.LoadData<int,dynamic>(sql,new {});
    }

        public async Task InsertOnStock(int idStock,ComponenteFornecedor componenteFornecedor){
            string sql = @"SELECT * FROM Componente WHERE id = @idComponente;";
            var verify1 = (await _db.LoadData<int,dynamic>(sql,new{componenteFornecedor.IdComponente})).FirstOrDefault();
            sql = @"SELECT * FROM Fornecedor WHERE id = @idFornecedor;";
            var verify2 = (await _db.LoadData<string,dynamic>(sql,new{componenteFornecedor.IdFornecedor})).FirstOrDefault();
            sql = @"SELECT idComponente,idFornecedor FROM ComponenteFornecedor WHERE idComponente = @idComponente AND idFornecedor = @idFornecedor;";
            var verify3 = (await _db.LoadData<(int,string),dynamic>(sql,new {componenteFornecedor.IdComponente,componenteFornecedor.IdFornecedor})).FirstOrDefault();

            if(verify1 == componenteFornecedor.IdComponente && verify2 == componenteFornecedor.IdFornecedor){
                if(verify3.Item1 != componenteFornecedor.IdComponente || verify3.Item2 != componenteFornecedor.IdFornecedor){
                    sql = @"INSERT INTO ComponenteFornecedor (idComponente,idFornecedor,precoCompra)
                    VALUES (@idComponente,@idFornecedor,@precoCompra);";
                    await _db.SaveData(sql,new{componenteFornecedor.IdComponente,componenteFornecedor.IdFornecedor,componenteFornecedor.PrecoCompra});
                }

                sql = @"INSERT INTO StockComponente (idStock,idComponente,idFornecedor,quantidade)
                VALUES (@idStock,@idComponente,@idFornecedor,@quantidade);";
                await _db.SaveData(sql,new{idStock,componenteFornecedor.IdComponente,componenteFornecedor.IdFornecedor,componenteFornecedor.Quantidade});
            }
        }

}
