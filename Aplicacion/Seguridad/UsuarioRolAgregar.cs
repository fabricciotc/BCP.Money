using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class UsuarioRolAgregar
    {
        public class Ejecuta : IRequest{
            public string Username{set;get;}
            public string Rolname{set;get;}
        }
        public class EjecutaValidador: AbstractValidator<Ejecuta>{
            public EjecutaValidador(){
                RuleFor(d=>d.Username).NotEmpty();
                RuleFor(d=>d.Rolname).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            public Manejador(RoleManager<IdentityRole> roleManager,UserManager<Usuario> userManager){
                    _roleManager=roleManager;
                    _userManager=userManager;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByNameAsync(request.Rolname);
                if(role==null){
                    throw new System.Exception("Role no encontrado");
                }
                var usuarioItem = await _userManager.FindByNameAsync(request.Username);
                  if(usuarioItem==null){
                    throw new System.Exception("Usuario no encontrado");
                }
                var reuslt = await _userManager.AddToRoleAsync(usuarioItem,role.Name);
                if(reuslt.Succeeded){
                    return Unit.Value;
                }
                throw new System.Exception("No se puso agregar al usuario en el rol");
            }
        }
    }
}