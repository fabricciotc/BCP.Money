using System.Collections.Generic;
using MediatR;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using System.Linq;

namespace Aplicacion.Tipos_de_Cambio
{
    public class Consulta
    {
        public class Ejecutar: IRequest<List<Tipo_Cambio_DTO>> {}
        public class Manejador : IRequestHandler<Ejecutar, List<Tipo_Cambio_DTO>>
        {
            private readonly MonedasOnlineDbContext _context;
            public Manejador(MonedasOnlineDbContext context){
                this._context=context;
            }
            public async Task<List<Tipo_Cambio_DTO>> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                var tiposCambio = await _context.Tipo_Cambios
                    .Include(c=>c.MonedaDestino)
                    .Include(c=>c.MonedaOrigen)
                .ToListAsync();
                return tiposCambio.Select(c => new Tipo_Cambio_DTO{
                 Abreviacion=c.Abreviacion(),
                 Conversion=c.Conversion,
                 Descripcion=c.Descripcion,
                 fechaActualizacion=c.fechaActualizacion,
                 fechaCreacion=c.fechaCreacion,
                 Tipo_Cambio_Id=c.Tipo_Cambio_Id,
                 MonedaDestinoId=c.MonedaDestinoId,
                 MonedaDestino=c.MonedaDestino,
                 MonedaOrigen=c.MonedaOrigen,
                 MonedaOrigenId=c.MonedaOrigenId
                }).ToList();
            }
        }
    }
}