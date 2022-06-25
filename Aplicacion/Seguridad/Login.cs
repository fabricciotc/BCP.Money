using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class Login
    {
        public class Ejecuta : IRequest<UsuarioData>{
            public string Email{set;get;}
            public string Password{set;get;}
        }
        public class EjecutaValidacion : AbstractValidator<Ejecuta>{
             public EjecutaValidacion(){
                RuleFor( x => x.Email).NotEmpty();
                RuleFor( x => x.Password).NotEmpty();
             }   
        }
        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly SignInManager<Usuario> _signInManager;
            private readonly IJwtGenerador _jwtGenerador;
            public Manejador(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, IJwtGenerador jwtGenerador){
                this._signInManager=signInManager;
                this._userManager=userManager;
                this._jwtGenerador = jwtGenerador;

            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuario = await _userManager.FindByEmailAsync(request.Email);
                if(usuario==null){
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.Unauthorized); 
                }
                var roles = await _userManager.GetRolesAsync(usuario);
                var result = await _signInManager.CheckPasswordSignInAsync(usuario,request.Password,false);

                return result==SignInResult.Success?new UsuarioData { 
                    NombreCompleto = usuario.NombreCompleto, Username= usuario.UserName,
                    Token = _jwtGenerador.CrearToken(usuario,roles.ToList()), Email = usuario.Email
                }: throw new ManejadorExcepcion(System.Net.HttpStatusCode.Unauthorized); 
            }
        }
    }
}