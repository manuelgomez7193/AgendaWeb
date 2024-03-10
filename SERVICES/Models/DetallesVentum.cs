﻿using System;
using System.Collections.Generic;

namespace SERVICES.Models
{
    public partial class DetallesVentum
    {
        public int DetalleId { get; set; }
        public int? VentaId { get; set; }
        public int? ProductoId { get; set; }
        public int? Cantidad { get; set; }

        public virtual Producto? Producto { get; set; }
        public virtual Venta? Venta { get; set; }
    }
}
