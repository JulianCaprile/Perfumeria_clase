using System;
using System.Windows.Forms;

namespace Perfumeria
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }


        private void MostrarFormulario(Form formulario)
        {
            panelLogo.Visible = false; //ocultamos el logo cada vez que mostramos el formulario

            // Cierra formularios anteriores
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close();
            }

            // Configura e instancia
            formulario.MdiParent = this;
            formulario.Dock = DockStyle.Fill;
            formulario.FormBorderStyle = FormBorderStyle.None;

            // Cuando se cierra, vuelve el logo
            formulario.FormClosed += (s, args) => logo.Visible = true;

            formulario.Show();
        }

        private void btn_productos_Click(object sender, EventArgs e)
        {
            MostrarFormulario(new FormProductos());
        }

        private void btn_Compras_Click(object sender, EventArgs e)
        {
            MostrarFormulario(new FormCompras());
        }

        private void btn_Ventas_Click(object sender, EventArgs e)
        {
            MostrarFormulario(new Ventas()); // llamo a ventas
        }

        private void btn_Caja_Click(object sender, EventArgs e)
        {
            MostrarFormulario(new Caja()); 
        }
    }
}
