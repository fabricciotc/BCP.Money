using System.Diagnostics.Tracing;
using System.Threading.Tasks;
using Aplicacion.Seguridad;
using Dominio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [AllowAnonymous]
    public class UsuarioController : MiControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<UsuarioData>> Login(Login.Ejecuta parametros){
            return await mediator.Send(parametros);
        }
        [HttpPost("registrar")]
        public async Task<ActionResult<UsuarioData>> Registrar(Registrar.Ejecuta parametros){
            return await mediator.Send(parametros);
        }
        [HttpGet]
        public async Task<ActionResult<UsuarioData>> ObtenerUsuario(){
            return await mediator.Send(new UsuarioActual.Ejecutar());
        }
        [HttpPut]
        public async Task<ActionResult<UsuarioData>> ActualizarUsuario(UsuarioActulizar.Ejecuta parametros){
            return await mediator.Send(parametros);
        }
    }
}