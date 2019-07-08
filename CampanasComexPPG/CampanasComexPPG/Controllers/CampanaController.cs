using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using net.sf.mpxj;
using net.sf.mpxj.mpp;
using net.sf.mpxj.reader;
using EntidadesCampanasPPG.Campana;
using CampanasComexPPG.Models.Campana;
using NegocioCampanasPPG.Campana;
using EntidadesCampanasPPG.BDCampana;
using NegocioCampanasPPG.Catalogo;
using EntidadesCampanasPPG.Catalogo;
using NegocioCampanasPPG.Ldap;
using EntidadesCampanasPPG.Ldap;
using System.Globalization;
using System.Configuration;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;
using UtilidadesCampanasPPG;
using System.Text;
using LinqToExcel;
using ClosedXML.Excel;
using System.Data;
using CampanasComexPPG.Session;

namespace CampanasPPG.Controllers
{
    [SessionTimeout]
    public class CampanaController : Controller
    {
        #region Campana

        // GET: Campana
        [HttpGet]
        public ActionResult Campana()
        {
            if(Session["Usuario"] == null)
            {
                return RedirectToAction("Inicio",  "Inicio");
            }

            return View();
        }
        [HttpPost]
        public JsonResult GetCampana()
        {
            //OBTENER USUARIO
            Usuario usuario = (Usuario)Session["Usuario"];

            CampanaMOD campanaMOD = new CampanaMOD();

            CampanaNEG campanaNEG = new CampanaNEG();
            CampanaENT campanaENTReq = new CampanaENT();

            Campana campana = new Campana();

            CampanaENT campanaENTRes = new CampanaENT();
            CampanaENT campanaUsuarioENTRes = new CampanaENT();

            List<Campana> ListCampanaUsuario = new List<Campana>();

            TipoSellNEG tipoSellNEG = new TipoSellNEG();
            TipoSellENT tipoSellENTReq = new TipoSellENT();

            TipoSell tipoSell = new TipoSell();
            TipoSellENT tipoSellENTRes = new TipoSellENT();

            TipoCampaniaNEG tipoCampaniaNEG = new TipoCampaniaNEG();
            TipoCampaniaENT tipoCampaniaENTReq = new TipoCampaniaENT();

            TipoCampaniaENT tipoCampaniaENTRes = new TipoCampaniaENT();

            TipoAlcanceNEG tipoAlcanceNEG = new TipoAlcanceNEG();
            TipoAlcanceENT tipoAlcanceENTReq = new TipoAlcanceENT();

            TipoCampania tipoCampania = new TipoCampania();
            TipoAlcance tipoAlcance = new TipoAlcance();

            TipoAlcanceENT tipoAlcanceENTRes = new TipoAlcanceENT();

            List<TipoUrgente> ListTipoUrgente = new List<TipoUrgente>();

            TipoUrgente tipoUrgente = new TipoUrgente();

            List<UsuarioLdap> ListUsuarioLdap = new List<UsuarioLdap>();

            try
            {
                campanaENTReq.ListCampana = new List<Campana>();

                campanaENTReq.ListCampana.Add(campana);


                //OBTENER CAMPAÑAS
                campanaENTRes = campanaNEG.GetCampana(campanaENTReq);

                //ROL ADMINISTRADOR
                if (usuario.ID_RolCronograma == 1 || usuario.ID_RolCronograma == 2)
                {
                    campanaMOD.ListCampana = campanaENTRes.ListCampana.OrderBy(n => n.ID).ToList();
                }
                //ROL LIDER CAMPAÑA
                else if (usuario.ID_RolCronograma == 3)
                {
                    campanaMOD.ListCampana = campanaENTRes.ListCampana.Where(n => n.PPGID_Lider.ToLower().Contains(usuario.PPGID.ToLower())).OrderBy(n => n.ID).ToList();
                }
                //ROL USUARIO
                else if (usuario.ID_RolCronograma == 4)
                {
                    //OBTENER CAMPAÑAS POR USUARIO
                    campana = new Campana();
                    campana.PPG_ID = usuario.PPGID;

                    campanaENTReq = new CampanaENT();
                    campanaENTReq.ListCampana = new List<Campana>();

                    campanaENTReq.ListCampana.Add(campana);

                    campanaUsuarioENTRes = campanaNEG.GetCampanaUsuario(campanaENTReq);

                    ListCampanaUsuario = (from campania in campanaENTRes.ListCampana
                                          from campaniaUsuario in campanaUsuarioENTRes.ListCampana
                                          where campania.ID == campaniaUsuario.ID
                                          select campania).ToList();


                    campanaMOD.ListCampana = ListCampanaUsuario.OrderBy(n => n.ID).ToList();
                }


                Session["ListCampana"] = campanaMOD.ListCampana;

                campanaMOD.ListCampanaTemp = campanaMOD.ListCampana;

                Session["ListCampanaTemp"] = campanaMOD.ListCampanaTemp;

                //FILTRO POR USUARIO Y ROL

                //CATALOGOS

                tipoSellENTReq.ListTipoSell = new List<TipoSell>();

                tipoSellENTReq.ListTipoSell.Add(tipoSell);



                tipoSellENTRes = tipoSellNEG.GetTipoSell(tipoSellENTReq);

                campanaMOD.ListTipoSell = tipoSellENTRes.ListTipoSell;

                Session["ListTipoSell"] = campanaMOD.ListTipoSell;



                tipoCampaniaENTReq.ListTipoCampania = new List<TipoCampania>();
                tipoCampaniaENTReq.ListTipoCampania.Add(tipoCampania);



                tipoCampaniaENTRes = tipoCampaniaNEG.GetTipoCampania(tipoCampaniaENTReq);

                campanaMOD.ListTipoCampania = tipoCampaniaENTRes.ListTipoCampania;

                Session["ListTipoCampania"] = campanaMOD.ListTipoCampania;



                tipoAlcanceENTReq.ListTipoAlcance = new List<TipoAlcance>();

                tipoAlcanceENTReq.ListTipoAlcance.Add(tipoAlcance);



                tipoAlcanceENTRes = tipoAlcanceNEG.GetTipoAlcance(tipoAlcanceENTReq);

                campanaMOD.ListTipoAlcance = tipoAlcanceENTRes.ListTipoAlcance;

                Session["ListTipoAlcance"] = campanaMOD.ListTipoAlcance;



                tipoUrgente.ID = 1;
                tipoUrgente.Descripcion = "No";
                ListTipoUrgente.Add(tipoUrgente);

                tipoUrgente = new TipoUrgente();
                tipoUrgente.ID = 2;
                tipoUrgente.Descripcion = "Si";
                ListTipoUrgente.Add(tipoUrgente);

                campanaMOD.ListTipoUrgente = ListTipoUrgente;

                Session["ListTipoUrgente"] = campanaMOD.ListTipoUrgente;


                //OBTENER USUARIO LDAP
                ListUsuarioLdap = (List<UsuarioLdap>)Session["ListUsuarioLdap"];

                campanaMOD.Mensaje = campanaENTRes.Mensaje;
                campanaMOD.OK = campanaENTRes.OK;
            }
            catch (Exception ex)
            {
                campanaMOD.OK = 0;
                campanaMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetCampana, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            campanaMOD.MenuUsuario = usuario.MenuUsuario;
            campanaMOD.MenuCronograma = usuario.MenuCronograma;
            campanaMOD.MenuGrafico = usuario.MenuGrafico;
            campanaMOD.MenuConfiguracion = usuario.MenuConfiguracion;


            return Json(campanaMOD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SearchCampana(string camp_Number, string nombre_Camp, TipoUrgente tipoUrgente, TipoSell tipoSell, TipoCampania tipoCampania, TipoAlcance tipoAlcance)
        {
            CampanaMOD campanaMOD = new CampanaMOD();

            List<Campana> ListCampanaTemp = new List<Campana>();

            bool express = false;

            try
            {
                if (Session["ListCampana"] != null)
                {
                    ListCampanaTemp = (List<Campana>)Session["ListCampana"];

                    if (!string.IsNullOrEmpty(camp_Number))
                    {
                        ListCampanaTemp = ListCampanaTemp.Where(n => n.Camp_Number.ToUpper().Contains(camp_Number.ToUpper())).ToList();
                    }

                    if (!string.IsNullOrEmpty(nombre_Camp))
                    {
                        ListCampanaTemp = ListCampanaTemp.Where(n => n.Nombre_Camp.ToUpper().Contains(nombre_Camp.ToUpper())).ToList();
                    }

                    if (tipoUrgente != null && tipoUrgente.ID > 0)
                    {
                        express = tipoUrgente.Descripcion == "Si" ? true : false;
                        ListCampanaTemp = ListCampanaTemp.Where(n => n.Express == express).ToList();
                    }

                    if (tipoCampania != null && tipoCampania.ID > 0)
                    {
                        ListCampanaTemp = ListCampanaTemp.Where(n => n.ID_TipoCamp == tipoCampania.ID).ToList();
                    }

                    if (tipoSell != null && tipoSell.ID > 0)
                    {
                        ListCampanaTemp = ListCampanaTemp.Where(n => n.ID_TipoSell == tipoSell.ID).ToList();
                    }

                    if (tipoAlcance != null && tipoAlcance.ID > 0)
                    {
                        ListCampanaTemp = ListCampanaTemp.Where(n => n.ID_Alcance == tipoAlcance.ID).ToList();
                    }
                }

                if (ListCampanaTemp.Count > 0)
                {
                    campanaMOD.ListCampanaTemp = ListCampanaTemp;

                    Session["ListCampanaTemp"] = campanaMOD.ListCampanaTemp;

                    campanaMOD.OK = 1;

                    campanaMOD.Mensaje = "OK";

                }
                else
                {
                    campanaMOD.OK = 2;

                    campanaMOD.Mensaje = "No se encontro informacion de Campañas con los parametros agregados.";
                }
            }
            catch (Exception ex)
            {
                campanaMOD.OK = 0;
                campanaMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: SearchCampana, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            return Json(campanaMOD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetCronograma(Campana campana)
        {
            string EstatusAzul = ConfigurationManager.AppSettings["EstatusAzul"].ToString();
            string EstatusAmarillo = ConfigurationManager.AppSettings["EstatusAmarillo"].ToString();
            string EstatusVerde = ConfigurationManager.AppSettings["EstatusVerde"].ToString();
            string EstatusRojo = ConfigurationManager.AppSettings["EstatusRojo"].ToString();

            //string EstatusCampaniaVerde = "Campaña en tiempo programado";
            //string EstatusCampaniaAmarillo = "Campaña desfasada contra lo programado";
            //string EstatusCampaniaRojo = "Campaña en situación crítica";

            //string EstatusCampaniaCerradaVerde = "Campaña concluida en tiempo ";
            //cstring EstatusCampaniaCerradaRojo = "Campaña concluida fuera de tiempo";

            CampanaMOD campanaMOD = new CampanaMOD();

            CronogramaNEG CronogramaNEG = new CronogramaNEG();

            CronogramaENT CronogramaENTReq = new CronogramaENT();

            CronogramaENT CronogramaENTRes = new CronogramaENT();

            Cronograma Cronograma = new Cronograma();
            Cronograma CronogramaPadre = new Cronograma();
            Cronograma CronogramaResultado = new Cronograma();

            List<Cronograma> ListCronogramaTemp = new List<Cronograma>();
            List<int> ListPadreCronogramaUsuario = new List<int>();

            Usuario usuario = new Usuario();
            bool EsEditarCronograma = true;

            IFormatProvider culture = new CultureInfo("es-MX", true);

            try
            {
                //GUARDAR CAMPAÑA
                Session["Campana"] = campana;

                campanaMOD.Campana = campana;

                //OBTENER USUARIO
                usuario = (Usuario)Session["Usuario"];

                Cronograma.IDCampania = campana.ID;

                //REVISA EL ESTATUS DE LA CAMPAÑA
                if(campana.ID_Estatus == 3)
                {
                    EsEditarCronograma = false;
                }

                CronogramaENTReq.ListCronograma = new List<Cronograma>();

                CronogramaENTReq.ListCronograma.Add(Cronograma);

                CronogramaENTRes = CronogramaNEG.GetCronograma(CronogramaENTReq);

                if(CronogramaENTRes.OK != 1)
                {
                    campanaMOD.OK = 0;

                    campanaMOD.Mensaje = CronogramaENTRes.Mensaje;

                    return Json(campanaMOD, JsonRequestBehavior.AllowGet);
                }

                ListCronogramaTemp = CronogramaENTRes.ListCronograma;

                Session["ListCronogramaAll"] = CronogramaENTRes.ListCronograma;

                //CONSTRUIR ARBOL DE ACTIVIDADES
                if (CronogramaENTRes.ListCronograma.Count > 0)
                {
                    if(usuario.ID_RolCronograma == 1 || usuario.ID_RolCronograma == 2)
                    {
                        ListCronogramaTemp = CronogramaENTRes.ListCronograma;

                        campanaMOD.RolAdministrador = true;
                    }
                    else if (usuario.ID_RolCronograma == 3)
                    {
                        ListCronogramaTemp = CronogramaENTRes.ListCronograma;

                        campanaMOD.RolAdministrador = false;
                    }
                    else if(usuario.ID_RolCronograma == 4)
                    {
                        ListCronogramaTemp = ListCronogramaTemp.Where(n => n.PPGID == usuario.PPGID || n.PPGID_2 == usuario.PPGID).ToList();

                        ListPadreCronogramaUsuario = ListCronogramaTemp.GroupBy(m => m.IDPadre).Select(n => n.Key).ToList();

                        AgregarPadres(ListPadreCronogramaUsuario, ListCronogramaTemp, CronogramaENTRes.ListCronograma);

                        campanaMOD.RolAdministrador = false;

                        EsEditarCronograma = false;
                    }

                    //OBTENER FECHA MAXIMA Y MINIMA DE CRONOGRAMA
                    //CronogramaPadre = CronogramaENTRes.ListCronograma.Where(n => n.IDTarea == 1).FirstOrDefault();

                    //if(CronogramaPadre != null)
                    //{
                    //    CampanaMOD.MinFechaCronograma = CronogramaPadre.FechaInicio;
                    //    CampanaMOD.MaxFechaCronograma = CronogramaPadre.FechaFin;
                    //}


                    //ORDENA LAS ACTIVIDADES PARA MOSTRAR EL ARBOL
                    campanaMOD.ListCronograma = ListCronogramaTemp.OrderBy(n => n.IDTarea).ToList();

                    campanaMOD.ListCronograma.ForEach(n =>
                    {
                        n.RolAdministrador = campanaMOD.RolAdministrador;

                        if (n.IDPadre == 0)
                        {
                            n.IdTreePadre = "text-padre treegrid-" + n.IDTarea;

                            CronogramaResultado = n;

                            n.RolAdministrador = false;

                            campanaMOD.MinFechaCronograma = n.FechaInicio;
                            campanaMOD.MaxFechaCronograma = n.FechaFin;

                            n.EstatusColor = "";
                            n.EstatusColorReal = "";
                            n.EstatusActividad = "";
                        }
                        else
                        {
                            if(n.Padre)
                            {
                                n.IdTreePadre = "text-padre treegrid-" + n.IDTarea + " treegrid-parent-" + n.IDPadre;
                            }
                            else
                            {
                                n.IdTreePadre = "treegrid-" + n.IDTarea + " treegrid-parent-" + n.IDPadre;
                            }
                        }

                        #region CALULAR PORCENTAJES

                        if (n.IDTarea > 1)
                        {
                            //PORCENTAJE SISTEMA PROGRAMADO
                            if (n.PorcentajeUsuario < 100)
                            {
                                n.Concluida = false;

                                if (n.PorcentajeUsuario >= n.PorcentajeSistema)
                                {
                                    n.EstatusColor = "blue";
                                    n.EstatusActividad = EstatusAzul;

                                }
                                else
                                {
                                    n.EstatusColor = "yellow";
                                    n.EstatusActividad = EstatusAmarillo;
                                }

                                if (n.PorcentajeUsuario >= n.PorcentajeSistemaReal)
                                {
                                    n.EstatusColorReal = "blue";
                                    n.EstatusActividadReal = EstatusAzul;
                                }
                                else
                                {
                                    n.EstatusColorReal = "yellow";
                                    n.EstatusActividadReal = EstatusAmarillo;
                                }
                            }
                            else
                            {
                                n.Concluida = true;

                                if (DateTime.ParseExact(n.FechaFinReal, "dd/MM/yyyy", culture) <= DateTime.ParseExact(n.FechaFin, "dd/MM/yyyy", culture))
                                {
                                    n.EstatusColor = "green";
                                    n.EstatusActividad = EstatusVerde;

                                    n.EstatusColorReal = "green";
                                    n.EstatusActividadReal = EstatusVerde;
                                }
                                else
                                {
                                    n.EstatusColor = "red";
                                    n.EstatusActividad = EstatusRojo;

                                    n.EstatusColorReal = "red";
                                    n.EstatusActividadReal = EstatusRojo;
                                }
                            }
                        }

                        #endregion

                    });


                    if(CronogramaResultado != null)
                    {
                        campanaMOD.PorcentajeGeneralSistema = CronogramaResultado.PorcentajeSistema;
                        campanaMOD.PorcentajeGeneralUsuario = CronogramaResultado.PorcentajeUsuario;
                        campanaMOD.PorcentajeEsfuerzoUsuario = CronogramaResultado.PorcentajeUsuarioEsfuerzo;

                        if(CronogramaResultado.PorcentajeUsuario < 100)
                        {
                            campanaMOD.EstatusGeneral = "En Proceso";

                            if (CronogramaResultado.PorcentajeUsuario >= CronogramaResultado.PorcentajeSistema)
                            {
                                campanaMOD.EstatusSemaforoVerde = "light-visible";
                                campanaMOD.EstatusSemaforoAmarillo = "";
                                campanaMOD.EstatusSemaforoRojo = "";

                                campanaMOD.EstatusAvance = "Campaña en tiempo programado";
                            }
                            else
                            {
                                if (Math.Abs(CronogramaResultado.PorcentajeSistema - CronogramaResultado.PorcentajeUsuario) <= 25)
                                {
                                    campanaMOD.EstatusSemaforoVerde = "";
                                    campanaMOD.EstatusSemaforoAmarillo = "light-visible";
                                    campanaMOD.EstatusSemaforoRojo = "";

                                    campanaMOD.EstatusAvance = "Campaña desfasada contra lo programado";
                                }
                                else
                                {
                                    campanaMOD.EstatusSemaforoVerde = "";
                                    campanaMOD.EstatusSemaforoAmarillo = "";
                                    campanaMOD.EstatusSemaforoRojo = "light-visible";

                                    campanaMOD.EstatusAvance = "Campaña en situación crítica";
                                }
                            }
                        }
                        else
                        {
                            campanaMOD.EstatusGeneral = "Concluida";

                            if(DateTime.ParseExact(CronogramaResultado.FechaFin, "dd/MM/yyyy", culture) >=
                                DateTime.ParseExact(CronogramaResultado.FechaFinReal, "dd/MM/yyyy", culture))
                            {
                                campanaMOD.EstatusSemaforoVerde = "light-visible";
                                campanaMOD.EstatusSemaforoAmarillo = "";
                                campanaMOD.EstatusSemaforoRojo = "";

                                campanaMOD.EstatusAvance = "Campaña concluida en tiempo";
                            }
                            else
                            {
                                campanaMOD.EstatusSemaforoVerde = "";
                                campanaMOD.EstatusSemaforoAmarillo = "";
                                campanaMOD.EstatusSemaforoRojo = "light-visible";

                                campanaMOD.EstatusAvance = "Campaña concluida fuera de tiempo";
                            }
                        }
                    }

                    campanaMOD.Mensaje = CronogramaENTRes.Mensaje;

                    campanaMOD.OK = CronogramaENTRes.OK;

                }
                else
                {
                    campanaMOD.OK = 2;

                    campanaMOD.Mensaje = "No se encontro listado de actividades asociadas a la campaña.";
                }

                campanaMOD.EsEditarCronograma = EsEditarCronograma;
            }
            catch(Exception ex)
            {
                campanaMOD.ListCronograma = new List<Cronograma>();

                campanaMOD.OK = 0;
                campanaMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetCronograma, Source: " + ex.Source + ", Message: " + ex.Message);

            }


            return Json(campanaMOD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDetalleCampana(Campana campana)
        {
            CampanaMOD campanaMOD = new CampanaMOD();

            CampanaNEG campanaNEG = new CampanaNEG();

            MecanicaENT mecanicaENTReq = new MecanicaENT();

            MecanicaENT mecanicaENTRes = new MecanicaENT();


            campanaMOD.ListMecanicaRegalo = new List<MecanicaRegalo>();
            campanaMOD.ListMecanicaMultiplo = new List<MecanicaMultiplo>();
            campanaMOD.ListMecanicaDescuento = new List<MecanicaDescuento>();
            campanaMOD.ListMecanicaVolumen = new List<MecanicaVolumen>();
            campanaMOD.ListMecanicaKit = new List<MecanicaKit>();
            campanaMOD.ListMecanicaCombo = new List<MecanicaCombo>();

            try
            {
                mecanicaENTReq.IdCampana = campana.ID;
                mecanicaENTReq.ClaveCampana = campana.Camp_Number;

                mecanicaENTRes = campanaNEG.GetCampanaMecanica(mecanicaENTReq);

                if (mecanicaENTRes.OK == 1)
                {
                    campanaMOD.Mensaje = mecanicaENTRes.Mensaje;
                    campanaMOD.OK = mecanicaENTRes.OK;

                    campanaMOD.ListMecanicaRegalo = mecanicaENTRes.ListMecanicaRegalo;
                    campanaMOD.ListMecanicaMultiplo = mecanicaENTRes.ListMecanicaMultiplo;
                    campanaMOD.ListMecanicaDescuento = mecanicaENTRes.ListMecanicaDescuento;
                    campanaMOD.ListMecanicaVolumen = mecanicaENTRes.ListMecanicaVolumen;
                    campanaMOD.ListMecanicaKit = mecanicaENTRes.ListMecanicaKit;
                    campanaMOD.ListMecanicaCombo = mecanicaENTRes.ListMecanicaCombo;
                    campanaMOD.ListMecanicaTiendas = mecanicaENTRes.ListMecanicaTiendas;
                }
                else if (mecanicaENTRes.OK == 0)
                {
                    campanaMOD.Mensaje = "ERROR: Ocurrio un error inesperado, intente de nuevo o consulte con el administrador de sistemas.";
                    campanaMOD.OK = 0;
                }
               
            }
            catch (Exception ex)
            {
                campanaMOD.OK = 0;
                campanaMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetDetalleCampana, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            return Json(campanaMOD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SearchUsuarioLdap(string PPGID, string Correo, string Nombre)
        {
            CampanaMOD campanaMOD = new CampanaMOD();

            //OBTENER USUARIO LDAP
            List<UsuarioLdap> ListUsuarioLdap = (List<UsuarioLdap>)Session["ListUsuarioLdap"];
            List<UsuarioLdap> ListUsuarioLdapTemp = new List<UsuarioLdap>();

            try
            {
                ListUsuarioLdapTemp = ListUsuarioLdap;

                if (!string.IsNullOrEmpty(PPGID))
                {
                    ListUsuarioLdapTemp = ListUsuarioLdapTemp.Where(n => n.PPGID == PPGID).ToList();
                }

                if (!string.IsNullOrEmpty(Correo))
                {
                    ListUsuarioLdapTemp = ListUsuarioLdapTemp.Where(n => n.Email.Contains(Correo.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(Nombre))
                {
                    ListUsuarioLdapTemp = ListUsuarioLdapTemp.Where(n => n.Nombre.ToUpper().Contains(Nombre.ToUpper())).ToList();
                }

                campanaMOD.ListUsuarioLdapTemp = ListUsuarioLdapTemp;

                Session["ListUsuarioLdapTemp"] = ListUsuarioLdapTemp;

                if (ListUsuarioLdapTemp == null || ListUsuarioLdapTemp.Count <= 0)
                {
                    campanaMOD.Mensaje = "No se encontro informacion con los filtros agregados.";
                    campanaMOD.OK = 2;
                }
                else
                {
                    campanaMOD.Mensaje = "OK";
                    campanaMOD.OK = 1;
                }
            }
            catch (Exception ex)
            {
                campanaMOD.OK = 0;
                campanaMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: SearchUsuarioLdap, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            return Json(campanaMOD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EditUsuarioLdap(UsuarioLdap usuarioLdap, Cronograma cronograma)
        {
            CampanaMOD campanaMOD = new CampanaMOD();

            try
            {
                //OBTENER USUARIO LDAP
                cronograma.PPGID = usuarioLdap.PPGID;
                cronograma.NombreResponsable = usuarioLdap.Nombre;
                cronograma.Correo = usuarioLdap.Email;
            }
            catch (Exception ex)
            {
                campanaMOD.OK = 0;
                campanaMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: EditUsuarioLdap, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            return Json(campanaMOD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EditCronograma(List<Cronograma> ListCronograma)
        {
            List<Cronograma> ListCronogramaPadre = new List<Cronograma>();
            List<Cronograma> ListCronogramaEdit = new List<Cronograma>();
            List<Cronograma> ListCronogramaEditTemp = new List<Cronograma>();

            Cronograma cronogramaPorcentaje = new Cronograma();
            Cronograma CronogramaResultado = new Cronograma();
            Cronograma cronogramaTemp = new Cronograma();
            Cronograma cronogramaUpdate = new Cronograma();

            CampanaMOD campanaMOD = new CampanaMOD();

            IFormatProvider culture = new CultureInfo("es-MX", true);

            List<Cronograma> ListCronogramaAll = new List<Cronograma>();

            CronogramaNEG cronogramaNEG = new CronogramaNEG();
            CronogramaENT cronogramaENTReq = new CronogramaENT();

            CronogramaENT cronogramaENTRes = new CronogramaENT();

            List<int> ListCronogramaPadreGroup = new List<int>();

            try
            {
                ListCronogramaAll = (List<Cronograma>)Session["ListCronogramaAll"];
                ListCronogramaPadre = ListCronograma.Where(n => n.Padre == true).ToList();


                ListCronogramaEdit = ListCronograma.Where(n => n.Update == true).ToList();

                foreach (Cronograma cronograma in ListCronogramaEdit)
                {
                    cronogramaPorcentaje = ListCronogramaAll.Where(n => n.IDTarea == cronograma.IDTarea).FirstOrDefault();

                    if (cronogramaPorcentaje != null)
                    {
                        cronogramaPorcentaje.PorcentajeUsuario = cronograma.PorcentajeUsuario;
                    }
                }

                ListCronogramaPadreGroup = ListCronogramaEdit.Where(n => n.IDPadre > 0).GroupBy(m => m.IDPadre).Select(l => l.Key).ToList();

                if (ListCronogramaPadreGroup.Count > 0)
                {
                    AgregarPadres(ListCronogramaPadreGroup, ListCronogramaEdit, ListCronogramaAll);
                }

                //ACTUALIZAR DIFERENCIA DE FECHAS
                ListCronogramaEdit.ForEach(n =>
                {
                    n.Duracion = Convert.ToInt32((DateTime.ParseExact(n.FechaFin, "dd/MM/yyyy", culture) - DateTime.ParseExact(n.FechaInicio, "dd/MM/yyyy", culture)).TotalDays + 1);

                    if (n.PorcentajeUsuario >= 100)
                    {
                        n.FechaFinReal = DateTime.Now.ToString("dd/MM/yyyy");

                        cronogramaTemp = ListCronogramaAll.Where(m => m.Predecesor != null && !string.IsNullOrEmpty(m.Predecesor) && Convert.ToInt32(m.Predecesor) == n.IDTarea).FirstOrDefault();

                        if (cronogramaTemp != null)
                        {
                            ListCronogramaEditTemp.Add(cronogramaTemp);
                        }
                    }

                });

                ListCronogramaEditTemp.ForEach(n =>
                {
                    n.FechaInicioReal = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");

                    cronogramaUpdate = ListCronogramaEdit.Where(m => m.IDTarea == n.IDTarea).FirstOrDefault();

                    if (cronogramaUpdate != null)
                    {
                        cronogramaUpdate.FechaInicioReal = n.FechaInicioReal;
                    }
                    else
                    {
                        ListCronogramaEdit.Add(n);
                    }
                });


                cronogramaENTReq.ListCronograma = ListCronogramaEdit;


                cronogramaENTRes = cronogramaNEG.EditCronograma(cronogramaENTReq);

                if (cronogramaENTRes.OK == 1)
                {
                    campanaMOD.OK = cronogramaENTRes.OK;

                    campanaMOD.Mensaje = "Se actualizo correctamente la informacion de Cronograma.";

                    //ORDENA LAS ACTIVIDADES PARA MOSTRAR EL ARBOL
                    ListCronogramaAll.OrderBy(n => n.IDTarea).ToList().ForEach(n =>
                    {
                        if(n.IDTarea == 0)
                        {

                        }

                        cronogramaUpdate = ListCronogramaEdit.Where(m => m.IDTarea == n.IDTarea && m.Padre == false && m.PorcentajeUsuario >= 100).FirstOrDefault();

                        if (cronogramaUpdate != null)
                        {
                            n.FechaFinReal = cronogramaUpdate.FechaFinReal;
                        }

                        cronogramaUpdate = ListCronogramaEdit.Where(m => m.IDTarea == n.IDTarea && m.Padre == false && m.PorcentajeUsuario < 100).FirstOrDefault();

                        if (cronogramaUpdate != null)
                        {
                            n.FechaInicioReal = cronogramaUpdate.FechaInicioReal;
                        }

                        n.Update = false;

                        if (n.IDPadre == 0)
                        {
                            n.IdTreePadre = "text-padre treegrid-" + n.IDTarea;
                        }
                        else
                        {
                            if (n.Padre)
                            {
                                n.IdTreePadre = "text-padre treegrid-" + n.IDTarea + " treegrid-parent-" + n.IDPadre;
                            }
                            else
                            {
                                n.IdTreePadre = "treegrid-" + n.IDTarea + " treegrid-parent-" + n.IDPadre;
                            }
                        }

                        if (n.PorcentajeUsuario < 100)
                        {
                            if (n.PorcentajeUsuario >= n.PorcentajeSistema)
                            {
                                n.EstatusColor = "table-primary";
                            }
                            else
                            {
                                n.EstatusColor = "table-warning";
                            }

                            n.Concluida = false;
                        }
                        else
                        {
                            if (DateTime.ParseExact(n.FechaFinReal, "dd/MM/yyyy", culture) <=
                                DateTime.ParseExact(n.FechaFin, "dd/MM/yyyy", culture))
                            {
                                n.EstatusColor = "table-success";
                            }
                            else
                            {
                                n.EstatusColor = "table-danger";
                            }

                            n.Concluida = true;
                        }

                    });

                    //ACTUALIZAR INFORMACION GENERAL SEMAFORO
                    CronogramaResultado = ListCronogramaAll.Where(n => n.IDPadre == 0).FirstOrDefault();

                    if (CronogramaResultado != null)
                    {
                        campanaMOD.PorcentajeGeneralSistema = CronogramaResultado.PorcentajeSistema;
                        campanaMOD.PorcentajeGeneralUsuario = CronogramaResultado.PorcentajeUsuario;

                        if (CronogramaResultado.PorcentajeUsuario < 100)
                        {
                            campanaMOD.EstatusGeneral = "En Proceso";

                            if (CronogramaResultado.PorcentajeUsuario >= CronogramaResultado.PorcentajeSistema)
                            {
                                campanaMOD.EstatusSemaforoVerde = "light-visible";
                                campanaMOD.EstatusSemaforoAmarillo = "";
                                campanaMOD.EstatusSemaforoRojo = "";
                            }
                            else
                            {
                                if (Math.Abs(CronogramaResultado.PorcentajeSistema - CronogramaResultado.PorcentajeUsuario) <= 25)
                                {
                                    campanaMOD.EstatusSemaforoVerde = "";
                                    campanaMOD.EstatusSemaforoAmarillo = "light-visible";
                                    campanaMOD.EstatusSemaforoRojo = "";
                                }
                                else
                                {
                                    campanaMOD.EstatusSemaforoVerde = "";
                                    campanaMOD.EstatusSemaforoAmarillo = "";
                                    campanaMOD.EstatusSemaforoRojo = "light-visible";
                                }
                            }
                        }
                        else
                        {
                            campanaMOD.EstatusGeneral = "Concluida";

                            if (DateTime.ParseExact(CronogramaResultado.FechaFin, "dd/MM/yyyy", culture) <=
                                DateTime.ParseExact(CronogramaResultado.FechaFinReal, "dd/MM/yyyy", culture))
                            {
                                campanaMOD.EstatusSemaforoVerde = "light-visible";
                                campanaMOD.EstatusSemaforoAmarillo = "";
                                campanaMOD.EstatusSemaforoRojo = "";
                            }
                            else
                            {
                                campanaMOD.EstatusSemaforoVerde = "";
                                campanaMOD.EstatusSemaforoAmarillo = "";
                                campanaMOD.EstatusSemaforoRojo = "light-visible";
                            }
                        }
                    }


                    Session["ListCronogramaAll"] = ListCronogramaAll;

                    campanaMOD.ListCronograma = ListCronogramaAll.OrderBy(n => n.IDTarea).ToList();
                }
                else
                {
                    campanaMOD.OK = 0;

                    campanaMOD.Mensaje = "Ocurrio un error inesperado, no se guardo correctamente la informacion de Cronograma, intente de nuevo o consulte al administrador de sistemas.";
                }
            }
            catch (Exception ex)
            {
                campanaMOD.OK = 0;
                campanaMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: EditCronograma, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            return Json(campanaMOD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RecargaCronograma()
        {
            CampanaMOD campanaMOD = new CampanaMOD();
            
            string uniqueName = string.Empty;
            List<Cronograma> ListCronograma = new List<Cronograma>();
            bool correcto = true;

            string incluirSi = string.Empty;
            string incluirNo = string.Empty;
            string tipoFlujoArchivo = string.Empty;
            string tipoFlujoAprobacion = string.Empty;
            string tipoFlujoInformativo = string.Empty;
            string tipoFlujoActualizar = string.Empty;
            string rootPath = string.Empty;

            UsuarioLdap usuarioLdap = new UsuarioLdap();

            Parametro parametro = new Parametro();
            List<Parametro> ListParametro = new List<Parametro>();

            IFormatProvider culture = new CultureInfo("es-MX", true);

            List<UsuarioLdap> ListUsuarioLdap = new List<UsuarioLdap>();

            List<Cronograma> ListCronogramaExportar = new List<Cronograma>();

            CronogramaNEG CronogramaNEG = new CronogramaNEG();

            CronogramaENT CronogramaENTReq = new CronogramaENT();

            CronogramaENT CronogramaENTRes = new CronogramaENT();

            Cronograma Cronograma = new Cronograma();

            Cronograma cronogramaExportar = new Cronograma();

            Campana campana = new Campana();

            string ext = string.Empty;
            string fileSavePath = string.Empty;

            try
            {
                campanaMOD.ListCronograma = new List<Cronograma>();

                if (Request.Files["myfile"] != null)
                {
                    var file = Request.Files["myfile"];
                    if (file.FileName != "")
                    {
                        campana = (Campana)Session["Campana"];

                        //LISTA USUARIOS LDAP                       
                        ListUsuarioLdap = (List<UsuarioLdap>)Session["ListUsuarioLdap"];

                        if(ListUsuarioLdap.Count <= 0)
                        {
                            campanaMOD.Mensaje = "ERROR: Ocurrio un error inesperado, intenta de nuevo o consulta a tu administrador de sistemas.";

                            campanaMOD.OK = 0;

                            return Json(campanaMOD, JsonRequestBehavior.AllowGet);
                        }

                        //OBTENER CRONOGRAMA
                        campana = (Campana)Session["Campana"];

                        Cronograma.IDCampania = campana.ID;

                        CronogramaENTReq.ListCronograma = new List<Cronograma>();

                        CronogramaENTReq.ListCronograma.Add(Cronograma);

                        CronogramaENTRes = CronogramaNEG.GetCronograma(CronogramaENTReq);

                        ListCronogramaExportar = CronogramaENTRes.ListCronograma.OrderBy(n => n.IDTarea).ToList();

                        //OBTENER PARAMETROS
                        ListParametro = (List<Parametro>)Session["ListParametro"];

                        //DATOS PARA CATALOGO DE CRONOGRAMA

                        parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                        ConfigurationManager.AppSettings["IncluirSi"].ToString().ToUpper()).FirstOrDefault();
                        if (parametro != null)
                        {
                            incluirSi = parametro.Valor;
                        }

                        parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                            ConfigurationManager.AppSettings["IncluirNo"].ToString().ToUpper()).FirstOrDefault();
                        if (parametro != null)
                        {
                            incluirNo = parametro.Valor;
                        }

                        parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                            ConfigurationManager.AppSettings["TipoFlujoArchivo"].ToString().ToUpper()).FirstOrDefault();
                        if (parametro != null)
                        {
                            tipoFlujoArchivo = parametro.Valor;
                        }

                        parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                            ConfigurationManager.AppSettings["TipoFlujoAprobacion"].ToString().ToUpper()).FirstOrDefault();
                        if (parametro != null)
                        {
                            tipoFlujoAprobacion = parametro.Valor;
                        }

                        parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                            ConfigurationManager.AppSettings["TipoFlujoInformativo"].ToString().ToUpper()).FirstOrDefault();
                        if (parametro != null)
                        {
                            tipoFlujoInformativo = parametro.Valor;
                        }

                        parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                            ConfigurationManager.AppSettings["TipoFlujoActualizar"].ToString().ToUpper()).FirstOrDefault();
                        if (parametro != null)
                        {
                            tipoFlujoActualizar = parametro.Valor;
                        }

                        parametro = ListParametro.Where(n => n.Nombre.ToUpper() ==
                        ConfigurationManager.AppSettings["DirectorioCronograma"].ToString().ToUpper()).FirstOrDefault();
                        if (parametro != null)
                        {
                            rootPath = parametro.Valor;
                        }

                        //Get physical path of our folder where we want to save images
                        //var rootPath = Server.MapPath("~/Plan_Actividades");


                        ext = Path.GetExtension(file.FileName);

                        //Generate a unique name using Guid
                        uniqueName = Guid.NewGuid().ToString() + ext;


                        fileSavePath = Path.Combine(rootPath, uniqueName);

                        // Save the uploaded file to "UploadedFiles" folder
                        file.SaveAs(fileSavePath);

                        var book = new ExcelQueryFactory(fileSavePath);

                        ListCronograma = book.Worksheet("Cronograma").AsEnumerable()
                                           .Select(row => new Cronograma
                                           {
                                               IDTarea = row["ID"].Cast<int>(),
                                               IDPadre = row["ID_Padre"].Cast<int>(),
                                               Actividad = row["Actividad"],
                                               Duracion = row["Duracion"].Cast<decimal>(),
                                               FechaInicio = DateTime.ParseExact(row["Inicio"], "dd/MM/yyyy", culture).ToString("dd/MM/yyyy"),
                                               FechaFin = DateTime.ParseExact(row["Final"], "dd/MM/yyyy", culture).ToString("dd/MM/yyyy"),
                                               FechaInicioReal = DateTime.ParseExact(row["Inicio"], "dd/MM/yyyy", culture).ToString("dd/MM/yyyy"),
                                               FechaFinReal = DateTime.ParseExact(row["Final"], "dd/MM/yyyy", culture).ToString("dd/MM/yyyy"),
                                               TiempoOptimista = row["T_Optimista"].Cast<int>(),
                                               TiempoPesimista = row["T_Pesimista"].Cast<int>(),
                                               Predecesor = row["Predecesor"],
                                               Correo = row["Correo_Responsable"],
                                               Correo_2 = row["Correo_Responsable_2"],
                                               PorcentajeUsuario = row["Porcentaje"].Cast<decimal>(),
                                               PorcentajeSistema = 0,
                                               TipoFlujo = row["Tipo_Flujo"],
                                               IdTipoFlujo = row["Tipo_Flujo"].ToString().ToUpper() == tipoFlujoArchivo.ToUpper() ? 1 :
                                                            row["Tipo_Flujo"].ToString().ToUpper() == tipoFlujoAprobacion.ToUpper() ? 2 :
                                                            row["Tipo_Flujo"].ToString().ToUpper() == tipoFlujoInformativo.ToUpper() ? 3 :
                                                            row["Tipo_Flujo"].ToString().ToUpper() == tipoFlujoActualizar.ToUpper() ? 4 : 0,
                                               Incluir = row["Incluir"],
                                               IdIncluir = row["Incluir"].ToString().Trim().ToUpper() == incluirSi.ToUpper() ? 1 :
                                                                row["Incluir"].ToString().Trim().ToUpper() == incluirNo.ToUpper() ? 0 : 0,
                                               EstatusEnvio = 0,
                                               UsuarioCreacion = campana.PPG_ID
                                           }).ToList();


                        //AGREGAR NOMBRES RESPONSABLES
                        ListCronograma.ForEach(n => {
                            usuarioLdap = ListUsuarioLdap.Where(m => !string.IsNullOrEmpty(m.Email) && m.Email.ToLower() == n.Correo.ToLower()).FirstOrDefault();

                            if(usuarioLdap != null)
                            {
                                n.NombreResponsable = usuarioLdap.Nombre.ToUpper();
                            }

                            usuarioLdap = ListUsuarioLdap.Where(m => !string.IsNullOrEmpty(m.Email) && m.Email.ToLower() == n.Correo_2.ToLower()).FirstOrDefault();

                            if (usuarioLdap != null)
                            {
                                n.NombreResponsable_2 = usuarioLdap.Nombre.ToUpper();
                            }

                        });

                        //VALIDAR DATOS COMPLETOS
                        ValidarDatos(ref correcto, ListCronograma);

                        //VALIDAR PREDECESOR
                        ValidarPredecesor(ref correcto, ListCronograma);

                        //VALIDAR FECHAS REALES
                        ValidarFechasReales(ref correcto, ListCronograma);

                        //VALIDAR FECHAS PROGRAMADAS
                        ValidarFechasProgramadas(ref correcto, ListCronograma);


                        //ORDENAR CRONOGRAMA
                        ListCronograma = ListCronograma.OrderBy(n => n.IDTarea).ToList();

                        ListCronograma.ForEach(n =>
                        {
                            n.RolAdministrador = campanaMOD.RolAdministrador;

                            if (n.IDPadre == 0)
                            {
                                n.IdTreePadre = "text-padre treegrid-" + n.IDTarea;

                                n.RolAdministrador = false;

                                campanaMOD.MinFechaCronograma = n.FechaInicio;
                                campanaMOD.MaxFechaCronograma = n.FechaFin;
                            }
                            else
                            {
                                if (n.Padre)
                                {
                                    n.IdTreePadre = "text-padre treegrid-" + n.IDTarea + " treegrid-parent-" + n.IDPadre;
                                }
                                else
                                {
                                    n.IdTreePadre = "treegrid-" + n.IDTarea + " treegrid-parent-" + n.IDPadre;
                                }
                            }

                            if (n.PorcentajeUsuario < 100)
                            {
                                n.Concluida = false;

                                if (n.PorcentajeUsuario >= n.PorcentajeSistema)
                                {
                                    n.EstatusColor = "blue";

                                }
                                else
                                {
                                    n.EstatusColor = "yellow";
                                }
                            }
                            else
                            {
                                n.Concluida = true;

                                if (DateTime.ParseExact(n.FechaFinReal, "dd/MM/yyyy", culture) <=
                                    DateTime.ParseExact(n.FechaFin, "dd/MM/yyyy", culture))
                                {
                                    n.EstatusColor = "green";
                                }
                                else
                                {
                                    n.EstatusColor = "red";
                                }
                            }

                            if(n.IdIncluir == 0)
                            {
                                n.EstatusIncluir = "row-no-incluir";
                            }
                            else
                            {
                                n.EstatusIncluir = "";
                            }


                            //BUSCAR ACTIVIDADES QUE YA ESTAN EN 100%
                            cronogramaExportar = ListCronogramaExportar.Where(m => m.IDTarea == n.IDTarea).FirstOrDefault();

                            if (cronogramaExportar != null)
                            {
                                n.PorcentajeSistema = cronogramaExportar.PorcentajeSistema;

                                if (cronogramaExportar.PorcentajeSistema >= 100)
                                {
                                    n.IdIncluir = 1;

                                    n = cronogramaExportar;
                                }

                                if(cronogramaExportar.PorcentajeSistema > 0 || cronogramaExportar.PorcentajeUsuario > 0)
                                {
                                    n.FechaInicio = cronogramaExportar.FechaInicio;
                                    n.FechaInicioReal = cronogramaExportar.FechaInicioReal;

                                    n.IdIncluir = 1;
                                }
                            }

                        });

                    }
                }

                Session["ListCronogramaRecargar"] = ListCronograma;

                campanaMOD.ListCronogramaRecargar = ListCronograma;

                campanaMOD.Correcto = correcto;

                campanaMOD.Mensaje = "Se cargo correctamente el cronograma.";

                campanaMOD.OK = 1;
            }
            catch (Exception ex)
            {
                campanaMOD.OK = 0;
                campanaMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: EditCronograma, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            return Json(campanaMOD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ReSaveCronograma()
        {
            CampanaMOD campanaMOD = new CampanaMOD();

            Cronograma CronogramaResultado = new Cronograma();

            UsuarioLdap usuarioLdap = new UsuarioLdap();

            int versionCronograma = 0;

            IFormatProvider culture = new CultureInfo("es-MX", true);

            Usuario usuario = new Usuario();

            CronogramaENT cronogramaENTReq = new CronogramaENT();
            CronogramaENT cronogramaENTRes = new CronogramaENT();

            CronogramaNEG cronogramaNEG = new CronogramaNEG();
            List<Cronograma> ListCronograma = new List<Cronograma>();

            Campana campana = new Campana();

            List<UsuarioLdap> ListUsuarioLdap = new List<UsuarioLdap>();

            List<Cronograma> ListCronogramaAll = new List<Cronograma>();

            try
            {
                usuario = (Usuario)Session["Usuario"];

                if (usuario == null)
                {
                    campanaMOD.OK = 2;
                    campanaMOD.Mensaje = "Debe ingresar desde la sesion de sharepoint, para obtener los datos del usuario.";
                }

                ListCronograma = (List<Cronograma>)Session["ListCronogramaRecargar"];

                if (ListCronograma == null || ListCronograma.Count <= 0)
                {
                    campanaMOD.OK = 2;
                    campanaMOD.Mensaje = "Debe cargar la inforamcion de cronograma para actualizar la informacion.";
                }

                campana = (Campana)Session["Campana"];

                if (campana == null)
                {
                    campanaMOD.OK = 2;
                    campanaMOD.Mensaje = "Debe seleccionar una campaña para actualizar la informacion de cronograma.";
                }

                ListUsuarioLdap = (List<UsuarioLdap>)Session["ListUsuarioLdap"];

                if (ListUsuarioLdap == null || ListUsuarioLdap.Count <= 0)
                {
                    campanaMOD.OK = 2;
                    campanaMOD.Mensaje = "No se cargo correctamente la informacion de Ldap.";
                }

                ListCronogramaAll = (List<Cronograma>)Session["ListCronogramaAll"];

                if (ListCronogramaAll == null || ListCronogramaAll.Count <= 0)
                {
                    campanaMOD.OK = 2;
                    campanaMOD.Mensaje = "Se debe seleccionar un cronograma para posteriormente actualizar la informacion de cronograma.";
                }

                versionCronograma = ListCronogramaAll.Where(n => n.IDTarea == 1).FirstOrDefault().VersionCronograma; 


                ListCronograma.ForEach(n =>
                {
                    n.IDCampania = campana.ID;
                    n.VersionCronograma = versionCronograma;
                    n.UsuarioModificacion = usuario.PPGID;
                 

                    if (!string.IsNullOrEmpty(n.Correo))
                    {
                        usuarioLdap = ListUsuarioLdap.Where(m => m.Email == n.Correo.ToLower()).FirstOrDefault();

                        if (usuarioLdap != null)
                        {
                            n.NombreResponsable = usuarioLdap.Nombre;
                            n.PPGID = usuarioLdap.PPGID;
                        }
                    }

                    if (!string.IsNullOrEmpty(n.Correo_2))
                    {
                        usuarioLdap = ListUsuarioLdap.Where(m => m.Email == n.Correo_2.ToLower()).FirstOrDefault();

                        if (usuarioLdap != null)
                        {
                            n.NombreResponsable_2 = usuarioLdap.Nombre;
                            n.PPGID_2 = usuarioLdap.PPGID;
                        }
                    }

                    n.Padre = ListCronograma.Where(m => m.IDPadre == n.IDTarea).Count() > 0 ? true : false;
     
                });


                cronogramaENTReq.ListCronograma = ListCronograma.Where(n => n.IdIncluir == 1).ToList();


                cronogramaENTRes = cronogramaNEG.ReSaveCronograma(cronogramaENTReq);

                if (cronogramaENTRes.OK == 1 && cronogramaENTRes.ListCronograma.Count > 0)
                {
                    campanaMOD.ListCronogramaRecargar = new List<Cronograma>();

                    //ORDENA LAS ACTIVIDADES PARA MOSTRAR EL ARBOL
                    campanaMOD.ListCronograma = cronogramaENTRes.ListCronograma.OrderBy(n => n.IDTarea).ToList();

                    campanaMOD.ListCronograma.ForEach(n =>
                    {
                        n.RolAdministrador = campanaMOD.RolAdministrador;

                        if (n.IDPadre == 0)
                        {
                            n.IdTreePadre = "text-padre treegrid-" + n.IDTarea;

                            CronogramaResultado = n;

                            n.RolAdministrador = false;

                            campanaMOD.MinFechaCronograma = n.FechaInicio;
                            campanaMOD.MaxFechaCronograma = n.FechaFin;
                        }
                        else
                        {
                            if (n.Padre)
                            {
                                n.IdTreePadre = "text-padre treegrid-" + n.IDTarea + " treegrid-parent-" + n.IDPadre;
                            }
                            else
                            {
                                n.IdTreePadre = "treegrid-" + n.IDTarea + " treegrid-parent-" + n.IDPadre;
                            }
                        }

                    //PORCENTAJE SISTEMA PROGRAMADO
                    if (n.PorcentajeUsuario < 100)
                        {
                            n.Concluida = false;

                            if (n.PorcentajeUsuario >= n.PorcentajeSistema)
                            {
                                n.EstatusColor = "blue";

                            }
                            else
                            {
                                n.EstatusColor = "yellow";
                            }

                            if (n.PorcentajeUsuario >= n.PorcentajeSistemaReal)
                            {
                                n.EstatusColorReal = "blue";

                            }
                            else
                            {
                                n.EstatusColorReal = "yellow";
                            }
                        }
                        else
                        {
                            n.Concluida = true;

                            if (DateTime.ParseExact(n.FechaFinReal, "dd/MM/yyyy", culture) <= DateTime.ParseExact(n.FechaFin, "dd/MM/yyyy", culture))
                            {
                                n.EstatusColor = "green";

                                n.EstatusColorReal = "green";
                            }
                            else
                            {
                                n.EstatusColor = "red";

                                n.EstatusColorReal = "red";
                            }
                        }

                    });

                    if (CronogramaResultado != null)
                    {
                        campanaMOD.PorcentajeGeneralSistema = CronogramaResultado.PorcentajeSistema;
                        campanaMOD.PorcentajeGeneralUsuario = CronogramaResultado.PorcentajeUsuario;
                        campanaMOD.PorcentajeEsfuerzoUsuario = CronogramaResultado.PorcentajeUsuarioEsfuerzo;

                        if (CronogramaResultado.PorcentajeUsuario < 100)
                        {
                            campanaMOD.EstatusGeneral = "En Proceso";

                            if (CronogramaResultado.PorcentajeUsuario >= CronogramaResultado.PorcentajeSistema)
                            {
                                campanaMOD.EstatusSemaforoVerde = "light-visible";
                                campanaMOD.EstatusSemaforoAmarillo = "";
                                campanaMOD.EstatusSemaforoRojo = "";

                                campanaMOD.EstatusAvance = "Campaña en tiempo programado";
                            }
                            else
                            {
                                if (Math.Abs(CronogramaResultado.PorcentajeSistema - CronogramaResultado.PorcentajeUsuario) <= 25)
                                {
                                    campanaMOD.EstatusSemaforoVerde = "";
                                    campanaMOD.EstatusSemaforoAmarillo = "light-visible";
                                    campanaMOD.EstatusSemaforoRojo = "";

                                    campanaMOD.EstatusAvance = "Campaña desfasada contra lo programado";
                                }
                                else
                                {
                                    campanaMOD.EstatusSemaforoVerde = "";
                                    campanaMOD.EstatusSemaforoAmarillo = "";
                                    campanaMOD.EstatusSemaforoRojo = "light-visible";

                                    campanaMOD.EstatusAvance = "Campaña en situación crítica";
                                }
                            }
                        }
                        else
                        {
                            campanaMOD.EstatusGeneral = "Concluida";

                            if (DateTime.ParseExact(CronogramaResultado.FechaFin, "dd/MM/yyyy", culture) >=
                                DateTime.ParseExact(CronogramaResultado.FechaFinReal, "dd/MM/yyyy", culture))
                            {
                                campanaMOD.EstatusSemaforoVerde = "light-visible";
                                campanaMOD.EstatusSemaforoAmarillo = "";
                                campanaMOD.EstatusSemaforoRojo = "";

                                campanaMOD.EstatusAvance = "Campaña concluida en tiempo";
                            }
                            else
                            {
                                campanaMOD.EstatusSemaforoVerde = "";
                                campanaMOD.EstatusSemaforoAmarillo = "";
                                campanaMOD.EstatusSemaforoRojo = "light-visible";

                                campanaMOD.EstatusAvance = "Campaña concluida fuera de tiempo";
                            }
                        }
                    }

                    campanaMOD.OK = cronogramaENTRes.OK;
                    campanaMOD.Mensaje = "Se actualizo correctamente el archivo de Cronograma.";

                }
                else
                {
                    campanaMOD.OK = 0;

                    campanaMOD.Mensaje = "Ocurrio un error inesperado, no se guardo correctamente la informacion de Cronograma, intente de nuevo o consulte al administrador de sistemas.";

                }
            }
            catch(Exception ex)
            {
                campanaMOD.OK = 0;
                campanaMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: ReSaveCronograma, Source: " + ex.Source + ", Message: " + ex.Message);

            }


            return Json(campanaMOD, JsonRequestBehavior.AllowGet);
        }
        public void AgregarHijos(int IdCampana, Task Padre, List<object> ListActividades, List<Cronograma> ListCronograma)
        {
            Cronograma Cronograma;

            foreach (Task Hijo in ListActividades)
            {
                Cronograma = new Cronograma();

                Cronograma.IDCampania = IdCampana;
                Cronograma.IDTarea = Hijo.ID.intValue() + 1;
                Cronograma.IDPadre = Padre.ID.intValue() + 1;
                Cronograma.Actividad = Hijo.Name;
                Cronograma.NombreResponsable = Hijo.ResourceNames;
                Cronograma.Correo = Hijo.ResourceInitials;
                Cronograma.Padre = Hijo.ChildTasks.size() > 0 ? true : false;
                Cronograma.PorcentajeUsuario = Convert.ToDecimal(Hijo.PercentageComplete.floatValue());
                Cronograma.PorcentajeSistema = 0;
                Cronograma.Duracion = Convert.ToInt32(Hijo.Duration.Duration);
                Cronograma.FechaInicio = FromUnixTime(Hijo.Start.getTime()).ToString("dd/MM/yyyy");
                Cronograma.FechaFin = FromUnixTime(Hijo.Finish.getTime()).ToString("dd/MM/yyyy");
                Cronograma.FechaHoy = DateTime.Now.ToString("dd/MM/yyyy");
                Cronograma.FechaCreacion = DateTime.Now.ToString("dd/MM/yyyy");
                Cronograma.FechaModificacion = DateTime.Now.ToString("dd/MM/yyyy");
                Cronograma.Comentario = Hijo.Notes;
                Cronograma.IdTreePadre = "treegrid-" + (Hijo.ID.intValue() + 1) + " treegrid-parent-" + (Padre.ID.intValue() + 1);
                Cronograma.VersionCronograma = 0;

                ListCronograma.Add(Cronograma);

                if (Hijo.ChildTasks.size() > 0)
                {
                    AgregarHijos(IdCampana, Hijo, Hijo.ChildTasks.toArray().ToList(), ListCronograma);
                }
            }
        }
        public void AgregarPadres(List<int> ListCronogramaPadreGroup, List<Cronograma> ListCronogramaEdit, List<Cronograma> ListCronogramaAll)
        {
            List<Cronograma> ListCronogramaEditTemp = new List<Cronograma>();
            Cronograma cronograma = new Cronograma();
            decimal porcentaje = 0;
            DateTime maxFecha = new DateTime();
            DateTime minFecha = new DateTime();
            IFormatProvider culture = new CultureInfo("es-MX", true);

            foreach (int padre in ListCronogramaPadreGroup)
            {
                cronograma = ListCronogramaAll.Where(n => n.IDTarea == padre).FirstOrDefault();

                if(cronograma != null)
                {
                    //porcentaje = Convert.ToDecimal((ListCronogramaAll.Where(n => n.IDPadre == padre).Sum(m => m.PorcentajeUsuario)/ ListCronogramaAll.Where(n => n.IDPadre == padre).Count()).ToString("0.##"));
                    porcentaje = Convert.ToDecimal(((DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", culture) - DateTime.ParseExact(cronograma.FechaInicio, "dd/MM/yyyy", culture)).TotalDays) /
                                    ((DateTime.ParseExact(cronograma.FechaFin, "dd/MM/yyyy", culture) - DateTime.ParseExact(cronograma.FechaInicio, "dd/MM/yyyy", culture)).TotalDays + 1)) * 100;
                    minFecha = ListCronogramaAll.Where(n => n.IDPadre == padre).Min(m => DateTime.ParseExact(m.FechaInicio, "dd/MM/yyyy", culture));
                    maxFecha = ListCronogramaAll.Where(n => n.IDPadre == padre).Max(m => DateTime.ParseExact(m.FechaFin, "dd/MM/yyyy", culture));

                    cronograma.PorcentajeUsuario = Math.Round(porcentaje, 2);
                    cronograma.FechaInicio = minFecha.ToString("dd/MM/yyyy");
                    cronograma.FechaFin = maxFecha.ToString("dd/MM/yyyy");
                    cronograma.Update = true;

                    ListCronogramaEdit.Add(cronograma);
                    ListCronogramaEditTemp.Add(cronograma);
                }
            }

            ListCronogramaPadreGroup = ListCronogramaEditTemp.Where(n => n.IDPadre > 0).GroupBy(m => m.IDPadre).Select(l => l.Key).ToList();

            ListCronogramaPadreGroup = (from lcp in ListCronogramaPadreGroup
                                        from lce in ListCronogramaEdit
                                        where lcp != lce.IDTarea
                                        group lcp by lcp into n
                                        select n.Key).ToList();

            if (ListCronogramaPadreGroup.Count > 0)
            {
                AgregarPadres(ListCronogramaPadreGroup, ListCronogramaEdit, ListCronogramaAll);
            }

        }
        public void ExportarCronograma()
        {
            Campana campana = new Campana();

            CronogramaNEG CronogramaNEG = new CronogramaNEG();

            CronogramaENT CronogramaENTReq = new CronogramaENT();

            CronogramaENT CronogramaENTRes = new CronogramaENT();

            Cronograma Cronograma = new Cronograma();

            try
            {
                campana = (Campana)Session["Campana"];

                Cronograma.IDCampania = campana.ID;

                CronogramaENTReq.ListCronograma = new List<Cronograma>();

                CronogramaENTReq.ListCronograma.Add(Cronograma);

                CronogramaENTRes = CronogramaNEG.GetCronograma(CronogramaENTReq);

                List<Cronograma> ListaCronograma = new List<Cronograma>();
                ListaCronograma = CronogramaENTRes.ListCronograma.OrderBy(n => n.IDTarea).ToList();

                DataTable dtCronograma = new DataTable();

                if(ListaCronograma.Count > 0)
                {
                    dtCronograma.Columns.Add("ID");
                    dtCronograma.Columns.Add("ID_Padre");
                    dtCronograma.Columns.Add("Actividad");
                    dtCronograma.Columns.Add("Duracion");
                    dtCronograma.Columns.Add("Inicio");
                    dtCronograma.Columns.Add("Final");
                    dtCronograma.Columns.Add("T_Optimista");
                    dtCronograma.Columns.Add("T_Pesimista");
                    dtCronograma.Columns.Add("Predecesor");
                    dtCronograma.Columns.Add("Correo_Responsable");
                    dtCronograma.Columns.Add("Correo_Responsable_2");
                    dtCronograma.Columns.Add("Porcentaje");
                    dtCronograma.Columns.Add("Tipo_Flujo");
                    dtCronograma.Columns.Add("Incluir");
                }

                ListaCronograma.ForEach(c =>
                {
                    dtCronograma.Rows.Add(c.IDTarea,
                                          c.IDPadre,
                                          c.Actividad,
                                          c.Duracion,
                                          c.FechaInicio,
                                          c.FechaFin,
                                          c.TiempoOptimista,
                                          c.TiempoPesimista,
                                          c.Predecesor,
                                          c.Correo,
                                          c.Correo_2,
                                          c.PorcentajeUsuario,
                                          c.TipoFlujo,
                                          c.Incluir);
                });


                string nombreArchivoCronograma = Guid.NewGuid().ToString() + ".xlsx";

                //GridView grid = new GridView();

                //var ListCronogramaExportar = (from c in ListaCronograma.OrderBy(n=> n.IDTarea)
                //                                      select new
                //                                      {
                //                                          ID = c.IDTarea,
                //                                          ID_Padre = c.IDPadre,
                //                                          Actividad = c.Actividad,
                //                                          Duracion = c.Duracion,
                //                                          Inicio = c.FechaInicio,
                //                                          Final = c.FechaFin,
                //                                          T_Optimista = c.TiempoOptimista,
                //                                          T_Pesimista = c.TiempoPesimista,
                //                                          Predecesor = c.Predecesor,
                //                                          Correo_Responsable = c.Correo,
                //                                          Correo_Responsable_2 = c.Correo_2,
                //                                          Porcentaje = c.PorcentajeUsuario,
                //                                          Tipo_Flujo = c.TipoFlujo,
                //                                          Incluir = 1
                //                                      }).ToList();


                //grid.DataSource = ListCronogramaExportar;

                //grid.DataBind();

                //StringWriter sw = new StringWriter();
                //HtmlTextWriter htw = new HtmlTextWriter(sw);

                //grid.RenderControl(htw);

                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(dtCronograma, "Cronograma");

                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("content-disposition", "attachment; filename=" + nombreArchivoCronograma);

                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        MyMemoryStream.WriteTo(Response.OutputStream);
                        Response.Flush();
                        Response.End();
                    }

                }

                //Response.Write(sw.ToString());
                //Response.End();
                //Response.Flush();
            }
            catch (Exception ex)
            {
                ArchivoLog.EscribirLog(null, "Error:" + ex.Message + ", Trace:" + ex.StackTrace);
            }
        }
        private void ValidarDatos(ref bool Correcto, List<Cronograma> ListCronograma)
        {
            StringBuilder validacion = new StringBuilder();
            List<Cronograma> ListCronogramaValidacion;

            ListCronogramaValidacion = ListCronograma.Where(n => n.IDTarea == 0
                                                                            || n.IDPadre == 0
                                                                            || string.IsNullOrEmpty(n.Actividad)
                                                                            || n.Duracion == 0
                                                                            || string.IsNullOrEmpty(n.FechaInicio.ToString())
                                                                            || string.IsNullOrEmpty(n.FechaFin.ToString())
                                                                            || n.TiempoOptimista == 0
                                                                            || n.TiempoPesimista == 0
                                                                            || string.IsNullOrEmpty(n.Correo)
                                                                            || string.IsNullOrEmpty(n.Correo_2)
                                                                            || string.IsNullOrEmpty(n.TipoFlujo)
                                                                            || string.IsNullOrEmpty(n.Incluir)).ToList();

            ListCronogramaValidacion.ForEach(n =>
            {
                n.Padre = ListCronograma.Where(m => m.IDPadre == n.IDTarea).Count() > 0 ? true : false;
            });

            foreach (Cronograma cronograma in ListCronogramaValidacion)
            {
                validacion.Clear();

                if (cronograma.IDTarea <= 0)
                {
                    validacion.AppendLine("Error: No tiene informacion o esta incorrecto ID_Tarea");

                    Correcto = false;
                }

                if (cronograma.IDPadre < 0)
                {
                    validacion.AppendLine("Error: No tiene informacion o esta incorrecto ID_Padre");

                    Correcto = false;
                }

                if (string.IsNullOrEmpty(cronograma.Actividad))
                {
                    validacion.AppendLine("Error: No tiene informacion o esta incorrecto Actividad");

                    Correcto = false;
                }

                if (cronograma.Duracion == 0)
                {
                    validacion.AppendLine("Error: No tiene informacion o esta incorrecto Duracion");

                    Correcto = false;
                }

                if (cronograma.FechaInicio == null || string.IsNullOrEmpty(cronograma.FechaInicio.ToString()))
                {
                    validacion.AppendLine("Error: No tiene informacion o esta incorrecto Fecha_Inicio");

                    Correcto = false;
                }

                if (cronograma.FechaFin == null || string.IsNullOrEmpty(cronograma.FechaFin.ToString()))
                {
                    validacion.AppendLine("Error: No tiene informacion o esta incorrecto Fecha_Fin");

                    Correcto = false;
                }

                if (cronograma.TiempoOptimista <= 0 && !cronograma.Padre)
                {
                    validacion.AppendLine("Error: No tiene informacion o esta incorrecto Tiempo_Optimista");

                    Correcto = false;
                }

                if (cronograma.TiempoPesimista <= 0 && !cronograma.Padre)
                {
                    validacion.AppendLine("Error: No tiene informacion o esta incorrecto Tiempo_Pesimista");

                    Correcto = false;
                }

                if (string.IsNullOrEmpty(cronograma.Correo) && !cronograma.Padre)
                {
                    validacion.AppendLine("Error: No tiene informacion o esta incorrecto Correo");

                    Correcto = false;
                }

                if (string.IsNullOrEmpty(cronograma.Correo_2) && !cronograma.Padre)
                {
                    validacion.AppendLine("Error: No tiene informacion o esta incorrecto Correo_2");

                    Correcto = false;
                }

                if (string.IsNullOrEmpty(cronograma.TipoFlujo) && !cronograma.Padre)
                {
                    validacion.AppendLine("Error: No tiene informacion o esta incorrecto Tipo_Flujo");

                    Correcto = false;
                }

                if (string.IsNullOrEmpty(cronograma.Incluir) && !cronograma.Padre)
                {
                    validacion.AppendLine("Error: No tiene informacion o esta incorrecto Incluir");

                    Correcto = false;
                }

                cronograma.ValidarDatos = cronograma.ValidarDatos + validacion.ToString();
            }
        }
        private void ValidarPredecesor(ref bool Correcto, List<Cronograma> ListCronograma)
        {
            List<Cronograma> ListCronogramaValidacion;
            StringBuilder validar = new StringBuilder();

            ListCronogramaValidacion = ListCronograma.Where(n => string.IsNullOrEmpty(n.Predecesor)).ToList();

            ListCronogramaValidacion.ForEach(n =>
            {
                n.Padre = ListCronograma.Where(m => m.IDPadre == n.IDTarea).Count() > 0 ? true : false;
            });

            foreach (Cronograma cronograma in ListCronogramaValidacion)
            {
                validar.Clear();

                if (string.IsNullOrEmpty(cronograma.Predecesor) && !cronograma.Padre)
                {
                    validar.AppendLine("Precaucion: No se agrego Predecesor, revise si es correcto.");

                    //Correcto = false;
                }

                cronograma.ValidarPredecesor = cronograma.ValidarPredecesor + validar.ToString();
            }

        }
        private void ValidarFechasReales(ref bool Correcto, List<Cronograma> ListCronograma)
        {
            List<Cronograma> ListCronogramaValidacion;
            StringBuilder validar = new StringBuilder();
            Cronograma cronogramaPadre = new Cronograma();

            IFormatProvider culture = new CultureInfo("es-MX", true);

            cronogramaPadre = ListCronograma.Where(n => n.IDTarea == 1 || n.IDPadre == 0).FirstOrDefault();

            if (cronogramaPadre == null)
            {
                validar.Clear();

                validar.AppendLine("Error: Se debe agregar correctamente la actividad Padre para cargar el archivo de Cronograma.");

                ListCronograma.FirstOrDefault().ValidarFecha = validar.ToString();

                Correcto = false;

                return;
            }

            //VALIDAR FECHAS MENORES A LA ACTIVIDAD INICIAL PADRE
            ListCronogramaValidacion = ListCronograma.Where(n => DateTime.ParseExact(n.FechaInicio, "dd/MM/yyyy", culture) <
                                                                DateTime.ParseExact(cronogramaPadre.FechaInicio, "dd/MM/yyyy", culture)).ToList();

            ListCronogramaValidacion.ForEach(n =>
            {
                n.Padre = ListCronograma.Where(m => m.IDPadre == n.IDTarea).Count() > 0 ? true : false;
            });

            foreach (Cronograma cronograma in ListCronogramaValidacion)
            {
                validar.Clear();

                if (DateTime.ParseExact(cronograma.FechaInicio, "dd/MM/yyyy", culture) <
                        DateTime.ParseExact(cronogramaPadre.FechaInicio, "dd/MM/yyyy", culture) && !cronograma.Padre)
                {
                    validar.AppendLine("Error: La Fecha_Inicio no puede ser menor a la fecha del cronograma.");

                    Correcto = false;
                }

                cronograma.ValidarFecha = cronograma.ValidarFecha + validar.ToString();
            }

            //VALIDAR FECHAS MAYORES A LA ACTIVIDAD FINAL PADRE
            ListCronogramaValidacion = ListCronograma.Where(n => DateTime.ParseExact(n.FechaFin, "dd/MM/yyyy", culture) >
                                                                DateTime.ParseExact(cronogramaPadre.FechaFin, "dd/MM/yyyy", culture)).ToList();

            ListCronogramaValidacion.ForEach(n =>
            {
                n.Padre = ListCronograma.Where(m => m.IDPadre == n.IDTarea).Count() > 0 ? true : false;
            });

            foreach (Cronograma cronograma in ListCronogramaValidacion)
            {
                validar.Clear();

                if (DateTime.ParseExact(cronograma.FechaFin, "dd/MM/yyyy", culture) >
                    DateTime.ParseExact(cronogramaPadre.FechaFin, "dd/MM/yyyy", culture) && 
                    !cronograma.Padre)
                {
                    validar.AppendLine("Error: La Fecha_Final no puede ser mayor a la fecha del cronograma.");

                    Correcto = false;
                }

                cronograma.ValidarFecha = cronograma.ValidarFecha + validar.ToString();
            }


            //VALIDAR FECHAS MENORES A LA ACTIVIDAD PADRE
            ListCronogramaValidacion = (from crono in ListCronograma
                                        from crono_2 in ListCronograma
                                        where crono.IDPadre == crono_2.IDTarea
                                         && DateTime.ParseExact(crono.FechaInicio, "dd/MM/yyyy", culture) <
                                         DateTime.ParseExact(crono_2.FechaInicio, "dd/MM/yyyy", culture)
                                        select crono).ToList();

            foreach (Cronograma cronograma in ListCronogramaValidacion)
            {
                validar.Clear();

                validar.AppendLine("Error: La Fecha_Inicio no puede ser menor a la fecha de su padre.");

                Correcto = false;

                cronograma.ValidarFecha = cronograma.ValidarFecha + validar.ToString();
            }

            //VALIDAR FECHAS MAYORES A LA ACTIVIDAD PADRE
            ListCronogramaValidacion = (from crono in ListCronograma
                                        from crono_2 in ListCronograma
                                        where crono.IDPadre == crono_2.IDTarea
                                         && DateTime.ParseExact(crono.FechaFin, "dd/MM/yyyy", culture) >
                                         DateTime.ParseExact(crono_2.FechaFin, "dd/MM/yyyy", culture)
                                        select crono).ToList();

            foreach (Cronograma cronograma in ListCronogramaValidacion)
            {
                validar.Clear();

                validar.AppendLine("Error: La Fecha_Final no puede ser mayor a la fecha de su padre.");

                Correcto = false;

                cronograma.ValidarFecha = cronograma.ValidarFecha + validar.ToString();
            }


            //VALIDAR QUE LA FECHA INICIAL NO SEA MAYOR A LA FINAL
            ListCronogramaValidacion = ListCronograma.Where(n => DateTime.ParseExact(n.FechaInicio, "dd/MM/yyyy", culture) 
                                                                > DateTime.ParseExact(n.FechaFin, "dd/MM/yyyy", culture)).ToList();

            foreach (Cronograma cronograma in ListCronogramaValidacion)
            {
                validar.Clear();

                validar.AppendLine("Error: La Fecha_Inicio no puede ser mayor a la Fecha_Final.");

                Correcto = false;

                cronograma.ValidarFecha = cronograma.ValidarFecha + validar.ToString();
            }

        }
        private void ValidarFechasProgramadas(ref bool Correcto, List<Cronograma> ListCronograma)
        {
            List<Cronograma> ListCronogramaValidacion;
            StringBuilder validar = new StringBuilder();

            IFormatProvider culture = new CultureInfo("es-MX", true);

            //VALIDAR TIEMPO OPTIMISTA
            ListCronogramaValidacion = ListCronograma.Where(n => (DateTime.ParseExact(n.FechaFin, "dd/MM/yyyy", culture) -
                                                                    DateTime.ParseExact(n.FechaInicio, "dd/MM/yyyy", culture)).TotalDays + 1 < n.TiempoOptimista).ToList();

            ListCronogramaValidacion.ForEach(n =>
            {
                n.Padre = ListCronograma.Where(m => m.IDPadre == n.IDTarea).Count() > 0 ? true : false;
            });

            foreach (Cronograma cronograma in ListCronogramaValidacion)
            {
                validar.Clear();

                if ((DateTime.ParseExact(cronograma.FechaFin, "dd/MM/yyyy", culture) -
                        DateTime.ParseExact(cronograma.FechaInicio, "dd/MM/yyyy", culture)).TotalDays + 1 < 
                        cronograma.TiempoOptimista && !cronograma.Padre)
                {
                    validar.AppendLine("Precaucion: La Duracion de la actividad es menor al Tiempo_Optimista.");

                    //Correcto = false;
                }

                cronograma.ValidarFechaProgramada = cronograma.ValidarFechaProgramada + validar.ToString();
            }

            //VALIDAR TIEMPO OPTIMISTA
            ListCronogramaValidacion = ListCronograma.Where(n => (DateTime.ParseExact(n.FechaFin, "dd/MM/yyyy", culture) -
                                                                    DateTime.ParseExact(n.FechaInicio, "dd/MM/yyyy", culture)).TotalDays + 1 > 
                                                                    n.TiempoPesimista).ToList();

            ListCronogramaValidacion.ForEach(n =>
            {
                n.Padre = ListCronograma.Where(m => m.IDPadre == n.IDTarea).Count() > 0 ? true : false;
            });

            foreach (Cronograma cronograma in ListCronogramaValidacion)
            {
                validar.Clear();

                if ((DateTime.ParseExact(cronograma.FechaFin, "dd/MM/yyyy", culture) -
                        DateTime.ParseExact(cronograma.FechaInicio, "dd/MM/yyyy", culture)).TotalDays + 1 > 
                        cronograma.TiempoPesimista && !cronograma.Padre)
                {
                    validar.AppendLine("Precaucion: La Duracion de la actividad es mayor al Tiempo_Pesimista.");

                    //Correcto = false;
                }

                cronograma.ValidarFechaProgramada = cronograma.ValidarFechaProgramada + validar.ToString();
            }

        }
        public DateTime FromUnixTime(long unixTimeMillis)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddMilliseconds(unixTimeMillis);
        }

        #endregion
    }
}