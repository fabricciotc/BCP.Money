using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Dominio;
using Aplicacion.Tipos_de_Cambio;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TipoCambioController : MiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Tipo_Cambio_DTO>>> Get()
        {
            return await mediator.Send(new Consulta.Ejecutar());
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Post(Nuevo.Ejecuta data)
        {
            return await mediator.Send(data);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(Guid id, Editar.Ejecutar data)
        {
            data.Tipo_Cambio_Id = id;
            return await mediator.Send(data);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await mediator.Send(new Eliminar.Ejecutar { Tipo_Cambio_Id = id });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipo_Cambio_DTO>> Detalle(Guid id)
        {
            return await mediator.Send(new ConsultaId.Ejecutar { Id = id });
        }
    }
}