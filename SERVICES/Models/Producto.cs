using System;
using System.Collections.Generic;

namespace SERVICES.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetallesVenta = new HashSet<DetallesVentum>();
        }

        public int ProductoId { get; set; }
        public string? Nombre { get; set; }
        public decimal? Precio { get; set; }

        public virtual ICollection<DetallesVentum> DetallesVenta { get; set; }
    }
}
