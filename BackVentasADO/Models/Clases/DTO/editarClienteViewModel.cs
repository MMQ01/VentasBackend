using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackVentasADO.Models.Clases.DTO
{
    public class editarClienteViewModel
    {
        public int id { get; set; }

        public string nombre { get; set; } = null;

        public string email { get; set; } = null;

        public int idCategoria { get; set; }
    }
}