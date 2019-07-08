using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampanasComexPPG.Models.Grafico
{
    public class GraficoBar
    {
        public string Titulo { set; get; }
        public decimal PorcUsuario { set; get; }
        public decimal PorcSistema { set; get; }
        public decimal PorcSistemaReal { set; get; }
    }
}