using Aplicacion.Cotizador;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using API.Controllers;

namespace API.Controllers
{
    public class CotizarController : MiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Cotizacion>> Post(Consulta.Ejecutar data)
        {
            return await mediator.Send(data);
        }
    }
}
