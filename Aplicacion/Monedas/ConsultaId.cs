using System;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using AutoMapper;
using Dominio;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Monedas
{
    public class ConsultaId
    {
       public class Ejecutar: IRequest<Moneda> {
           public Guid Id{set;get;}
       }
        public class Manejador : IRequestHandler<Ejecutar, Moneda>
        {
            private readonly MonedasOnlineDbContext _context;
            public Manejador(MonedasOnlineDbContext context){
                this._context=context;
            }
            public async Task<Moneda> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                Moneda curso = await _context.Monedas.FirstOrDefaultAsync( x=> x.MonedaId == request.Id);
                if(curso==null){
                        throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound,new {curso="No se encontro la Moneda"});
                }
                return (curso);
            }
        }    
    }
}