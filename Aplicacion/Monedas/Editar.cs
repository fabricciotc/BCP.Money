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

namespace Aplicacion.Monedas
{
    public class Editar
    {
        public class Ejecutar : IRequest{
            public Guid MonedaId{set;get;}
            public string Abreviacion { set; get; }
            public string Descripcion { set; get; }
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
                Moneda moneda = await _context.Monedas.FindAsync(request.MonedaId);
                if(moneda==null){
                        throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound,new {curso="No se encontro la Moneda"});
                }
                moneda.Abreviacion= request.Abreviacion ?? moneda.Abreviacion;
                moneda.Descripcion= request.Descripcion ?? moneda.Descripcion;
                moneda.fechaActualizacion= DateTime.UtcNow;
                
                var result= await _context.SaveChangesAsync();
                return result>0?Unit.Value:throw new Exception("Moneda no actulizada");
            }
        }
    }
}