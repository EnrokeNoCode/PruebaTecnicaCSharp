namespace POSBasic.Forms
{
    partial class FrmClientes
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
            txtCodCliente = new TextBox();
            dgvClientes = new DataGridView();
            panel2 = new Panel();
            btnLimpiar = new Button();
            btnNuevo = new Button();
            btnEliminar = new Button();
            btnEditar = new Button();
            btnGuardar = new Button();
            panel1 = new Panel();
            txtNroDoc = new TextBox();
            txtNombre = new TextBox();
            txtApellido = new TextBox();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtCodCliente
            // 
            txtCodCliente.Location = new Point(467, 141);
            txtCodCliente.Name = "txtCodCliente";
            txtCodCliente.Size = new Size(17, 23);
            txtCodCliente.TabIndex = 16;
            // 
            // dgvClientes
            // 
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(12, 170);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.Size = new Size(472, 150);
            dgvClientes.TabIndex = 15;
            dgvClientes.CellClick += dgvProductos_CellClick;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnLimpiar);
            panel2.Controls.Add(btnNuevo);
            panel2.Controls.Add(btnEliminar);
            panel2.Controls.Add(btnEditar);
            panel2.Controls.Add(btnGuardar);
            panel2.Location = new Point(387, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(97, 152);
            panel2.TabIndex = 14;
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
            // panel1
            // 
            panel1.Controls.Add(txtNroDoc);
            panel1.Controls.Add(txtNombre);
            panel1.Controls.Add(txtApellido);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(12, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(369, 152);
            panel1.TabIndex = 13;
            // 
            // txtNroDoc
            // 
            txtNroDoc.Location = new Point(117, 13);
            txtNroDoc.Name = "txtNroDoc";
            txtNroDoc.Size = new Size(230, 23);
            txtNroDoc.TabIndex = 0;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(117, 51);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(230, 23);
            txtNombre.TabIndex = 1;
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(117, 91);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(230, 23);
            txtApellido.TabIndex = 2;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 94);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 6;
            label3.Text = "Apellido";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 54);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 5;
            label2.Text = "Nombre";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 16);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 4;
            label1.Text = "Nro. Documento";
            // 
            // FrmClientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(491, 327);
            Controls.Add(txtCodCliente);
            Controls.Add(dgvClientes);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "FrmClientes";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmClientes";
            Load += FrmClientes_Load;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtCodCliente;
        private DataGridView dgvClientes;
        private Panel panel2;
        private Button btnLimpiar;
        private Button btnNuevo;
        private Button btnEliminar;
        private Button btnEditar;
        private Button btnGuardar;
        private Panel panel1;
        private TextBox txtNroDoc;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}