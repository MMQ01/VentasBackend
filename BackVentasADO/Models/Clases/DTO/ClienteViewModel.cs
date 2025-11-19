using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackVentasADO.Models.Clases.DTO
{
    public class ClienteViewModel
    {
        public int id { get; set; }

        public string nombre { get; set; } = null;

        public string email { get; set; } = null;

        public DateTime? fechaCreacion { get; set; }

        public string estado { get; set; }

        public string nombreCategoria { get; set; }
        public int identificacion { get; set; }

    }
}