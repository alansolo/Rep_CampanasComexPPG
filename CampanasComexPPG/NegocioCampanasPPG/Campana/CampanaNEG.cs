using DatosCampanasPPG.Campana;
using EntidadesCampanasPPG.BDCampana;
using EntidadesCampanasPPG.Campana;
using EntidadesCampanasPPG.Catalogo;
using EntidadesCampanasPPG.Ldap;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilidadesCampanasPPG;

namespace NegocioCampanasPPG.Campana
{
    public class CampanaNEG
    {
        public CampanaENT GetCampana(CampanaENT UsuarioENTReq)
        {
            CampanaENT CampanaENTRes = new CampanaENT();

            try
            {
                EntidadesCampanasPPG.BDCampana.Campana Campana = UsuarioENTReq.ListCampana.FirstOrDefault();

                DataTable dtCampana = new DataTable();

                CampanaDAT campanaDAT = new CampanaDAT();

                dtCampana = campanaDAT.GetCampana(Campana.ID, Campana.Camp_Number, Campana.Nombre_Camp, Campana.Lider_Campania);

                CampanaENTRes.ListCampana = dtCampana.AsEnumerable()
                                            .Select(row => new EntidadesCampanasPPG.BDCampana.Campana
                                            {
                                                ID = row.Field<int?>("ID").GetValueOrDefault(),
                                                Camp_Number = row.Field<string>("Camp_Number"),
                                                Nombre_Camp = row.Field<string>("Nombre_Camp"),
                                                Nombre_Usuario = row.Field<string>("Nombre_Usuario"),
                                                PPG_ID = row.Field<string>("PPGID"),
                                                Lider_Campania = row.Field<string>("Lider_Campania"),
                                                PPGID_Lider = row.Field<string>("PPGID_Lider"),
                                                Fecha_Inicio_Publico = row.Field<DateTime?>("Fecha_Inicio_Publico").GetValueOrDefault().ToString("dd/MM/yyyy") == "01/01/0001" ? "--/--/----": row.Field<DateTime?>("Fecha_Inicio_Publico").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                                Fecha_Fin_Publico = row.Field<DateTime?>("Fecha_Fin_Publico").GetValueOrDefault().ToString("dd/MM/yyyy") == "01/01/0001" ? "--/--/----": row.Field<DateTime?>("Fecha_Fin_Publico").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                                Fecha_Creacion = row.Field<DateTime?>("Fecha_Creacion").GetValueOrDefault().ToString("dd/MM/yyyy") == "01/01/0001" ? "--/--/----": row.Field<DateTime?>("Fecha_Creacion").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                                ID_Moneda = row.Field<int?>("ID_Moneda").GetValueOrDefault(),
                                                Moneda = row.Field<string>("Moneda"),
                                                ID_TipoCamp = row.Field<int?>("ID_TipoCamp").GetValueOrDefault(),
                                                TipoCamp = row.Field<string>("TipoCamp"),
                                                ID_Alcance = row.Field<int?>("ID_Alcance").GetValueOrDefault(),
                                                Alcance = row.Field<string>("Alcance"),
                                                ID_TipoSell = row.Field<int?>("ID_TipoSell").GetValueOrDefault(),
                                                TipoSell = row.Field<string>("TipoSell"),
                                                Express = row.Field<bool?>("Express").GetValueOrDefault(),
                                                ID_Estatus = row.Field<int?>("ID_Estatus").GetValueOrDefault(),
                                                Estatus = row.Field<string>("Estatus"),
                                                EstatusCat = row.Field<string>("EstatusCat"),
                                                TipoSubCanal = row.Field<string>("TipoSubCanal")
                                            }).ToList();

                CampanaENTRes.ListCampana.Where(n => n.Express != null).ToList().ForEach(m =>
                {
                    m.strExpress = m.Express == true ? "Si" : "No";
                });

                CampanaENTRes.OK = 1;
                CampanaENTRes.Mensaje = "OK";
            }
            catch(Exception ex)
            {
                CampanaENTRes.OK = 0;
                CampanaENTRes.Mensaje = "ERROR: Service: GetCampana, Source: " + ex.Source + ", Message: " + ex.Message;

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetCampana, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return CampanaENTRes;
        }
        public CampanaENT GetCampanaUsuario(CampanaENT UsuarioENTReq)
        {
            CampanaENT CampanaENTRes = new CampanaENT();

            try
            {
                EntidadesCampanasPPG.BDCampana.Campana Campana = UsuarioENTReq.ListCampana.FirstOrDefault();

                DataTable dtCampana = new DataTable();

                CampanaDAT campanaDAT = new CampanaDAT();

                dtCampana = campanaDAT.GetCampanaUsuario(Campana.PPG_ID);

                CampanaENTRes.ListCampana = dtCampana.AsEnumerable()
                                            .Select(row => new EntidadesCampanasPPG.BDCampana.Campana
                                            {
                                                ID = row.Field<int?>("ID").GetValueOrDefault(),
                                                Camp_Number = row.Field<string>("Camp_Number"),
                                                Nombre_Camp = row.Field<string>("Nombre_Camp")
                                            }).ToList();

                CampanaENTRes.OK = 1;
                CampanaENTRes.Mensaje = "OK";
            }
            catch(Exception ex)
            {
                CampanaENTRes.OK = 0;
                CampanaENTRes.Mensaje = "ERROR: Service: GetCampanaUsuario, Source: " + ex.Source + ", Message: " + ex.Message;

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetCampanaUsuario, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return CampanaENTRes;
        }
        public CampanaENT GetCampanaCronograma(CampanaENT UsuarioENTReq)
        {
            CampanaENT CampanaENTRes = new CampanaENT();

            try
            {
                EntidadesCampanasPPG.BDCampana.Campana Campana = UsuarioENTReq.ListCampana.FirstOrDefault();

                DataTable dtCampana = new DataTable();

                CampanaDAT campanaDAT = new CampanaDAT();

                dtCampana = campanaDAT.GetCampanaCronograma(Campana.ID, Campana.Camp_Number, Campana.Nombre_Camp, Campana.Lider_Campania);

                CampanaENTRes.ListCampana = dtCampana.AsEnumerable()
                                            .Select(row => new EntidadesCampanasPPG.BDCampana.Campana
                                            {
                                                ID = row.Field<int?>("ID").GetValueOrDefault(),
                                                Camp_Number = row.Field<string>("Camp_Number"),
                                                Nombre_Camp = row.Field<string>("Nombre_Camp"),
                                                Nombre_Usuario = row.Field<string>("Nombre_Usuario"),
                                                PPG_ID = row.Field<string>("PPGID"),
                                                Lider_Campania = row.Field<string>("Lider_Campania"),
                                                PPGID_Lider = row.Field<string>("PPGID_Lider"),
                                                Fecha_Inicio_Publico = row.Field<DateTime?>("Fecha_Inicio_Publico").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                                Fecha_Fin_Publico = row.Field<DateTime?>("Fecha_Fin_Publico").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                                Fecha_Creacion = row.Field<DateTime?>("Fecha_Creacion").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                                Fecha_Inicio = row.Field<DateTime?>("FechaInicio").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                                Fecha_Inicio_Real = row.Field<DateTime?>("FechaInicioReal").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                                Fecha_Fin = row.Field<DateTime?>("FechaFin").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                                Fecha_Fin_Real = row.Field<DateTime?>("FechaFinReal").GetValueOrDefault().ToString("dd/MM/yyyy"),
                                                PorcUsuario = row.Field<decimal?>("PorcentajeUsuario").GetValueOrDefault(),
                                                PorcSistema = row.Field<decimal?>("PorcentajeSistema").GetValueOrDefault(),
                                                PorcSistemaReal = row.Field<decimal?>("PorcentajeSistemaReal").GetValueOrDefault(),
                                                ID_Moneda = row.Field<int?>("ID_Moneda").GetValueOrDefault(),
                                                ID_TipoCamp = row.Field<int?>("ID_TipoCamp").GetValueOrDefault(),
                                                ID_Alcance = row.Field<int?>("ID_Alcance").GetValueOrDefault(),
                                                Alcance = row.Field<string>("Alcance"),
                                                Express = row.Field<bool?>("Express").GetValueOrDefault(),
                                                Estatus = row.Field<string>("Estatus"),
                                                ID_Estatus = row.Field<int?>("ID_Estatus").GetValueOrDefault(),
                                                EstatusCat = row.Field<string>("EstatusCat")
                                            }).ToList();

                CampanaENTRes.OK = 1;
                CampanaENTRes.Mensaje = "OK";
            }
            catch (Exception ex)
            {
                CampanaENTRes.OK = 0;
                CampanaENTRes.Mensaje = "ERROR: Service: GetCampanaCronograma, Source: " + ex.Source + ", Message: " + ex.Message;

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetCampanaCronograma, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return CampanaENTRes;
        }
        public MecanicaENT GetCampanaMecanica(MecanicaENT mecanicaENTReq)
        {
            MecanicaENT mecanicaENTRes = new MecanicaENT();
            DataSet dsMecanicas = new DataSet();

            try
            {
                if (mecanicaENTReq == null)
                {
                    return mecanicaENTRes;
                }

                CampanaDAT campanaDAT = new CampanaDAT();

                dsMecanicas = campanaDAT.GetCampanaMecanica(mecanicaENTReq.IdCampana);

                //REGALO
                mecanicaENTRes.ListMecanicaRegalo = dsMecanicas.Tables[0].AsEnumerable()
                    .Select(row => new MecanicaRegalo
                    {
                        ClaveCampana = mecanicaENTReq.ClaveCampana,
                        IdCampana = row.Field<int?>("ID_Campania").GetValueOrDefault(),
                        Familia = row.Field<string>("Familia"),
                        SKU = row.Field<string>("Articulo"),
                        Descripcion = row.Field<string>("Descripcion"),
                        Tipo = row.Field<string>("Tipo"),
                        Grupo_Regalo = row.Field<int?>("Grupo_Regalo").GetValueOrDefault(),
                        NumeroHijo = row.Field<int?>("Numero_Hijo").GetValueOrDefault(),
                        Alcance = row.Field<string>("Alcance"),
                        Capacidad = row.Field<string>("Capacidad"),
                        Dinamica = row.Field<string>("Dinamica"),
                        VLitrosAnioAnt = row.Field<decimal?>("Ventas_Litros_Anio_Anterior").GetValueOrDefault(),
                        PLitrosSinCamp = row.Field<decimal?>("Presupuesto_Litros_Sin_Campania").GetValueOrDefault(),
                        PLitrosConCamp = row.Field<decimal?>("Presupuesto_Litros_Con_Campania").GetValueOrDefault()
                    }).ToList();

                //MULTIPLO
                mecanicaENTRes.ListMecanicaMultiplo = dsMecanicas.Tables[1].AsEnumerable()
                    .Select(row => new MecanicaMultiplo
                    {
                        ClaveCampana = mecanicaENTReq.ClaveCampana,
                        IdCampana = row.Field<int?>("ID_Campania").GetValueOrDefault(),
                        Familia = row.Field<string>("Familia"),
                        SKU = row.Field<string>("SKU"),
                        Descripcion = row.Field<string>("Descripcion"),
                        Alcance = row.Field<string>("Alcance"),
                        Capacidad = row.Field<string>("Capacidad"),
                        Dinamica = row.Field<string>("Dinamica"),
                        Multiplo_Padre = row.Field<int?>("MultiploPadre").GetValueOrDefault(),
                        Multiplo_Hijo = row.Field<int?>("MultiploHijo").GetValueOrDefault(),
                        Punto_Venta = row.Field<string>("Punto_Venta"),
                        VLitrosAnioAnt = row.Field<decimal?>("Venta_Anio_Anterior").GetValueOrDefault(),
                        PLitrosSinCamp = row.Field<decimal?>("Presupuesto_Sin_Campania").GetValueOrDefault(),
                        PLitrosConCamp = row.Field<decimal?>("Presupuesto_Con_Campania").GetValueOrDefault()
                    }).ToList();

                //DESCUENTO
                mecanicaENTRes.ListMecanicaDescuento = dsMecanicas.Tables[2].AsEnumerable()
                    .Select(row => new MecanicaDescuento
                    {
                        ClaveCampana = mecanicaENTReq.ClaveCampana,
                        IdCampana = row.Field<int?>("ID_Campania").GetValueOrDefault(),
                        Familia = row.Field<string>("Familia"),
                        SKU = row.Field<string>("Articulo"),
                        Descripcion = row.Field<string>("Descripcion"),
                        Alcance = row.Field<string>("Alcance"),
                        Capacidad = row.Field<string>("Capacidad"),
                        Dinamica = row.Field<string>("Dinamica"),
                        Porcentaje = row.Field<decimal?>("Porcentaje").GetValueOrDefault(),
                        Importe = row.Field<decimal?>("Importe").GetValueOrDefault(),
                        VLitrosAnioAnt = row.Field<decimal?>("VentaAñoAnterior").GetValueOrDefault(),
                        PLitrosSinCamp = row.Field<decimal?>("PresupuestoSinCampaña").GetValueOrDefault(),
                        PLitrosConCamp = row.Field<decimal?>("PresupuestoConCampaña").GetValueOrDefault()
                    }).ToList();

                //VOLUMEN
                mecanicaENTRes.ListMecanicaVolumen = dsMecanicas.Tables[3].AsEnumerable()
                    .Select(row => new MecanicaVolumen
                    {
                        ClaveCampana = mecanicaENTReq.ClaveCampana,
                        IdCampana = row.Field<int?>("ID_Campania").GetValueOrDefault(),
                        Familia = row.Field<string>("Familia"),
                        SKU = row.Field<string>("Articulo"),
                        Descripcion = row.Field<string>("Descripcion"),
                        Bloque = row.Field<int?>("Bloque").GetValueOrDefault(),
                        Alcance = row.Field<string>("Alcance"),
                        Capacidad = row.Field<string>("Capacidad"),
                        Dinamica = row.Field<string>("Dinamica"),
                        De = row.Field<decimal?>("VentaDesde").GetValueOrDefault(),
                        Hasta = row.Field<decimal?>("VentaHasta").GetValueOrDefault(),
                        Descuento = row.Field<decimal?>("PorcentajeDescuento").GetValueOrDefault(),
                        VLitrosAnioAnt = row.Field<decimal?>("VentaAnioAnterior").GetValueOrDefault(),
                        PLitrosSinCamp = row.Field<decimal?>("PresupuestoSinCampania").GetValueOrDefault(),
                        PLitrosConCamp = row.Field<decimal?>("PresupuestoConCampania").GetValueOrDefault()
                    }).ToList();

                //KIT
                mecanicaENTRes.ListMecanicaKit = dsMecanicas.Tables[4].AsEnumerable()
                    .Select(row => new MecanicaKit
                    {
                        ClaveCampana = mecanicaENTReq.ClaveCampana,
                        IdCampana = row.Field<int?>("ID_Campania").GetValueOrDefault(),
                        Familia = row.Field<string>("Familia"),
                        SKU = row.Field<string>("Articulo"),
                        Descripcion = row.Field<string>("Descripcion"),
                        Alcance = row.Field<string>("Alcance"),
                        Capacidad = row.Field<string>("Capacidad"),
                        Dinamica = row.Field<string>("Dinamica"),
                        Porcentaje = row.Field<decimal?>("Porcentaje").GetValueOrDefault(),
                        Importe = row.Field<decimal?>("Importe").GetValueOrDefault(),
                        VLitrosAnioAnt = row.Field<decimal?>("VentaAnioAnterior").GetValueOrDefault(),
                        PLitrosSinCamp = row.Field<decimal?>("PresupuestoSinCampania").GetValueOrDefault(),
                        PLitrosConCamp = row.Field<decimal?>("PresupuestoConCampania").GetValueOrDefault()
                    }).ToList();

                //COMBO
                mecanicaENTRes.ListMecanicaCombo = dsMecanicas.Tables[5].AsEnumerable()
                    .Select(row => new MecanicaCombo
                    {
                        ClaveCampana = mecanicaENTReq.ClaveCampana,
                        IdCampana = row.Field<int?>("ID_Campania").GetValueOrDefault(),
                        Familia = row.Field<string>("Familia"),
                        SKU = row.Field<string>("Articulo"),
                        Descripcion = row.Field<string>("Descripcion"),
                        Tipo = row.Field<string>("TipoArticulo"),
                        GrupoCombo = row.Field<int?>("Grupo_Combo").GetValueOrDefault(),
                        NumeroPadre = row.Field<int?>("Numero_Padre").GetValueOrDefault(),
                        NumeroHijo = row.Field<int?>("Numero_Hijo").GetValueOrDefault(),
                        Alcance = row.Field<string>("Alcance"),
                        Capacidad = row.Field<string>("Capacidad"),
                        Dinamica = row.Field<string>("Dinamica"),
                        VLitrosAnioAnt = row.Field<decimal?>("VentaAnioAnterior").GetValueOrDefault(),
                        PLitrosSinCamp = row.Field<decimal?>("PresupuestoSinCampania").GetValueOrDefault(),
                        PLitrosConCamp = row.Field<decimal?>("PresupuestoConCampania").GetValueOrDefault()
                    }).ToList();

                //TIENDAS
                mecanicaENTRes.ListMecanicaTiendas = dsMecanicas.Tables[6].AsEnumerable()
                    .Select(row => new MecanicaTiendas
                    {
                        ClaveCampania = mecanicaENTReq.ClaveCampana,
                        IdCampania = row.Field<int?>("ID_Campania").GetValueOrDefault(),
                        Id_Tienda = row.Field<int?>("ID_Tienda").GetValueOrDefault(),
                        Bill_To = row.Field<string>("Bill_To"),
                        Customer_Name = row.Field<string>("Customer_Name"),
                        Region = row.Field<string>("Region"),
                        Descripcion_Region = row.Field<string>("Descripcion_Region"),
                        Descripcion_Zona = row.Field<string>("Descripcion_Zona"),
                        Segmento = row.Field<string>("Segmento"),
                        Clave_Sobrepecio  = row.Field<string>("Clave_Sobreprecio"),
                    }).ToList();

                mecanicaENTRes.OK = 1;
                mecanicaENTRes.Mensaje = "OK";

            }
            catch (Exception ex)
            {
                mecanicaENTRes.OK = 0;
                mecanicaENTRes.Mensaje = "ERROR: Service: GetCampanaMecanica, Source: " + ex.Source + ", Message: " + ex.Message;

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetCampanaMecanica, Source: " + ex.Source + ", Message: " + ex.Message);
            }

            return mecanicaENTRes;
        }
    }
}
