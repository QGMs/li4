using CyberForgeMS.Data.Utilizadores;
using CyberForgeMS.Pages;
using Microsoft.Net.Http.Headers;

namespace CyberForgeMS.Business.Utilizadores;


public class UserState{
    public string Role {get; set;} = string.Empty;

    public UtilizadorModel? User {get; set;} = null;

    public int IdLinha {get; set;} = -1;

    public event Action? OnChange;

    public void SetState(UtilizadorModel? utilizador,string role){
        User = utilizador;
        Role = role;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();

}