using CampanasComexPPG.Session;
using EntidadesCampanasPPG.BDCampana;
using EntidadesCampanasPPG.Catalogo;
using EntidadesCampanasPPG.Ldap;
using EntidadesCampanasPPG.Usuario;
using NegocioCampanasPPG.Catalogo;
using NegocioCampanasPPG.Ldap;
using NegocioCampanasPPG.Usuario;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilidadesCampanasPPG;

namespace CampanasPPG.Controllers
{
    //[SessionTimeout]
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Inicio()
        {
            return View();
        }
        public JsonResult ValidUsuario(string Correo)
        {
            Usuario usuarioCronograma = new Usuario();
            Usuario usuario = new Usuario();

            Parametro parametro = new Parametro();
            string serverLdap = string.Empty;

            ParametroENT parametroENTRes = new ParametroENT();
            ParametroNEG parametroNEG = new ParametroNEG();

            LdapENT ldapENTReq = new LdapENT();
            LdapENT ldapENTRes = new LdapENT();
            LdapNEG ldapNEG = new LdapNEG();

            UsuarioENT usuarioENTReq = new UsuarioENT();
            UsuarioENT usuarioENTRes = new UsuarioENT();
            UsuarioNEG usuarioNeg = new UsuarioNEG();

            UsuarioLdap usuarioLdap = new UsuarioLdap();

            try
            {
                if (Correo == string.Empty)
                {
                    usuario.Mensaje = "Ocurrio un error inesperado, no se mando la informacion del usuario.";
                    usuario.ID_RolCronograma = 0;
                    usuario.OK = 0;

                    ArchivoLog.EscribirLog(null, "ERROR: Service: ValidUsuario, Message: " + usuario.Mensaje);

                    return Json(usuario, JsonRequestBehavior.AllowGet);
                }

                if (Session["Usuario"] == null)
                {
                    usuario.Correo = Correo;

                    //OBTENER PARAMETROS

                    parametroENTRes = parametroNEG.GetParametro();

                    if (parametroENTRes.OK == 0)
                    {
                        usuario.Mensaje = "Ocurrio un error inesperado, no se pudo obtener la informacion de los parametros globales de Campañas, intente de nuvo o consulte al administrador de sistemas.";
                        usuario.OK = 0;
                        usuario.ID_RolCronograma = 0;

                        ArchivoLog.EscribirLog(null, "ERROR: Service: ValidUsuario, Message: " + usuario.Mensaje);

                        return Json(usuario, JsonRequestBehavior.AllowGet);
                    }

                    Session["ListParametro"] = parametroENTRes.ListParametro;

                    parametro = parametroENTRes.ListParametro.Where(n => n.Nombre.ToUpper() ==
                                                ConfigurationManager.AppSettings["ServerLdap"].ToUpper()).FirstOrDefault();

                    if (parametro != null)
                    {
                        serverLdap = parametro.Valor;
                    }

                    //OBTENER USUARIO LDAP

                    ldapENTReq.ServerLdap = serverLdap;

                    ldapENTRes = ldapNEG.GetUsuarioLdap(ldapENTReq);

                    if (ldapENTRes.OK == 0)
                    {
                        usuario.Mensaje = "Ocurrio un error inesperado, no se pudo obtener la informacion del Usuario en Active Directory, intente de nuvo o consulte al administrador de sistemas.";
                        usuario.OK = 0;
                        usuario.ID_RolCronograma = 0;

                        ArchivoLog.EscribirLog(null, "ERROR: Service: ValidUsuario, Message: " + usuario.Mensaje);

                        return Json(usuario, JsonRequestBehavior.AllowGet);
                    }

                    Session["ListUsuarioLdap"] = ldapENTRes.ListUsuarioLdap;

                    if (ldapENTRes.ListUsuarioLdap != null && ldapENTRes.ListUsuarioLdap.Count > 0)
                    {
                        if (string.IsNullOrEmpty(usuario.Correo))
                        {
                            usuario.Correo = "-1";
                        }

                        usuarioLdap = ldapENTRes.ListUsuarioLdap.Where(n => n.Email.ToLower() == usuario.Correo.ToLower()).FirstOrDefault();

                        if (usuarioLdap == null)
                        {
                            usuario.ID_RolCronograma = 0;
                            usuario.Mensaje = "No tiene los permisos suficientes para acceder a cronograma, usuario no encontrado en Active Directory, contacte a su administrador de sistema.";
                            usuario.ID_RolCronograma = 0;

                            ArchivoLog.EscribirLog(null, "ERROR: Service: ValidUsuario, Message: " + usuario.Mensaje);

                            return Json(usuario, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            usuario.PPGID = usuarioLdap.PPGID;
                            usuario.Nombre = usuarioLdap.Nombre;
                        }
                    }


                    usuarioENTReq.ListUsuario = new List<Usuario>();
                    usuarioENTReq.ListUsuario.Add(usuario);


                    usuarioENTRes = usuarioNeg.GetUsuario(usuarioENTReq);

                    if (usuarioENTRes.OK == 0)
                    {
                        usuario = new Usuario();

                        usuario.OK = 0;
                        usuario.Mensaje = "ERROR: Ocurrio un error inesperado, no se pudo obtener la informacion del Usuario que ingreso a Cronograma, intente de nuevo o consulte al administrador de sistemas.";
                        usuario.ID_RolCronograma = 0;

                        ArchivoLog.EscribirLog(null, "ERROR: Service: ValidUsuario, Message: " + usuario.Mensaje);

                        Session["Usuario"] = new Usuario();


                        return Json(usuario, JsonRequestBehavior.AllowGet);
                    }

                    usuarioCronograma = usuarioENTRes.ListUsuario.FirstOrDefault();

                    if (usuarioCronograma == null || usuarioCronograma.ID_RolCronograma <= 0)
                    {
                        usuario = new Usuario();
                        usuario.ID_RolCronograma = 0;
                        usuario.Mensaje = "No tiene los permisos suficientes para acceder a cronograma, usuario no registrado en cronograma, contacte a su administrador de sistema.";

                        ArchivoLog.EscribirLog(null, "ERROR: Service: ValidUsuario, Message: " + usuario.Mensaje);

                        return Json(usuario, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        usuario.ID_RolCronograma = usuarioCronograma.ID_RolCronograma;
                        usuario.Rol = usuarioCronograma.Rol;
                        usuario.RolDescription = usuarioCronograma.RolDescription;

                        if (usuario.ID_RolCronograma == 1)
                        {
                            usuario.MenuUsuario = true;
                            usuario.MenuCronograma = true;
                            usuario.MenuGrafico = true;
                            usuario.MenuConfiguracion = true;
                        }
                        else if (usuario.ID_RolCronograma == 2)
                        {
                            usuario.MenuUsuario = true;
                            usuario.MenuCronograma = true;
                            usuario.MenuGrafico = true;
                            usuario.MenuConfiguracion = false;
                        }
                        else if (usuario.ID_RolCronograma == 3)
                        {
                            usuario.MenuUsuario = false;
                            usuario.MenuCronograma = true;
                            usuario.MenuGrafico = true;
                            usuario.MenuConfiguracion = false;
                        }
                        else if (usuario.ID_RolCronograma == 4)
                        {
                            usuario.MenuUsuario = false;
                            usuario.MenuCronograma = true;
                            usuario.MenuGrafico = false;
                            usuario.MenuConfiguracion = false;
                        }

                        Session["Usuario"] = usuario;

                        usuario.OK = 1;
                        usuario.Mensaje = "OK";

                    }
                }
                else
                {
                    usuario = (Usuario)Session["Usuario"];

                    usuario.OK = 1;
                    usuario.Mensaje = "OK";
                }
            }
            catch(Exception ex)
            {
                usuario.OK = 0;
                usuario.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";
                usuario.ID_RolCronograma = 0;

                ArchivoLog.EscribirLog(null, "ERROR: Service: ValidUsuario, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            return Json(usuario, JsonRequestBehavior.AllowGet);
        }
    }
}