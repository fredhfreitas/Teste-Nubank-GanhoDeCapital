using Xunit;
using GanhoDeCapital.Business;
using GanhoDeCapital.Model;
using System.Collections.Generic;
using System.Text.Json;
using GanhoDeCapital;
using GanhoDeCapital.Service;

namespace GanhoDeCapitalTeste.Business
{
    public class CalculadoraDeImpostosTest
    {
        ICalculadoraDeImpostos calculadoraDeImpostos;

        [Theory]
        [InlineData(15, 50, 10, 15)]
        public void VerificaSeVendaRetornaTaxaIgualAZero(decimal quantidade, decimal custoUnitario, decimal media, decimal valorDeCompra)
        {
            calculadoraDeImpostos = new CalculadoraDeImpostos();
            
            var retorno = calculadoraDeImpostos.Venda(quantidade, custoUnitario, media, valorDeCompra);
            
            Assert.True(retorno == 0);
        }

        [Theory]
        [InlineData(15000)]
        public void VerificaSePercentualSobreLucroEIgualA3000(decimal lucro) {
            
            calculadoraDeImpostos = new CalculadoraDeImpostos();

            var retorno = calculadoraDeImpostos.PercentualSobreLucro(lucro);

            Assert.Equal(3000, retorno);
        }
    }
}
