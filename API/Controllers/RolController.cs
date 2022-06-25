using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Aplicacion.Seguridad;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;


namespace API.Controllers
{
    public class RolController : MiControllerBase
    {
        [HttpPost("crear")]
        public async Task<ActionResult<Unit>> Crear(RoleNuevo.Ejecuta parametros){
            return await mediator.Send(parametros); 
        }
        [HttpGet("lista")]
        public async Task<ActionResult<List<IdentityRole>>> Lista(){
            return await mediator.Send(new RoleLista.Ejecuta{});
        }
        [HttpDelete("eliminar")]
        public async Task<ActionResult<Unit>> Crear(RoleEliminar.Ejecuta parametros){
            return await mediator.Send(parametros); 
        }   
        [HttpPost("agregarRolUsuario")]
        public async Task<ActionResult<Unit>> AgregarRolUsuario(UsuarioRolAgregar.Ejecuta parametros){
            return await mediator.Send(parametros);
        } 
        [HttpPost("eliminarRolUsuario")]
        public async Task<ActionResult<Unit>> EliminarRolUsuario(UsuarioRolEliminar.Ejecuta parametros){
            return await mediator.Send(parametros);
        } 
        [HttpPost("obtenerrolesusuario")]
        public async Task<ActionResult<List<string>>> obtenerrolesusuario(ObtenerRolesUsuario.Ejecuta parametros){
            return await mediator.Send(parametros);
        } 
    }
}