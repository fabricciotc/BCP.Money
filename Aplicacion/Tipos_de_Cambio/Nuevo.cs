using System.Security.Cryptography.X509Certificates;
using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;
using FluentValidation;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Aplicacion.ManejadorError;

namespace Aplicacion.Tipos_de_Cambio
{
    public class Nuevo
    {
        public class Ejecuta : IRequest{
            public string Descripcion { set; get; }
            public decimal Conversion { set; get; }
            public Guid MonedaOrigenId { set; get; }
            public Guid MonedaDestinoId { set; get; }
        }
        public class EjecutaValidacion : AbstractValidator<Ejecuta>{
            public EjecutaValidacion(){
                RuleFor(x => x.Descripcion).NotEmpty();
                RuleFor(x => x.Conversion).NotEmpty();
                RuleFor(x => x.MonedaOrigenId).NotEmpty();
                RuleFor(x => x.MonedaDestinoId).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly MonedasOnlineDbContext _context;
            public Manejador(MonedasOnlineDbContext context){
                this._context=context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var monedaOrigen = await _context.Monedas.FirstOrDefaultAsync(c => c.MonedaId == request.MonedaOrigenId);
                if(monedaOrigen == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new { curso = "No se encontro la Moneda de Origen" });
                }
                var monedaFinal = await _context.Monedas.FirstOrDefaultAsync(c => c.MonedaId == request.MonedaDestinoId);
                if(monedaFinal == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new { curso = "No se encontro la Moneda de Destino" });
                }
                Guid _TipoCambioId = Guid.NewGuid();
                _context.Add( new Tipo_Cambio{
                    Tipo_Cambio_Id = _TipoCambioId,
                    Descripcion= request.Descripcion,
                    fechaActualizacion= DateTime.UtcNow,
                    fechaCreacion = DateTime.UtcNow,
                    Conversion=request.Conversion,
                    MonedaDestinoId=request.MonedaDestinoId,
                    MonedaOrigenId=request.MonedaOrigenId
                });
                var result = await _context.SaveChangesAsync();
                return result>0?Unit.Value:throw new Exception("No se pudo insertar el Tipo de Cambio");
            }

         
        }

    }
}