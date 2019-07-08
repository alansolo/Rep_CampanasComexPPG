using EntidadesCampanasPPG.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCampanasPPG.BDCampana;
using System.Data;
using DatosCampanasPPG.Catalogo;
using UtilidadesCampanasPPG;

namespace NegocioCampanasPPG.Catalogo
{
    public class TipoAlcanceNEG
    {
        public TipoAlcanceENT GetTipoAlcance(TipoAlcanceENT tipoCampaniaENTReq)
        {
            TipoAlcanceENT tipoAlcanceENTRes = new TipoAlcanceENT();
            TipoAlcance tipoAlcance = new TipoAlcance();
            DataTable dtTipoAlcance = new DataTable();

            try
            {
                tipoAlcance = tipoCampaniaENTReq.ListTipoAlcance.FirstOrDefault();

                if (tipoAlcance == null)
                {
                    return tipoAlcanceENTRes;
                }

                TipoAlcanceDAT tipoAlcanceDAT = new TipoAlcanceDAT();

                dtTipoAlcance = tipoAlcanceDAT.GetTipoAlcance(tipoAlcance.ID, tipoAlcance.Alcance);

                tipoAlcanceENTRes.ListTipoAlcance = dtTipoAlcance.AsEnumerable()
                                            .Select(row => new TipoAlcance
                                            {
                                                ID = row.Field<int?>("ID").GetValueOrDefault(),
                                                Alcance = row.Field<string>("Alcance"),
                                                Descripcion = row.Field<string>("Descripcion")
                                            }).ToList();

                tipoAlcanceENTRes.OK = 0;
                tipoAlcanceENTRes.Mensaje = "OK";
            }
            catch (Exception ex)
            {
                tipoAlcanceENTRes.ListTipoAlcance = new List<TipoAlcance>();

                tipoAlcanceENTRes.OK = 0;
                tipoAlcanceENTRes.Mensaje = "ERROR: Service: GetTipoAlcance, Source: " + ex.Source + ", Message: " + ex.Message;

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetTipoAlcance, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return tipoAlcanceENTRes;
        }
    }
}
