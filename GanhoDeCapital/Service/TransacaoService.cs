using GanhoDeCapital.Business;
using GanhoDeCapital.Entity;
using System.Collections.Generic;
using System.Text.Json;

namespace GanhoDeCapital.Service
{
    public class TransacaoService
    {
        public string Calcula(string entrada)
        {
            string taxa = string.Empty;
            var lista = Util.RetornaListaTransacao(entrada);

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
