using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Dominio;
using Aplicacion.Monedas;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MonedaController : MiControllerBase
    {
        
        [HttpGet]
        public async Task<ActionResult<List<Moneda>>> Get(){
            return await mediator.Send(new Consulta.Ejecutar());
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Post(Nuevo.Ejecuta data){
            return await mediator.Send(data);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(Guid id,Editar.Ejecutar data){
            data.MonedaId=id;
            return await mediator.Send(data);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id){
            return await mediator.Send(new Eliminar.Ejecutar{MonedaId=id});
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Moneda>> Detalle(Guid id){
            return await mediator.Send(new ConsultaId.Ejecutar{Id=id});
        }
    }
}