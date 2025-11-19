using BackVentasADO.Models.Clases.DTO;
using BackVentasADO.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using static BackVentasADO.Models.Clases.DTO.verPedidoViewModel;

namespace BackVentasADO.Controllers.Services
{
    public class PedidoServices
    {

        public CrearPedidoViewModel guardarPedido(CrearPedidoViewModel p)
        {

            VentasEntities _context = new VentasEntities();
            DbContextTransaction transaccion = null;
            try
            {

                transaccion = _context.Database.BeginTransaction();
                {

                    var pedido = new Pedidos();
                    pedido.Total = p.total;
                    pedido.IdCliente = p.idCliente;
                    pedido.FechaCreacion = DateTime.Now;
                    pedido.Estado = "SI";

                    _context.Pedidos.Add(pedido);
                    _context.SaveChanges();

                    foreach (var d in p.detallesPedido)
                    {
                        var detalle = new Productos_Pedidos();
                        detalle.Cantidad = d.cantidad;
                        detalle.IdProducto = d.idProducto;
                        detalle.IdPedido = pedido.Id;
                        _context.Productos_Pedidos.Add(detalle);
                        _context.SaveChanges();
                    }

                    transaccion.Commit();

                }

            }
            catch (Exception)
            {

                if (transaccion != null)
                { transaccion.Rollback(); }
            }

            return p;
        }

        public List<VerPedidoViewModel> verPedidosXCliente(int Identificacion)
        {
            VentasEntities _context = new VentasEntities();

            List<VerPedidoViewModel> lista = new List<VerPedidoViewModel>();


            var cliente = _context.Cliente.FirstOrDefault(cli => cli.Identificacion == Identificacion);

            if (cliente == null)
            {
                return lista;
            }


            List<Pedidos> listaPedidos = (from p in _context.Pedidos
            where
                                         p.IdCliente == cliente.Id && p.Estado == "SI"
                                         select p).ToList();

            foreach (Pedidos pedido in listaPedidos)
            {
                VerPedidoViewModel auxPedido = new VerPedidoViewModel();

                auxPedido.total = pedido.Total;
                auxPedido.cliente = cliente.Nombre;
                auxPedido.id = pedido.Id;
                List<Productos_Pedidos> listaDetalle = (from pd in _context.Productos_Pedidos
                                                        where
                pd.IdPedido == pedido.Id
                                                      select pd).ToList();

                foreach (Productos_Pedidos productoPedido in listaDetalle)
                {
                    VerPedidoDetalleProductoViewModel prod = new VerPedidoDetalleProductoViewModel();
                    prod.cantidad = productoPedido.Cantidad;

                    var prodAux = _context.Productos.FirstOrDefault(p => p.Id == productoPedido.IdProducto);

                    prod.nombreProducto = prodAux.Nombre;

                    auxPedido.detallesProductosPedido.Add(prod);
                }

                lista.Add(auxPedido);

            }

            return lista;
        }

        public string activarPedido(int id)
        {
            VentasEntities _context = new VentasEntities();

            var pedido = _context.Pedidos.Where(x => x.Id == id && x.Estado == "NO").FirstOrDefault();

            if (pedido == null)
            {
                return "El pedido ya ha sido activado o no existe";
            }
            pedido.Estado = "SI";
            _context.SaveChanges();

            return "El pedido activado exitosamente";
        }

        public string inactivarPedido(int id)
        {
            VentasEntities _context = new VentasEntities();

            var pedido = _context.Pedidos.Where(x => x.Id == id && x.Estado == "SI").FirstOrDefault();

            if (pedido == null)
            {
                return "El pedido ya ha sido inactivado o no existe";
            }
            pedido.Estado = "NO";
            _context.SaveChanges();

            return "El pedido inactivado exitosamente";
        }

        public string borrarPedido(int id)
        {
            VentasEntities _context = new VentasEntities();
            var pedido = _context.Pedidos.FirstOrDefault(x => x.Id == id);


            if (pedido == null)
            {
                return "Producto ya ha sido eliminado o no existe";
            }
            _context.Pedidos.Remove(pedido);
            _context.SaveChanges();

            return "Producto eliminado exitosamente";

        }

        public List<VerPedidoViewModel> verPedidos()
        {
            VentasEntities _context = new VentasEntities();
            List<VerPedidoViewModel> lista = new List<VerPedidoViewModel>();


            var cliente = _context.Cliente.ToList();




            List<Pedidos> listaPedidos = _context.Pedidos.ToList();

            foreach (Pedidos pedido in listaPedidos)
            {
                VerPedidoViewModel auxPedido = new VerPedidoViewModel();

                auxPedido.total = pedido.Total;
                auxPedido.cliente = cliente.Find(c => c.Id == pedido.IdCliente).Nombre;
                auxPedido.id = pedido.Id;
                auxPedido.estado = pedido.Estado;
                List<Productos_Pedidos> listaDetalle = (from pd in _context.Productos_Pedidos
                                                        where
                                                     pd.IdPedido == pedido.Id
                                                      select pd).ToList();

                foreach (Productos_Pedidos productoPedido in listaDetalle)
                {
                    VerPedidoDetalleProductoViewModel prod = new VerPedidoDetalleProductoViewModel();
                    prod.cantidad = productoPedido.Cantidad;

                    var prodAux = _context.Productos.FirstOrDefault(p => p.Id == productoPedido.IdProducto);

                    prod.nombreProducto = prodAux.Nombre;

                    auxPedido.detallesProductosPedido.Add(prod);
                }

                lista.Add(auxPedido);

            }

            return lista;
        }
    }
}