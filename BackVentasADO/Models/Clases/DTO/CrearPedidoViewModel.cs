using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackVentasADO.Models.Clases.DTO
{
    public class CrearPedidoViewModel
    {
        public int idCliente { get; set; }

        public decimal total { get; set; }

        public DateTime fechaCreacion { get; set; }

        public List<CrearDetallePedidoViewModel> detallesPedido { get; set; }


        public CrearPedidoViewModel()
        {
            this.detallesPedido = new List<CrearDetallePedidoViewModel>();
        }

    }

    public class CrearDetallePedidoViewModel
    {
        public int idProducto { get; set; }
        public int cantidad { get; set; }
    }


}