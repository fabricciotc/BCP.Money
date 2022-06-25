using System.Reflection.Metadata.Ecma335;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Aplicacion.ManejadorError;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistencia;
using FluentValidation;

namespace Aplicacion.Seguridad
{
    public class Registrar
    {
        public class Ejecuta : IRequest<UsuarioData>{
            public string NombreCompleto {set;get;}
            public string Email{set;get;}
            public string Password{set;get;}
            public string Username{set;get;}
        }
        public class EjecutaValidador : AbstractValidator<Ejecuta>{
            public EjecutaValidador(){
                RuleFor(x=>x.NombreCompleto).NotEmpty();
                RuleFor(x=>x.Email).NotEmpty();
                RuleFor(x=>x.Password).NotEmpty();
                RuleFor(x=>x.Username).NotEmpty();
            }
        }
        public class Manejador : IRequestHandler<Ejecuta, UsuarioData>
        {
            private readonly MonedasOnlineDbContext _context;
            private readonly UserManager<Usuario> _usermManager;
            private readonly IJwtGenerador _jwtGenerador;

            public Manejador(MonedasOnlineDbContext context,
                             UserManager<Usuario> userManager,
                             IJwtGenerador jwtGenerador)
            {
                    this._context= context;
                    this._usermManager=userManager;
                    this._jwtGenerador=jwtGenerador;
            }
            public async Task<UsuarioData> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var existe = await _context.Users.Where(user=>user.Email==request.Email).AnyAsync();
                if(existe){
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.BadRequest,new {mensaje="Existe ya un usuario con este Email"});
                }
                var existeun = await _context.Users.Where(user=>user.UserName==request.Username).AnyAsync();
                if(existeun){
                    throw new ManejadorExcepcion(System.Net.HttpStatusCode.BadRequest,new {mensaje="Existe ya un usuario con este Username"});
                }
                var usuario = new Usuario {
                    NombreCompleto = request.NombreCompleto,
                    Email = request.Email,
                    UserName = request.Username
                };
                var result = await _usermManager.CreateAsync(usuario,request.Password);
                if(result.Succeeded){
                    return new UsuarioData{
                        NombreCompleto = usuario.NombreCompleto,
                        Token = _jwtGenerador.CrearToken(usuario,null),
                        Username = usuario.UserName,
                        Email = usuario.Email
                    };
                }
                throw new System.Exception("No se pudo agregar al nuevo usuario");
            }
        }
    }
}