using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class Cotizacion
    {
        public string AbreviacionOrigen { set; get; }
        public string AbreviacionDestino { set; get; }
        public decimal MontoInicial { set; get; }
        public string AbreviacionCambio { set; get; }
        public decimal MontoFinal { set; get; }
    }
}
