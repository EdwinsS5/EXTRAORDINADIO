using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace GYMEXTRAOR
{
    public partial class FormMiembros : Form
    {
        private string connectionString = "Server=DESKTOP-P5L5BPG\\SQLEXPRESS01;Database=GYM;Integrated Security=True; TrustServerCertificate=True; ";

        public FormMiembros()
        {
            InitializeComponent();
        }


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text))
            {
                MessageBox.Show("Por favor, complete todos los campos requeridos.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Miembros (Nombre, Apellido, FechaNacimiento, Email, Telefono) VALUES (@Nombre, @Apellido, @FechaNacimiento, @Email, @Telefono)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                command.Parameters.AddWithValue("@Apellido", txtApellido.Text);
                command.Parameters.AddWithValue("@FechaNacimiento", dtpFechaNacimiento.Value);
                command.Parameters.AddWithValue("@Email", txtEmail.Text);
                command.Parameters.AddWithValue("@Telefono", txtTelefono.Text);

                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Miembro agregado exitosamente.");
                CargarMiembros();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Miembros WHERE Nombre LIKE '%' + @Nombre + '%'";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nombre", txtBuscar.Text);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dgvMiembros.DataSource = table;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMiembros.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("¿Está seguro de que desea eliminar este miembro?", "Confirmación", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dgvMiembros.SelectedRows[0].Cells["ID_Miembro"].Value);
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "DELETE FROM Miembros WHERE ID_Miembro = @ID";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@ID", id);

                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Miembro eliminado exitosamente.");
                        CargarMiembros();
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un miembro para eliminar.");
            }
        }

        private void CargarMiembros()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Miembros";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);

                dgvMiembros.DataSource = table;
            }
        }

        private void FormMiembros_Load(object sender, EventArgs e)
        {
            CargarMiembros();
        }
    }
}
