﻿using GanhoDeCapital.Business;
using GanhoDeCapital.Model;
using System.Collections.Generic;
using System.Text.Json;

namespace GanhoDeCapital.Service
{
    public class TransacaoService : ITransacaoService
    {
        public string RetornaTaxas(string entrada)
        {
            string taxa = string.Empty;
            //Retorna a lista de ações em string
            var lista = Util.RetornaListaTransacao(entrada);
            //TODO:implementar métodos assincronos no futuro
            foreach (var valor in lista)
            {
                var acoes = JsonSerializer.Deserialize<List<Acao>>(valor);

                CalculadoraDeImpostos calculadoraDeImpostos = new CalculadoraDeImpostos();

                taxa += JsonSerializer.Serialize(calculadoraDeImpostos.Calcula(acoes));
            }

            return taxa;
        }
    }
}
