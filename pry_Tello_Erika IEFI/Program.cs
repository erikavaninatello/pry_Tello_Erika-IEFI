using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pry_Tello_Erika_IEFI
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

        
            frmBienvenida bienvenida = new frmBienvenida();

            if (bienvenida.ShowDialog() == DialogResult.OK)
            {
            
                string perfilSeleccionado = bienvenida.PerfilSeleccionado;

             
                frmInicioSesion inicioSesion = new frmInicioSesion(perfilSeleccionado);

                if (inicioSesion.ShowDialog() == DialogResult.OK)
                {
                    int idUsuario = inicioSesion.IdUsuario;

                   
                    Application.Run(new frmPrincipal(idUsuario, perfilSeleccionado));
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                Application.Exit();
            }

        }
    }
}
