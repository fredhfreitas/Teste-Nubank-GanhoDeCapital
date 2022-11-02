using GanhoDeCapital.Model;
using System.Collections.Generic;

namespace GanhoDeCapital.Business
{
    public interface ICalculadoraDeImpostos
    {
        IList<Taxa> Calcula(IList<Acao> listaDeAcoes);
        decimal Venda(decimal quantidade, decimal custoUnitario, decimal media, decimal valorDeCompra);
        decimal PercentualSobreLucro(decimal lucro);
    }
}