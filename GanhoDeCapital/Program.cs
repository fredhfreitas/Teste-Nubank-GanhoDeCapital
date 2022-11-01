using GanhoDeCapital.Business;
using GanhoDeCapital.Service;
using System;

namespace GanhoDeCapital
{
    internal class Program
    {   
        static void Main(string[] args)
        {
            Console.WriteLine("***********Entrada*************");
            Console.WriteLine("Informe os valores:");

            var entrada = Console.ReadLine();

            ITransacaoService transacaoService = new TransacaoService();

            Console.WriteLine("***********Saída*************");
            Console.WriteLine(transacaoService.Calcula(entrada));
        }
    }
}
