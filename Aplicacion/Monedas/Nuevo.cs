using System.Security.Cryptography.X509Certificates;
using System;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using MediatR;
using Persistencia;
using FluentValidation;
using System.Collections.Generic;

namespace Aplicacion.Monedas
{
    public class Nuevo
    {
        public class Ejecuta : IRequest{
            public string Abreviacion { set; get; }
            public string Descripcion { set; get; }
        }
        public class EjecutaValidacion : AbstractValidator<Ejecuta>{
            public EjecutaValidacion(){
                RuleFor( x => x.Abreviacion).NotEmpty();
                RuleFor( x => x.Descripcion).NotEmpty();
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
                //_context.Database.BeginTransaction();
                Guid _MonedaId = Guid.NewGuid();
                _context.Add( new Moneda{
                    MonedaId = _MonedaId,
                    Abreviacion=request.Abreviacion, Descripcion= request.Descripcion,
                    fechaActualizacion= DateTime.UtcNow,
                    fechaCreacion = DateTime.UtcNow
                });
                var result = await _context.SaveChangesAsync();
                //await _context.Database.CommitTransactionAsync();
                return result>0?Unit.Value:throw new Exception("No se pudo insertar la Moneda");
            }

         
        }

    }
}