using EntidadesCampanasPPG.BDCampana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCampanasPPG.Ldap
{
    public class LdapENT
    {
        public List<UsuarioLdap> ListUsuarioLdap { get; set; }
        public string ServerLdap { get; set; }
        public string Mensaje { get; set; }
        public int OK { get; set; }
    }
}
