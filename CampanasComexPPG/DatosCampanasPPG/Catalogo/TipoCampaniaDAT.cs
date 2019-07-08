using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosCampanasPPG.Catalogo
{
    public class TipoCampaniaDAT:BD
    {
        public DataTable GetTipoCampania(Nullable<int> Id, string TipoCamp)
        {
            const string spName = "MostrarTipoCampania";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro;

            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@ID";
                if(Id != null && Id > 0)
                {
                    Parametro.Value = Id;
                }             
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@TipoCamp";
                if(TipoCamp != null)
                {
                    Parametro.Value = TipoCamp;
                }
                ListParametros.Add(Parametro);

                return base.ExecuteDataTable(spName, ListParametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
