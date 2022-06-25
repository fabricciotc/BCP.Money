using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dominio
{
    public class Tipo_Cambio
    {
        public Guid Tipo_Cambio_Id{set;get;}
        public string Descripcion { set; get; }
        [Column(TypeName="decimal(18,4)")]
        public decimal Conversion{set;get;}
        public Guid MonedaOrigenId{set;get;}
        [ForeignKey("MonedaOrigenId")]
        public virtual Moneda MonedaOrigen{set;get;}
        public Guid MonedaDestinoId { set; get; }
        [ForeignKey("MonedaDestinoId")]
        public virtual Moneda MonedaDestino { set; get; }
        public DateTime fechaActualizacion { set; get; }
        public DateTime fechaCreacion { set; get; }
        public string Abreviacion()
        {
            return MonedaOrigen.Abreviacion + "/" + MonedaDestino.Abreviacion;
        }

    }
}