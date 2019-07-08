using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosCampanasPPG.Campana
{
    public class CampanaDAT:BD
    {
        public DataTable GetCampana(int IdCampana, string ClaveCampana, string NombreCampana, string LiderCampana)
        {
            const string spName = "MostrarDetCampania";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro;

            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@IDCampania";
                if (IdCampana > 0)
                {
                    Parametro.Value = IdCampana;
                }
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@ClvCamp";
                Parametro.Value = ClaveCampana;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@NombreCamp";
                Parametro.Value = NombreCampana;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@Lider_PPG";
                Parametro.Value = LiderCampana;
                ListParametros.Add(Parametro);

                return base.ExecuteDataTable(spName, ListParametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetCampanaUsuario(string PPGID)
        {
            const string spName = "MostrarCampanaUsuario";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro;

            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@PPGID";
                Parametro.Value = PPGID;
                ListParametros.Add(Parametro);

                return base.ExecuteDataTable(spName, ListParametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetCampanaCronograma(int IdCampana, string ClaveCampana, string NombreCampana, string LiderCampana)
        {
            const string spName = "MostrarDetCampaniaCrono";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro;

            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@IDCampania";
                if (IdCampana > 0)
                {
                    Parametro.Value = IdCampana;
                }
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@ClvCamp";
                Parametro.Value = ClaveCampana;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@NombreCamp";
                Parametro.Value = NombreCampana;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@Lider_PPG";
                Parametro.Value = LiderCampana;
                ListParametros.Add(Parametro);

                return base.ExecuteDataTable(spName, ListParametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataSet GetCampanaMecanica(Nullable<int> IdCampana)
        {
            const string spName = "MostrarMecanica";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro;

            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@ID_Campania";
                if (IdCampana > 0)
                {
                    Parametro.Value = IdCampana;
                }
                ListParametros.Add(Parametro);

                return base.ExecuteDataSet(spName, ListParametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
