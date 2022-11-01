using System;
using System.Collections.Generic;
using System.Text;

namespace GanhoDeCapital
{
    public static class Util
    {
        /// <summary>
        /// Retorna a lista de transações formatada
        /// </summary>
        /// <param name="entrada">string</param>
        /// <returns>string</returns>
        public static List<string> RetornaListaTransacao(string entrada)
        {
            List<string> transacoes = new List<string>();

            string transacao = string.Empty;

            foreach (var valor in entrada)
            {
                transacao += valor.ToString();

                if (valor.Equals(']'))
                {
                    transacoes.Add(transacao);
                    transacao = string.Empty;
                }
            }

            return transacoes;

        }
    }
}
