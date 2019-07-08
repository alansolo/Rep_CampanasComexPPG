using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampanasComexPPG.Models.Grafico
{
    public class GraficoMOD
    {
        public List<EntidadesCampanasPPG.BDCampana.Campana> ListCampana { get; set; }
        public List<EntidadesCampanasPPG.BDCampana.Campana> ListCampanaCrono { get; set; }
        public List<GraficoPie> ListGraficoPieAlcance { get; set; }
        public List<GraficoPie> ListGraficoPieEstatus { get; set; }
        public List<GraficoPie> ListGraficoPieEjecucion { get; set; }
        public List<GraficoPie> ListGraficoPieProgreso { get; set; }
        public List<GraficoBar> ListGraficoBar { get; set; }
        public string Mensaje { get; set; }
        public int OK { get; set; }
        //MENU
        public bool MenuUsuario { get; set; }
        public bool MenuCronograma { get; set; }
        public bool MenuGrafico { get; set; }
        public bool MenuConfiguracion { get; set; }
    }
}