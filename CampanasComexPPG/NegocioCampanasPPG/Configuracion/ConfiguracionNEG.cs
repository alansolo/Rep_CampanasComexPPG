using DatosCampanasPPG.Catalogo;
using DatosCampanasPPG.Configuracion;
using EntidadesCampanasPPG.BDCampana;
using EntidadesCampanasPPG.Configuracion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilidadesCampanasPPG;

namespace NegocioCampanasPPG.Configuracion
{
    public class ConfiguracionNEG
    {
        public ConfiguracionENT GetUsuarioPasswordShareP(ConfiguracionENT configuracionENTReq)
        {
            ConfiguracionENT configuracionENTRes = new ConfiguracionENT();

            ParametroDAT parametroDAT = new ParametroDAT();

            DataTable dtParametro = new DataTable();

            DataRow dr;

            try
            {
                dtParametro = parametroDAT.GetParametro(0, null);

                dr = dtParametro.AsEnumerable().Where(n => n["Nombre"].ToString().ToUpper() ==
                                                    ConfigurationManager.AppSettings["UsuarioSharePoint"].ToString().ToUpper()).FirstOrDefault();
                if (dr != null)
                {
                    configuracionENTRes.UsuarioShareP = dr["Valor"].ToString();
                }

                dr = dtParametro.AsEnumerable().Where(n => n["Nombre"].ToString().ToUpper() ==
                                                    ConfigurationManager.AppSettings["PasswordSharePoint"].ToString().ToUpper()).FirstOrDefault();
                if (dr != null)
                {
                    configuracionENTRes.PasswordShareP = dr["Valor"].ToString();
                }

                configuracionENTRes.OK = 1;
                configuracionENTRes.Mensaje = "OK";
            }
            catch(Exception ex)
            {
                configuracionENTRes.OK = 0;
                configuracionENTRes.Mensaje = "ERROR: Service: GetUsuarioPasswordShareP, Source: " + ex.Source + ", Message: " + ex.Message;

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetUsuarioPasswordShareP, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return configuracionENTRes;
        }
        public ConfiguracionENT EditUsuarioPasswordShareP(ConfiguracionENT configuracionENTReq)
        {
            ConfiguracionENT configuracionENTRes = new ConfiguracionENT();
            ConfiguracionDAT configuracionDAT = new ConfiguracionDAT();

            int resultado = 0;

            try
            {
                resultado = configuracionDAT.EditUsuarioPasswordShareP(configuracionENTReq.UsuarioShareP,
                                                       configuracionENTReq.PasswordShareP);

                if (resultado > 0)
                {
                    configuracionENTRes.Mensaje = "OK";
                    configuracionENTRes.OK = 1;
                }
                else
                {
                    configuracionENTRes.Mensaje = "ERROR: Service: EditUsuarioPasswordShareP, Message: Ocurrio un problema con el SP para actualizar el Usuairo y Password de Share Point.";
                    configuracionENTRes.OK = 0;

                    ArchivoLog.EscribirLog(null, "ERROR: Service: EditUsuarioPasswordShareP, Message: Ocurrio un problema con el SP para actualizar el Usuairo y Password de Share Point.");
                }

            }
            catch (Exception ex)
            {
                configuracionENTRes.Mensaje = "ERROR: Service: EditUsuarioPasswordShareP, Source: " + ex.Source + ", Message: " + ex.Message;
                configuracionENTRes.OK = 0;

                ArchivoLog.EscribirLog(null, "ERROR: Service: EditUsuarioPasswordShareP, Source: " + ex.Source + ", Message: " + ex.Message);
            }
                       
            return configuracionENTRes;
        }


    }
}
