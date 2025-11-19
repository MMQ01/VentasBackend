using BackVentasADO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text.Json;
using BackVentasADO.Models.Clases;
using BackVentasADO.Models.Clases.DTO;

namespace BackVentasADO.Controllers
{
    public class CategoriasController : ApiController
    {


        [HttpGet]
        [Route("api/categoria")]
        public Resultado getCategorias()
        {
            Resultado res=new Resultado();
            try
            {
                VentasEntities _context = new VentasEntities();
                var lista = _context.Categorias
                .Select(x => new CategoiaViewModel
                {
                    id = x.Id,
                    nombre = x.Nombre,
                })
                .ToList();

                res.respuesta = lista;
                res.mensaje = "OK";

            }
            catch (Exception ex)
            {
                res.respuesta = ex.Message;
                res.mensaje = "Error";
                return res;

            }

            return res;
        }

       
    }
}
