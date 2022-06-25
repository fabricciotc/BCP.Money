using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class ObtenerRolesUsuario
    {
        public class Ejecuta : IRequest<List<string>>{
            public string Username{set;get;}
        }
        public class EjecutaValidador: AbstractValidator<Ejecuta>{
            public EjecutaValidador(){
                RuleFor(x=>x.Username).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta, List<string>>
        {
            private readonly UserManager<Usuario> _userManager;
            public Manejador(UserManager<Usuario> userManager){
                    _userManager=userManager;
            }
            public async Task<List<string>> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuarioItem = await _userManager.FindByNameAsync(request.Username);
                if(usuarioItem==null){
                    throw new System.Exception("Usuario no encontrado");
                }
                var lista = await _userManager.GetRolesAsync(usuarioItem);
                if(lista.Count==0){
                    throw new System.Exception("Usuario no tiene roles asignados");
                }
                return lista.ToList();
                throw new System.NotImplementedException();
            }
        }
    }
}