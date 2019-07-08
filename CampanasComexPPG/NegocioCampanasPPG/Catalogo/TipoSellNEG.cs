using DatosCampanasPPG.Catalogo;
using EntidadesCampanasPPG.Catalogo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCampanasPPG.BDCampana;
using System.Data;
using UtilidadesCampanasPPG;

namespace NegocioCampanasPPG.Catalogo
{
    public class TipoSellNEG
    {
        public TipoSellENT GetTipoSell(TipoSellENT TipoSellENTReq)
        {
            TipoSellENT TipoSellENTRes = new TipoSellENT();
            TipoSell tipoSell = new TipoSell();
            DataTable dtTipoSell = new DataTable();

            try
            {
                tipoSell = TipoSellENTReq.ListTipoSell.FirstOrDefault();

                if(tipoSell == null)
                {
                    return TipoSellENTRes;
                }

                TipoSellDAT tipoSellDAT = new TipoSellDAT();

                dtTipoSell = tipoSellDAT.GetTipoSell(tipoSell.ID, tipoSell.Sell);

                TipoSellENTRes.ListTipoSell = dtTipoSell.AsEnumerable()
                                            .Select(row => new TipoSell
                                            {
                                                ID = row.Field<int?>("ID").GetValueOrDefault(),
                                                Sell = row.Field<string>("TipoSell"),
                                                Comentarios = row.Field<string>("Comentarios")
                                            }).ToList();

                TipoSellENTRes.OK = 0;
                TipoSellENTRes.Mensaje = "OK";
            }
            catch (Exception ex)
            {
                TipoSellENTRes.ListTipoSell = new List<TipoSell>();

                TipoSellENTRes.OK = 0;
                TipoSellENTRes.Mensaje = "ERROR: Service: GetTipoSell, Source: " + ex.Source + ", Message: " + ex.Message;

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetTipoSell, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return TipoSellENTRes;
        }
    }
}
