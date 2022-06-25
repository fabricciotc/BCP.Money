using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Moneda
    {
        [Key]
        public Guid MonedaId { set; get; }
        public string Abreviacion { set; get; }
        public string Descripcion { set; get; }
        public DateTime fechaCreacion { set; get; }
        public DateTime fechaActualizacion { set; get; }
        public List<Tipo_Cambio> cambiosOrigen { set; get; }
        public List<Tipo_Cambio> cambiosDestino { set; get; }
    }
}