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

namespace Aplicacion.Tipos_de_Cambio
{
    public class Eliminar
    {
        public class Ejecutar : IRequest{
            public Guid Tipo_Cambio_Id {set;get;}
        }
        
     
        public class Manejador : IRequestHandler<Ejecutar>
        {
            private readonly MonedasOnlineDbContext _context;
            public Manejador(MonedasOnlineDbContext context){
                this._context=context;
            }
            public async Task<Unit> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
               
                 Tipo_Cambio moneda = await _context.Tipo_Cambios.FirstOrDefaultAsync(x=>x.Tipo_Cambio_Id==request.Tipo_Cambio_Id);
                    if(moneda==null){
                        // throw new Exception("Curso no encontrado");
                        throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound,new {curso="No se encontro el Tipo de Cambio"});
                    }
                _context.Remove(moneda);
                var result=await _context.SaveChangesAsync();
                return result>0?Unit.Value:throw new System.NotImplementedException();
            }
        }
    }
}