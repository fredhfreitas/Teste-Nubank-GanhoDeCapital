using GanhoDeCapital.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GanhoDeCapital.Business
{
    public class CalculadoraDeImpostos : ICalculadoraDeImpostos
    {
        decimal _mediaPonderadaAtual;
        decimal _prejuizo;
        decimal _lucro;
        decimal _valorDeCompra;
        bool _recalculaMedia;
        IList<Acao> _acoesProcessadas;
        bool _recalculaLucro;

        public IList<Taxa> Calcula(IList<Acao> listaDeAcoes)
        {
            IList<Taxa> taxas = new List<Taxa>();
            //Ações que serão processadas
            _acoesProcessadas = new List<Acao>();

            //identifica uma nova compra
            bool novaCompra = false;

            foreach (var acao in listaDeAcoes)
            {
                decimal imposto = 0;
                _acoesProcessadas.Add(acao);

                switch (acao.Operacao)
                {
                    case "buy":
                        novaCompra = true;
                        Compra(acao.Quantidade, acao.CustoUnitario);
                        break;
                    case "sell":
                        //Caso tenha ocorrido uma nova compra eu devo recalcular a média
                        if (novaCompra)
                            MediaPonderada(acao.Quantidade);

                        imposto = Venda(acao.Quantidade, acao.CustoUnitario);
                        novaCompra = false;
                        break;
                }

                //Inclui as taxas das ações que foram processadas
                taxas.Add(new Taxa { Tax = imposto.ToString("F") });
            }

            return taxas;
        }

        public decimal QuantidadeDeAcoesAtual()
        {
            decimal qtdAcaoCompra = _acoesProcessadas.Where(item => item.Operacao.Equals("buy")).Sum(item => item.Quantidade);
            decimal qtdVenda = _acoesProcessadas.Where(item => item.Operacao.Equals("sell")).Sum(item => item.Quantidade);

            return Math.Abs(qtdAcaoCompra - qtdVenda);
        }

        public void Compra(decimal quantidade, decimal custoUnitario)
        {
            //Se a média já foi calculada e é uma nova compra tenho que atualizar o valor de compra para o atual e recalcular a média na venda
            if (_mediaPonderadaAtual > 0)
            {
                _valorDeCompra = custoUnitario;
                _recalculaMedia = true;
            }

            //Se é a primeira compra
            if (_valorDeCompra == 0)
                _valorDeCompra += custoUnitario;
        }
        public decimal Venda(decimal quantidade, decimal custoUnitario)
        {
            decimal retorno = 0;
            //resolve o caso 5 verificar
            if (custoUnitario == _mediaPonderadaAtual)
            {
                _recalculaLucro = true;
            }

            decimal prejuizo = Prejuizo(quantidade, custoUnitario);
            decimal lucro = Lucro(quantidade, custoUnitario);

            //Se a transação resultou em prejuízo na compra
            if (prejuizo > 0 && lucro == 0)
            {
                return retorno;
            }

            DeduzLucroDoPrejuizo(lucro, prejuizo);

            if (_prejuizo > 0 || (_prejuizo > _lucro))
                return retorno;

            //Se ação de compra custo x quantidade for menor que 20000
            //não paga imposto
            var valorImpostoIsento = MenorQue20000(quantidade, custoUnitario);

            if (valorImpostoIsento)
            {
                return retorno;
            }

            retorno = PercentualSobreLucro();

            return retorno;
        }
        public void DeduzLucroDoPrejuizo(decimal lucro, decimal prejuizo)
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
        public decimal PercentualSobreLucro()
        {
            decimal imposto;

            imposto = (_lucro * 20) / 100;

            return imposto;
        }
        public bool MenorQue20000(decimal quantidade, decimal custoUnitario)
        {
            bool retorno = false;

            decimal total = quantidade * custoUnitario;

            if (total <= 20000)//Abaixo ou igual a este valor não paga imposto
            {
                retorno = true;
            }

            return retorno;
        }

        public decimal Lucro(decimal quantidade, decimal custoUnitario)
        {
            if ((custoUnitario > _mediaPonderadaAtual))
            {
                //var quantidadeAtual = QuantidadeDeAcoesAtual();

                //Se não teve lucro e nem prejuizo anterior eu devo calcular o lucro de maneira diferente
                if (_recalculaLucro)
                    _lucro = _valorDeCompra * quantidade;
                else
                    _lucro = System.Math.Abs(_valorDeCompra - custoUnitario) * quantidade;

            }
            else _lucro = 0;//Para não pegar valores anteriores

            return _lucro;
        }

        public decimal MediaPonderada(decimal quantidadeCompra)
        {
            decimal novaMedia = 0;
            var acoesCompra = _acoesProcessadas.Where(item => item.Operacao.Equals("buy"));
            decimal qtdAcaoCompra = acoesCompra.Sum(item => item.Quantidade * item.CustoUnitario);
            decimal quantidadeAtual = QuantidadeDeAcoesAtual();

            if (!_recalculaMedia)
            {
                novaMedia = ((quantidadeAtual * _mediaPonderadaAtual) + (qtdAcaoCompra)) / (quantidadeAtual + quantidadeCompra);
            }
            else
            {  //Se houve uma compra e todas as ações foram vendidas. O valor de compra vira a nova média
                decimal ultimaCompra = acoesCompra.LastOrDefault().CustoUnitario;

                novaMedia = ultimaCompra;
            }

            _mediaPonderadaAtual = Math.Round(novaMedia, 2);

            return _mediaPonderadaAtual;
        }

        public decimal Prejuizo(decimal quantidade, decimal custoUnitario)
        {
            //Prejuizo
            if (custoUnitario < _mediaPonderadaAtual)
            {
                if (_prejuizo > 0)
                {
                    var resultado = Math.Abs(((_valorDeCompra - custoUnitario)) * quantidade);

                    _prejuizo = _prejuizo - resultado;
                }
                else
                    _prejuizo = Math.Abs(_valorDeCompra - custoUnitario) * quantidade;

            }

            return _prejuizo;
        }

    }
}
