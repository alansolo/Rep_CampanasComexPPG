using DatosCampanasPPG.Catalogo;
using DatosCampanasPPG.Ldap;
using EntidadesCampanasPPG.BDCampana;
using EntidadesCampanasPPG.Ldap;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilidadesCampanasPPG;

namespace NegocioCampanasPPG.Ldap
{
    public class LdapNEG
    {
        private List<UsuarioLdap> GetUsuario(string serverLdap)
        {
            List<UsuarioLdap> ListUsurioLdapTemp = new List<UsuarioLdap>();
            List<UsuarioLdap> ListUsurioLdap = new List<UsuarioLdap>();
            //string serverLdap = string.Empty;
            string directorioLdap = string.Empty;

            LdapDAT ldapDAT = new LdapDAT();
            DataTable dtDirectorioLdap = new DataTable();
            List<DirectorioActivo> ListDirectorioActivo = new List<DirectorioActivo>();

            //List<Parametro> ListParametro = new List<Parametro>();
            //Parametro parametro = new Parametro();

            try
            {
                //ListParametro = (List<Parametro>)Session["ListParametro"];

                //dtParametro = parametroDAT.GetParametro(0, null);

                //ListParametro = dtParametro.AsEnumerable()
                //                .Select(n => new Parametro
                //                {
                //                    Id = n.Field<int?>("Id").GetValueOrDefault(),
                //                    Nombre = n.Field<string>("Nombre"),
                //                    Valor = n.Field<string>("Valor")
                //                }).ToList();

                //parametro = ListParametro.Where(n => n.Nombre.ToUpper() == ConfigurationManager.AppSettings["ServerLdap"].ToUpper()).FirstOrDefault();

                dtDirectorioLdap = ldapDAT.GetDirectorioLdap(0, null);

                ListDirectorioActivo = dtDirectorioLdap.AsEnumerable()
                                        .Select(n => new DirectorioActivo
                                        {
                                            Id = n.Field<int?>("Id").GetValueOrDefault(),
                                            Clave = n.Field<string>("Clave"),
                                            Descripcion = n.Field<string>("Descripcion"),
                                            Ldap = n.Field<string>("Ldap")
                                        }).ToList();

                //DIRECTORIO LDAP
                //serverLdap = parametro.Valor;

                foreach (DirectorioActivo directorio in ListDirectorioActivo)
                {
                    //DIRECTORIO
                    directorioLdap = directorio.Ldap;

                    using (var context = new PrincipalContext(ContextType.Domain, serverLdap, directorioLdap))
                    {
                        UserPrincipal userPrincipal = new UserPrincipal(context);

                        using (var searcher = new PrincipalSearcher(userPrincipal))
                        {
                            var ListUserPrincipal = searcher.FindAll().Cast<UserPrincipal>().ToList();

                            ListUsurioLdapTemp = ListUserPrincipal.Select(row => new UsuarioLdap
                            {
                                Email = row.EmailAddress != null ? row.EmailAddress.ToLower() : string.Empty,
                                Nombre = row.Name,
                                PPGID = row.SamAccountName
                            }).ToList();

                            ListUsurioLdap.AddRange(ListUsurioLdapTemp);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ListUsurioLdap;
        }
        public LdapENT GetUsuarioLdap(LdapENT ldapENTReq)
        {
            LdapENT ldapENTRes = new LdapENT();

            Ldap.LdapNEG ldap = new Ldap.LdapNEG();

            try
            {
                ldapENTRes.ListUsuarioLdap = ldap.GetUsuario(ldapENTReq.ServerLdap);

                ldapENTRes.Mensaje = "OK";
                ldapENTRes.OK = 1;
            }
            catch (Exception ex)
            {
                ldapENTRes.Mensaje = "ERROR: Service: GetUsuarioLdap, Source: " + ex.Source + ", Message: " + ex.Message;
                ldapENTRes.OK = 0;

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetUsuarioLdap, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return ldapENTRes;
        }
    }
}
