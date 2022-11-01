﻿using GanhoDeCapital.Model;
using System.Collections.Generic;

namespace GanhoDeCapital.Business
{
    public interface ICalculadoraDeImpostos
    {
        IList<Taxa> Calcula(IList<Acao> listaDeAcoes);
    }
}