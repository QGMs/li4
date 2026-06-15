using CyberForgeMS.Business.Utilizadores;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace CyberForgeMS.Data.Utilizadores;

public class UtilizadoresDAO(ISqlDataAccess db) : IUtilizadoresDAO{
    private readonly ISqlDataAccess _db = db;

    public Task<List<UtilizadorModel>> GetUtilizadores(){
        string sql = "SELECT * from [dbo].[Utilizador]";

        return _db.LoadData<UtilizadorModel, dynamic>(sql,new { });
    }

    public async Task<UtilizadorModel?> GetUtilizadorById(string username){
        string sql = @"SELECT * FROM [dbo].[Utilizador] WHERE email = @username";
        return (await _db.LoadData<UtilizadorModel,dynamic>(sql, new {username})).FirstOrDefault();
    } 

    public async Task<(UtilizadorModel?,string)> ValidateUser(string username, string password){

        string sql = @"SELECT * FROM [dbo].[Utilizador] WHERE email = @username";
        var user = (await _db.LoadData<UtilizadorModel,dynamic>(sql, new {username})).FirstOrDefault();

        if(user == null || user.Email != username || user.Password != password) return (null,String.Empty);

        sql = @"SELECT id FROM [dbo].[Funcionario] WHERE id = @id";
        var worker = (await _db.LoadData<int?,dynamic>(sql, new {user.Id})).FirstOrDefault();
        if(worker != null && worker == user.Id) {

            sql = @"SELECT id FROM [dbo].[Admin] WHERE id = @id";
            var admin = (await _db.LoadData<int?,dynamic>(sql, new {user.Id})).FirstOrDefault();
            if(admin != null && admin == user.Id){
                return (user,"Admin");

            } else {

                return (user,"Funcionario");
                
            }

        } else {
            return (user,"Cliente");
        }
    }


    public Task InsertUtilizador(UtilizadorModel user){
        string sql = @"INSERT INTO [dbo].[Utilizador] (nome,email,contacto,password,nascimento,morada)
                        VALUES (@nome,@email,@contacto,@password,@nascimento,@morada);";     
        return _db.SaveData(sql, new {user.Nome,user.Email,user.Contacto,user.Password,user.Nascimento,user.Morada});
    }

    public async Task InsertFuncionario(Funcionario funcionario){
        string sql = @"SELECT id FROM [dbo].[Utilizador] WHERE email = @email;";
        var exists = await _db.LoadData<int,dynamic>(sql,new{ funcionario.Email });
        if(exists == null || exists.Count == 0) {
            sql = @"INSERT INTO Utilizador (nome,email,contacto,password,nascimento,morada)
                        values (@nome,@email,@contacto,@password,@nascimento,@morada);";
        
            string nome = funcionario.Nome;
            string email = funcionario.Email;
            string contacto = funcionario.Contacto;
            string password = funcionario.Password;
            DateTime nascimento = funcionario.Nascimento;
            string morada = funcionario.Morada;

            await _db.SaveData(sql,new {nome,email,contacto,password,nascimento,morada});
        }
        sql = @"SELECT id FROM [dbo].[Utilizador] WHERE email = @email;";
        exists = await _db.LoadData<int,dynamic>(sql,new{ funcionario.Email });
        sql = @"SELECT id FROM Funcionario WHERE id = @id;";
        int id = exists.FirstOrDefault();
        exists = await _db.LoadData<int,dynamic>(sql,new{id});
        if(exists != null && exists.Count != 0) return;

        sql = @"INSERT INTO [dbo].[Funcionario] (id,NIF,idLinha)
        VALUES (@id,@NIF,@idLinha);";
        Console.WriteLine(id);
        int NIF = funcionario.NIF;
        int idLinha = funcionario.IdLinha;
        await _db.SaveData(sql,new {id,NIF,idLinha});    
    }

    public async Task<List<Funcionario>> GetFuncionarios(){
        string sql = @"SELECT Utilizador.*,Funcionario.NIF,Funcionario.idLinha 
        FROM Utilizador JOIN Funcionario 
        ON Utilizador.id = Funcionario.id;";
        return await _db.LoadData<Funcionario,dynamic>(sql,new {});
    }       

    public async Task<int> GetIdLinhaFuncionario(int id){
        string sql = @"SELECT LinhaMontagem.idStock FROM [dbo].[LinhaMontagem] 
        JOIN [dbo].[Funcionario] ON LinhaMontagem.idLinha = Funcionario.idLinha";
        return (await _db.LoadData<int,dynamic>(sql,new { id })).FirstOrDefault();
    }
}