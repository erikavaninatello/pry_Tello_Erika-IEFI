using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pry_Tello_Erika_IEFI
{
    public partial class frmBienvenida : Form
    {
        public string PerfilSeleccionado { get; private set; } = "";
        public frmBienvenida()
        {
            InitializeComponent();
        }

        private void frmBienvenida_Load(object sender, EventArgs e)
        {

        }

        private void pbAdmin_Click(object sender, EventArgs e)
        {
            PerfilSeleccionado = "Admin";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void pbUsuario_Click(object sender, EventArgs e)
        {
            PerfilSeleccionado = "Empleado";
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AbrirLogin(string perfil)
        {
            frmInicioSesion loginForm = new frmInicioSesion(perfil);
            loginForm.ShowDialog();
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

       

        private void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            string cadenaConexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\SistemaEmpleados.mdb";

            try
            {
                using (OleDbConnection conn = new OleDbConnection(cadenaConexion))
                {
                    conn.Open();
                    string query = "SELECT Usuario, Clave FROM Usuarios";
                    OleDbCommand cmd = new OleDbCommand(query, conn);

                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        string salida = "Usuarios registrados:\n\n";

                        while (reader.Read())
                        {
                            string usuario = reader["Usuario"].ToString();
                            string clave = reader["Clave"].ToString();
                            salida += $"Usuario: {usuario} - Clave: {clave}\n";
                        }

                        MessageBox.Show(salida, "Lista de Usuarios");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer usuarios: " + ex.Message);
            }
        }
    }

}




