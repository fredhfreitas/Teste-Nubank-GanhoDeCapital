using GanhoDeCapital.Business;
using Xunit;

namespace GanhoDeCapitalTeste.Business
{
    public class CalculadoraDeImpostosTest
    {
        ICalculadoraDeImpostos calculadoraDeImpostos;

        [Theory]
        [InlineData(15, 50, 10, 15)]
        [InlineData(10, 20, 15, 20)]
        public void VerificaSeVendaRetornaTaxaIgualAZero(decimal quantidade, decimal custoUnitario, decimal media, decimal valorDeCompra)
        {
            calculadoraDeImpostos = new CalculadoraDeImpostos();
            
            var retorno = calculadoraDeImpostos.Venda(quantidade, custoUnitario, media, valorDeCompra);
            
            Assert.True(retorno == 0);
        }

        [Theory]
        [InlineData(15000)]
        public void VerificaSePercentual20PoercentoSobreLucro(decimal lucro) {
            
            calculadoraDeImpostos = new CalculadoraDeImpostos();

            var retorno = calculadoraDeImpostos.PercentualSobreLucro(lucro);
            var imposto20Porcento = (lucro * 20) / 100;
            
            Assert.Equal(retorno, imposto20Porcento);
        }
    }
}
