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
        public ListaCategoria getCategorias()
        {
            ListaCategoria res =new ListaCategoria();
            try
            {
                VentasEntities _context = new VentasEntities();
                var lista = _context.Categorias
                .Select(x => new CategoriaDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                })
                .ToList();

                res.Lista_Categoria = lista;
                res.Respuesta = "OK";

            }
            catch (Exception ex)
            {
                res.Mensaje = ex.Message;
                res.Respuesta = "Error";
                return res;

            }

            return res;
        }

       
    }
}
