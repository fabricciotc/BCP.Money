using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.ManejadorError;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class RoleEliminar
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
                if(await _roleManager.RoleExistsAsync(request.Nombre)){
                    var result = await _roleManager.DeleteAsync(await _roleManager.FindByNameAsync(request.Nombre));
                    if(result.Succeeded){
                        return Unit.Value;
                    }
                }
                throw new ManejadorExcepcion(HttpStatusCode.BadRequest,new {mensaje="No existe el Rol"});
            }
        }
    }
}