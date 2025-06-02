using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.Native.WinApi;

namespace pry_Tello_Erika_IEFI
{
    public partial class frmAdministrarUsuarios : Form
    {
        private clsUsuarioDAO usuarioDAO = new clsUsuarioDAO();
        private int idSeleccionado = -1;
        public frmAdministrarUsuarios()
        {
            InitializeComponent();
            CargarUsuarios();
            CargarRoles();
            this.dgvUsuarios.CellClick += new DataGridViewCellEventHandler(this.dgvUsuarios_CellClick);

        }
        private void CargarUsuarios()
        {
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = usuarioDAO.ObtenerUsuarios();

            dgvUsuarios.Columns["Id"].Visible = false;
            dgvUsuarios.Columns["Usuario"].Visible = true;
            dgvUsuarios.Columns["Clave"].Visible = true; 
            dgvUsuarios.Columns["NombreCompleto"].Visible = true;
            dgvUsuarios.Columns["IdRol"].Visible = true;
            dgvUsuarios.Columns["Tarea"].Visible = true;
            dgvUsuarios.Columns["Fecha"].Visible = true;
        }

        private void CargarRoles()
        {
            List<Rol> roles = new List<Rol>
                {
                   new Rol { Id = 1, Nombre = "Admin" },
                   new Rol { Id = 2, Nombre = "Usuario" }
                };

            cmbRol.DataSource = roles;
            cmbRol.DisplayMember = "Nombre";
            cmbRol.ValueMember = "Id";
            cmbRol.SelectedIndex = 0;
        }

        private void frmAdministrarUsuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {

            clsUsuario usuario = ObtenerDatosFormulario();
            usuarioDAO.AgregarUsuario(usuario);
            CargarUsuarios();
           

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == -1)
            {
                MessageBox.Show("Seleccioná un usuario.");
                return;
            }

            clsUsuario usuario = ObtenerDatosFormulario();
            usuario.Id = idSeleccionado;
            usuarioDAO.ModificarUsuario(usuario);
            CargarUsuarios();
           
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == -1)
            {
                MessageBox.Show("Seleccioná un usuario para eliminar.");
                return;
            }

            usuarioDAO.EliminarUsuario(idSeleccionado);
            CargarUsuarios();

        }


        private void dgvUsuarios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvUsuarios.Rows[e.RowIndex];
                idSeleccionado = Convert.ToInt32(fila.Cells["Id"].Value);

                txtUsuario.Text = fila.Cells["Usuario"].Value.ToString();
                txtClave.Text = fila.Cells["Clave"].Value.ToString(); 
                txtNombreCompleto.Text = fila.Cells["NombreCompleto"].Value.ToString();
                cmbRol.Text = fila.Cells["IdRol"].Value.ToString();
                txtTarea.Text = fila.Cells["Tarea"].Value.ToString();
                dtpFecha.Value = Convert.ToDateTime(fila.Cells["Fecha"].Value);
            }

        }

        private clsUsuario ObtenerDatosFormulario()
        {
            return new clsUsuario
            {
                Usuario = txtUsuario.Text,
                Clave = txtClave.Text,
                NombreCompleto = txtNombreCompleto.Text,
                IdRol = Convert.ToInt32(cmbRol.SelectedValue), 
                Tarea = txtTarea.Text,
                Fecha = dtpFecha.Value
            };
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string buscarTermino = txtUsuario.Text.Trim();

            if (string.IsNullOrEmpty(buscarTermino))
            {
                MessageBox.Show("Por favor ingrese un término de búsqueda.");
                return;
            }


            var usuariosFiltrados = usuarioDAO.ObtenerUsuarios()
                                               .Where(u => u.Usuario.Contains(buscarTermino))
                                               .ToList();


            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = usuariosFiltrados;
            dgvUsuarios.Columns["Id"].Visible = false;
        }
      
    }



}
