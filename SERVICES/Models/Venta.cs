using System;
using System.Collections.Generic;

namespace SERVICES.Models
{
    public partial class Venta
    {
        public Venta()
        {
            DetallesVenta = new HashSet<DetallesVentum>();
        }

        public int VentaId { get; set; }
        public DateTime? Fecha { get; set; }
        public decimal? Monto { get; set; }
        public int? ClienteId { get; set; }

        public virtual ICollection<DetallesVentum> DetallesVenta { get; set; }
    }
}
