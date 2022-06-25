using System.Collections.Generic;
using MediatR;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;

namespace Aplicacion.Monedas
{
    public class Consulta
    {
        public class Ejecutar: IRequest<List<Moneda>> {}
        public class Manejador : IRequestHandler<Ejecutar, List<Moneda>>
        {
            private readonly MonedasOnlineDbContext _context;
            public Manejador(MonedasOnlineDbContext context){
                this._context=context;
            }
            public async Task<List<Moneda>> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var cursos = await _context.Monedas
                    .Include(c=>c.cambiosDestino)
                    .Include(c=>c.cambiosOrigen)
                    .ToListAsync();
                return cursos;
            }
        }
    }
}