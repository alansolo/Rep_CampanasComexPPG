using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosCampanasPPG.Usuario
{
    public class UsuarioDAT: BD
    {
        public DataTable GetUsuario(string PPGID, string Correo, string Nombre)
        {
            const string spName = "BuscarUsuario";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro;

            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@PPGID";
                if (!string.IsNullOrEmpty(PPGID))
                {
                    Parametro.Value = PPGID;
                }
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@Correo";
                if (!string.IsNullOrEmpty(Correo))
                {
                    Parametro.Value = Correo;
                }
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@Nombre";
                if (!string.IsNullOrEmpty(Nombre))
                {
                    Parametro.Value = Nombre;
                }
                ListParametros.Add(Parametro);

                return base.ExecuteDataTable(spName, ListParametros);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public DataTable AddUsuario(string PpgId, string Nombre, string Correo, Nullable<int> IdRol, int Tipo)
        {
            const string spName = "UpdateUsuarioCrono";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro;

            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@PPGID";
                Parametro.Value = PpgId;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@Nombre";
                Parametro.Value = Nombre;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@Correo";
                Parametro.Value = Correo;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@ID_Rol";
                Parametro.Value = IdRol;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@V";
                Parametro.Value = Tipo;
                ListParametros.Add(Parametro);

                return base.ExecuteDataTable(spName, ListParametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int EditUsuario(string PpgId, string Nombre, string Correo, Nullable<int> IdRol, int Tipo)
        {
            const string spName = "UpdateUsuarioCrono";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro;

            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@PPGID";
                Parametro.Value = PpgId;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@Nombre";
                Parametro.Value = Nombre;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@Correo";
                Parametro.Value = Correo;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@ID_Rol";
                Parametro.Value = IdRol;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@V";
                Parametro.Value = Tipo;
                ListParametros.Add(Parametro);

                return base.ExecuteNonQuery(spName, ListParametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int RemoveUsuario(string PpgId, string Nombre, string Correo, Nullable<int> IdRol, int Tipo)
        {
            const string spName = "UpdateUsuarioCrono";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro;

            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@PPGID";
                Parametro.Value = PpgId;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@Nombre";
                Parametro.Value = Nombre;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@Correo";
                Parametro.Value = Correo;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@ID_Rol";
                Parametro.Value = IdRol;
                ListParametros.Add(Parametro);

                Parametro = new SqlParameter();
                Parametro.ParameterName = "@V";
                Parametro.Value = Tipo;
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
