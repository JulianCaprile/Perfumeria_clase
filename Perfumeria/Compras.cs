using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Perfumeria
{
    public partial class FormCompras : Form
    {
        //llamos a la base de datos
        private string connectionString = "server=localhost;database=perfumeria;user=root;password=;";
        private int idSeleccionado = 0;
        //inicializamos el id
        public FormCompras() //clase compras
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill; 
        }

        // Al cargar el formulario, mostramos los productos en la dtg
        //generamos evento de carga

        private void FormCompras_Load(object sender, EventArgs e)
        {
            CargarCompras();
        }

        private void CargarCompras()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try //intentar
                {
                    conn.Open();
                    string query = "SELECT id, articulo, descripcion, cantidad, precio FROM compras";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable(); // es como una tabla en memoria que guarda los resultados.
                    adapter.Fill(dt); // llena esa tabla con los datos obtenidos.
                    dgvCompras.DataSource = dt; //Asigna los datos de la tabla (dt) como origen de datos del DataGridView llamado dgvProductos.
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("Error al cargar compras: " + ex.Message);
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


        //muestra todos los datos del cuadrito del data grid
        private void dgvCompras_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCompras.Rows[e.RowIndex];
                idSeleccionado = Convert.ToInt32(row.Cells["id"].Value);
                txtArticulo.Text = row.Cells["articulo"].Value.ToString();
                txtDescripcion.Text = row.Cells["descripcion"].Value.ToString();
                txtCantidad.Text = row.Cells["cantidad"].Value.ToString();
                txtPrecio.Text = row.Cells["precio"].Value.ToString();
            }
        }


        //btn agregar
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO compras (articulo, descripcion, cantidad, precio) VALUES (@articulo, @descripcion, @cantidad, @precio)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@articulo", txtArticulo.Text);
                    cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@cantidad", Convert.ToInt32(txtCantidad.Text));
                    cmd.Parameters.AddWithValue("@precio", Convert.ToInt32(txtPrecio.Text));

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Compra agregada correctamente.");
                    CargarCompras();     // Refresca la tabla
                    LimpiarCampos();     // Limpia los TextBox
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar la compra: " + ex.Message);
            }
        }

        //btn modificar


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
                    string query = "UPDATE compras SET descripcion = @descripcion, cantidad = @cantidad, precio = @precio WHERE articulo = @articulo";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@cantidad", Convert.ToInt32(txtCantidad.Text));
                    cmd.Parameters.AddWithValue("@precio", Convert.ToInt32(txtPrecio.Text));
                    cmd.Parameters.AddWithValue("@articulo", idSeleccionado);

                    int filas = cmd.ExecuteNonQuery();

                    if (filas > 0)
                        MessageBox.Show("Compra modificada correctamente.");
                    else
                        MessageBox.Show("No se modifico ninguna fila.");

                    CargarCompras();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar la compra: " + ex.Message);
            }
        }

        // boton eliminar click

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Selecciona una compra para eliminar.");
                return;
            }

            var confirmar = MessageBox.Show("¿Estas seguro que queres eliminar esta compra?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirmar == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM compras WHERE articulo = @articulo";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@articulo", idSeleccionado);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Compra eliminada correctamente.");
                        CargarCompras();
                        LimpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la compra: " + ex.Message);
                }
            }
        }

        //buscamos por id

        private void btnBuscarPorId_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarId.Text))
            {
                MessageBox.Show("Ingresa un ID para buscar.");
                return;
            }

            int idBuscado;
            if (!int.TryParse(txtBuscarId.Text, out idBuscado))
            {
                MessageBox.Show("El ID debe ser un numero.");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM compras WHERE articulo = @articulo";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@articulo", idBuscado);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Guardamos el ID buscado
                        idSeleccionado = idBuscado;

                        // Mostramos los datos
                        txtArticulo.Text = reader["articulo"].ToString();
                        txtDescripcion.Text = reader["descripcion"].ToString();
                        txtCantidad.Text = reader["cantidad"].ToString();
                        txtPrecio.Text = reader["precio"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("No se encontro la compra.");
                        LimpiarCampos();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar la compra: " + ex.Message);
                }
            }
        }




    }
}
