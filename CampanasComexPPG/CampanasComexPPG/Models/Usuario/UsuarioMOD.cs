using EntidadesCampanasPPG.BDCampana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampanasPPG.Models.Usuario
{
    public class UsuarioMOD
    {
        public List<EntidadesCampanasPPG.BDCampana.Usuario> ListUsuario { get; set; }
        public List<EntidadesCampanasPPG.BDCampana.Usuario> ListUsuarioTemp { get; set; }
        public List<RolCronograma> ListRolCronograma { get; set; }
        public List<UsuarioLdap> ListUsuarioLdapTemp { get; set; }
        public string Mensaje { get; set; }
        public int OK { get; set; }
        //MENU
        public bool MenuUsuario { get; set; }
        public bool MenuCronograma { get; set; }
        public bool MenuGrafico { get; set; }
        public bool MenuConfiguracion { get; set; }

    }
}