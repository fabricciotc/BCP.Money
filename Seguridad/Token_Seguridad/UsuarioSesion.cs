using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Aplicacion.Contratos;

namespace Seguridad.Token_Seguridad
{
    public class UsuarioSesion : IUsuarioSesion
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UsuarioSesion(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
        }
        public string ObtenerUsuarioSesion()
        {
            var username = httpContextAccessor.HttpContext.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return username;
        }

    }
}