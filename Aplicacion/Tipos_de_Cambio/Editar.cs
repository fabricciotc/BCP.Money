using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Persistencia;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Tipos_de_Cambio
{
    public class Editar
    {
        public class Ejecutar : IRequest{
            public Guid Tipo_Cambio_Id { set; get; }
            public string Descripcion { set; get; }
            public decimal? Conversion { set; get; }
            public Guid? MonedaOrigenId { set; get; }
            public Guid? MonedaDestinoId { set; get; }
        }
        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly MonedasOnlineDbContext _context;
            public Manejador(MonedasOnlineDbContext context)
            {
                this._context=context;
            }


            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                Tipo_Cambio tipoCambio = await _context.Tipo_Cambios.FirstOrDefaultAsync(x=>x.Tipo_Cambio_Id==request.Tipo_Cambio_Id);
                if(tipoCambio == null){
                        throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound,new {curso="No se encontro el Tipo de Cambio"});
                }
                var monedaOrigen = await _context.Monedas.FirstOrDefaultAsync(c => c.MonedaId == request.MonedaOrigenId);
                if (monedaOrigen == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new { curso = "No se encontro la Moneda de Origen" });
                }
                var monedaFinal = await _context.Monedas.FirstOrDefaultAsync(c => c.MonedaId == request.MonedaDestinoId);
                if (monedaFinal == null)
                {
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound, new { curso = "No se encontro la Moneda de Destino" });
                }
                tipoCambio.Descripcion= request.Descripcion ?? tipoCambio.Descripcion;
                tipoCambio.MonedaDestinoId = request.MonedaDestinoId ?? tipoCambio.MonedaDestinoId;
                tipoCambio.MonedaOrigenId = request.MonedaOrigenId ?? tipoCambio.MonedaOrigenId;
                tipoCambio.Conversion = request.Conversion ?? tipoCambio.Conversion;
                tipoCambio.fechaActualizacion= DateTime.UtcNow;
                
                var result= await _context.SaveChangesAsync();
                return result>0?Unit.Value:throw new Exception("Tipo de cambio no actulizado");
            }
        }
    }
}