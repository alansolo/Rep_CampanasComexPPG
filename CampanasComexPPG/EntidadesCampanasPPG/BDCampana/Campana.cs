//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntidadesCampanasPPG.BDCampana
{
    using System;
    using System.Collections.Generic;
    
    public partial class Campana
    {
        public int ID { get; set; }
        public string Camp_Number { get; set; }
        public string Nombre_Camp { get; set; }
        public string Fecha_de_Alta { get; set; }
        public Nullable<int> ID_Negocio_Lider { get; set; }
        public string PPGID_Lider { get; set; }
        public string Lider_Campania { get; set; }
        public decimal PorcUsuario { get; set; }
        public decimal PorcSistema { get; set; }
        public decimal PorcSistemaReal { get; set; }
        public Nullable<int> ID_SubCanal { get; set; }
        public Nullable<int> ID_Moneda { get; set; }
        public string Moneda { get; set; }
        public Nullable<bool> Express { get; set; }
        public string strExpress { get; set; }
        public int ID_Estatus { get; set; }
        public string Estatus { get; set; }
        public string EstatusCat { get; set; }
        public Nullable<int> ID_TipoSell{ get; set; }
        public string TipoSell { get; set; }
        public Nullable<int> ID_TipoCamp { get; set; }
        public string TipoCamp { get; set; }
        public Nullable<int> ID_Alcance { get; set; }
        public string Alcance { get; set; }
        public string Clientes_otros_canales { get; set; }
        public string Fecha_Inicio_SubCanal { get; set; }
        public string Fecha_Fin_SubCanal { get; set; }
        public string Fecha_Inicio_Publico { get; set; }
        public string Fecha_Fin_Publico { get; set; }
        public string PPG_ID { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Fecha_Creacion { get; set; }
        public string Fecha_Modificacion { get; set; }
        public string Fecha_Inicio { get; set; }
        public string Fecha_Inicio_Real { get; set; }
        public string Fecha_Fin { get; set; }
        public string Fecha_Fin_Real { get; set; }
        public string TipoSubCanal { get; set; }
    }
}