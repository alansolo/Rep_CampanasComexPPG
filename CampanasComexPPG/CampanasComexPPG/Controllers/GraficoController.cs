using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CampanasComexPPG.Models.Grafico;
using CampanasComexPPG.Session;
using EntidadesCampanasPPG.BDCampana;
using EntidadesCampanasPPG.Campana;
using NegocioCampanasPPG.Campana;
using UtilidadesCampanasPPG;

namespace CampanasPPG.Controllers
{
    [SessionTimeout]
    public class GraficoController : Controller
    {
        // GET: Grafico
        [HttpGet]
        public ActionResult Grafico()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Inicio", "Inicio");
            }

            return View();
        }
        [HttpPost]
        public JsonResult GetCampanaAlcance()
        {
            GraficoMOD GraficoMOD = new GraficoMOD();
            List<GraficoBar> ListGraficoBar = new List<GraficoBar>();

            List<GraficoPie> ListGraficoPieAlcance = new List<GraficoPie>();
            List<GraficoPie> ListGraficoPieEstatus = new List<GraficoPie>();
            List<GraficoPie> ListGraficoPieEjecucionTemp = new List<GraficoPie>();
            List<GraficoPie> ListGraficoPieEjecucionTotal = new List<GraficoPie>();
            List<GraficoPie> ListGraficoPieEjecucion = new List<GraficoPie>();
            List<GraficoPie> ListGraficoPieProgresoTemp = new List<GraficoPie>();
            List<GraficoPie> ListGraficoPieProgresoTotal = new List<GraficoPie>();
            List<GraficoPie> ListGraficoPieProgreso = new List<GraficoPie>();

            List<Campana> ListCampanaTemp = new List<Campana>();

            //IFormatProvider culture = new CultureInfo("es-MX", true);

            CampanaNEG CampanaNEG = new CampanaNEG();

            CampanaENT CampanaENTReq = new CampanaENT();

            Campana Campana = new Campana();

            //OBTENER USUARIO
            Usuario usuario = new Usuario();

            CampanaENT CampanaENTRes = new CampanaENT();


            //if (Session["ListCampanaCrono"] == null || Session["ListGraficoBar"] == null ||
            //        Session["ListCampana"] == null || Session["ListGraficoPieAlcance"] == null ||
            //        Session["ListGraficoPieEstatus"] == null || Session["ListGraficoPieEjecucion"] == null ||
            //        Session["ListGraficoPieProgreso"] == null)
            //{

            try
            {
                usuario = (Usuario)Session["Usuario"];

                CampanaENTReq.ListCampana = new List<Campana>();

                CampanaENTReq.ListCampana.Add(Campana);
              

                //Datos grafico bar
                CampanaENTRes = new CampanaENT();

                CampanaENTRes = CampanaNEG.GetCampanaCronograma(CampanaENTReq);

                if (CampanaENTRes.OK == 0)
                {
                    GraficoMOD.ListGraficoBar = new List<GraficoBar>();
                    GraficoMOD.ListGraficoPieAlcance = new List<GraficoPie>();
                    GraficoMOD.ListGraficoPieEjecucion = new List<GraficoPie>();
                    GraficoMOD.ListGraficoPieEstatus = new List<GraficoPie>();
                    GraficoMOD.ListGraficoPieProgreso = new List<GraficoPie>();

                    GraficoMOD.ListCampana = new List<Campana>();
                    GraficoMOD.ListCampanaCrono = new List<Campana>();

                    GraficoMOD.MenuUsuario = usuario.MenuUsuario;
                    GraficoMOD.MenuCronograma = usuario.MenuCronograma;
                    GraficoMOD.MenuGrafico = usuario.MenuGrafico;
                    GraficoMOD.MenuConfiguracion = usuario.MenuConfiguracion;

                    GraficoMOD.Mensaje = "ERROR: Ocurrio un problema inesperado, no se pudo obtener la informacion de las campañas para construir las graficas, intenta de nuevo o consulta al administrador de sistemas.";
                    GraficoMOD.OK = 0;

                    return Json(GraficoMOD, JsonRequestBehavior.AllowGet);
                }

                if (CampanaENTRes.ListCampana.Count > 0)
                {
                    if (usuario.ID_RolCronograma == 1 || usuario.ID_RolCronograma == 2)
                    {
                        ListCampanaTemp = CampanaENTRes.ListCampana;
                    }
                    else if (usuario.ID_RolCronograma == 3)
                    {
                        ListCampanaTemp = CampanaENTRes.ListCampana.Where(n => n.PPGID_Lider == usuario.PPGID).ToList();
                    }
                    //else if (usuario.ID_RolCronograma == 3)
                    //{
                    //ListCampanaTemp = CampanaENTRes.ListCampana.Where(n => n.PPGID_Lider == usuario.PPGID).ToList();
                    //}
                }

                GraficoMOD.ListCampanaCrono = ListCampanaTemp;

                ListGraficoBar = GraficoMOD.ListCampanaCrono.Where(n => n.ID_Estatus == 2).AsEnumerable()
                                    //Where(n => n.PPGID_Lider == "")
                                    .Select(row => new GraficoBar
                                    {
                                        Titulo = row.Nombre_Camp,
                                        PorcUsuario = row.PorcUsuario,
                                        PorcSistema = row.PorcSistema,
                                        PorcSistemaReal = row.PorcSistemaReal
                                    }).ToList();

                GraficoMOD.ListGraficoBar = ListGraficoBar;

                Session["ListGraficoBar"] = GraficoMOD.ListGraficoBar;

                Session["ListCampanaCrono"] = GraficoMOD.ListCampanaCrono;


                //Datos grafico pie
                //CampanaENTRes = CampanaNEG.GetCampana(CampanaENTReq);

                //GraficoMOD.ListCampana = CampanaENTRes.ListCampana;

                //Datos por alcance
                ListGraficoPieAlcance = (from c in GraficoMOD.ListCampanaCrono.Where(n => n.ID_Estatus == 2).AsEnumerable()
                                             //where c.PPGID_Lider == ""
                                         group c by new
                                         {
                                             c.Alcance
                                         } into gcs
                                         select new GraficoPie
                                         {
                                             Titulo = gcs.Key.Alcance,
                                             Valor = gcs.Count()
                                         }).ToList();

                GraficoMOD.ListGraficoPieAlcance = ListGraficoPieAlcance;
                Session["ListGraficoPieAlcance"] = GraficoMOD.ListGraficoPieAlcance;


                //Datos por estatus
                ListGraficoPieEstatus = (from c in GraficoMOD.ListCampanaCrono.Where(n => n.ID_Estatus == 2).AsEnumerable()
                                             //where c.PPGID_Lider == ""
                                         group c by new
                                         {
                                             c.EstatusCat
                                         } into gcs
                                         select new GraficoPie
                                         {
                                             Titulo = gcs.Key.EstatusCat,
                                             Valor = gcs.Count()
                                         }).ToList();

                GraficoMOD.ListGraficoPieEstatus = ListGraficoPieEstatus;
                Session["ListGraficoPieEstatus"] = GraficoMOD.ListGraficoPieEstatus;


                //Datos campaña por ejecutar
                ListGraficoPieEjecucionTemp = GraficoMOD.ListCampanaCrono.Where(n => n.ID_Estatus == 1).AsEnumerable()
                                                //.Where(n => DateTime.ParseExact(n.Fecha_Inicio, "dd/MM/yyyy", culture) > DateTime.Now)
                                                .Select(row => new GraficoPie
                                                {
                                                    Titulo = "Por ejecutar",
                                                    Valor = 1
                                                }).ToList();


                if (ListGraficoPieEjecucionTemp.Count > 0)
                {
                    ListGraficoPieEjecucionTotal.AddRange(ListGraficoPieEjecucionTemp);
                }

                //Datos campaña en ejecucion
                ListGraficoPieEjecucionTemp = GraficoMOD.ListCampanaCrono.Where(n => n.ID_Estatus == 2).AsEnumerable()
                                                //.Where(n => DateTime.ParseExact(n.Fecha_Inicio, "dd/MM/yyyy", culture) >= DateTime.Now
                                                //            && DateTime.ParseExact(n.Fecha_Fin_Real, "dd/MM/yyyy", culture) <= DateTime.Now)
                                                .Select(row => new GraficoPie
                                                {
                                                    Titulo = "Ejecutando",
                                                    Valor = 2
                                                }).ToList();

                if (ListGraficoPieEjecucionTemp.Count > 0)
                {
                    ListGraficoPieEjecucionTotal.AddRange(ListGraficoPieEjecucionTemp);
                }

                //Datos campaña finalizada
                ListGraficoPieEjecucionTemp = GraficoMOD.ListCampanaCrono.Where(n => n.ID_Estatus == 3).AsEnumerable()
                                                //.Where(n => DateTime.ParseExact(n.Fecha_Fin_Real, "dd/MM/yyyy", culture) > DateTime.Now)
                                                .Select(row => new GraficoPie
                                                {
                                                    Titulo = "Finalizada",
                                                    Valor = 3
                                                }).ToList();

                if (ListGraficoPieEjecucionTemp.Count > 0)
                {
                    ListGraficoPieEjecucionTotal.AddRange(ListGraficoPieEjecucionTemp);
                }

                ListGraficoPieEjecucion = (from c in ListGraficoPieEjecucionTotal
                                               //where c.PPGID_Lider == ""
                                           group c by new
                                           {
                                               c.Titulo
                                           } into gcs
                                           select new GraficoPie
                                           {
                                               Titulo = gcs.Key.Titulo,
                                               Valor = gcs.Count()
                                           }).ToList();

                GraficoMOD.ListGraficoPieEjecucion = ListGraficoPieEjecucion;
                Session["ListGraficoPieEjecucion"] = GraficoMOD.ListGraficoPieEjecucion;


                //Datos campaña en progreso
                ListGraficoPieProgresoTemp = GraficoMOD.ListCampanaCrono.Where(n => n.ID_Estatus == 2 &&
                                                n.PorcUsuario >= n.PorcSistema).AsEnumerable()
                                            .Select(row => new GraficoPie
                                            {
                                                Titulo = "Campaña en tiempo",
                                                Valor = 1
                                            }).ToList();

                if (ListGraficoPieProgresoTemp.Count > 0)
                {
                    ListGraficoPieProgresoTotal.AddRange(ListGraficoPieProgresoTemp);
                }

                ListGraficoPieProgresoTemp = GraficoMOD.ListCampanaCrono.Where(n => n.ID_Estatus == 2 &&
                                                n.PorcUsuario < n.PorcSistema && Math.Abs(n.PorcSistema - n.PorcUsuario) <= 25).AsEnumerable()
                                            .Select(row => new GraficoPie
                                            {
                                                Titulo = "Campaña desfasada",
                                                Valor = 2
                                            }).ToList();

                if (ListGraficoPieProgresoTemp.Count > 0)
                {
                    ListGraficoPieProgresoTotal.AddRange(ListGraficoPieProgresoTemp);
                }

                ListGraficoPieProgresoTemp = GraficoMOD.ListCampanaCrono.Where(n => n.ID_Estatus == 2 &&
                                                n.PorcUsuario < n.PorcSistema && Math.Abs(n.PorcSistema - n.PorcUsuario) > 25).AsEnumerable()
                                            .Select(row => new GraficoPie
                                            {
                                                Titulo = "Campaña situacion critica",
                                                Valor = 3
                                            }).ToList();

                if (ListGraficoPieProgresoTemp.Count > 0)
                {
                    ListGraficoPieProgresoTotal.AddRange(ListGraficoPieProgresoTemp);
                }

                ListGraficoPieProgreso = (from c in ListGraficoPieProgresoTotal
                                          group c by new
                                          {
                                              c.Titulo
                                          } into gcs
                                          select new GraficoPie
                                          {
                                              Titulo = gcs.Key.Titulo,
                                              Valor = gcs.Count()
                                          }).ToList();

                GraficoMOD.ListGraficoPieProgreso = ListGraficoPieProgreso;
                Session["ListGraficoPieProgreso"] = GraficoMOD.ListGraficoPieProgreso;


                Session["ListCampana"] = GraficoMOD.ListCampana;

                GraficoMOD.OK = CampanaENTRes.OK;

                Session["OK"] = GraficoMOD.OK;

                GraficoMOD.Mensaje = CampanaENTRes.Mensaje;

                Session["Mensaje"] = GraficoMOD.Mensaje;


                //}
                //else
                //{
                //    //Datos campaña
                //    GraficoMOD.ListCampanaCrono = (List<Campana>)Session["ListCampanaCrono"];

                //    //Datos grafico bar
                //    GraficoMOD.ListGraficoBar = (List<GraficoBar>)Session["ListGraficoBar"];


                //    //Datos grafico pie alcance
                //    GraficoMOD.ListGraficoPieAlcance = (List<GraficoPie>)Session["ListGraficoPieAlcance"];

                //    //Datos grafico pie estatus
                //    GraficoMOD.ListGraficoPieEstatus = (List<GraficoPie>)Session["ListGraficoPieEstatus"];

                //    //Datos grafico pie ejecucion
                //    GraficoMOD.ListGraficoPieEjecucion = (List<GraficoPie>)Session["ListGraficoPieEjecucion"];

                //    //Datos grafico pie ejecucion
                //    GraficoMOD.ListGraficoPieProgreso = (List<GraficoPie>)Session["ListGraficoPieProgreso"];


                //    //Datos grafico pie
                //    GraficoMOD.ListCampana = (List<Campana>)Session["ListCampana"];


                //    GraficoMOD.OK = (int)Session["OK"];

                //    GraficoMOD.Mensaje = Session["Mensaje"].ToString();

                //}
            }
            catch(Exception ex)
            {
                GraficoMOD.Mensaje = "ERROR: Ocurrio un error inesperado, no se pudo procesar la informacion para mostrar las graficas de las Campañas, intenta de nuevo o consulta al administarador de sistemas.";
                GraficoMOD.OK = 0;

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetCampanaAlcance, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            GraficoMOD.MenuUsuario = usuario.MenuUsuario;
            GraficoMOD.MenuCronograma = usuario.MenuCronograma;
            GraficoMOD.MenuGrafico = usuario.MenuGrafico;
            GraficoMOD.MenuConfiguracion = usuario.MenuConfiguracion;


            return Json(GraficoMOD, JsonRequestBehavior.AllowGet);
        }
    }
}