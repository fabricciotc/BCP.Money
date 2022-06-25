using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Aplicacion.Contratos;
using Dominio;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Aplicacion.Seguridad
{
    public class UsuarioActual
    {
        public class Ejecutar : IRequest<UsuarioData> {}
        public class Manejador : IRequestHandler<Ejecutar, UsuarioData>
        {
            private readonly UserManager<Usuario> _userManager;
            private readonly IJwtGenerador _jwtGenerador;
            private readonly IUsuarioSesion _usuarioSesion;
            public Manejador(UserManager<Usuario> userManager, IJwtGenerador jwtGenerador, IUsuarioSesion usuarioSesion){
             this._jwtGenerador=jwtGenerador;
             this._userManager=userManager;
             this._usuarioSesion=usuarioSesion;   
            }
            public async Task<UsuarioData> Handle(Ejecutar request, CancellationToken cancellationToken)
            {
               var usuario =await _userManager.FindByNameAsync(_usuarioSesion.ObtenerUsuarioSesion());
               var roles = await _userManager.GetRolesAsync(usuario);
                return new UsuarioData {
                    Email=usuario.Email,
                    NombreCompleto = usuario.NombreCompleto,
                    Username = usuario.UserName,
                    Token = _jwtGenerador.CrearToken(usuario,roles.ToList())
                };
            }
        }
    }
}