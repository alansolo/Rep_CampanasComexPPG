using EntidadesCampanasPPG.BDCampana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCampanasPPG.Usuario
{
    public class UsuarioENT
    {
        public List<EntidadesCampanasPPG.BDCampana.Usuario> ListUsuario { get; set; }
        public string Mensaje { get; set; }
        public int OK { get; set; }
    }
}
