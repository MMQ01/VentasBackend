using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackVentasADO.Models.Clases.DTO
{
    public class LoginViewModel
    {
        public int identificacion { get; set; }
        public string contraseña { get; set; }
    }
}