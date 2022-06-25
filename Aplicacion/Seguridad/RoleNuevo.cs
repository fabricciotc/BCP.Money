using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class RoleNuevo
    {
        public class Ejecuta : IRequest{
            public string Nombre{set;get;}
        }
        public class ValidaEjecuta: AbstractValidator<Ejecuta>{
            public ValidaEjecuta(){
                RuleFor(d=>d.Nombre).NotEmpty();
            }            
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly RoleManager<IdentityRole> _roleManager;
            public Manejador(RoleManager<IdentityRole> roleManager){
                _roleManager=roleManager;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.Nombre);
                if(role==null){
                    var result = await _roleManager.CreateAsync(new IdentityRole{ Name=request.Nombre });
                    return result.Succeeded? Unit.Value:
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.BadRequest,
                    new {Mensaje = "No se pudo guardar el rol"});
                }
                throw new ManejadorExcepcion(System.Net.HttpStatusCode.BadRequest,new {Mensaje = "Ya se tiene un Rol creado con ese mismo nombre"});
            }
        }
    }
}