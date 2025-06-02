using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pry_Tello_Erika_IEFI
{
    public class clsAuditoria
    {
        public TimeSpan? HoraSalida { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaIngreso { get; set; }
        public TimeSpan HoraIngreso { get; set; }

        public clsAuditoria() { }

    }

}








