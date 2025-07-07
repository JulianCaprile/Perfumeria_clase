namespace Perfumeria
{
    partial class Menu
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_productos = new System.Windows.Forms.Button();
            this.btn_Compras = new System.Windows.Forms.Button();
            this.btn_Caja = new System.Windows.Forms.Button();
            this.btn_Ventas = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.logo = new System.Windows.Forms.Label();
            this.panelLogo.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_productos
            // 
            this.btn_productos.AutoSize = true;
            this.btn_productos.BackColor = System.Drawing.Color.Transparent;
            this.btn_productos.FlatAppearance.BorderSize = 0;
            this.btn_productos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_productos.Font = new System.Drawing.Font("Irish Grover", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_productos.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_productos.Location = new System.Drawing.Point(105, 48);
            this.btn_productos.Name = "btn_productos";
            this.btn_productos.Size = new System.Drawing.Size(144, 31);
            this.btn_productos.TabIndex = 0;
            this.btn_productos.Text = "PRODUCTOS";
            this.btn_productos.UseVisualStyleBackColor = false;
            this.btn_productos.Click += new System.EventHandler(this.btn_productos_Click);
            // 
            // btn_Compras
            // 
            this.btn_Compras.AutoSize = true;
            this.btn_Compras.BackColor = System.Drawing.Color.Transparent;
            this.btn_Compras.FlatAppearance.BorderSize = 0;
            this.btn_Compras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Compras.Font = new System.Drawing.Font("Irish Grover", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Compras.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Compras.Location = new System.Drawing.Point(244, 48);
            this.btn_Compras.Name = "btn_Compras";
            this.btn_Compras.Size = new System.Drawing.Size(144, 31);
            this.btn_Compras.TabIndex = 1;
            this.btn_Compras.Text = "COMPRAS";
            this.btn_Compras.UseVisualStyleBackColor = false;
            this.btn_Compras.Click += new System.EventHandler(this.btn_Compras_Click);
            // 
            // btn_Caja
            // 
            this.btn_Caja.AutoSize = true;
            this.btn_Caja.BackColor = System.Drawing.Color.Transparent;
            this.btn_Caja.FlatAppearance.BorderSize = 0;
            this.btn_Caja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Caja.Font = new System.Drawing.Font("Irish Grover", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Caja.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Caja.Location = new System.Drawing.Point(544, 48);
            this.btn_Caja.Name = "btn_Caja";
            this.btn_Caja.Size = new System.Drawing.Size(144, 31);
            this.btn_Caja.TabIndex = 2;
            this.btn_Caja.Text = "CAJA";
            this.btn_Caja.UseVisualStyleBackColor = false;
            this.btn_Caja.Click += new System.EventHandler(this.btn_Caja_Click);
            // 
            // btn_Ventas
            // 
            this.btn_Ventas.AutoSize = true;
            this.btn_Ventas.BackColor = System.Drawing.Color.Transparent;
            this.btn_Ventas.FlatAppearance.BorderSize = 0;
            this.btn_Ventas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Ventas.Font = new System.Drawing.Font("Irish Grover", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Ventas.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_Ventas.Location = new System.Drawing.Point(394, 48);
            this.btn_Ventas.Name = "btn_Ventas";
            this.btn_Ventas.Size = new System.Drawing.Size(144, 31);
            this.btn_Ventas.TabIndex = 3;
            this.btn_Ventas.Text = "VENTAS";
            this.btn_Ventas.UseVisualStyleBackColor = false;
            this.btn_Ventas.Click += new System.EventHandler(this.btn_Ventas_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.logo);
            this.panelLogo.Location = new System.Drawing.Point(244, 182);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(317, 113);
            this.panelLogo.TabIndex = 6;
            // 
            // logo
            // 
            this.logo.AutoSize = true;
            this.logo.Font = new System.Drawing.Font("Irish Grover", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logo.Location = new System.Drawing.Point(0, 0);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(317, 113);
            this.logo.TabIndex = 8;
            this.logo.Text = "PARIS";
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.BackgroundImage = global::Perfumeria.Properties.Resources.Imagen_editada;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_Ventas);
            this.Controls.Add(this.btn_Caja);
            this.Controls.Add(this.btn_Compras);
            this.Controls.Add(this.btn_productos);
            this.Controls.Add(this.panelLogo);
            this.Name = "Menu";
            this.Text = "Form1";
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btn_productos;
        public System.Windows.Forms.Button btn_Compras;
        public System.Windows.Forms.Button btn_Caja;
        public System.Windows.Forms.Button btn_Ventas;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label logo;
    }
}

