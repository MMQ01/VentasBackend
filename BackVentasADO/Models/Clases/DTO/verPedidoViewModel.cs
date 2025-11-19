using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackVentasADO.Models.Clases.DTO
{
    public class verPedidoViewModel
    {
        public class VerPedidoViewModel
        {
            public int id { get; set; }
            public string cliente { get; set; }
            public decimal total { get; set; }
            public string estado { get; set; }
            public List<VerPedidoDetalleProductoViewModel> detallesProductosPedido { get; set; }

            public VerPedidoViewModel()
            {
                this.detallesProductosPedido = new List<VerPedidoDetalleProductoViewModel>();
            }
        }

        public class VerPedidoDetalleProductoViewModel
        {

            public string nombreProducto { get; set; }
            public decimal cantidad { get; set; }

        }
    }
}