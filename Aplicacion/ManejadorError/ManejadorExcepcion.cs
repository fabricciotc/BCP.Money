using System.Net;
using System;

namespace Aplicacion.ManejadorError
{
    public class ManejadorExcepcion : Exception
    {
        public HttpStatusCode _code{get;}
        public object _errores{get;}
        public ManejadorExcepcion(HttpStatusCode code,object errores = null){
            this._code=code;
            this._errores= errores;
        }
    }
}