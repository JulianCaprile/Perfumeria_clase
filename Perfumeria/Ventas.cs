using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Perfumeria
{
    public partial class Ventas : Form
    {
        private string connectionString = "server=localhost;database=perfumeria;user=root;password=;";
        private int idSeleccionado = 0;

        public Ventas()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            CargarVentas();
        }

        private void CargarVentas()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, articulo, descripcion, cantidad, precio FROM ventas";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dgvVentas.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar ventas: " + ex.Message);
                }
            }
        }

        private void LimpiarCampos()
        {
            txtArticulo.Clear();
            txtDescripcion.Clear();
            txtCantidad.Clear();
            txtPrecio.Clear();
            idSeleccionado = 0;
        }

        private void dgvVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvVentas.Rows[e.RowIndex];
                idSeleccionado = Convert.ToInt32(row.Cells["id"].Value);
                txtArticulo.Text = row.Cells["articulo"].Value.ToString();
                txtDescripcion.Text = row.Cells["descripcion"].Value.ToString();
                txtCantidad.Text = row.Cells["cantidad"].Value.ToString();
                txtPrecio.Text = row.Cells["precio"].Value.ToString();
            }
        }

                                        // ALTA

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO ventas (articulo, descripcion, cantidad, precio) VALUES (@articulo, @descripcion, @cantidad, @precio)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                   
                    cmd.Parameters.AddWithValue("@articulo", txtArticulo.Text);
                    cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@cantidad", Convert.ToInt32(txtCantidad.Text));
                    cmd.Parameters.AddWithValue("@precio", Convert.ToInt32(txtPrecio.Text));

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Compra agregada correctamente.");
                    CargarVentas();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la compra: " + ex.Message);
            }
        }

                                                //MODIFICACION

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Selecciona una compra para modificar.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE compras SET articulo = @articulo, descripcion = @descripcion, cantidad = @cantidad, precio = @precio WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@articulo", txtArticulo.Text);
                    cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@cantidad", Convert.ToInt32(txtCantidad.Text));
                    cmd.Parameters.AddWithValue("@precio", Convert.ToInt32(txtPrecio.Text));
                    cmd.Parameters.AddWithValue("@id", idSeleccionado);

                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                        MessageBox.Show("Compra modificada correctamente.");
                    else
                        MessageBox.Show("No se modifico ninguna fila.");

                    CargarVentas();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la compra: " + ex.Message);
            }
        }

                                            // ELIMINAR

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Selecciona una compra para eliminar.");
                return;
            }

            var confirmar = MessageBox.Show("¿Estás seguro que queres eliminar esta compra?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirmar == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM compras WHERE id = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", idSeleccionado);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Compra eliminada correctamente.");
                        CargarVentas();
                        LimpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la compra: " + ex.Message);
                }
            }
        }

                                                   // BUSCAR POR ID

        private void btnBuscarPorId_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarId.Text))
            {
                MessageBox.Show("Ingresa un ID para buscar.");
                return;
            }

            if (!int.TryParse(txtBuscarId.Text, out int idBuscado))
            {
                MessageBox.Show("El ID debe ser un numero.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM ventas WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", idBuscado);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        idSeleccionado = idBuscado;
                        txtArticulo.Text = reader["articulo"].ToString();
                        txtDescripcion.Text = reader["descripcion"].ToString();
                        txtCantidad.Text = reader["cantidad"].ToString();
                        txtPrecio.Text = reader["precio"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontro la venta.");
                        LimpiarCampos();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar la venta: " + ex.Message);
                }
            }
        }
    }
}
