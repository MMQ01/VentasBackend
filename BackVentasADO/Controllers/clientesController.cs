using BackVentasADO.Controllers.Services;
using BackVentasADO.Models.Clases;
using BackVentasADO.Models.Clases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Mvc;

namespace BackVentasADO.Controllers
{
    public class clientesController : ApiController
    {


        private clienteServices _clientesService = new clienteServices();

        //public clientesController(clienteServices clienteService)
        //{
        //    _clientesService = clienteService;
        //}


        [HttpGet]
        [Route("api/Clientes")]
        public Resultado getClientes()
        {
            Resultado res = new Resultado();
            try
            {

                var lista = _clientesService.GetClientes();
                res.respuesta = lista;
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
        [Route("api/Clientes")]
        public Resultado crearCliente([FromBody] crearClienteViewModel cli)
        {
            Resultado res = new Resultado();
            try
            {

                var cliente = _clientesService.crearCliente(cli);
                res.respuesta = cliente;
                res.mensaje = "OK";
                if (cliente == null)
                {
                    res.respuesta = "Identificacion del cliente ya existe";
                }
            }
            catch (Exception)
            {

                res.mensaje = "Error";
                return res;
            }

            return res;
        }


        [HttpGet]
        [Route("api/Clientes/{id:int}")]
        public Resultado getCliente(int id)
        {
            Resultado res = new Resultado();
            try
            {

                var cliente = _clientesService.getCliente(id);
                res.respuesta = cliente;
                res.mensaje = "OK";

                if (cliente == null)
                {
                    res.respuesta = "Cliente no existe";
                }
            }
            catch (Exception)
            {

                res.mensaje = "Error";
                return res;
            }

            return res;
        }

        [HttpPut]
        [Route("api/Clientes")]
        public Resultado editarCliente(editarClienteViewModel cli)
        {
            Resultado res = new Resultado();
            try
            {

                var cliente = _clientesService.editarCliente(cli);
                res.respuesta = cliente;
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
        [Route("api/Clientes/InactivarCliente")]
        public Resultado inactivarCliente([FromBody] int id)
        {
            Resultado res = new Resultado();
            try
            {

                var cliente = _clientesService.inactivarCliente(id);

                res.respuesta = cliente;
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
        [Route("api/Clientes/ActivarCliente")]
        public Resultado activarCliente([FromBody] int id)
        {
            Resultado res = new Resultado();
            try
            {

                var cliente = _clientesService.activarCliente(id);

                res.respuesta = cliente;
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
        [Route("api/Clientes/{id:int}")]
        public Resultado deleteCliente(int id)
        {
            Resultado res = new Resultado();
            try
            {

                var cliente = _clientesService.deleteCliente(id);

                res.respuesta = cliente;
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
