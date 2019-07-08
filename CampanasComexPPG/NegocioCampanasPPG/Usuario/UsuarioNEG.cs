using DatosCampanasPPG.Usuario;
using EntidadesCampanasPPG.Usuario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilidadesCampanasPPG;

namespace NegocioCampanasPPG.Usuario
{
    public class UsuarioNEG
    {
        public UsuarioENT GetUsuario(UsuarioENT UsuarioENTReq)
        {
            UsuarioENT usuarioENTRes = new UsuarioENT();

            try
            {
                EntidadesCampanasPPG.BDCampana.Usuario usuario = UsuarioENTReq.ListUsuario.FirstOrDefault();

                DataTable dtUsuario = new DataTable();

                UsuarioDAT usuarioDAT = new UsuarioDAT();

                //if(string.IsNullOrEmpty(usuario.Correo))
                //{
                //    usuario.Correo = "-1";
                //}

                dtUsuario = usuarioDAT.GetUsuario(string.Empty, usuario.Correo, string.Empty);


                usuarioENTRes.ListUsuario = dtUsuario.AsEnumerable()
                                            .Select(row => new EntidadesCampanasPPG.BDCampana.Usuario
                                            {
                                                ID = row.Field<int?>("ID").GetValueOrDefault(),
                                                PPGID = row.Field<string>("PPGID"),
                                                Nombre = row.Field<string>("Nombre"),
                                                Correo = row.Field<string>("Correo"),
                                                ID_RolCronograma = row.Field<int?>("ID_RolCronograma").GetValueOrDefault(),
                                                Rol = row.Field<string>("Rol"),
                                                RolDescription = row.Field<string>("Descripcion"),
                                                Estatus = row.Field<int?>("Estatus").GetValueOrDefault()
                                            }).ToList();

                usuarioENTRes.Mensaje = "OK";

                usuarioENTRes.OK = 1;
            }
            catch(Exception ex)
            {
                usuarioENTRes.Mensaje = "ERROR: Service: GetUsuario, Source: " + ex.Source + ", Message: " + ex.Message;

                usuarioENTRes.OK = 0;

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetUsuario, Source: " + ex.Source + ", Message: " + ex.Message);

            }

            return usuarioENTRes;
        }
        public UsuarioENT AddUsuario(UsuarioENT UsuarioENTReq)
        {
            const int SpAdd = 1;
            UsuarioENT usuarioENTRes = new UsuarioENT();

            try
            {
                EntidadesCampanasPPG.BDCampana.Usuario usuario = UsuarioENTReq.ListUsuario.FirstOrDefault();

                DataTable dtUsuario = new DataTable();

                UsuarioDAT usuarioDAT = new UsuarioDAT();

                dtUsuario = usuarioDAT.AddUsuario(usuario.PPGID, usuario.Nombre, usuario.Correo, usuario.ID_RolCronograma, SpAdd);

                usuarioENTRes.ListUsuario = dtUsuario.AsEnumerable()
                                            .Select(row => new EntidadesCampanasPPG.BDCampana.Usuario
                                            {
                                                ID = row.Field<int?>("ID").GetValueOrDefault(),
                                                PPGID = row.Field<string>("PPGID"),
                                                Nombre = row.Field<string>("Nombre"),
                                                Correo = row.Field<string>("Correo"),
                                                ID_RolCronograma = row.Field<int?>("ID_RolCronograma").GetValueOrDefault(),
                                                Rol = row.Field<string>("Rol"),
                                                RolDescription = row.Field<string>("Descripcion")
                                            }).ToList();

                usuarioENTRes.Mensaje = "OK";

                usuarioENTRes.OK = 1;
            }
            catch(Exception ex)
            {
                usuarioENTRes.Mensaje = "ERROR: Service: AddUsuario, Source: " + ex.Source + ", Message: " + ex.Message;

                usuarioENTRes.OK = 0;

                ArchivoLog.EscribirLog(null, "ERROR: Service: AddUsuario, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return usuarioENTRes;
        }
        public UsuarioENT EditUsuario(UsuarioENT UsuarioENTReq)
        {
            const int SpAdd = 2;
            UsuarioENT usuarioENTRes = new UsuarioENT();

            try
            {
                EntidadesCampanasPPG.BDCampana.Usuario usuario = UsuarioENTReq.ListUsuario.FirstOrDefault();

                int resUpdate = 0;

                UsuarioDAT usuarioDAT = new UsuarioDAT();

                resUpdate = usuarioDAT.EditUsuario(usuario.PPGID, usuario.Nombre, usuario.Correo, usuario.ID_RolCronograma, SpAdd);

                usuarioENTRes.Mensaje = "OK";

                usuarioENTRes.OK = 1;
            }
            catch (Exception ex)
            {
                usuarioENTRes.Mensaje = "ERROR: Service: EditUsuario, Source: " + ex.Source + ", Message: " + ex.Message;

                usuarioENTRes.OK = 0;

                ArchivoLog.EscribirLog(null, "ERROR: Service: EditUsuario, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return usuarioENTRes;
        }
        public UsuarioENT RemoveUsuario(UsuarioENT UsuarioENTReq)
        {
            const int SpAdd = 3;
            UsuarioENT usuarioENTRes = new UsuarioENT();

            try
            {
                EntidadesCampanasPPG.BDCampana.Usuario usuario = UsuarioENTReq.ListUsuario.FirstOrDefault();

                int resUpdate = 0;

                UsuarioDAT usuarioDAT = new UsuarioDAT();

                resUpdate = usuarioDAT.RemoveUsuario(usuario.PPGID, usuario.Nombre, usuario.Correo, usuario.ID_RolCronograma, SpAdd);

                usuarioENTRes.Mensaje = "OK";

                usuarioENTRes.OK = 1;
            }
            catch (Exception ex)
            {
                usuarioENTRes.Mensaje = "ERROR: Service: RemoveUsuario, Source: " + ex.Source + ", Message: " + ex.Message;

                usuarioENTRes.OK = 0;

                ArchivoLog.EscribirLog(null, "ERROR: Service: RemoveUsuario, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return usuarioENTRes;
        }
    }
}
