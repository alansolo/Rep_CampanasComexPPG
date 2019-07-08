using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosCampanasPPG.Configuracion
{
    public class ConfiguracionDAT:BD
    {
        public DataTable GetUsuarioPasswordShareP(string UsuarioShareP, string PasswordShareP)
        {
            const string spName = "MostrarUsuarioPasswordShareP";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro;

            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@ClvCamp";
                Parametro.Value = UsuarioShareP;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@NombreCamp";
                Parametro.Value = PasswordShareP;
                ListParametros.Add(Parametro);

                return base.ExecuteDataTable(spName, ListParametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int EditUsuarioPasswordShareP(string UsuarioShareP, string PasswordShareP)
        {
            const string spName = "EditUsuarioPasswordShareP";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro;

            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@UsuarioSharePoint";
                Parametro.Value = UsuarioShareP;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@PasswordSharePoint";
                Parametro.Value = PasswordShareP;
                ListParametros.Add(Parametro);

                return base.ExecuteNonQuery(spName, ListParametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
