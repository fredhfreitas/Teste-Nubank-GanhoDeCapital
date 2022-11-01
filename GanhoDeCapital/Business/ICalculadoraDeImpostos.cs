using GanhoDeCapital.Entity;
using System.Collections.Generic;

namespace GanhoDeCapital.Business
{
    public interface ICalculadoraDeImpostos
    {
        IList<Taxa> Calcula(IList<Acao> listaDeAcoes);
        void Compra(decimal quantidade, decimal custoUnitario);
        void DeduzLucroDoPrejuizo(decimal lucro, decimal prejuizo);
        decimal Lucro(decimal quantidade, decimal custoUnitario);
        decimal MediaPonderada(decimal quantidadeCompra);
        bool MenorQue20000(decimal quantidade, decimal custoUnitario);
        decimal PercentualSobreLucro();
        decimal Prejuizo(decimal quantidade, decimal custoUnitario);
        decimal QuantidadeDeAcoesAtual();
        decimal Venda(decimal quantidade, decimal custoUnitario);
    }
}