using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;

namespace Aplicacion.Seguridad
{
    public class UsuarioActulizar
    {
        public class Ejecuta : IRequest<UsuarioData>{
                public string NombreCompleto{set;get;}
                public string Email{set;get;}
                public string Password{set;get;}
                public string Username{set;get;}             
        }
        public class EjecutaValidador: AbstractValidator<Ejecuta>{
            public EjecutaValidador(){
                RuleFor(x=>x.Email).EmailAddress().NotEmpty();
                RuleFor(x=>x.Password).NotEmpty();
                RuleFor(x=>x.NombreCompleto).NotEmpty();
                RuleFor(x=>x.Username).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly MonedasOnlineDbContext _context;
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;
            public Manejador(UserManager<Usuario> userManager,MonedasOnlineDbContext context,IJwtGenerador jwtGenerador){
                _userManager=userManager;
                _context=context;
                _jwtGenerador=jwtGenerador;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var usuarioIden= await _userManager.FindByNameAsync(request.Username);
                if(usuarioIden==null){
                    throw new System.Exception("Usuario por editar no encontrado");
                }
                var passwordresult = await _userManager.CheckPasswordAsync(usuarioIden,request.Password);
                if(!passwordresult){
                    throw new System.Exception("La clave ingresada no concuerda con la del usuario");
                }
                var resultado = await _context.Users.Where(x=>x.Email==request.Email&&x.UserName!=request.Username).AnyAsync();
                if(resultado){
                    throw new System.Exception("No se puede usar ese email, porque ya se encuentra en uso");
                }
                usuarioIden.NombreCompleto= request.NombreCompleto;
                usuarioIden.Email=request.Email;
                var updated = await _userManager.UpdateAsync(usuarioIden);
                if(updated.Succeeded){
                var roles= await _userManager.GetRolesAsync(usuarioIden);
                return new UsuarioData{
                    NombreCompleto= usuarioIden.NombreCompleto,
                    Username = usuarioIden.UserName,
                    Email=usuarioIden.Email,
                    Token= _jwtGenerador.CrearToken(usuarioIden,roles.ToList())
                };
                }
                throw new System.Exception("No se pudo actualizar la información de esta usuario");
            }
        }
    }
}