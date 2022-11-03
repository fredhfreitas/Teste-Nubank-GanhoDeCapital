using GanhoDeCapital.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GanhoDeCapital.Business
{
    public class CalculadoraDeImpostos : ICalculadoraDeImpostos
    {
        decimal _mediaPonderadaAtual;
        decimal _prejuizo;
        decimal _lucro;
        decimal _valorDeCompra;
        bool _recalculaMedia;
        bool _recalculaLucro;
        IList<Acao> _acoesProcessadas;

        /// <summary>
        /// Devolte todas as taxas das operações que foram processadas
        /// </summary>
        /// <param name="listaDeAcoes"></param>
        /// <returns></returns>
        public IList<Taxa> Calcula(IList<Acao> listaDeAcoes)
        {
            IList<Taxa> taxas = new List<Taxa>();
            //Ações que serão processadas
            _acoesProcessadas = new List<Acao>();

            //identifica uma nova compra
            bool novaCompra = false;
            decimal media = 0;
            decimal valorDeCompra = 0;
            foreach (var acao in listaDeAcoes)
            {
                decimal imposto = 0;
                _acoesProcessadas.Add(acao);

                switch (acao.Operacao)
                {
                    case "buy":
                        novaCompra = true;
                        //Atualiza o valor de compra
                        valorDeCompra = Compra(acao.CustoUnitario, media);
                        break;
                    case "sell":

                        //Caso tenha ocorrido uma nova compra eu devo recalcular a média
                        if (novaCompra)
                            media = MediaPonderada(acao.Quantidade, _mediaPonderadaAtual, _recalculaMedia, _acoesProcessadas);

                        imposto = Venda(acao.Quantidade, acao.CustoUnitario, media, valorDeCompra);
                        novaCompra = false;
                        break;
                }

                //Inclui as taxas das ações que foram processadas
                taxas.Add(new Taxa { Tax = imposto.ToString("F") });
            }

            return taxas;
        }

        public decimal Venda(decimal quantidade, decimal custoUnitario, decimal media, decimal valorDeCompra)
        {
            decimal prejuizo = CalculaPrejuizo(quantidade, custoUnitario, media, valorDeCompra);
            decimal lucro = CalculaLucro(quantidade, custoUnitario, media, valorDeCompra);

            decimal retorno = ResultouEmPrejuizo(prejuizo, lucro);

            DeduzLucroPrejuizo(lucro, prejuizo);

            //Se ação de compra custo x quantidade for menor que 20000
            //não paga imposto caso contrário tenho que calcular o percentual sobre o lucro
            if (!LucroMenorQue20000(quantidade, custoUnitario))
                retorno = PercentualSobreLucro(_lucro);

            return retorno;
        }

        /// <summary>
        /// Realiza os tratamentos para realizar a compra
        /// </summary>
        /// <param name="custoUnitario"></param>
        /// <param name="media"></param>
        /// <returns>Retorna o valor de Compra</returns>
        private decimal Compra(decimal custoUnitario, decimal media)
        {
            //Se a média já foi calculada e é uma nova compra tenho que atualizar o valor de compra para o atual e recalcular a média na venda
            if (media > 0)
            {
                _valorDeCompra = custoUnitario;
                _recalculaMedia = true;
            }

            //Se é a primeira compra
            if (_valorDeCompra == 0)
                _valorDeCompra = custoUnitario;

            return _valorDeCompra;
        }

        private decimal QuantidadeDeAcoesAtual(IList<Acao> acoesProcessadas)
        {
            decimal qtdAcaoCompra = acoesProcessadas.Where(item => item.Operacao.Equals("buy")).Sum(item => item.Quantidade);
            decimal qtdVenda = acoesProcessadas.Where(item => item.Operacao.Equals("sell")).Sum(item => item.Quantidade);

            return Math.Abs(qtdAcaoCompra - qtdVenda);
        }
        private decimal ResultouEmPrejuizo(decimal prejuizo, decimal lucro)
        {
            decimal retorno = 0;
            //Se a transação resultou em prejuízo na compra
            if (prejuizo > 0 && lucro == 0)
            {
                retorno = 0;
            }

            return retorno;
        }
        private void DeduzLucroPrejuizo(decimal lucro, decimal prejuizo)
        {
            /*deduzir prejuizo do lucro e possivelmente zerar criar uma regra*/
            if (lucro > prejuizo)
            {
                _lucro = lucro - prejuizo;
                _prejuizo = 0;
            }
            else
            {
                _prejuizo = prejuizo - lucro;
                _lucro = 0;
            }
        }
        public decimal PercentualSobreLucro(decimal lucro)
        {
            decimal imposto;

            imposto = (lucro * 20) / 100;

            return imposto;
        }
        public bool LucroMenorQue20000(decimal quantidade, decimal custoUnitario)
        {
            bool retorno = false;

            decimal total = quantidade * custoUnitario;

            if (total <= 20000)//Abaixo ou igual a este valor não paga imposto
            {
                retorno = true;
            }

            return retorno;
        }

        public decimal CalculaLucro(decimal quantidade, decimal custoUnitario, decimal media, decimal valorDeCompra)
        {

            //resolve o caso 5 verificar
            if (custoUnitario == media)
            {
                _recalculaLucro = true;
            }

            if ((custoUnitario > media))
            {
                //Se não teve lucro e nem prejuizo anterior eu devo calcular o lucro de maneira diferente
                if (_recalculaLucro)
                    _lucro = valorDeCompra * quantidade;
                else
                    _lucro = Math.Abs(valorDeCompra - custoUnitario) * quantidade;

            }
            else _lucro = 0;//Para não pegar valores anteriores

            return _lucro;
        }

        private decimal MediaPonderada(decimal quantidadeCompra, decimal mediaPonderadaAtual, bool recalculaMedia, IList<Acao> acoesProcessadas)
        {
            decimal novaMedia = 0;
            var acoesCompra = acoesProcessadas.Where(item => item.Operacao.Equals("buy"));
            decimal qtdAcaoCompra = acoesCompra.Sum(item => item.Quantidade * item.CustoUnitario);
            decimal quantidadeAtual = QuantidadeDeAcoesAtual(acoesProcessadas);

            if (!recalculaMedia)
            {
                novaMedia = ((quantidadeAtual * mediaPonderadaAtual) + (qtdAcaoCompra)) / (quantidadeAtual + quantidadeCompra);
            }
            else
            {  //Se houve uma compra e todas as ações foram vendidas. O valor de compra vira a nova média
                decimal ultimaCompra = acoesCompra.LastOrDefault().CustoUnitario;

                novaMedia = ultimaCompra;
            }

            _mediaPonderadaAtual = Math.Round(novaMedia, 2);

            return _mediaPonderadaAtual;
        }

        private decimal CalculaPrejuizo(decimal quantidade, decimal custoUnitario, decimal media, decimal valorDeCompra)
        {
            //Prejuizo
            if (custoUnitario < media)
            {
                if (_prejuizo > 0)
                {
                    var resultado = Math.Abs(((valorDeCompra - custoUnitario)) * quantidade);

                    _prejuizo = _prejuizo - resultado;
                }
                else
                    _prejuizo = Math.Abs(valorDeCompra - custoUnitario) * quantidade;
            }

            return _prejuizo;
        }

    }
}
