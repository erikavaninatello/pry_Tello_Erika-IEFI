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
    public partial class frmReporte : Form
    {
        private clsUsuarioDAO usuarioDAO = new clsUsuarioDAO();
        public frmReporte()
        {
            InitializeComponent();
        }

        private void frmReporte_Load(object sender, EventArgs e)
        {
            var listaUsuarios = usuarioDAO.ObtenerUsuarios();

            dgvReporte.DataSource = listaUsuarios;

            if (dgvReporte.Columns.Contains("Id")) dgvReporte.Columns["Id"].Visible = false;
            if (dgvReporte.Columns.Contains("Usuario")) dgvReporte.Columns["Usuario"].HeaderText = "Usuario";
            if (dgvReporte.Columns.Contains("NombreCompleto")) dgvReporte.Columns["NombreCompleto"].HeaderText = "Nombre";
            if (dgvReporte.Columns.Contains("Tarea")) dgvReporte.Columns["Tarea"].HeaderText = "Tarea";
            if (dgvReporte.Columns.Contains("Fecha")) dgvReporte.Columns["Fecha"].HeaderText = "Fecha";
            if (dgvReporte.Columns.Contains("HorarioInicio")) dgvReporte.Columns["HorarioInicio"].HeaderText = "Hora Inicio";
            if (dgvReporte.Columns.Contains("HorarioFin")) dgvReporte.Columns["HorarioFin"].HeaderText = "Hora Fin";
        }
    }
}



