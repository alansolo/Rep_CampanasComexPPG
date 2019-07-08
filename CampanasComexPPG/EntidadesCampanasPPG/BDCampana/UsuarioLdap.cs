using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCampanasPPG.BDCampana
{
    public class UsuarioLdap
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string PPGID { get; set; }
        public int ID_RolCronograma { get; set; }
        public string Rol { get; set; }
        public string RolDescription { get; set; }
        public RolCronograma RolCronograma = new RolCronograma();
        public string Mensaje { get; set; }
        public bool Seleccionar { get; set; }
    }
}
