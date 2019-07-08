using DatosCampanasPPG.Catalogo;
using EntidadesCampanasPPG.BDCampana;
using EntidadesCampanasPPG.Catalogo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilidadesCampanasPPG;

namespace NegocioCampanasPPG.Catalogo
{
    public class ParametroNEG
    {
        public ParametroENT GetParametro()
        {
            ParametroENT parametroENTRes = new ParametroENT();
            ParametroDAT parametroDAT = new ParametroDAT();

            DataTable dtParametro = new DataTable();

            try
            {
                dtParametro = parametroDAT.GetParametro(0, null);

                parametroENTRes.ListParametro = dtParametro.AsEnumerable()
                                    .Select(n => new Parametro
                                    {
                                        Id = n.Field<int?>("Id").GetValueOrDefault(),
                                        Nombre = n.Field<string>("Nombre"),
                                        Valor = n.Field<string>("Valor")
                                    }).ToList();

                parametroENTRes.Mensaje = "OK";
                parametroENTRes.OK = 1;
            }
            catch(Exception ex)
            {
                parametroENTRes.ListParametro = new List<Parametro>(); ;

                parametroENTRes.Mensaje = "ERROR: Service: GetParametro, Source: " + ex.Source + ", Message: " + ex.Message;
                parametroENTRes.OK = 0;

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetParametro, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            return parametroENTRes;
        }
    }
}
