using System;
using Xunit;
using GanhoDeCapital.Business;
using GanhoDeCapital.Entity;
using System.Collections.Generic;
using System.Text.Json;

namespace GanhoDeCapitalTeste.Business
{
    public class CalculadoraDeImpostosTeste
    {
        [Fact]
        public void VerificaSeQuantidadeDeAcoesProcessadasAtualEIgualAZero()
        {
            string entrada = "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}]";

            var acoes = JsonSerializer.Deserialize<List<Acao>>(entrada);

            CalculadoraDeImpostos calculadora = new CalculadoraDeImpostos();
            calculadora.Calcula(acoes);
            var retorno = calculadora.QuantidadeDeAcoesAtual();

            Assert.Equal(0, retorno);
        }

        [Fact]
        public void VerificaSeMediaEIgualA10()
        {
            string entrada = "[{\"operation\":\"buy\", \"unit-cost\":10.00, \"quantity\": 100},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50},{\"operation\":\"sell\", \"unit-cost\":15.00, \"quantity\": 50}]";

            var acoes = JsonSerializer.Deserialize<List<Acao>>(entrada);

            CalculadoraDeImpostos calculadora = new CalculadoraDeImpostos();
            calculadora.Calcula(acoes);

            var retorno = calculadora.MediaPonderada(100);

            Assert.Equal(10, retorno);
        }        
    }
}
