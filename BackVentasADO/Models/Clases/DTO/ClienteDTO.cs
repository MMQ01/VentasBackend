using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackVentasADO.Models.Clases.DTO
{
    public class ClienteDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> fechaCreacion { get; set; }
        public string estado { get; set; }
        public int idCategoria { get; set; }
        public int identificacion { get; set; }
    }
}