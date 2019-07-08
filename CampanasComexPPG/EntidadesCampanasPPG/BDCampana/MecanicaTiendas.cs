using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCampanasPPG.BDCampana
{
    public class MecanicaTiendas
    {
        public int IdCampania { get; set; }
        public string ClaveCampania { get; set; }
        public int Id_Tienda { get; set; }
        public string Bill_To { get; set; }
        public string Customer_Name { get; set; }
        public string Region { get; set; }
        public string Descripcion_Region { get; set; }
        public string Descripcion_Zona { get; set; }
        public string Segmento { get; set; }
        public string Clave_Sobrepecio { get; set; }
    }
}
