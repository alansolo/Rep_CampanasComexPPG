using DatosCampanasPPG.Campana;
using EntidadesCampanasPPG.Campana;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilidadesCampanasPPG;

namespace NegocioCampanasPPG.Campana
{
    public class CronogramaNEG
    {
        public CronogramaENT GetCronograma(CronogramaENT CronogramaENTReq)
        {
            CronogramaENT CronogramaENTRes = new CronogramaENT();

            try
            {
                EntidadesCampanasPPG.BDCampana.Cronograma cronograma = CronogramaENTReq.ListCronograma.FirstOrDefault();

                DataTable dtCronograma = new DataTable();

                CronogramaDAT cronogramaDAT = new CronogramaDAT();

                dtCronograma = cronogramaDAT.GetCronograma(cronograma.IDCampania);

                CronogramaENTRes.ListCronograma = dtCronograma.AsEnumerable()
                                           .Select(row => new EntidadesCampanasPPG.BDCampana.Cronograma
                                           {
                                               ID = row.Field<int?>("ID").GetValueOrDefault(),
                                               IDCampania = row.Field<int?>("IDCampania").GetValueOrDefault(),
                                               IDPadre = row.Field<int?>("IDPadre").GetValueOrDefault(),
                                               Padre = row.Field<bool?>("Padre").GetValueOrDefault(),
                                               IDTarea = row.Field<int?>("IDTarea").GetValueOrDefault(),
                                               Actividad = row.Field<string>("Actividad"),
                                               PorcentajeUsuario = row.Field<decimal?>("PorcentajeUsuario").GetValueOrDefault(),
                                               PorcentajeSistema = row.Field<decimal?>("PorcentajeSistema").GetValueOrDefault(),
                                               PorcentajeUsuarioEsfuerzo = row.Field<decimal?>("PorcentajeUsuEsfuerzo").GetValueOrDefault(),
                                               PorcentajeSistemaReal = row.Field<decimal?>("PorcentajeSistemaReal").GetValueOrDefault(),
                                               PorcentajeDiferencia = row.Field<decimal?>("PorcentajeDiferencia").GetValueOrDefault(),
                                               Predecesor = row.Field<string>("Predecesor"),
                                               Duracion = row.Field<decimal?>("Duracion").GetValueOrDefault(),
                                               FechaInicio = row.Field<DateTime?>("FechaInicio").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                               FechaFin = row.Field<DateTime?>("FechaFin").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                               FechaInicioReal = row.Field<DateTime?>("FechaInicioReal").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                               FechaFinReal = row.Field<DateTime?>("FechaFinReal").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                               TiempoOptimista = row.Field<int?>("TiempoOptimista").GetValueOrDefault(),
                                               TiempoPesimista = row.Field<int?>("TiempoPesimista").GetValueOrDefault(),
                                               NombreResponsable = row.Field<string>("NombreResponsable"),
                                               Correo = row.Field<string>("Correo"),
                                               PPGID = row.Field<string>("PPGID"),
                                               NombreResponsable_2 = row.Field<string>("NombreResponsable_2"),
                                               Correo_2 = row.Field<string>("Correo_2"),
                                               PPGID_2 = row.Field<string>("PPGID_2"),
                                               Comentario = row.Field<string>("Comentario"),
                                               VersionCronograma = row.Field<int?>("VersionCronograma").GetValueOrDefault(),
                                               FechaHoy = row.Field<DateTime?>("FechaHoy").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                               FechaCreacion = row.Field<DateTime?>("FechaCreacion").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                               FechaModificacion = row.Field<DateTime?>("FechaModificacion").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                               UsuarioCreacion = row.Field<string>("UsuarioCreacion"),
                                               UsuarioModificacion = row.Field<string>("UsuarioModificacion"),
                                               TipoFlujo = row.Field<string>("TipoFlujo"),
                                               Incluir = row.Field<string>("Incluir"),
                                               EstatusEnvio = row.Field<int?>("EstatusEnvio").GetValueOrDefault(),
                                               Update = false
                                           }).ToList();

                CronogramaENTRes.Mensaje = "OK";

                CronogramaENTRes.OK = 1;
            }
            catch (Exception ex)
            {
                CronogramaENTRes.Mensaje = "ERROR: Service: GetCronograma, Source: " + ex.Source + ", Message: " + ex.Message;

                CronogramaENTRes.OK = 0;

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetCronograma, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return CronogramaENTRes;
        }
        public CronogramaENT EditCronograma(CronogramaENT CronogramaENTReq)
        {
            CronogramaENT CronogramaENTRes = new CronogramaENT();
            CronogramaENTRes.ListCronograma = new List<EntidadesCampanasPPG.BDCampana.Cronograma>();
            DataTable dtCronograma = new DataTable();

            try
            {
                CronogramaDAT cronogramaDAT = new CronogramaDAT();

                dtCronograma = cronogramaDAT.EditCronograma(CronogramaENTReq.ListCronograma);

                CronogramaENTRes.ListCronograma = dtCronograma.AsEnumerable()
                           .Select(row => new EntidadesCampanasPPG.BDCampana.Cronograma
                           {
                               ID = row.Field<int?>("ID").GetValueOrDefault(),
                               IDCampania = row.Field<int?>("IDCampania").GetValueOrDefault(),
                               IDPadre = row.Field<int?>("IDPadre").GetValueOrDefault(),
                               Padre = row.Field<bool?>("Padre").GetValueOrDefault(),
                               IDTarea = row.Field<int?>("IDTarea").GetValueOrDefault(),
                               Actividad = row.Field<string>("Actividad"),
                               PorcentajeUsuario = row.Field<decimal?>("PorcentajeUsuario").GetValueOrDefault(),
                               PorcentajeSistema = row.Field<decimal?>("PorcentajeSistema").GetValueOrDefault(),
                               PorcentajeDiferencia = row.Field<decimal?>("PorcentajeDiferencia").GetValueOrDefault(),
                               Predecesor = row.Field<string>("Predecesor"),
                               Duracion = row.Field<decimal?>("Duracion").GetValueOrDefault(),
                               FechaInicio = row.Field<DateTime?>("FechaInicio").GetValueOrDefault().ToString("dd/MM/yyyy"),
                               FechaFin = row.Field<DateTime?>("FechaFin").GetValueOrDefault().ToString("dd/MM/yyyy"),
                               FechaInicioReal = row.Field<DateTime?>("FechaInicioReal").GetValueOrDefault().ToString("dd/MM/yyyy"),
                               FechaFinReal = row.Field<DateTime?>("FechaFinReal").GetValueOrDefault().ToString("dd/MM/yyyy"),
                               TiempoOptimista = row.Field<int?>("TiempoOptimista").GetValueOrDefault(),
                               TiempoPesimista = row.Field<int?>("TiempoPesimista").GetValueOrDefault(),
                               NombreResponsable = row.Field<string>("NombreResponsable"),
                               Correo = row.Field<string>("Correo"),
                               PPGID = row.Field<string>("PPGID"),
                               NombreResponsable_2 = row.Field<string>("NombreResponsable_2"),
                               Correo_2 = row.Field<string>("Correo_2"),
                               PPGID_2 = row.Field<string>("PPGID_2"),
                               Comentario = row.Field<string>("Comentario"),
                               VersionCronograma = row.Field<int?>("VersionCronograma").GetValueOrDefault(),
                               FechaHoy = row.Field<DateTime?>("FechaHoy").GetValueOrDefault().ToString("dd/MM/yyyy"),
                               FechaCreacion = row.Field<DateTime?>("FechaCreacion").GetValueOrDefault().ToString("dd/MM/yyyy"),
                               FechaModificacion = row.Field<DateTime?>("FechaModificacion").GetValueOrDefault().ToString("dd/MM/yyyy"),
                               UsuarioCreacion = row.Field<string>("UsuarioCreacion"),
                               UsuarioModificacion = row.Field<string>("UsuarioModificacion"),
                               IdTipoFlujo = row.Field<int?>("IdTipoFlujo").GetValueOrDefault(),
                               TipoFlujo = row.Field<string>("TipoFlujo"),
                               IdIncluir = row.Field<int?>("IdIncluir").GetValueOrDefault(),
                               Incluir = row.Field<string>("Incluir")
                           }).ToList();

                CronogramaENTRes.Mensaje = "OK";

                CronogramaENTRes.OK = 1;

            }
            catch(Exception ex)
            {
                CronogramaENTRes.Mensaje = "ERROR: Service: EditCronograma, Source: " + ex.Source + ", Message: " + ex.Message;

                CronogramaENTRes.OK = 0;

                ArchivoLog.EscribirLog(null, "ERROR: Service: EditCronograma, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return CronogramaENTRes;
        }
        public CronogramaENT ReSaveCronograma(CronogramaENT CronogramaENTReq)
        {

            CronogramaENT CronogramaENTRes = new CronogramaENT();
            CronogramaENTRes.ListCronograma = new List<EntidadesCampanasPPG.BDCampana.Cronograma>();
            DataTable dtCronograma = new DataTable();

            try
            {
                CronogramaDAT cronogramaDAT = new CronogramaDAT();

                CronogramaENTReq.ListCronograma = CronogramaENTReq.ListCronograma.Where(n => n.IdIncluir == 1).ToList();

                dtCronograma = cronogramaDAT.SaveReCronograma(CronogramaENTReq.ListCronograma);

                CronogramaENTRes.ListCronograma = dtCronograma.AsEnumerable()
                           .Select(row => new EntidadesCampanasPPG.BDCampana.Cronograma
                           {
                               ID = row.Field<int?>("ID").GetValueOrDefault(),
                               IDCampania = row.Field<int?>("IDCampania").GetValueOrDefault(),
                               IDPadre = row.Field<int?>("IDPadre").GetValueOrDefault(),
                               Padre = row.Field<bool?>("Padre").GetValueOrDefault(),
                               IDTarea = row.Field<int?>("IDTarea").GetValueOrDefault(),
                               Actividad = row.Field<string>("Actividad"),
                               PorcentajeUsuario = row.Field<decimal?>("PorcentajeUsuario").GetValueOrDefault(),
                               PorcentajeSistema = row.Field<decimal?>("PorcentajeSistema").GetValueOrDefault(),
                               PorcentajeDiferencia = row.Field<decimal?>("PorcentajeDiferencia").GetValueOrDefault(),
                               PorcentajeSistemaReal = row.Field<decimal?>("PorcentajeSistemaReal").GetValueOrDefault(),
                               PorcentajeUsuarioEsfuerzo = row.Field<decimal?>("PorcentajeUsuEsfuerzo").GetValueOrDefault(),
                               Predecesor = row.Field<string>("Predecesor"),
                               Duracion = row.Field<decimal?>("Duracion").GetValueOrDefault(),
                               FechaInicio = row.Field<DateTime?>("FechaInicio").GetValueOrDefault().ToString("dd/MM/yyyy"),
                               FechaFin = row.Field<DateTime?>("FechaFin").GetValueOrDefault().ToString("dd/MM/yyyy"),
                               FechaInicioReal = row.Field<DateTime?>("FechaInicioReal").GetValueOrDefault().ToString("dd/MM/yyyy"),
                               FechaFinReal = row.Field<DateTime?>("FechaFinReal").GetValueOrDefault().ToString("dd/MM/yyyy"),
                               TiempoOptimista = row.Field<int?>("TiempoOptimista").GetValueOrDefault(),
                               TiempoPesimista = row.Field<int?>("TiempoPesimista").GetValueOrDefault(),
                               NombreResponsable = row.Field<string>("NombreResponsable"),
                               Correo = row.Field<string>("Correo"),
                               PPGID = row.Field<string>("PPGID"),
                               NombreResponsable_2 = row.Field<string>("NombreResponsable_2"),
                               Correo_2 = row.Field<string>("Correo_2"),
                               PPGID_2 = row.Field<string>("PPGID_2"),
                               Comentario = row.Field<string>("Comentario"),
                               VersionCronograma = row.Field<int?>("VersionCronograma").GetValueOrDefault(),
                               FechaHoy = row.Field<DateTime?>("FechaHoy").GetValueOrDefault().ToString("dd/MM/yyyy"),
                               FechaCreacion = row.Field<DateTime?>("FechaCreacion").GetValueOrDefault().ToString("dd/MM/yyyy"),
                               FechaModificacion = row.Field<DateTime?>("FechaModificacion").GetValueOrDefault().ToString("dd/MM/yyyy"),
                               UsuarioCreacion = row.Field<string>("UsuarioCreacion"),
                               UsuarioModificacion = row.Field<string>("UsuarioModificacion"),
                               IdTipoFlujo = row.Field<int?>("IdTipoFlujo").GetValueOrDefault(),
                               TipoFlujo = row.Field<string>("TipoFlujo"),
                               IdIncluir = row.Field<int?>("IdIncluir").GetValueOrDefault(),
                               Incluir = row.Field<string>("Incluir")
                           }).ToList();

                CronogramaENTRes.Mensaje = "OK";

                CronogramaENTRes.OK = 1;

            }
            catch (Exception ex)
            {
                CronogramaENTRes.Mensaje = "ERROR: Service: ReSaveCronograma, Source: " + ex.Source + ", Message: " + ex.Message;

                CronogramaENTRes.OK = 0;

                ArchivoLog.EscribirLog(null, "ERROR: Service: ReSaveCronograma, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return CronogramaENTRes;
        }
    }
}
