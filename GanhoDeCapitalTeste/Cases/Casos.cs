using Xunit;
using GanhoDeCapital.Business;
using GanhoDeCapital.Model;
using System.Collections.Generic;
using System.Text.Json;
using GanhoDeCapital;
using GanhoDeCapital.Service;

namespace GanhoDeCapitalTeste.Casos
{
    public class Casos
    {
        ICalculadoraDeImpostos calculadoraDeImpostos;

        [Theory]
        [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}]")]
        public void Caso1(string entrada)
        {
            calculadoraDeImpostos = new CalculadoraDeImpostos();
            var acoes = JsonSerializer.Deserialize<IList<Acao>>(entrada);
            var retorno = calculadoraDeImpostos.Calcula(acoes);

            var taxaComparacao = new List<Taxa>();
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });

            var obj1Str = JsonSerializer.Serialize(retorno);
            var obj2Str = JsonSerializer.Serialize(taxaComparacao);

            Assert.True(obj1Str.Equals(obj2Str));
        }

        [Theory]
        [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}][{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000},{\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}]")]
        public void Caso1MaisCaso2(string entrada)
        {
            string taxa = string.Empty;
            var lista = Util.RetornaListaTransacao(entrada);

            foreach (var valor in lista)
            {
                var acoes = JsonSerializer.Deserialize<List<Acao>>(valor);

                calculadoraDeImpostos = new CalculadoraDeImpostos();

                taxa += JsonSerializer.Serialize(calculadoraDeImpostos.Calcula(acoes));
            }

            var taxaComparacao1 = new List<Taxa>();
            taxaComparacao1.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao1.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao1.Add(new Taxa() { Tax = "0,00" });

            var taxaComparacao2 = new List<Taxa>();
            taxaComparacao2.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao2.Add(new Taxa() { Tax = "10000,00" });
            taxaComparacao2.Add(new Taxa() { Tax = "0,00" });

            string obj1Str = JsonSerializer.Serialize(taxaComparacao1);
            string obj2Str = JsonSerializer.Serialize(taxaComparacao2);

            Assert.True(taxa.Equals(obj1Str+obj2Str));
        }

        [Theory]
        [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 5000},{\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000}]")]
        public void Caso2(string entrada)
        {
            calculadoraDeImpostos = new CalculadoraDeImpostos();
            var acoes = JsonSerializer.Deserialize<IList<Acao>>(entrada);
            var retorno = calculadoraDeImpostos.Calcula(acoes);

            var taxaComparacao = new List<Taxa>();
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "10000,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });

            var obj1Str = JsonSerializer.Serialize(retorno);
            var obj2Str = JsonSerializer.Serialize(taxaComparacao);

            Assert.True(obj1Str.Equals(obj2Str));
        }

        [Theory]
        [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":5.00, \"quantity\": 5000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 3000}]")]
        public void Caso3(string entrada)
        {
            calculadoraDeImpostos = new CalculadoraDeImpostos();
            var acoes = JsonSerializer.Deserialize<IList<Acao>>(entrada);
            var retorno = calculadoraDeImpostos.Calcula(acoes);

            var taxaComparacao = new List<Taxa>();
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "1000,00" });

            var obj1Str = JsonSerializer.Serialize(retorno);
            var obj2Str = JsonSerializer.Serialize(taxaComparacao);

            Assert.True(obj1Str.Equals(obj2Str));
        }

        [Theory]
        [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\": 5000},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 10000}]")]
        public void Caso4(string entrada)
        {
            calculadoraDeImpostos = new CalculadoraDeImpostos();
            var acoes = JsonSerializer.Deserialize<IList<Acao>>(entrada);
            var retorno = calculadoraDeImpostos.Calcula(acoes);

            var taxaComparacao = new List<Taxa>();
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });

            var obj1Str = JsonSerializer.Serialize(retorno);
            var obj2Str = JsonSerializer.Serialize(taxaComparacao);

            Assert.True(obj1Str.Equals(obj2Str));
        }

        [Theory]
        [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"buy\", \"unit-cost\":25.00, \"quantity\": 5000},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\": 5000}]")]
        public void Caso5(string entrada)
        {
            calculadoraDeImpostos = new CalculadoraDeImpostos();
            var acoes = JsonSerializer.Deserialize<IList<Acao>>(entrada);
            var retorno = calculadoraDeImpostos.Calcula(acoes);

            var taxaComparacao = new List<Taxa>();
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "10000,00" });

            var obj1Str = JsonSerializer.Serialize(retorno);
            var obj2Str = JsonSerializer.Serialize(taxaComparacao);

            Assert.True(obj1Str.Equals(obj2Str));
        }

        [Theory]
        [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":2.00, \"quantity\": 5000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000},{\"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\": 1000}]")]
        public void Caso6(string entrada)
        {
            calculadoraDeImpostos = new CalculadoraDeImpostos();
            var acoes = JsonSerializer.Deserialize<IList<Acao>>(entrada);
            var retorno = calculadoraDeImpostos.Calcula(acoes);

            var taxaComparacao = new List<Taxa>();
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "3000,00" });

            var obj1Str = JsonSerializer.Serialize(retorno);
            var obj2Str = JsonSerializer.Serialize(taxaComparacao);

            Assert.True(obj1Str.Equals(obj2Str));
        }

        [Theory]
        [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":2.00, \"quantity\": 5000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000},{\"operation\":\"sell\", \"unit-cost\":20.00, \"quantity\": 2000},{\"operation\":\"sell\", \"unit-cost\":25.00, \"quantity\": 1000},{\"operation\":\"buy\", \"unit-cost\":20.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 5000},{\"operation\":\"sell\", \"unit-cost\":30.00, \"quantity\": 4350},{\"operation\":\"sell\", \"unit-cost\":30.00, \"quantity\": 650}]")]
        public void Caso7(string entrada)
        {
            calculadoraDeImpostos = new CalculadoraDeImpostos();
            var acoes = JsonSerializer.Deserialize<IList<Acao>>(entrada);
            var retorno = calculadoraDeImpostos.Calcula(acoes);

            var taxaComparacao = new List<Taxa>();
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "3000,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "3700,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });

            var obj1Str = JsonSerializer.Serialize(retorno);
            var obj2Str = JsonSerializer.Serialize(taxaComparacao);

            Assert.True(obj1Str.Equals(obj2Str));
        }

        [Theory]
        [InlineData("[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":50.00, \"quantity\": 10000},{\"operation\":\"buy\", \"unit-cost\":20.00, \"quantity\": 10000},{\"operation\":\"sell\", \"unit-cost\":50.00, \"quantity\": 10000}]")]
        public void Caso8(string entrada)
        {
            calculadoraDeImpostos = new CalculadoraDeImpostos();
            var acoes = JsonSerializer.Deserialize<IList<Acao>>(entrada);
            var retorno = calculadoraDeImpostos.Calcula(acoes);

            var taxaComparacao = new List<Taxa>();
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "80000,00" });
            taxaComparacao.Add(new Taxa() { Tax = "0,00" });
            taxaComparacao.Add(new Taxa() { Tax = "60000,00" });

            var obj1Str = JsonSerializer.Serialize(retorno);
            var obj2Str = JsonSerializer.Serialize(taxaComparacao);

            Assert.True(obj1Str.Equals(obj2Str));
        }
    }
}
