using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace pry_Tello_Erika_IEFI
{
    public partial class frmRegristroTareas : Form
    {
       

        public frmRegristroTareas()
        {
            InitializeComponent();
        }

        private void frmRegristroTareas_Load(object sender, EventArgs e)
        {
            cmbTareas.Items.AddRange(new string[] { "Inspección", "Reclamos", "Visitas" });
            cmbLugar.Items.AddRange(new string[] { "Servicio", "Empresa", "Oficina" });

            string usuarioRol = clsSesionActual.Rol;

            if (usuarioRol != "admin")
            {
               
                btnAgregarNuevaTarea.Visible = false;
                btnAgregarNuevoLugar.Visible = false;
            }
            else
            {
                
                btnAgregarNuevaTarea.Visible = true;
                btnAgregarNuevoLugar.Visible = true;
            }

         
            cbInsumo.Visible = true;
            cbEstudio.Visible = true;
            cbVacacion.Visible = true;
            cbRecibo.Visible = true;
            cbSalario.Visible = true;
            txtComentario.Visible = true;

            dgvTareas.Columns.Add("Fecha", "Fecha");
            dgvTareas.Columns.Add("Tarea", "Tarea");
            dgvTareas.Columns.Add("Lugar", "Lugar");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cmbTareas.SelectedItem == null || cmbLugar.SelectedItem == null)
            {
                MessageBox.Show("Debe seleccionar una tarea y un lugar.");
                return;
            }

          
            string fecha = dtpFecha.Value.ToShortDateString();
            string tarea = cmbTareas.SelectedItem.ToString();
            string lugar = cmbLugar.SelectedItem.ToString();

            dgvTareas.Rows.Add(fecha, tarea, lugar);
        }

        private void bntAgregarNuevaTarea_Click(object sender, EventArgs e)
        {
            string nuevaTarea = Microsoft.VisualBasic.Interaction.InputBox("Ingrese la nueva tarea:", "Agregar tarea");

            if (!string.IsNullOrWhiteSpace(nuevaTarea) && !cmbTareas.Items.Contains(nuevaTarea))
            {
                cmbTareas.Items.Add(nuevaTarea);
                MessageBox.Show("Tarea agregada correctamente.");
            }
            else
            {
                MessageBox.Show("La tarea ya existe o no se ingresó un valor válido.");
            }
        }

        private void btnAgregarNuevoLugar_Click(object sender, EventArgs e)
        {
            string nuevoLugar = Microsoft.VisualBasic.Interaction.InputBox("Ingrese el nuevo lugar:", "Agregar lugar");

            if (!string.IsNullOrWhiteSpace(nuevoLugar) && !cmbLugar.Items.Contains(nuevoLugar))
            {
                cmbLugar.Items.Add(nuevoLugar);
                MessageBox.Show("Lugar agregado correctamente.");
            }
            else
            {
                MessageBox.Show("El lugar ya existe o no se ingresó un valor válido.");
            }
        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            List<string> detallesSeleccionados = new List<string>();

            if (cbInsumo.Checked) detallesSeleccionados.Add("Insumo");
            if (cbEstudio.Checked) detallesSeleccionados.Add("Estudio");
            if (cbVacacion.Checked) detallesSeleccionados.Add("Vacación");
            if (cbRecibo.Checked) detallesSeleccionados.Add("Recibo");
            if (cbSalario.Checked) detallesSeleccionados.Add("Salario");

            string comentario = txtComentario.Text.Trim(); 



            if (detallesSeleccionados.Count == 0 && string.IsNullOrWhiteSpace(comentario))
            {
                MessageBox.Show("Debe seleccionar al menos un detalle o ingresar un comentario.");
                return;
            }

        
            string detalles = string.Join(", ", detallesSeleccionados);
            string mensajeFinal = $"Detalles: {detalles}\nComentario: {comentario}";

            
            MessageBox.Show("Detalle agregado:\n" + mensajeFinal);

            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        public DataGridView ObtenerDgvTareas()
        {
            return dgvTareas;
        }
        public DataGridView DgvTareas => dgvTareas;

        public List<string> DetallesSeleccionados
        {
            get
            {
                List<string> detalles = new List<string>();
                if (cbInsumo.Checked) detalles.Add("Insumo");
                if (cbEstudio.Checked) detalles.Add("Estudio");
                if (cbVacacion.Checked) detalles.Add("Vacación");
                if (cbRecibo.Checked) detalles.Add("Recibo");
                if (cbSalario.Checked) detalles.Add("Salario");
                return detalles;
            }
        }

        public string Comentario => txtComentario.Text.Trim();

        private frmRegristroTareas formularioTareas;

    }
}
