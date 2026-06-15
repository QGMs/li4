using CyberForgeMS.Business.Utilizadores;
using CyberForgeMS.Data.Utilizadores;

namespace CyberForgeMS.Data.Utilizadores;

public interface IUtilizadoresDAO{
    Task<List<UtilizadorModel>> GetUtilizadores();

    Task InsertUtilizador(UtilizadorModel user);

    Task InsertFuncionario(Funcionario funcionario);

    Task<(UtilizadorModel?,string)> ValidateUser(string username, string password);

    Task<UtilizadorModel?> GetUtilizadorById(string username);

    Task<int> GetIdLinhaFuncionario(int id);

    Task<List<Funcionario>> GetFuncionarios();
}