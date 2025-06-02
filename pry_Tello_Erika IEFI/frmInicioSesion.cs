using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pry_Tello_Erika_IEFI
{   
    public partial class frmInicioSesion : Form
    {
        private string perfilSeleccionado;
        public int IdUsuario { get; set; }
        public frmInicioSesion(string perfil)
        {
            InitializeComponent();

            perfilSeleccionado = perfil;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUsuario.Text;
            string clave = txtClave.Text;

            clsUsuarioDAO dao = new clsUsuarioDAO();

            if (dao.ValidarUsuario(nombreUsuario, clave))
            {
                IdUsuario = dao.ObtenerIdUsuario(nombreUsuario);

          
                frmPrincipal frm = new frmPrincipal(IdUsuario, perfilSeleccionado);
                frm.Show();
                //this.Hide(); 
            }
            else
            {
                MessageBox.Show("Usuario o clave incorrectos.");
            }
        }
        

        private void frmInicioSesion_Load(object sender, EventArgs e)
        {

        }
    }
}
