 using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient; // conectamos a sql

namespace Perfumeria
{
    // Cadena de conexion a la base de datos
    public partial class FormProductos : Form
    {
        private string connectionString = "server=localhost;database=perfumeria;user=root;password=;";

        // Guarda el ID del producto seleccionado

        private int idSeleccionado = 0;


        public FormProductos()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None; // Sin bordes
            this.Dock = DockStyle.Fill; // Que ocupe todo el formulario padre

        }
        // Al cargar el formulario, mostramos los productos en la dtg
        //generamos evento de carga
        private void FormProductos_Load(object sender, EventArgs e)
        {
            //fncion que se conecta a la base de datos 
            CargarProductos();
        }
       
        // Obtiene todos los productos desde la base de datos y los muestra en el DataGridView

        private void CargarProductos()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString)) //asegura que la funcion cierre correctamente aunque haya erores
            {
                try
                {
                    conn.Open(); //intentaoms abrir la conexion
                    string query = "SELECT id, nombre, descripcion, precio, stock FROM productos"; //selecciona todas las columnas
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);  //ejecuta la consulta.
                    DataTable dt = new DataTable();  // es como una tabla en memoria que guarda los resultados.
                    adapter.Fill(dt); // llena esa tabla con los datos obtenidos.
                    dgvProductos.DataSource = dt; //Asigna los datos de la tabla (dt) como origen de datos del DataGridView llamado dgvProductos.


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar productos: " + ex.Message);
                }
            }
        }


        //AGREGAR PRODUCTO


        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO productos (nombre, descripcion, precio, stock) VALUES (@nombre, @descripcion, @precio, @stock)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                    cmd.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
                    cmd.Parameters.AddWithValue("@precio", Convert.ToDecimal(txtPrecio.Text));
                    cmd.Parameters.AddWithValue("@stock", Convert.ToInt32(txtStock.Text));
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Producto agregado.");
                    CargarProductos();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar el producto: " + ex.Message);
            }
        }


        //MODIFICAR PRODUCTOS 


        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un producto para modificar.");
                return;
            }

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE productos SET nombre = @nombre, descripcion = @descripcion, precio = @precio, stock = @stock WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Tomamos los valores actuales
                    string nombre = txtNombre.Text;
                    string descripcion = txtDescripcion.Text;
                    decimal precio = Convert.ToDecimal(txtPrecio.Text);
                    int stock = Convert.ToInt32(txtStock.Text);

                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@descripcion", descripcion);
                    cmd.Parameters.AddWithValue("@precio", precio);
                    cmd.Parameters.AddWithValue("@stock", stock);
                    cmd.Parameters.AddWithValue("@id", idSeleccionado);

                    //prueba de verificacion que la consulta se hizo bien
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Filas afectadas: {filasAfectadas}");

                    if (filasAfectadas > 0)
                    {
                        MessageBox.Show(" Producto modificado correctamente.");
                    }
                    else
                    {
                        MessageBox.Show(" No se modifico ningun producto. ¿Los datos eran iguales?");
                    }

                    CargarProductos();
                    LimpiarCampos();

                    txtBuscarId.Clear();
                    idSeleccionado = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar el producto: " + ex.Message);
            }
        }




        //ELIMINAR PRODCTOS


        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (idSeleccionado == 0)
            {
                MessageBox.Show("Seleccione un producto para eliminar.");
                return;
            }

            var confirmar = MessageBox.Show("¿Estás seguro que queres eliminar el producto?", "Confirmar", MessageBoxButtons.YesNo);
            if (confirmar == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM productos WHERE id = @id";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", idSeleccionado);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Producto eliminado.");
                        CargarProductos();
                        LimpiarCampos();
                        txtBuscarId.Clear();
                        idSeleccionado = 0;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el producto: " + ex.Message);
                }
            }
        }


        // DATA GRID PRODCUTOS

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvProductos.Rows[e.RowIndex];
                idSeleccionado = Convert.ToInt32(row.Cells["id"].Value);
                txtNombre.Text = row.Cells["nombre"].Value.ToString();
                txtDescripcion.Text = row.Cells["descripcion"].Value.ToString();
                txtPrecio.Text = row.Cells["precio"].Value.ToString();
                txtStock.Text = row.Cells["stock"].Value.ToString();
            }
        }
        // Limpia todos los campos del formulario

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStock.Clear();

        }

        private void FormProductos_Load_1(object sender, EventArgs e)
        {
            CargarProductos();
        }


        // buscamos por id
        private void btnBuscarPorId_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarId.Text))
            {
                MessageBox.Show("Ingrese un ID para buscar.");
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
                    string query = "SELECT * FROM productos WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", idBuscado);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Rellenar los campos del formulario
                        idSeleccionado = idBuscado;
                        txtNombre.Text = reader["nombre"].ToString();
                        txtDescripcion.Text = reader["descripcion"].ToString();
                        txtPrecio.Text = reader["precio"].ToString();
                        txtStock.Text = reader["stock"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Producto no encontrado.");
                        LimpiarCampos();
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al buscar el producto: " + ex.Message);
                }
            }

            MessageBox.Show("ID encontrado y cargado: " + idSeleccionado);
        }


    }
}
