using DatosCampanasPPG.Catalogo;
using EntidadesCampanasPPG.BDCampana;
using EntidadesCampanasPPG.Catalogo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilidadesCampanasPPG;

namespace NegocioCampanasPPG.Catalogo
{
    public class RolCronogramaNEG
    {
        public RolCronogramaENT GetRolCronograma(RolCronogramaENT rolCronogramaENTReq)
        {
            DataTable dtRolCronograma = new DataTable();

            RolCronograma rolCronograma = rolCronogramaENTReq.ListRolCronograma.FirstOrDefault();

            RolCronogramaENT rolCronogramaENTRes = new RolCronogramaENT();

            RolCronogramaDAT rolCronogramaDAT = new RolCronogramaDAT();

            try
            {
                dtRolCronograma = rolCronogramaDAT.GetRolCronograma(rolCronograma.ID, rolCronograma.Rol);

                rolCronogramaENTRes.ListRolCronograma = dtRolCronograma.AsEnumerable()
                                            .Select(row => new RolCronograma
                                            {
                                                ID = row.Field<int?>("ID").GetValueOrDefault(),
                                                Rol = row.Field<string>("Rol"),
                                                Descripcion = row.Field<string>("Descripcion")
                                            }).ToList();

                rolCronogramaENTRes.Mensaje = "OK";
                rolCronogramaENTRes.OK = 1;

            }
            catch(Exception ex)
            {
                rolCronogramaENTRes.ListRolCronograma = new List<RolCronograma>();

                rolCronogramaENTRes.Mensaje = "ERROR: Service: GetRolCronograma, Source: " + ex.Source + ", Message: " + ex.Message;
                rolCronogramaENTRes.OK = 0;

                ArchivoLog.EscribirLog(null, "ERROR: Service: GetRolCronograma, Source: " + ex.Source + ", Message: " + ex.Message);
            }


            return rolCronogramaENTRes;
        }
    }
}
