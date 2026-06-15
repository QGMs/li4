using CyberForgeMS.Business.Stock;
using Microsoft.AspNetCore.Components.Web;

namespace CyberForgeMS.Data.Stock;

public class ComponenteDAO(ISqlDataAccess db) : IComponenteDAO
{
    private readonly ISqlDataAccess _db = db;
    Task<List<Componente>> IComponenteDAO.GetAllComponentes()
    {
        string sql = "SELECT * FROM [dbo].[Componente]";
        return _db.LoadData<Componente,dynamic>(sql,new { });
    }

    async Task<Componente?> IComponenteDAO.GetComponenteById(int id)
    {
        string sql = @"SELECT * FROM [dbo].[Componente] WHERE id = @id" ;
        var list = await _db.LoadData<Componente,dynamic>(sql,new { id });
        return list.FirstOrDefault();
    }   

    async Task IComponenteDAO.PutComponente(Componente componente)
    {
        string sql = @"SELECT * FROM [dbo].[Componente] WHERE designacao = @designacao" ;
        var list = await _db.LoadData<Componente,dynamic>(sql,new { componente.Designacao });   
        if(list.FirstOrDefault() == null || list == null){
            sql = @"INSERT INTO [dbo].[Componente] (designacao,tipo,tempoMontagem,precoVenda)
            values (@designacao,@tipo,@tempoMontagem,@precoVenda)";
            await _db.SaveData(sql,new {componente.Designacao,componente.Tipo,componente.TempoMontagem,componente.PrecoVenda});
        } else {
            return;
        }
    }

    Task IComponenteDAO.RemoveComponente(Componente componente)
    {
        string sql = @"DELETE FROM [dbo].[Componente] WHERE id = @id";
        return _db.SaveData(sql,new {componente.Id});
    }

    async Task<Fornecedor?> IComponenteDAO.GetFornecedor(string idFornecedor)
    {
        string sql = @"SELECT * FROM [dbo].[Fornecedor] WHERE id = @idFornecedor";
        return (await _db.LoadData<Fornecedor,dynamic>(sql,new { idFornecedor })).FirstOrDefault();
    }

    public async Task PutFornecedor(Fornecedor fornecedor){
        string sql = @"INSERT INTO [dbo].[Fornecedor] (id,contacto,email,morada)
        VALUES (@id,@contacto,@email,@morada);";
        await _db.SaveData(sql,
        new {fornecedor.Id,fornecedor.Contacto,fornecedor.Email,fornecedor.Morada});
    }

    public async Task<List<Fornecedor>> GetFornecedores(){
        string sql = @"SELECT * FROM Fornecedor";
        return await _db.LoadData<Fornecedor,dynamic>(sql,new {});
    }

}