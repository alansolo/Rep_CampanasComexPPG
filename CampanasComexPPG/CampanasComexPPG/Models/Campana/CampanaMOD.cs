using EntidadesCampanasPPG.BDCampana;
using EntidadesCampanasPPG.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampanasComexPPG.Models.Campana
{
    public class CampanaMOD
    {
        public List<EntidadesCampanasPPG.BDCampana.Campana> ListCampana { get; set; }
        public List<EntidadesCampanasPPG.BDCampana.Campana> ListCampanaTemp { get; set; }
        public decimal PorcentajeGeneralUsuario { get; set; }
        public decimal PorcentajeGeneralSistema { get; set; }
        public decimal PorcentajeEsfuerzoUsuario { get; set; }
        public string EstatusSemaforoVerde { get; set; }
        public string EstatusSemaforoAmarillo { get; set; }
        public string EstatusSemaforoRojo { get; set; }
        public string EstatusGeneral { get; set; }
        public string EstatusAvance { get; set; }
        public string MaxFechaCronograma { get; set; }
        public string MinFechaCronograma { get; set; }
        public List<Cronograma> ListCronograma { get; set; }
        public List<Cronograma> ListNewCronograma { get; set; }
        public List<Cronograma> ListCronogramaRecargar { get; set; }
        public List<TipoSell> ListTipoSell { get; set; }
        public List<TipoAlcance> ListTipoAlcance { get; set; }
        public List<TipoCampania> ListTipoCampania { get; set; }
        public List<TipoUrgente> ListTipoUrgente { get; set; }
        public List<MecanicaRegalo> ListMecanicaRegalo { get; set; }
        public List<MecanicaMultiplo> ListMecanicaMultiplo { get; set; }
        public List<MecanicaDescuento> ListMecanicaDescuento { get; set; }
        public List<MecanicaVolumen> ListMecanicaVolumen { get; set; }
        public List<MecanicaKit> ListMecanicaKit { get; set; }
        public List<MecanicaCombo> ListMecanicaCombo { get; set; }
        public List<MecanicaTiendas> ListMecanicaTiendas { get; set; }
        public List<UsuarioLdap> ListUsuarioLdapTemp { get; set; }
        public EntidadesCampanasPPG.BDCampana.Campana Campana { get; set; }
        public bool RolAdministrador { get; set; }
        public string Mensaje { get; set; }
        public int OK { get; set; }
        public bool Correcto { get; set; }
        public bool EsEditarCronograma { get; set; }

        //MENU
        public bool MenuUsuario { get; set; }
        public bool MenuCronograma { get; set; }
        public bool MenuGrafico { get; set; }
        public bool MenuConfiguracion { get; set; }
    }
}