using CampanasComexPPG.Session;
using CampanasPPG.Models.Usuario;
using EntidadesCampanasPPG.BDCampana;
using EntidadesCampanasPPG.Catalogo;
using EntidadesCampanasPPG.Usuario;
using NegocioCampanasPPG.Catalogo;
using NegocioCampanasPPG.Usuario;
using System;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UtilidadesCampanasPPG;

namespace CampanasPPG.Controllers
{
    [SessionTimeout]
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult Usuario()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Inicio", "Inicio");
            }

            return View();
        }
        [HttpPost]
        public JsonResult GetUsuario()
        {
            UsuarioMOD usuarioMOD = new UsuarioMOD();
         
            UsuarioENT usuarioENTReq = new UsuarioENT();
            Usuario usuarioSession = new Usuario();
            Usuario usuario = new Usuario();

            UsuarioENT usuarioENTRes = new UsuarioENT();
            UsuarioNEG usuarioNEG = new UsuarioNEG();

            RolCronogramaENT rolCronogramaENTReq = new RolCronogramaENT();
            RolCronograma rolCronograma = new RolCronograma();

            RolCronogramaENT rolCronogramaENTRes = new RolCronogramaENT();
            RolCronogramaNEG rolCronogramaNEG = new RolCronogramaNEG();

            try
            {
                usuarioSession = (Usuario)Session["Usuario"];

                usuario.PPGID = string.Empty;
                usuario.Nombre = string.Empty;

                //OBTENER USUARIOS
                usuarioENTReq.ListUsuario = new List<Usuario>();
                usuarioENTReq.ListUsuario.Add(usuario);

                usuarioENTRes = usuarioNEG.GetUsuario(usuarioENTReq);

                if (usuarioENTRes.OK == 0)
                {
                    usuarioMOD.ListUsuario = new List<Usuario>();
                    usuarioMOD.ListUsuarioTemp = new List<Usuario>();
                    Session["ListUsuario"] = new List<Usuario>();
                    Session["ListUsuarioTemp"] = new List<Usuario>();

                    usuarioMOD.ListRolCronograma = new List<RolCronograma>();
                    Session["ListRolCronograma"] = new List<RolCronograma>();

                    usuarioMOD.MenuUsuario = usuarioSession.MenuUsuario;
                    usuarioMOD.MenuCronograma = usuarioSession.MenuCronograma;
                    usuarioMOD.MenuGrafico = usuarioSession.MenuGrafico;
                    usuarioMOD.MenuConfiguracion = usuarioSession.MenuConfiguracion;

                    usuarioMOD.OK = 0;
                    usuarioMOD.Mensaje = "ERROR: Ocurrio un error inesperado, no se pudo obtener la informacion del usuario para ingresar a Cronograma, intente de nuevo o consulte al administrador de sistemas.";

                    return Json(usuarioMOD, JsonRequestBehavior.AllowGet);
                }


                usuarioENTRes.ListUsuario = usuarioENTRes.ListUsuario.Where(n => n.Estatus > 0).ToList();

                usuarioMOD.ListUsuario = usuarioENTRes.ListUsuario;
                usuarioMOD.ListUsuarioTemp = usuarioENTRes.ListUsuario;

                Session["ListUsuario"] = usuarioENTRes.ListUsuario;
                Session["ListUsuarioTemp"] = usuarioENTRes.ListUsuario;

                //OBTENER ROLES
                rolCronograma.ID = 0;
                rolCronograma.Rol = null;

                rolCronogramaENTReq.ListRolCronograma = new List<RolCronograma>();

                rolCronogramaENTReq.ListRolCronograma.Add(rolCronograma);

                rolCronogramaENTRes = rolCronogramaNEG.GetRolCronograma(rolCronogramaENTReq);

                if (rolCronogramaENTRes.OK == 0)
                {
                    usuarioMOD.ListRolCronograma = new List<RolCronograma>();
                    Session["ListRolCronograma"] = rolCronogramaENTRes.ListRolCronograma;

                    usuarioMOD.MenuUsuario = usuarioSession.MenuUsuario;
                    usuarioMOD.MenuCronograma = usuarioSession.MenuCronograma;
                    usuarioMOD.MenuGrafico = usuarioSession.MenuGrafico;
                    usuarioMOD.MenuConfiguracion = usuarioSession.MenuConfiguracion;

                    usuarioMOD.OK = 0;
                    usuarioMOD.Mensaje = "ERROR: Ocurio un error inesperado, no se pudo obtener la informacion de los Roles para ingresar a Cronograma, intente de nuevo o consulte al administrador de sistemas.";

                    return Json(usuarioMOD, JsonRequestBehavior.AllowGet);
                }

                usuarioMOD.ListRolCronograma = rolCronogramaENTRes.ListRolCronograma;
                Session["ListRolCronograma"] = rolCronogramaENTRes.ListRolCronograma;

                usuarioMOD.OK = 1;
                usuarioMOD.Mensaje = "OK";
            }
            catch(Exception ex)
            {
                usuarioMOD.OK = 0;
                usuarioMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetUsuario, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            usuarioMOD.MenuUsuario = usuarioSession.MenuUsuario;
            usuarioMOD.MenuCronograma = usuarioSession.MenuCronograma;
            usuarioMOD.MenuGrafico = usuarioSession.MenuGrafico;
            usuarioMOD.MenuConfiguracion = usuarioSession.MenuConfiguracion;


            return Json(usuarioMOD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SearchUsuario(string PPGID, string Correo, string Usuario, RolCronograma RolCronograma)
        {
            UsuarioMOD usuarioMOD = new UsuarioMOD();
            List<Usuario> ListUsuario = new List<Usuario>();
            List<Usuario> ListUsuarioTemp = new List<Usuario>();

            try
            {
                ListUsuario = (List<Usuario>)Session["ListUsuario"];

                ListUsuarioTemp = (from UsuarioDeta in ListUsuario
                                   where (string.IsNullOrEmpty(PPGID) || UsuarioDeta.PPGID.ToUpper() == PPGID.ToUpper().Trim()) &&
                                           (string.IsNullOrEmpty(Correo) || UsuarioDeta.Correo.ToLower().Contains(Correo.ToLower().Trim())) &&
                                           (string.IsNullOrEmpty(Usuario) || UsuarioDeta.Nombre.ToUpper().Contains(Usuario.ToUpper().Trim())) &&
                                           (string.IsNullOrEmpty(RolCronograma.Rol) || UsuarioDeta.Rol.ToUpper() == RolCronograma.Rol.ToUpper().Trim())
                                   select new Usuario
                                   {
                                       ID = UsuarioDeta.ID,
                                       PPGID = UsuarioDeta.PPGID,
                                       Nombre = UsuarioDeta.Nombre,
                                       ID_RolCronograma = UsuarioDeta.ID_RolCronograma,
                                       Rol = UsuarioDeta.Rol,
                                       RolDescription = UsuarioDeta.RolDescription,
                                       Correo = UsuarioDeta.Correo
                                   }).ToList();


                Session["ListUsuarioTemp"] = ListUsuarioTemp;

                usuarioMOD.ListUsuarioTemp = ListUsuarioTemp;

                if (usuarioMOD.ListUsuarioTemp == null || usuarioMOD.ListUsuarioTemp.Count <= 0)
                {
                    usuarioMOD.OK = 2;
                    usuarioMOD.Mensaje = "No se encontro informacion con los parametros de busqueda.";
                }
                else
                {
                    usuarioMOD.OK = 1;
                    usuarioMOD.Mensaje = "OK";
                }

            }
            catch (Exception ex)
            {
                usuarioMOD.OK = 0;
                usuarioMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: SearchUsuario, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return Json(usuarioMOD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddUsuario(UsuarioLdap usuarioLdap, RolCronograma rol)
        {
            UsuarioMOD usuarioMOD = new UsuarioMOD();

            UsuarioNEG usuarioNEG = new UsuarioNEG();
            UsuarioENT usuarioENTReq = new UsuarioENT();

            List<Usuario> ListUsuario = new List<Usuario>();

            UsuarioENT usuarioENTRes = new UsuarioENT();

            Usuario usuario = new Usuario();

            try
            {
                usuario.PPGID = usuarioLdap.PPGID.ToUpper();
                usuario.Nombre = usuarioLdap.Nombre;
                usuario.Correo = usuarioLdap.Email.ToLower();

                usuarioENTReq.ListUsuario = new List<Usuario>();

                ListUsuario = (List<Usuario>)Session["ListUsuario"];

                if (ListUsuario.Where(n => n.PPGID == usuario.PPGID).Count() > 0)
                {
                    usuarioMOD.OK = 2;

                    usuarioMOD.Mensaje = "El PPGID ingresado ya existe, agregue uno nuevo para guardar la informacion.";

                    usuarioMOD.ListUsuario = ListUsuario;

                    usuarioMOD.ListUsuarioTemp = ListUsuario;

                    return Json(usuarioMOD, JsonRequestBehavior.AllowGet);
                }

                usuario.Nombre = usuario.Nombre.ToUpper();
                usuario.Correo = usuario.Correo.ToLower();
                usuario.PPGID = usuario.PPGID.ToLower();

                usuario.ID_RolCronograma = rol.ID;
                usuario.Rol = rol.Rol;
                usuario.RolCronograma = rol;

                usuarioENTReq.ListUsuario.Add(usuario);

                usuarioENTRes = usuarioNEG.AddUsuario(usuarioENTReq);

                if (usuarioENTRes.Mensaje == "OK")
                {
                    usuarioMOD.OK = 1;

                    usuarioMOD.Mensaje = "Se guardo correctamente el Usuario.";

                    usuario = usuarioENTRes.ListUsuario.FirstOrDefault();

                    ListUsuario.Add(usuario);

                    usuarioMOD.ListUsuario = ListUsuario;

                    usuarioMOD.ListUsuarioTemp = ListUsuario;
                }
                else
                {
                    usuarioMOD.OK = 0;

                    usuarioMOD.Mensaje = "ERROR: Ocurrio un error inesperado, no se guardo de forma correcta el Usuario, intenta de nuevo o consulta al administrador de sistemas.";
                }
            }
            catch (Exception ex)
            {
                usuarioMOD.OK = 0;
                usuarioMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: AddUsuario, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            return Json(usuarioMOD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult EditUsuario(Usuario usuario, RolCronograma rol)
        {
            UsuarioMOD usuarioMOD = new UsuarioMOD();

            UsuarioNEG usuarioNEG = new UsuarioNEG();
            UsuarioENT usuarioENTReq = new UsuarioENT();

            UsuarioENT usuarioENTRes = new UsuarioENT();

            List<Usuario> ListUsuario = new List<Usuario>();

            try
            {
                ListUsuario = (List<Usuario>)Session["ListUsuario"];

                usuarioENTReq.ListUsuario = new List<Usuario>();

                usuario.Nombre = usuario.Nombre.ToUpper();
                usuario.Correo = usuario.Correo.ToLower();
                usuario.PPGID = usuario.PPGID.ToLower();

                usuario.ID_RolCronograma = rol.ID;
                usuario.Rol = rol.Rol;
                usuario.RolCronograma = rol;

                usuarioENTReq.ListUsuario.Add(usuario);


                usuarioENTRes = usuarioNEG.EditUsuario(usuarioENTReq);

                if (usuarioENTRes.Mensaje == "OK")
                {
                    usuarioMOD.OK = 1;

                    usuarioMOD.Mensaje = "Se actualizo correctamente el Usuario.";

                    ListUsuario.Where(n => n.Correo.ToLower() == usuario.Correo.ToLower()).ToList()
                        .ForEach(n =>
                        {
                            n.ID_RolCronograma = rol.ID;
                            n.Rol = rol.Rol;
                            n.RolDescription = rol.Descripcion;
                        });

                    usuarioMOD.ListUsuario = ListUsuario;
                }
                else
                {
                    usuarioMOD.OK = 0;

                    usuarioMOD.Mensaje = "ERROR: Ocurrio un error inesperado, no se actualizo de forma correcta el Usuario, intenta de nuevo o consulta al administrador de sistemas.";
                }
            }
            catch (Exception ex)
            {
                usuarioMOD.OK = 0;
                usuarioMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: EditUsuario, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            return Json(usuarioMOD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RemoveUsuario(Usuario usuario)
        {
            UsuarioMOD usuarioMOD = new UsuarioMOD();

            UsuarioENT usuarioENTRes = new UsuarioENT();

            List<Usuario> ListUsuario = new List<Usuario>();

            UsuarioNEG usuarioNEG = new UsuarioNEG();
            UsuarioENT usuarioENTReq = new UsuarioENT();

            try
            {
                usuarioENTReq.ListUsuario = new List<Usuario>();

                usuarioENTReq.ListUsuario.Add(usuario);

                ListUsuario = (List<Usuario>)Session["ListUsuario"];


                usuarioENTRes = usuarioNEG.RemoveUsuario(usuarioENTReq);

                if (usuarioENTRes.Mensaje == "OK")
                {
                    usuarioMOD.OK = 1;

                    usuarioMOD.Mensaje = "Se elimino correctamente el Usuario.";

                    ListUsuario.Remove(ListUsuario.Where(n => n.PPGID == usuario.PPGID).FirstOrDefault());

                    usuarioMOD.ListUsuario = ListUsuario;

                    usuarioMOD.ListUsuarioTemp = ListUsuario;
                }
                else
                {
                    usuarioMOD.OK = 0;

                    usuarioMOD.Mensaje = "ERROR: Ocurrio un error inesperado, no se elimino de forma correcta el Usuario, intenta de nuevo o consulta al administrador de sistemas.";
                }
            }
            catch (Exception ex)
            {
                usuarioMOD.OK = 0;
                usuarioMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: RemoveUsuario, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            return Json(usuarioMOD, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SearchUsuarioLdap(string PPGID, string Correo, string Nombre)
        {
            UsuarioMOD usuarioMOD = new UsuarioMOD();

            //OBTENER USUARIO LDAP
            List<UsuarioLdap> ListUsuarioLdap = new List<UsuarioLdap>();
            List<UsuarioLdap> ListUsuarioLdapTemp = new List<UsuarioLdap>();

            try
            {
                ListUsuarioLdap = (List<UsuarioLdap>)Session["ListUsuarioLdap"];

                ListUsuarioLdapTemp = ListUsuarioLdap;

                if (!string.IsNullOrEmpty(PPGID))
                {
                    ListUsuarioLdapTemp = ListUsuarioLdapTemp.Where(n => n.PPGID == PPGID).ToList();
                }

                if (!string.IsNullOrEmpty(Correo))
                {
                    ListUsuarioLdapTemp = ListUsuarioLdapTemp.Where(n => n.Email.ToLower().Contains(Correo.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(Nombre))
                {
                    ListUsuarioLdapTemp = ListUsuarioLdapTemp.Where(n => n.Nombre.ToUpper().Contains(Nombre.ToUpper())).ToList();
                }

                usuarioMOD.ListUsuarioLdapTemp = ListUsuarioLdapTemp;

                Session["ListUsuarioLdapTemp"] = ListUsuarioLdapTemp;

                if (ListUsuarioLdapTemp == null || ListUsuarioLdapTemp.Count <= 0)
                {
                    usuarioMOD.Mensaje = "No se encontro informacion con los filtros agregados.";
                    usuarioMOD.OK = 2;
                }
                else
                {
                    usuarioMOD.Mensaje = "OK";
                    usuarioMOD.OK = 1;
                }
            }
            catch (Exception ex)
            {
                usuarioMOD.OK = 0;
                usuarioMOD.Mensaje = "ERROR: Ocurrio un error inesperado al cargar la informacion de la pagina, intenta cargar de nuevo la pagina o consulta al administrador de sistemas.";

                ArchivoLog.EscribirLog(null, "ERROR: Service: SearchUsuarioLdap, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            return Json(usuarioMOD, JsonRequestBehavior.AllowGet);
        }
    }
}