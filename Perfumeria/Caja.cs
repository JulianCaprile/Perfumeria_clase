using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Perfumeria
{
    public partial class Caja : Form
    {
        // Cadena de conexión a tu base de datos
        private string connectionString = "server=localhost;database=perfumeria;user=root;password=;";

        public Caja() 
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
        }

        private void Caja_Load(object sender, EventArgs e) //mostramos  el resumen llamando a la clase cargar resuemn
        {
            CargarResumenCaja();
        }

        private void CargarResumenCaja() 
        {
            double entrada = ObtenerTotal("compras");
            double salida = ObtenerTotal("ventas");
            double total = entrada - salida; //restamos lo que compramos - lo que vendimos

            // Crear tabla temporal para mostrar en el DGV 
            // para eso creamos el objeto data table con sus atributos
            DataTable dt = new DataTable();
            dt.Columns.Add("Entrada", typeof(double)); //añadimos las columnas
            dt.Columns.Add("Salida", typeof(double));
            dt.Columns.Add("Total", typeof(double));

            dt.Rows.Add(entrada, salida, total);

            dgvCaja.DataSource = dt;
        }

        private double ObtenerTotal(string tabla)
        {
            double total = 0;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = $"SELECT SUM(precio) FROM {tabla}";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                        total = Convert.ToDouble(result);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al obtener total de {tabla}: " + ex.Message);
                }
            }

            return total;
        }
    }
}
