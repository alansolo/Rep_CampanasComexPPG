using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampanasComexPPG.Models.Configuracion
{
    public class UsuarioPasswordSharePMOD
    {
        public string UsuarioShareP { get; set; }
        public string PasswordShareP { get; set; }
        public string Mensaje { get; set; }
        public int OK { get; set; }

        //MENU
        public bool MenuUsuario { get; set; }
        public bool MenuCronograma { get; set; }
        public bool MenuGrafico { get; set; }
        public bool MenuConfiguracion { get; set; }
    }
}