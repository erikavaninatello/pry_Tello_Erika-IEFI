using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pry_Tello_Erika_IEFI
{
    public class clsUsuario
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public int IdRol { get; set; }

        public string NombreCompleto { get; set; }
        public string Tarea { get; set; }
        public DateTime Fecha { get; set; }

    }
}

