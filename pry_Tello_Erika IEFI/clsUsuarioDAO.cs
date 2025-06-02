using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pry_Tello_Erika_IEFI
{
    public class clsUsuarioDAO
    {
        private clsConexionBD conexionBD;

        public clsUsuarioDAO()
        {
            conexionBD = new clsConexionBD();
        }

        public bool ValidarUsuario(string nombreUsuario, string clave)
        {
            bool esValido = false;

            try
            {
                using (OleDbConnection conn = conexionBD.AbrirConexion())
                {
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Usuario = ? AND Clave = ?";
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("?", nombreUsuario);
                    cmd.Parameters.AddWithValue("?", clave); 

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    esValido = count > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}");
            }

            return esValido;
        }
        public int ObtenerIdUsuario(string nombreUsuario)
        {
            int idUsuario = 0;

            try
            {
                using (OleDbConnection conn = conexionBD.AbrirConexion())
                {
                    string query = "SELECT Id FROM Usuarios WHERE Usuario = ?";
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("?", nombreUsuario);

                    idUsuario = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener el ID del usuario: {ex.Message}");
            }

            return idUsuario;
        }

        public List<clsUsuario> ObtenerUsuarios()
        {
            List<clsUsuario> usuarios = new List<clsUsuario>();

            try
            {
                using (OleDbConnection conn = conexionBD.AbrirConexion())
                {
                    
                    string query = "SELECT Id, Usuario, Clave, IdRol, NombreCompleto, Tarea, Fecha FROM Usuarios";
                    OleDbCommand cmd = new OleDbCommand(query, conn);

                    OleDbDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        usuarios.Add(new clsUsuario
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Usuario = reader["Usuario"].ToString(),
                            Clave = reader["Clave"].ToString(), 
                            IdRol = Convert.ToInt32(reader["IdRol"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(), 
                            Tarea = reader["Tarea"].ToString(), 
                            Fecha = Convert.ToDateTime(reader["Fecha"]) 
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener los usuarios: {ex.Message}");
            }

            return usuarios;          
        }

        public void RegistrarInicioSesion(clsAuditoria auditoria)
        {
            try
            {
                using (OleDbConnection conn = conexionBD.AbrirConexion())
                {
                    string query = "INSERT INTO Auditoria (IdUsuario, FechaIngreso, HoraIngreso, HoraSalida) VALUES (?, ?, ?, ?)";
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("?", auditoria.IdUsuario);
                    cmd.Parameters.AddWithValue("?", auditoria.FechaIngreso);
                    cmd.Parameters.AddWithValue("?", auditoria.HoraIngreso);
                    cmd.Parameters.AddWithValue("?", (object)auditoria.HoraSalida ?? DBNull.Value); // Si la HoraSalida es null, se inserta DBNull

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar el inicio de sesión: {ex.Message}");
            }
        }

        public void RegistrarSalidaSesion(clsAuditoria auditoria)
        {
            try
            {
                using (OleDbConnection conn = conexionBD.AbrirConexion())
                {
                    string query = "UPDATE Auditoria SET HoraSalida = ? WHERE IdUsuario = ? AND FechaIngreso = ?";
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("?", auditoria.HoraSalida);
                    cmd.Parameters.AddWithValue("?", auditoria.IdUsuario);
                    cmd.Parameters.AddWithValue("?", auditoria.FechaIngreso);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar la salida de sesión: {ex.Message}");
            }
        }

        public void AgregarUsuario(clsUsuario usuario)
        {
            using (OleDbConnection conexion = conexionBD.AbrirConexion())
            {
                string query = "INSERT INTO Usuarios (Usuario, Clave, NombreCompleto, IdRol, Tarea, Fecha) VALUES (?, ?, ?, ?, ?, ?)";
                using (OleDbCommand comando = new OleDbCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("?", usuario.Usuario);
                    comando.Parameters.AddWithValue("?", usuario.Clave); 
                    comando.Parameters.AddWithValue("?", usuario.NombreCompleto);
                    comando.Parameters.AddWithValue("?", usuario.IdRol);
                    comando.Parameters.AddWithValue("?", usuario.Tarea);
                    comando.Parameters.AddWithValue("?", usuario.Fecha);
                    comando.ExecuteNonQuery();
                }
            }

        }

        public void ModificarUsuario(clsUsuario usuario)
        {
            using (OleDbConnection conexion = conexionBD.AbrirConexion())
            {
                string query = "UPDATE Usuarios SET Usuario=?, Clave=?, NombreCompleto=?, IdRol=?, Tarea=?, Fecha=? WHERE Id=?";
                using (OleDbCommand comando = new OleDbCommand(query, conexion))
                {
                    comando.Parameters.AddWithValue("?", usuario.Usuario);
                    comando.Parameters.AddWithValue("?", usuario.Clave); 
                    comando.Parameters.AddWithValue("?", usuario.NombreCompleto);
                    comando.Parameters.AddWithValue("?", usuario.IdRol);
                    comando.Parameters.AddWithValue("?", usuario.Tarea);
                    comando.Parameters.AddWithValue("?", usuario.Fecha);
                    comando.Parameters.AddWithValue("?", usuario.Id);
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void EliminarUsuario(int id)
        {
            try
            {
                using (OleDbConnection conn = conexionBD.AbrirConexion())
                {
                    string query = "DELETE FROM Usuarios WHERE Id = ?";
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("?", id);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar usuario: {ex.Message}");
            }
        }

    }
}



    




        

    








    




