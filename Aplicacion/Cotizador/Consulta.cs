using System.Collections.Generic;
using MediatR;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using System.Linq;
using Aplicacion.ManejadorError;

namespace Aplicacion.Cotizador
{
    public class Consulta
    {
        public class Ejecutar: IRequest<Cotizacion> {
            public string AbreviacionOrigen { set; get; }
            public string AbreviacionDestino { set; get; }
            public decimal MontoInicial { set; get; }
            public bool Preferencial { set; get; }
        }
        public class Manejador : IRequestHandler<Ejecutar, Cotizacion>
        {
            private readonly MonedasOnlineDbContext _context;
            public Manejador(MonedasOnlineDbContext context){
                this._context=context;
            }
            public async Task<Cotizacion> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var cursos = await _context.Tipo_Cambios
                     .Include(c => c.MonedaOrigen)
                     .Include(c => c.MonedaDestino)
                     .Where(c => c.MonedaOrigen.Abreviacion == request.AbreviacionOrigen
                     && c.MonedaDestino.Abreviacion == request.AbreviacionDestino
                     )
                     .FirstOrDefaultAsync();
                if (cursos == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new { curso = "No se encontro registro del tipo de cambio que deseas hacer" });
                }
                //TASA DE 20% DE DESCUENTO PARA CLIENTES PREFERENCIALES
                double tasaPreferencial = request.Preferencial == true ? 0.8 : 1;
                ////////////////////////////////////////////////////////
                Cotizacion coti = new Cotizacion
                {
                    AbreviacionCambio = cursos.Abreviacion(),
                    AbreviacionDestino = request.AbreviacionDestino,
                    AbreviacionOrigen = request.AbreviacionOrigen,
                    MontoInicial = request.MontoInicial,
                    MontoFinal = request.MontoInicial * cursos.Conversion * (decimal)tasaPreferencial
                };
                return coti;
            }
        }
    }
}