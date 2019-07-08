using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadesCampanasPPG.BD;
using EntidadesCampanasPPG.BDCampana;
using System.Globalization;

namespace DatosCampanasPPG.Campana
{
    public class CronogramaDAT : BD
    {
        public DataTable GetCronograma(int IdCampana)
        {
            const string spName = "MostrarCronograma";
            List<SqlParameter> ListParametros = new List<SqlParameter>();
            SqlParameter Parametro;

            try
            {
                Parametro = new SqlParameter();
                Parametro.ParameterName = "@IDCampania";
                Parametro.Value = IdCampana;
                ListParametros.Add(Parametro);

                return base.ExecuteDataTable(spName, ListParametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable EditCronograma(List<Cronograma> ListCronograma)
        {
            const string spName = "UpdateCronogramaVersion";

            List<SqlParameter> ListParametros = new List<SqlParameter>();
            List<SqlParameterGroup> ListParametrosGrupo = new List<SqlParameterGroup>();

            SqlParameterGroup ParameterGroup;
            SqlParameter Parametro;

            try
            {
                IFormatProvider culture = new CultureInfo("es-MX", true);

                foreach (Cronograma cronograma in ListCronograma)
                {
                    ListParametros = new List<SqlParameter>();

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@IDCampania";
                    Parametro.Value = cronograma.IDCampania;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@IDTarea";
                    Parametro.Value = cronograma.IDTarea;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@Duracion";
                    Parametro.Value = cronograma.Duracion;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@FechaInicio";
                    Parametro.Value = DateTime.ParseExact(cronograma.FechaInicio, "dd/MM/yyyy", culture);
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@FechaFin";
                    Parametro.Value = DateTime.ParseExact(cronograma.FechaFin, "dd/MM/yyyy", culture);
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@FechaInicioReal";
                    Parametro.Value = DateTime.ParseExact(cronograma.FechaInicioReal, "dd/MM/yyyy", culture);
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@FechaFinReal";
                    Parametro.Value = DateTime.ParseExact(cronograma.FechaFinReal, "dd/MM/yyyy", culture);
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@Comentario";
                    Parametro.Value = cronograma.Comentario;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@PorcentajeUsu";
                    Parametro.Value = cronograma.PorcentajeUsuario;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@NombreResponsable";
                    Parametro.Value = cronograma.NombreResponsable;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@Correo";
                    Parametro.Value = cronograma.Correo;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@PPGID";
                    Parametro.Value = cronograma.PPGID;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@NombreResponsable_2";
                    Parametro.Value = cronograma.NombreResponsable_2;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@Correo_2";
                    Parametro.Value = cronograma.Correo_2;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@PPGID_2";
                    Parametro.Value = cronograma.PPGID_2;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@Version";
                    Parametro.Value = cronograma.VersionCronograma;
                    ListParametros.Add(Parametro);

                    ParameterGroup = new SqlParameterGroup();
                    ParameterGroup.ListSqlParameter = ListParametros;

                    ListParametrosGrupo.Add(ParameterGroup);
                }

                return base.ExecuteDataTable(spName, ListParametrosGrupo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable SaveReCronograma(List<Cronograma> ListCronograma)
        {
            string resultado = string.Empty;
            const string spName = "UpdateCronogramaNuevaVersion";

            List<SqlParameter> ListParametros = new List<SqlParameter>();
            List<SqlParameterGroup> ListParametrosGrupo = new List<SqlParameterGroup>();

            SqlParameterGroup ParameterGroup;
            SqlParameter Parametro;

            try
            {
                IFormatProvider culture = new CultureInfo("es-MX", true);

                foreach (Cronograma cronograma in ListCronograma)
                {
                    ListParametros = new List<SqlParameter>();

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@IDCampania";
                    Parametro.Value = cronograma.IDCampania;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@IDTarea";
                    Parametro.Value = cronograma.IDTarea;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@IDPadre";
                    Parametro.Value = cronograma.IDPadre;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@Padre";
                    Parametro.Value = cronograma.Padre;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@Actividad";
                    Parametro.Value = cronograma.Actividad;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@Duracion";
                    Parametro.Value = cronograma.Duracion;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@FechaInicio";
                    Parametro.Value = DateTime.ParseExact(cronograma.FechaInicio, "dd/MM/yyyy", culture);
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@FechaFin";
                    Parametro.Value = DateTime.ParseExact(cronograma.FechaFin, "dd/MM/yyyy", culture);
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@FechaInicioReal";
                    Parametro.Value = DateTime.ParseExact(cronograma.FechaInicioReal, "dd/MM/yyyy", culture);
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@FechaFinReal";
                    Parametro.Value = DateTime.ParseExact(cronograma.FechaFinReal, "dd/MM/yyyy", culture);
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@Comentario";
                    Parametro.Value = cronograma.Comentario;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@PorcentajeUsu";
                    Parametro.Value = cronograma.PorcentajeUsuario;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@PorcentajeSis";
                    Parametro.Value = cronograma.PorcentajeSistema;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@PorcentajeUsuEsfuerzo";
                    Parametro.Value = cronograma.PorcentajeUsuarioEsfuerzo;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@PorcentajeSistemaReal";
                    Parametro.Value = cronograma.PorcentajeSistemaReal;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@NombreResponsable";
                    Parametro.Value = cronograma.NombreResponsable;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@Correo";
                    Parametro.Value = cronograma.Correo;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@PPGID";
                    Parametro.Value = cronograma.PPGID;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@NombreResponsable_2";
                    Parametro.Value = cronograma.NombreResponsable_2;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@Correo_2";
                    Parametro.Value = cronograma.Correo_2;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@PPGID_2";
                    Parametro.Value = cronograma.PPGID_2;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@Version";
                    Parametro.Value = cronograma.VersionCronograma;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@TiempoPesimista";
                    Parametro.Value = cronograma.TiempoPesimista;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@TiempoOptimista";
                    Parametro.Value = cronograma.TiempoOptimista;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@Predecesor";
                    Parametro.Value = cronograma.Predecesor;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@UsuarioCreacion";
                    Parametro.Value = cronograma.UsuarioCreacion;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@UsuarioModificacion";
                    Parametro.Value = cronograma.UsuarioModificacion;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@IdTipoFlujo";
                    Parametro.Value = cronograma.IdTipoFlujo;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@TipoFlujo";
                    Parametro.Value = cronograma.TipoFlujo;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@IdIncluir";
                    Parametro.Value = cronograma.IdIncluir;
                    ListParametros.Add(Parametro);

                    Parametro = new SqlParameter();
                    Parametro.ParameterName = "@Incluir";
                    Parametro.Value = cronograma.Incluir;
                    ListParametros.Add(Parametro);

                    ParameterGroup = new SqlParameterGroup();
                    ParameterGroup.ListSqlParameter = ListParametros;

                    ListParametrosGrupo.Add(ParameterGroup);
                }

                return base.ExecuteDataTable(spName, ListParametrosGrupo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
