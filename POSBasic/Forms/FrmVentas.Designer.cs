namespace POSBasic.Forms
{
    partial class FrmVentas
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtCodigoBarra = new TextBox();
            txtDescripcion = new TextBox();
            txtPrecioVenta = new TextBox();
            txtCantidad = new TextBox();
            dgvVentasDet = new DataGridView();
            Producto = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            panel1 = new Panel();
            lblCliente = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            txtCliente = new TextBox();
            txtNumVenta = new TextBox();
            txtFechaVenta = new TextBox();
            panel2 = new Panel();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            btnLimpiar = new Button();
            btnNuevo = new Button();
            btnEliminar = new Button();
            btnEditar = new Button();
            btnGuardar = new Button();
            txtTotalVenta = new TextBox();
            btnAgregarDetalle = new Button();
            label7 = new Label();
            label8 = new Label();
            btnEliminarDetalle = new Button();
            txtCodVenta = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvVentasDet).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // txtCodigoBarra
            // 
            txtCodigoBarra.Location = new Point(29, 199);
            txtCodigoBarra.Name = "txtCodigoBarra";
            txtCodigoBarra.Size = new Size(100, 23);
            txtCodigoBarra.TabIndex = 0;
            txtCodigoBarra.KeyDown += txtCodigoBarra_KeyDown;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(135, 199);
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(190, 23);
            txtDescripcion.TabIndex = 1;
            // 
            // txtPrecioVenta
            // 
            txtPrecioVenta.Location = new Point(331, 199);
            txtPrecioVenta.Name = "txtPrecioVenta";
            txtPrecioVenta.Size = new Size(85, 23);
            txtPrecioVenta.TabIndex = 2;
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(422, 199);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(85, 23);
            txtCantidad.TabIndex = 3;
            // 
            // dgvVentasDet
            // 
            dgvVentasDet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvVentasDet.Location = new Point(29, 241);
            dgvVentasDet.Name = "dgvVentasDet";
            dgvVentasDet.Size = new Size(734, 168);
            dgvVentasDet.TabIndex = 4;
            dgvVentasDet.CellClick += dgvVentasDet_CellClick;
            // 
            // Producto
            // 
            Producto.AutoSize = true;
            Producto.Location = new Point(29, 181);
            Producto.Name = "Producto";
            Producto.Size = new Size(56, 15);
            Producto.TabIndex = 5;
            Producto.Text = "Producto";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(135, 181);
            label1.Name = "label1";
            label1.Size = new Size(69, 15);
            label1.TabIndex = 6;
            label1.Text = "Descripcion";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(331, 181);
            label2.Name = "label2";
            label2.Size = new Size(53, 15);
            label2.TabIndex = 7;
            label2.Text = "Precio V.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(422, 181);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 8;
            label3.Text = "Cantidad";
            // 
            // panel1
            // 
            panel1.Controls.Add(lblCliente);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txtCliente);
            panel1.Controls.Add(txtNumVenta);
            panel1.Controls.Add(txtFechaVenta);
            panel1.Location = new Point(29, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(533, 149);
            panel1.TabIndex = 9;
            // 
            // lblCliente
            // 
            lblCliente.AutoSize = true;
            lblCliente.Location = new Point(222, 73);
            lblCliente.Name = "lblCliente";
            lblCliente.Size = new Size(0, 15);
            lblCliente.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(18, 70);
            label6.Name = "label6";
            label6.Size = new Size(44, 15);
            label6.TabIndex = 8;
            label6.Text = "Cliente";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(18, 40);
            label5.Name = "label5";
            label5.Size = new Size(66, 15);
            label5.TabIndex = 7;
            label5.Text = "Num Venta";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(18, 12);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 6;
            label4.Text = "Fecha Venta";
            // 
            // txtCliente
            // 
            txtCliente.Location = new Point(106, 67);
            txtCliente.Name = "txtCliente";
            txtCliente.Size = new Size(100, 23);
            txtCliente.TabIndex = 2;
            txtCliente.KeyDown += txtCliente_KeyDown;
            // 
            // txtNumVenta
            // 
            txtNumVenta.Location = new Point(106, 37);
            txtNumVenta.Name = "txtNumVenta";
            txtNumVenta.Size = new Size(100, 23);
            txtNumVenta.TabIndex = 1;
            // 
            // txtFechaVenta
            // 
            txtFechaVenta.Location = new Point(106, 8);
            txtFechaVenta.Name = "txtFechaVenta";
            txtFechaVenta.Size = new Size(100, 23);
            txtFechaVenta.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnAnterior);
            panel2.Controls.Add(btnSiguiente);
            panel2.Controls.Add(btnLimpiar);
            panel2.Controls.Add(btnNuevo);
            panel2.Controls.Add(btnEliminar);
            panel2.Controls.Add(btnEditar);
            panel2.Controls.Add(btnGuardar);
            panel2.Location = new Point(595, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(168, 152);
            panel2.TabIndex = 15;
            // 
            // btnAnterior
            // 
            btnAnterior.Location = new Point(90, 65);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(75, 23);
            btnAnterior.TabIndex = 14;
            btnAnterior.Text = "Anterior";
            btnAnterior.UseVisualStyleBackColor = true;
            btnAnterior.Click += btnAnterior_Click;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Location = new Point(90, 37);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(75, 23);
            btnSiguiente.TabIndex = 13;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.UseVisualStyleBackColor = true;
            btnSiguiente.Click += btnSiguiente_Click;
            // 
            // btnLimpiar
            // 
            btnLimpiar.Location = new Point(11, 121);
            btnLimpiar.Name = "btnLimpiar";
            btnLimpiar.Size = new Size(75, 23);
            btnLimpiar.TabIndex = 12;
            btnLimpiar.Text = "Limpiar";
            btnLimpiar.UseVisualStyleBackColor = true;
            btnLimpiar.Click += btnLimpiar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(11, 94);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(75, 23);
            btnNuevo.TabIndex = 11;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(11, 66);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(75, 23);
            btnEliminar.TabIndex = 10;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnEditar
            // 
            btnEditar.Location = new Point(11, 37);
            btnEditar.Name = "btnEditar";
            btnEditar.Size = new Size(75, 23);
            btnEditar.TabIndex = 9;
            btnEditar.Text = "Editar";
            btnEditar.UseVisualStyleBackColor = true;
            btnEditar.Click += btnEditar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(11, 8);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(75, 23);
            btnGuardar.TabIndex = 8;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // txtTotalVenta
            // 
            txtTotalVenta.Location = new Point(663, 426);
            txtTotalVenta.Name = "txtTotalVenta";
            txtTotalVenta.Size = new Size(100, 23);
            txtTotalVenta.TabIndex = 3;
            // 
            // btnAgregarDetalle
            // 
            btnAgregarDetalle.Location = new Point(513, 199);
            btnAgregarDetalle.Name = "btnAgregarDetalle";
            btnAgregarDetalle.Size = new Size(122, 23);
            btnAgregarDetalle.TabIndex = 16;
            btnAgregarDetalle.Text = "Agregar Detalle";
            btnAgregarDetalle.UseVisualStyleBackColor = true;
            btnAgregarDetalle.Click += btnAgregarDetalle_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(571, 429);
            label7.Name = "label7";
            label7.Size = new Size(64, 15);
            label7.TabIndex = 10;
            label7.Text = "Total Venta";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.IndianRed;
            label8.Location = new Point(12, 453);
            label8.Name = "label8";
            label8.Size = new Size(535, 21);
            label8.TabIndex = 17;
            label8.Text = "OBS: En los campos de Cliente y Producto pulsar F5 para buscar datos";
            // 
            // btnEliminarDetalle
            // 
            btnEliminarDetalle.Location = new Point(643, 199);
            btnEliminarDetalle.Name = "btnEliminarDetalle";
            btnEliminarDetalle.Size = new Size(120, 23);
            btnEliminarDetalle.TabIndex = 18;
            btnEliminarDetalle.Text = "Eliminar Detalle";
            btnEliminarDetalle.UseVisualStyleBackColor = true;
            btnEliminarDetalle.Click += btnEliminarDetalle_Click;
            // 
            // txtCodVenta
            // 
            txtCodVenta.Location = new Point(10, 164);
            txtCodVenta.Name = "txtCodVenta";
            txtCodVenta.Size = new Size(13, 23);
            txtCodVenta.TabIndex = 19;
            // 
            // FrmVentas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(787, 483);
            Controls.Add(txtCodVenta);
            Controls.Add(btnEliminarDetalle);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(btnAgregarDetalle);
            Controls.Add(txtTotalVenta);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Producto);
            Controls.Add(dgvVentasDet);
            Controls.Add(txtCantidad);
            Controls.Add(txtPrecioVenta);
            Controls.Add(txtDescripcion);
            Controls.Add(txtCodigoBarra);
            Name = "FrmVentas";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmVentas";
            Load += FrmVentas_Load;
            ((System.ComponentModel.ISupportInitialize)dgvVentasDet).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCodigoBarra;
        private TextBox txtDescripcion;
        private TextBox txtPrecioVenta;
        private TextBox txtCantidad;
        private DataGridView dgvVentasDet;
        private Label Producto;
        private Label label1;
        private Label label2;
        private Label label3;
        private Panel panel1;
        private Panel panel2;
        private Button btnAnterior;
        private Button btnSiguiente;
        private Button btnLimpiar;
        private Button btnNuevo;
        private Button btnEliminar;
        private Button btnEditar;
        private Button btnGuardar;
        private TextBox txtCliente;
        private TextBox txtNumVenta;
        private TextBox txtFechaVenta;
        private TextBox txtTotalVenta;
        private Label label4;
        private Label lblCliente;
        private Label label6;
        private Label label5;
        private Button btnAgregarDetalle;
        private Label label7;
        private Label label8;
        private Button btnEliminarDetalle;
        private TextBox txtCodVenta;
    }
}