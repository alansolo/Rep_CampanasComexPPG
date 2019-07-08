using EntidadesCampanasPPG.BDCampana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCampanasPPG.Campana
{
    public class MecanicaENT
    {
        public int IdCampana{ get; set;}
        public string ClaveCampana { get; set; }
        public List<MecanicaRegalo> ListMecanicaRegalo { get; set; }
        public List<MecanicaMultiplo> ListMecanicaMultiplo { get; set; }
        public List<MecanicaDescuento> ListMecanicaDescuento { get; set; }
        public List<MecanicaVolumen> ListMecanicaVolumen { get; set; }
        public List<MecanicaKit> ListMecanicaKit { get; set; }
        public List<MecanicaCombo> ListMecanicaCombo { get; set; }
        public List<MecanicaTiendas> ListMecanicaTiendas { get; set; }
        public string Mensaje { get; set; }
        public int OK { get; set; }
    }
}
