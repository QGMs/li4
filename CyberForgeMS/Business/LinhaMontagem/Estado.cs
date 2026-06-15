using CyberForgeMS.Business.Encomendas;
using CyberForgeMS.Business.Stock;

namespace CyberForgeMS.Business.LinhaMontagem;


public class Estado {
    public List<Componente> PorMontar {get; set;}
    public List<Componente> Montado {get; set;}
    public int TempoRestanteProducao {get; set;}
    public Produto aSerFeito {get; set;}
    public List<Produto> PorFazer {get; set;} 

    public Encomenda? Encomenda {get; set;}

}