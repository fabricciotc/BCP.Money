using System;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using AutoMapper;
using Dominio;
using System.Linq;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Tipos_de_Cambio
{
    public class ConsultaId
    {
       public class Ejecutar: IRequest<Tipo_Cambio_DTO> {
           public Guid Id{set;get;}
       }
        public class Manejador : IRequestHandler<Ejecutar, Tipo_Cambio_DTO>
        {
            private readonly MonedasOnlineDbContext _context;
            public Manejador(MonedasOnlineDbContext context){
                this._context=context;
            }
            public async Task<Tipo_Cambio_DTO> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
                Tipo_Cambio curso = await _context.Tipo_Cambios
                    .Include(c=>c.MonedaOrigen)
                    .Include(c=>c.MonedaDestino)
                    .FirstOrDefaultAsync( x=> x.Tipo_Cambio_Id == request.Id);
                if(curso==null){
                        throw new ManejadorExcepcion(System.Net.HttpStatusCode.NotFound,new {curso="No se encontro el Tipo de Cambio"});
                }
                return  new Tipo_Cambio_DTO
                {
                    Abreviacion = curso.Abreviacion(),
                    Conversion = curso.Conversion,
                    Descripcion = curso.Descripcion,
                    fechaActualizacion = curso.fechaActualizacion,
                    fechaCreacion = curso.fechaCreacion,
                    Tipo_Cambio_Id = curso.Tipo_Cambio_Id,
                    MonedaDestinoId = curso.MonedaDestinoId,
                    MonedaDestino = curso.MonedaDestino,
                    MonedaOrigen = curso.MonedaOrigen,
                    MonedaOrigenId = curso.MonedaOrigenId
                };
            }
        }    
    }
}