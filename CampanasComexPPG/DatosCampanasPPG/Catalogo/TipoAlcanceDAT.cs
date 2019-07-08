using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosCampanasPPG.Catalogo
{
    public class TipoAlcanceDAT:BD
    {
        public DataTable GetTipoAlcance(Nullable<int> Id, string Alcance)
        {
            const string spName = "MostrarTipoAlcance";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro;

            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@ID";
                if (Id != null && Id > 0)
                {
                    Parametro.Value = Id;
                }
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@Alcance";
                Parametro.Value = Alcance;
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
