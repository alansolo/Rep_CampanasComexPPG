﻿using EntidadesCampanasPPG.BDCampana;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadesCampanasPPG.Catalogo
{
    public class TipoSellENT
    {
        public List<TipoSell> ListTipoSell { get; set; }
        public string Mensaje { get; set; }
        public int OK { get; set; }
    }
}
