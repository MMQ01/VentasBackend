using BackVentasADO.Models.Clases.DTO;
using BackVentasADO.Models.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BackVentasADO.Controllers.Services;


namespace BackVentasADO.Controllers
{
    public class PedidosController : ApiController
    {

        PedidoServices _pedidoServices = new PedidoServices();

        [HttpPost]
        [Route("api/Pedidos")]
        public Resultado guardarPedido(CrearPedidoViewModel pedido)
        {
            Resultado res = new Resultado();
            try
            {


                res.respuesta = _pedidoServices.guardarPedido(pedido);
                res.mensaje = "OK";

            }
            catch (Exception)
            {

                res.mensaje = "Error";
                return res;
            }
            return res;
        }

        [HttpGet]
        [Route("api/Pedidos/{identificacion:int}")]

        public Resultado listarPedidosXCliente(int identificacion)
        {
            Resultado res = new Resultado();
            try
            {

                var lista = _pedidoServices.verPedidosXCliente(identificacion);
                res.respuesta = lista;
                if (lista == null)
                {
                    res.respuesta = "Sin pedidos";
                }
                res.mensaje = "OK";

            }
            catch (Exception)
            {

                res.mensaje = "Error";
                return res;
            }
            return res;
        }

        [HttpPost]
        [Route("api/Pedidos/InactivarPedido")]
        public Resultado inactivarPedido([FromBody] int id)
        {
            Resultado res = new Resultado();
            try
            {

                var pedido = _pedidoServices.inactivarPedido(id);

                res.respuesta = pedido;
                res.mensaje = "OK";
            }
            catch (Exception)
            {

                res.mensaje = "Error";
                return res;
            }
            return res;
        }

        [HttpPost]
        [Route("api/Pedidos/ActivarPedido")]
        public Resultado activarPedido([FromBody] int id)
        {
            Resultado res = new Resultado();
            try
            {

                var pedido = _pedidoServices.activarPedido(id);

                res.respuesta = pedido;
                res.mensaje = "OK";
            }
            catch (Exception)
            {

                res.mensaje = "Error";
                return res;
            }

            return res;

        }

        [HttpDelete]
        [Route("api/Pedidos")]
        public Resultado borrarPedido(int id)
        {
            Resultado res = new Resultado();
            try
            {

                var pedido = _pedidoServices.borrarPedido(id);

                res.respuesta = pedido;
                res.mensaje = "OK";
            }
            catch (Exception)
            {

                res.mensaje = "Error";
                return res;
            }

            return res;
        }

        [HttpGet]
        [Route("api/Pedidos")]
        public Resultado listarPedidos()
        {
            Resultado res = new Resultado();
            try
            {

                var lista = _pedidoServices.verPedidos();
                res.respuesta = lista;
                if (lista == null)
                {
                    res.respuesta = "Sin pedidos";
                }
                res.mensaje = "OK";

            }
            catch (Exception)
            {

                res.mensaje = "Error";
                return res;
            }

            return res;
        }
    }
}
