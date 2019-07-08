using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCampanasPPG.Campana
{
    public class T_CAM_ACTIVIDAD
    {
        public long ID_CAMPANA { get; set; }
        public string CAMPANA { get; set; }
        public long ID_ACTIVIDAD { get; set; }
        public string ACTIVIDAD { get; set; }
        public string TREE_ACTIVIDAD { get; set; }
        public double DURACION { get; set; }
        public DateTime COMIENZO { get; set; }
        public string STR_COMIENZO { get; set; }
        public DateTime FIN { get; set; }
        public string STR_FIN { get; set; }
        public string RECURSO { get; set; }
        public double PORCENTAJE { get; set; }
        public bool PADRE { get; set; }
        public bool HIJO { get; set; }
        public long ID_PADRE { get; set; }
        public string TREE_PADRE { get; set; }
    }
}
