namespace POSBasic.Utils
{
    partial class FrmBuscar
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
            panel1 = new Panel();
            label2 = new Label();
            label1 = new Label();
            txtBuscar = new TextBox();
            dgvResultado = new DataGridView();
            label8 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResultado).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtBuscar);
            panel1.Location = new Point(15, 12);
            panel1.Name = "panel1";
            panel1.Size = new Size(570, 85);
            panel1.TabIndex = 0;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.IndianRed;
            label2.Location = new Point(3, 60);
            label2.Name = "label2";
            label2.Size = new Size(352, 17);
            label2.TabIndex = 19;
            label2.Text = "OBS: Solo hace la busqueda por el campo DESCRIPCION";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 28);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 1;
            label1.Text = "BUSCAR";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(77, 25);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(472, 23);
            txtBuscar.TabIndex = 0;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
            // 
            // dgvResultado
            // 
            dgvResultado.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvResultado.Location = new Point(16, 103);
            dgvResultado.Name = "dgvResultado";
            dgvResultado.Size = new Size(569, 190);
            dgvResultado.TabIndex = 1;
            dgvResultado.CellDoubleClick += dgvResultado_CellDoubleClick;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.IndianRed;
            label8.Location = new Point(15, 306);
            label8.Name = "label8";
            label8.Size = new Size(561, 17);
            label8.TabIndex = 18;
            label8.Text = "OBS: Para recupera el Dato debemos Pulsar Doble Click con el Mouse en la Linea deseada";
            // 
            // FrmBuscar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(605, 347);
            Controls.Add(label8);
            Controls.Add(dgvResultado);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmBuscar";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmBuscar";
            Load += FrmBuscar_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvResultado).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private TextBox txtBuscar;
        private DataGridView dgvResultado;
        private Label label1;
        private Label label8;
        private Label label2;
    }
}