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
    public partial class frmPrincipal : Form
    {
        private int usuarioId;
        private string perfilSeleccionado;
        private Timer timer = new Timer();

        public frmPrincipal(int idUsuario, string perfilSeleccionado)
        {
            

            InitializeComponent();

            this.usuarioId = idUsuario;
            this.perfilSeleccionado = perfilSeleccionado;

            this.FormClosing += frmPrincipal_FormClosing;
            this.Load += frmPrincipal_Load;
            timer.Tick += timer_Tick;


            if (perfilSeleccionado == "Admin")
            {
            
             
            }
            else if (perfilSeleccionado == "Empleado")
            {
                
         
            }
        }
        
        private void timer_Tick(object sender, EventArgs e)
        {
            lblHoraIngreso.Text = $"Hora: {DateTime.Now.ToShortTimeString()}";
        }

        private void pbUsuario_Click(object sender, EventArgs e)
        {
            frmAdministrarUsuarios v = new frmAdministrarUsuarios();
            v.ShowDialog();
        }


        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
          
        }
        

        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            clsAuditoria auditoria = new clsAuditoria
            {
                IdUsuario = usuarioId,
                FechaIngreso = DateTime.Now.Date,
                HoraSalida = DateTime.Now.TimeOfDay
            };

            clsUsuarioDAO usuarioDAO = new clsUsuarioDAO();
            usuarioDAO.RegistrarSalidaSesion(auditoria);

            RegistrarSalida();
        }
      

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            timer.Interval = 1000; 
            timer.Start();

            clsUsuarioDAO daoUsuario = new clsUsuarioDAO();
            var usuario = daoUsuario.ObtenerUsuarios().FirstOrDefault(u => u.Id == usuarioId);

            if (usuario != null)
            {
                lblUsuario.Text = $"Usuario: {usuario.Usuario}";
                lblRol.Text = $"Rol: {(usuario.IdRol == 1 ? "Admin" : "Empleado")}";
                lblFechaIngreso.Text = $"Fecha: {DateTime.Now.ToShortDateString()}";
                lblHoraIngreso.Text = $"Hora: {DateTime.Now.ToShortTimeString()}";

                clsAuditoria sesion = new clsAuditoria
                {
                    IdUsuario = usuarioId,
                    FechaIngreso = DateTime.Now.Date,
                    HoraIngreso = DateTime.Now.TimeOfDay,
                    HoraSalida = null // La salida aún no se ha registrado
                };

                clsUsuarioDAO auditoriaDAO = new clsUsuarioDAO();
                auditoriaDAO.RegistrarInicioSesion(sesion);
            }
        }
        private void RegistrarSalida()
        {
            clsAuditoria auditoria = new clsAuditoria
            {
                IdUsuario = usuarioId,
                FechaIngreso = DateTime.Now.Date,
                HoraSalida = DateTime.Now.TimeOfDay
            };

            new clsUsuarioDAO().RegistrarSalidaSesion(auditoria);
        }
    }
    
}






