using BackVentasADO.Clases.DTO;
using BackVentasADO.Models;
using BackVentasADO.Models.Clases;
using BackVentasADO.Models.Clases.DTO;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BackVentasADO.Controllers.Services
{
    public class PedidoServices
    {

        public csListaPedidos getListaPedidos()
        {
            csListaPedidos res = new csListaPedidos();
            try
            {
                VentasEntities _context = new VentasEntities();


                List<PedidoDTO> lista = (from PED in _context.Pedidos
                                         join CLI in _context.Clientes
                                           on PED.IdCliente equals CLI.Id
                                         select new PedidoDTO
                                         {
                                             Id = PED.Id,
                                             ClienteID = PED.IdCliente,
                                             UsuarioID = PED.IdUsuario,
                                             fechaCreacion = PED.FechaCreacion,
                                             NombreCliente = CLI.Nombre,
                                             Origen = PED.Origen,
                                             total = PED.Total,
                                             detallesPedido = (from DET in _context.PedidoDetalle
                                                               join PRO in _context.Productos
                                                                on DET.IdProducto equals PRO.Id
                                                               where DET.IdPedido == PED.Id
                                                               select new DetallePedido
                                                               {
                                                                   Id = DET.Id,
                                                                   IdPedido = DET.IdPedido,
                                                                   IdProducto = DET.IdProducto,
                                                                   NombreProducto = PRO.Nombre,
                                                                   cantidad = DET.Cantidad,
                                                                   Precio = (decimal)PRO.Precio
                                                               }).ToList()

                                         }).OrderByDescending(x => x.fechaCreacion).ToList();

                res.Respuesta = "OK";
                res.Lista_Pedidos = lista;


            }
            catch (Exception ex)
            {


                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }


            return res;
        }

        public csListaPedidos getListaPedidosXCliente(int Id)
        {
            csListaPedidos res = new csListaPedidos();
            try
            {
                VentasEntities _context = new VentasEntities();


                List<PedidoDTO> lista = (from PED in _context.Pedidos
                                         join CLI in _context.Clientes
                                            on PED.IdCliente equals CLI.Id
                                         where  PED.Estado == true &&
                                                PED.IdCliente == Id
                                         select new PedidoDTO
                                         {
                                             Id = PED.Id,
                                             ClienteID = PED.IdCliente,
                                             UsuarioID = PED.IdUsuario,
                                             fechaCreacion = PED.FechaCreacion,
                                             Origen = PED.Origen,
                                             NombreCliente = CLI.Nombre,
                                             total = PED.Total,
                                             detallesPedido = (from DET in _context.PedidoDetalle
                                                               join PRO in _context.Productos 
                                                                on DET.IdProducto equals PRO.Id
                                                               where DET.IdPedido == PED.Id
                                                               select new DetallePedido
                                                               {
                                                                   Id = DET.Id,
                                                                   IdPedido = DET.IdPedido,
                                                                   NombreProducto = PRO.Nombre,
                                                                   IdProducto = DET.IdProducto,
                                                                   cantidad = DET.Cantidad,
                                                                   Precio = (decimal)PRO.Precio
                                                               }).ToList()

                                         }).OrderByDescending(x => x.fechaCreacion).ToList();

                res.Respuesta = "OK";
                res.Lista_Pedidos = lista;


            }
            catch (Exception ex)
            {


                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }


            return res;
        }

        public csListaPedidos getListaPedidosXUsuario(int Id)
        {
            csListaPedidos res = new csListaPedidos();
            try
            {
                VentasEntities _context = new VentasEntities();


                List<PedidoDTO> lista = (from PED in _context.Pedidos
                                         join CLI in _context.Clientes
                                           on PED.IdCliente equals CLI.Id
                                         where PED.Estado == true &&
                                                PED.IdUsuario == Id
                                         select new PedidoDTO
                                         {
                                             Id = PED.Id,
                                             ClienteID = PED.IdCliente,
                                             UsuarioID = PED.IdUsuario,
                                             fechaCreacion = PED.FechaCreacion,
                                             Origen = PED.Origen,
                                             total = PED.Total,
                                             NombreCliente = CLI.Nombre,
                                             detallesPedido = (from DET in _context.PedidoDetalle
                                                               join PRO in _context.Productos
                                                                on DET.IdProducto equals PRO.Id
                                                               where DET.IdPedido == PED.Id
                                                               select new DetallePedido
                                                               {
                                                                   Id = DET.Id,
                                                                   IdPedido = DET.IdPedido,
                                                                   NombreProducto = PRO.Nombre,
                                                                   IdProducto = DET.IdProducto,
                                                                   cantidad = DET.Cantidad,
                                                                   Precio = (decimal)PRO.Precio
                                                               }).ToList()

                                         }).OrderByDescending(x => x.fechaCreacion).ToList();

                res.Respuesta = "OK";
                res.Lista_Pedidos = lista;


            }
            catch (Exception ex)
            {


                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }


            return res;
        }

        public csPedido getPedido(int Id)
        {
            csPedido res = new csPedido();
            try
            {
                VentasEntities _context = new VentasEntities();


                PedidoDTO pedido = (from PED in _context.Pedidos
                                         join CLI in _context.Clientes
                                           on PED.IdCliente equals CLI.Id
                                         where PED.Estado == true &&
                                                PED.Id == Id
                                         select new PedidoDTO
                                         {
                                             Id = PED.Id,
                                             ClienteID = PED.IdCliente,
                                             UsuarioID = PED.IdUsuario,
                                             fechaCreacion = PED.FechaCreacion,
                                             Origen = PED.Origen,
                                             total = PED.Total,
                                             NombreCliente = CLI.Nombre,
                                             detallesPedido = (from DET in _context.PedidoDetalle
                                                               join PRO in _context.Productos
                                                                on DET.IdProducto equals PRO.Id
                                                               where DET.IdPedido == PED.Id
                                                               select new DetallePedido
                                                               {
                                                                   Id = DET.Id,
                                                                   IdPedido = DET.IdPedido,
                                                                   NombreProducto = PRO.Nombre,
                                                                   IdProducto = DET.IdProducto,
                                                                   cantidad = DET.Cantidad,
                                                                   Precio = (decimal)PRO.Precio
                                                               }).OrderByDescending(x=> x.cantidad).ToList()

                                         }).FirstOrDefault();

                res.Respuesta = "OK";
                res.Pedido = pedido;


            }
            catch (Exception ex)
            {


                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }


            return res;
        }

        public Resultado guardarPedido(PedidoDTO pedido)
        {
            Resultado res = new Resultado();

            VentasEntities _context = new VentasEntities();
            DbContextTransaction transaccion = null;
            try
            {

                transaccion = _context.Database.BeginTransaction();
                {

                    Pedidos newPedido = new Pedidos();
                    newPedido.IdCliente = pedido.ClienteID;
                    newPedido.Total = pedido.total;
                    newPedido.FechaCreacion = DateTime.Now;
                    newPedido.Estado = true;
                    newPedido.Origen = pedido.Origen;
                    newPedido.IdUsuario = pedido.UsuarioID;

                    _context.Pedidos.Add(newPedido);
                    //_context.SaveChanges();

                    foreach (var detalle in pedido.detallesPedido)
                    {
                        PedidoDetalle newDetalle = new PedidoDetalle();
                        newDetalle.Cantidad = detalle.cantidad;
                        newDetalle.IdProducto = detalle.IdProducto;
                        newDetalle.IdPedido = newPedido.Id;
                        _context.PedidoDetalle.Add(newDetalle);
                        //_context.SaveChanges();
                    }
                    _context.SaveChanges();
                    transaccion.Commit();


                    var contextHub = GlobalHost.ConnectionManager.GetHubContext<PedidosHub>();

                    contextHub.Clients.All.ReceiveAllOrders(new PedidoDTO
                    {
                        Id = newPedido.Id,
                        ClienteID = newPedido.IdCliente,
                        total = newPedido.Total,
                        fechaCreacion = newPedido.FechaCreacion,
                        detallesPedido = null,
                        NombreCliente = ClienteServices.getNombreCliente(newPedido.IdCliente),
                        Origen = newPedido.Origen,
                        UsuarioID = newPedido.IdUsuario
                    });


                }

                res.Respuesta = "OK";

            }
            catch (Exception ex)
            {

                if (transaccion != null)
                { transaccion.Rollback(); }

                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }

            return res;
        }


        public Resultado activarPedido(int id)
        {
            Resultado res = new Resultado();
            try
            {
                VentasEntities _context = new VentasEntities();


              var pedido = (from PED in _context.Pedidos
                                         where PED.IdCliente == id
                                           select PED).FirstOrDefault();

                if (pedido == null)
                {
                    res.Respuesta = "ERROR";
                    res.Mensaje = "El pedido no existe";

                    return res;
                }
                else if (pedido != null && pedido.Estado == true)
                {

                    res.Respuesta = "ERROR";
                    res.Mensaje = "El pedido ya se encuentra activo";

                    return res;

                }

                pedido.Estado = true;                  
                
                res.Respuesta = "OK";
               

            }
            catch (Exception ex)
            {


                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }


            return res;
        }

        public Resultado inactivarPedido(int id)
        {
            Resultado res = new Resultado();
            try
            {
                VentasEntities _context = new VentasEntities();


                var pedido = (from PED in _context.Pedidos
                              where PED.IdCliente == id
                              select PED).FirstOrDefault();

                if (pedido == null)
                {
                    res.Respuesta = "ERROR";
                    res.Mensaje = "El pedido no existe";

                    return res;
                }
                else if (pedido != null && pedido.Estado == false)
                {

                    res.Respuesta = "ERROR";
                    res.Mensaje = "El pedido ya se encuentra inactivo";

                    return res;

                }

                pedido.Estado = false;

                res.Respuesta = "OK";


            }
            catch (Exception ex)
            {


                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }


            return res;
        }

        public csCarrito getListaProductosCarrito(int usuarioID)
        {
            csCarrito res = new csCarrito();
            try
            {
                VentasEntities _context = new VentasEntities();


                var lCarrito = (from CAR in _context.tmp_carrito
                              join PRO in _context.Productos
                                  on CAR.Producto_Id equals PRO.Id
                                join USU in _context.Usuarios
                                  on CAR.Usuario_Id equals USU.Id
                                where CAR.Usuario_Id == usuarioID &&
                                     PRO.Estado == true &&
                                     USU.Estado == true 

                              select new
                              {
                                  PRO,
                                  CAR,
                                  USU

                              }).ToList();

                if (lCarrito == null)
                {
                    res.Respuesta = "ERROR";
                    res.Mensaje = "El Usuario no tiene productos en el carrito";

                    return res;
                }

                foreach (var lProducto in lCarrito)
                {
                    
                    CarritoDTO carritoDTO = new CarritoDTO();

                    carritoDTO.ID = lProducto.CAR.ID;
                    carritoDTO.Usuario_ID = lProducto.CAR.Usuario_Id;
                    carritoDTO.Nombre_Usuario = lProducto.PRO.Nombre;
                    carritoDTO.Producto_ID = lProducto.CAR.Producto_Id;
                    carritoDTO.Nombre_Producto = lProducto.PRO.Nombre;
                    carritoDTO.Precio = (double)lProducto.PRO.Precio;
                    carritoDTO.SKU = lProducto.PRO.SKU;
                    carritoDTO.Cantidad = (double)lProducto.CAR.Cantidad;

                    res.Lista_Productos.Add(carritoDTO);
                }



                res.Respuesta = "OK";


            }
            catch (Exception ex)
            {


                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }


            return res;
        }

        public Resultado eliminarCarrito(int usuarioID)
        {
            Resultado res = new Resultado();
            try
            {
                VentasEntities _context = new VentasEntities();


                var lCarrito = (from CAR in _context.tmp_carrito
                                join PRO in _context.Productos
                                    on CAR.Producto_Id equals PRO.Id
                                join USU in _context.Usuarios
                                  on CAR.Usuario_Id equals USU.Id
                                where CAR.Usuario_Id == usuarioID

                                select CAR).ToList();

                if (lCarrito == null)
                {
                    res.Respuesta = "ERROR";
                    res.Mensaje = "El Usuario no tiene productos en el carrito";

                    return res;
                }

                _context.tmp_carrito.RemoveRange(lCarrito);
                _context.SaveChanges();



                res.Respuesta = "OK";


            }
            catch (Exception ex)
            {


                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }


            return res;
        }

        public Resultado eliminarProductoCarrito(int usuarioID, int productoID)
        {
            Resultado res = new Resultado();
            try
            {
                VentasEntities _context = new VentasEntities();


                var lCarrito = (from CAR in _context.tmp_carrito
                                join PRO in _context.Productos
                                    on CAR.Producto_Id equals PRO.Id
                                join USU in _context.Usuarios
                                  on CAR.Usuario_Id equals USU.Id
                                where CAR.Usuario_Id == usuarioID &&
                                      CAR.Producto_Id == productoID

                                select CAR).FirstOrDefault();

                if (lCarrito == null)
                {
                    res.Respuesta = "ERROR";
                    res.Mensaje = "El Usuario no tiene productos en el carrito";

                    return res;
                }

                res.Respuesta = "OK";

                _context.tmp_carrito.Remove(lCarrito);
                _context.SaveChanges();





            }
            catch (Exception ex)
            {


                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }


            return res;
        }

        public Resultado actualizarProductoCarrito(int usuarioID, int productoID, double Cantidad = 1)
        {
            Resultado res = new Resultado();
            try
            {
                VentasEntities _context = new VentasEntities();


                var lProducto = (from PRO in _context.Productos
                                where PRO.Id == productoID &&
                                      PRO.Estado == true
                                select PRO).FirstOrDefault();

                if (lProducto == null)
                {
                    res.Respuesta = "ERROR";
                    res.Mensaje = "El producto no está disponible";

                    return res;
                }

                var lCarrito = (from CAR in _context.tmp_carrito
                                join PRO in _context.Productos
                                    on CAR.Producto_Id equals PRO.Id
                                join USU in _context.Usuarios
                                  on CAR.Usuario_Id equals USU.Id
                                where CAR.Usuario_Id == usuarioID &&
                                      CAR.Producto_Id == productoID &&
                                        PRO.Estado == true &&
                                        USU.Estado == true

                                select CAR).FirstOrDefault();

                if (lCarrito == null)
                {

                    tmp_carrito tmp_Carrito = new tmp_carrito();
                    tmp_Carrito.Usuario_Id = usuarioID;
                    tmp_Carrito.Producto_Id = productoID;
                    tmp_Carrito.Cantidad = Cantidad;

                    _context.tmp_carrito.Add(tmp_Carrito);
                }
                else if (lCarrito.Cantidad == Cantidad)
                {
                    res.Respuesta = "ERROR";
                    res.Mensaje = "El producto ya está registrado en el carrito.";
                    return res;

                } else if (lCarrito.Cantidad <= 0)
                {
                    eliminarProductoCarrito(usuarioID, productoID);
                    res.Respuesta = "OK";
                    return res;
                }
                else
                {
                    lCarrito.Cantidad = Cantidad;
                }

                _context.SaveChanges();


                res.Respuesta = "OK";


            }
            catch (Exception ex)
            {


                res.Respuesta = "ERROR";
                res.Mensaje = ex.ToString();
                return res;
            }


            return res;
        }

    }
}