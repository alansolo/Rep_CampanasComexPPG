using CampanasComexPPG.Models.Configuracion;
using EntidadesCampanasPPG.BDCampana;
using EntidadesCampanasPPG.Configuracion;
using NegocioCampanasPPG.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilidadesCampanasPPG;

namespace CampanasComexPPG.Controllers
{
    public class ConfiguracionController : Controller
    {
        // GET: Configuracion
        [HttpGet]
        public ActionResult Configuracion()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Inicio", "Inicio");
            }

            return View();
        }
        [HttpPost]
        public JsonResult GetUsuarioPasswordShareP()
        {
            //OBTENER USUARIO
            UsuarioPasswordSharePMOD usuarioPasswordSharePMOD = new UsuarioPasswordSharePMOD();

            ConfiguracionNEG configuracionNEG = new ConfiguracionNEG();

            ConfiguracionENT configuracionENTRes = new ConfiguracionENT();

            ConfiguracionENT configuracionENTReq = new ConfiguracionENT();

            Usuario usuarioSession = new Usuario();

            try
            {
                usuarioSession = (Usuario)Session["Usuario"];

                configuracionENTRes = configuracionNEG.GetUsuarioPasswordShareP(configuracionENTReq);

                if (!string.IsNullOrEmpty(configuracionENTRes.UsuarioShareP) && !string.IsNullOrEmpty(configuracionENTRes.PasswordShareP))
                {
                    usuarioPasswordSharePMOD.UsuarioShareP = configuracionENTRes.UsuarioShareP;
                    usuarioPasswordSharePMOD.PasswordShareP = configuracionENTRes.PasswordShareP;

                    usuarioPasswordSharePMOD.OK = configuracionENTRes.OK;
                    usuarioPasswordSharePMOD.Mensaje = "OK";
                }
                else
                {
                    usuarioPasswordSharePMOD.OK = 0;
                    usuarioPasswordSharePMOD.Mensaje = "ERROR: Ocurrio un error inesperado, no se encontro informacion del usuario y password de Share Point, intente de nuevo o consulte al administrador.";
                }

            }
            catch(Exception ex)
            {
                usuarioPasswordSharePMOD.OK = 0;
                usuarioPasswordSharePMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetUsuarioPasswordShareP, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            usuarioPasswordSharePMOD.MenuUsuario = usuarioSession.MenuUsuario;
            usuarioPasswordSharePMOD.MenuGrafico = usuarioSession.MenuGrafico;
            usuarioPasswordSharePMOD.MenuCronograma = usuarioSession.MenuCronograma;
            usuarioPasswordSharePMOD.MenuConfiguracion = usuarioSession.MenuConfiguracion;

            return Json(usuarioPasswordSharePMOD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EditUsuarioPasswordShareP(string usuarioShareP, string passwordShareP)
        {
            //OBTENER USUARIO
            UsuarioPasswordSharePMOD usuarioPasswordSharePMOD = new UsuarioPasswordSharePMOD();

            ConfiguracionNEG configuracionNEG = new ConfiguracionNEG();

            ConfiguracionENT configuracionENTRes = new ConfiguracionENT();

            ConfiguracionENT configuracionENTReq = new ConfiguracionENT();

            try
            {
                configuracionENTReq.UsuarioShareP = usuarioShareP;
                configuracionENTReq.PasswordShareP = passwordShareP;

                configuracionENTRes = configuracionNEG.EditUsuarioPasswordShareP(configuracionENTReq);

                if (configuracionENTRes.OK == 1)
                {
                    usuarioPasswordSharePMOD.OK = configuracionENTRes.OK;
                    usuarioPasswordSharePMOD.Mensaje = "Se actualizo correctamente el usuario y password de Share Point.";
                }
                else
                {
                    usuarioPasswordSharePMOD.OK = 0;
                    usuarioPasswordSharePMOD.Mensaje = "ERROR: Ocurrio un error inesperado, no se actualizo correctamente el usuario y password de Share Point, intente de nuevo o consulte al administrador de sistemas.";
                }
            }
            catch(Exception ex)
            {
                usuarioPasswordSharePMOD.OK = 0;
                usuarioPasswordSharePMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas. ";

                ArchivoLog.EscribirLog(null, "ERROR: Service: EditUsuarioPasswordShareP, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            return Json(usuarioPasswordSharePMOD, JsonRequestBehavior.AllowGet);
        }
    }
}