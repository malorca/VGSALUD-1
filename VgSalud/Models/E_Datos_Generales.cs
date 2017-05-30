using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VgSalud.Models
{
    public class E_Datos_Generales
    {

        public decimal igv { get; set; }
        public decimal Tipo_Cambio { get; set; }
        public bool MUESTRA_ANTECEDENTE { get; set; }
        public bool ATENCIONESPAGADAS { get; set; }

    }
}