using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GanhoDeCapital.Entity
{
    public class Acao
    {
        private string _operacao;
        private decimal _custoUnitario;
        private decimal _quantidade;

        [JsonPropertyName("operation")]
        public string Operacao { get => _operacao; set => _operacao = value; }

        [JsonPropertyName("unit-cost")]
        public decimal CustoUnitario { get => _custoUnitario; set => _custoUnitario = Math.Round(value, 2); }
        [JsonPropertyName("quantity")]
        public decimal Quantidade { get => _quantidade; set => _quantidade = Math.Round(value, 2); }
    }
}
