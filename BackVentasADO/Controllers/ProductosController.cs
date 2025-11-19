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
    public class ProductosController : ApiController
    {


        private  productosServices _productoService = new productosServices();

        //public ProductosController(productosServices productoService)
        //{
        //    _productoService = productoService;
        //}


        [HttpPost]
        [Route("api/Productos/Crear")]
        public Resultado createProducto([FromBody] productoViewModel prod)
        {
            Resultado res = new Resultado();
            try
            {


                var producto = _productoService.crearProducto(prod);


                res.respuesta = producto;
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
        [Route("api/Productos/ProductosActivos")]
        public Resultado getProductosActivos()
        {
          
            Resultado res = new Resultado();
            try
            {

                var lista = _productoService.getProductosActivos();
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

        [HttpGet]
        [Route("api/Productos/ProductosAll")]
        public Resultado getProductosAll()
        {
            Resultado res = new Resultado();
            try
            {

                var lista = _productoService.getProductosAll();
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

        [HttpGet]
        [Route("api/Productos/{id:int}")]
        public Resultado getProducto(int id)
        {
            Resultado res = new Resultado();
            try
            {

                var producto = _productoService.getProducto(id);



                res.respuesta = producto;
                if (producto == null)
                {
                    res.respuesta = "Producto no encontrado";
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
        [Route("api/Productos/InactivarProducto")]

        public Resultado inactivarProducto([FromBody] int id)
        {
            Resultado res = new Resultado();
            try
            {

                var producto = _productoService.inactivarProducto(id);

                res.respuesta = producto;
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
        [Route("api/Productos/ActivarProducto")]
        public Resultado activarProducto([FromBody] int id)
        {
            Resultado res = new Resultado();
            try
            {

                var producto = _productoService.activarProducto(id);

                res.respuesta = producto;
                res.mensaje = "OK";
            }
            catch (Exception)
            {

                res.mensaje = "Error";
                return res;
            }

            return res;

        }

        [HttpPut]
        [Route("api/Productos")]
        public Resultado editarProducto([FromBody] productoViewModel prod)
        {
            Resultado res = new Resultado();
            try
            {

                var producto = _productoService.editProducto(prod);

                res.respuesta = producto;
                if (producto == null)
                {
                    res.respuesta = "Producto no existe";
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

        [HttpDelete]
        [Route("api/Productos/{id:int}")]
        public Resultado deleteProducto(int id)
        {
            Resultado res = new Resultado();
            try
            {

                var producto = _productoService.deleteProducto(id);

                res.respuesta = producto;
                if (producto == null)
                {
                    res.respuesta = "Producto no encontrado";
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
