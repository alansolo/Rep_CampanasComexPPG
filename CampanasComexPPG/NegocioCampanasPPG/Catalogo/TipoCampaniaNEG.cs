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
    public class TipoCampaniaNEG
    {
        public TipoCampaniaENT GetTipoCampania(TipoCampaniaENT tipoCampaniaENTReq)
        {
            TipoCampaniaENT tipoCampaniaENTRes = new TipoCampaniaENT();
            TipoCampania tipoCampania = new TipoCampania();
            DataTable dtTipoCampania = new DataTable();

            try
            {
                tipoCampania = tipoCampaniaENTReq.ListTipoCampania.FirstOrDefault();

                if (tipoCampania == null)
                {
                    return tipoCampaniaENTRes;
                }

                TipoCampaniaDAT tipoCampaniaDAT = new TipoCampaniaDAT();

                dtTipoCampania = tipoCampaniaDAT.GetTipoCampania(tipoCampania.ID, tipoCampania.Tipo);

                tipoCampaniaENTRes.ListTipoCampania = dtTipoCampania.AsEnumerable()
                                            .Select(row => new TipoCampania
                                            {
                                                ID = row.Field<int?>("ID").GetValueOrDefault(),
                                                Tipo = row.Field<string>("TipoCamp"),
                                                Comentarios = row.Field<string>("Comentarios")
                                            }).ToList();

                tipoCampaniaENTRes.OK = 1;
                tipoCampaniaENTRes.Mensaje = "OK";
            }
            catch (Exception ex)
            {
                tipoCampaniaENTRes.ListTipoCampania = new List<TipoCampania>();

                tipoCampaniaENTRes.OK = 0;
                tipoCampaniaENTRes.Mensaje = "ERROR: Service: GetTipoCampania, Source: " + ex.Source + ", Message: " + ex.Message;

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetTipoCampania, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return tipoCampaniaENTRes;
        }
    }
}
