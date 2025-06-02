using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pry_Tello_Erika_IEFI
{
    public class clsConexionBD : IDisposable
    {
        private OleDbConnection conexion;
        //private static string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\SistemaEmpleados.mdb";
        string connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=SistemaEmpleados.mdb";

        public OleDbConnection AbrirConexion()
        {
            try
            {
                conexion = new OleDbConnection(connectionString);
                conexion.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al intentar conectar con la base de datos: {ex.Message}");
                throw new Exception("No se pudo establecer una conexión con la base de datos.", ex);
            }
            return conexion;
        }


        public void CerrarConexion()
        {
            if (conexion != null && conexion.State == System.Data.ConnectionState.Open)
            {
                conexion.Close();
            }
        }
        public void Dispose()
        {
            CerrarConexion();
            GC.SuppressFinalize(this);
        }
    }


}

       

