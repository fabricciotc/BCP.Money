using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Monedas
{
    public class Eliminar
    {
        public class Ejecutar : IRequest{
            public Guid MonedaId {set;get;}
        }
        
     
        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly MonedasOnlineDbContext _context;
            public Manejador(MonedasOnlineDbContext context){
                this._context=context;
            }
            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
               
                 Moneda moneda = await _context.Monedas.FindAsync(request.MonedaId);
                    if(moneda==null){
                        // throw new Exception("Curso no encontrado");
                        throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound,new {curso="No se encontro la Moneda"});
                    }
                var list = await _context.Tipo_Cambios.Where(c => c.MonedaDestinoId == request.MonedaId ||
                c.MonedaOrigenId == request.MonedaId).ToListAsync();
                _context.RemoveRange(list);
                _context.Remove(moneda);
                var result=await _context.SaveChangesAsync();
                return result>0?Unit.Value:throw new System.NotImplementedException();
            }
        }
    }
}