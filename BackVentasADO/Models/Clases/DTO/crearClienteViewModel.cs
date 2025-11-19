using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackVentasADO.Models.Clases.DTO
{
    public class crearClienteViewModel
    {
        public int id { get; set; }

        public string nombre { get; set; } = null;

        public string email { get; set; } = null;

        public string contraseña { get; set; } = null;

        public DateTime? fechaCreacion { get; set; }

        public string estado { get; set; }

        public int idCategoria { get; set; }
        public int identificacion { get; set; }
    }
}