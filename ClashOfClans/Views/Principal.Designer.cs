namespace ClashOfClans
{
    partial class Principal
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BuscarClanButton = new Button();
            MembrosDataGridView = new DataGridView();
            MembroColumn = new DataGridViewTextBoxColumn();
            ListarMembrosButton = new Button();
            ((System.ComponentModel.ISupportInitialize)MembrosDataGridView).BeginInit();
            SuspendLayout();
            // 
            // BuscarClanButton
            // 
            BuscarClanButton.Location = new Point(86, 110);
            BuscarClanButton.Name = "BuscarClanButton";
            BuscarClanButton.Size = new Size(75, 23);
            BuscarClanButton.TabIndex = 0;
            BuscarClanButton.Text = "button1";
            BuscarClanButton.UseVisualStyleBackColor = true;
            BuscarClanButton.Click += BuscarClanButton_Click;
            // 
            // MembrosDataGridView
            // 
            MembrosDataGridView.AllowUserToAddRows = false;
            MembrosDataGridView.AllowUserToDeleteRows = false;
            MembrosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MembrosDataGridView.Columns.AddRange(new DataGridViewColumn[] { MembroColumn });
            MembrosDataGridView.Location = new Point(100, 246);
            MembrosDataGridView.Name = "MembrosDataGridView";
            MembrosDataGridView.ReadOnly = true;
            MembrosDataGridView.Size = new Size(468, 150);
            MembrosDataGridView.TabIndex = 1;
            // 
            // MembroColumn
            // 
            MembroColumn.DataPropertyName = "Name";
            MembroColumn.HeaderText = "Membro";
            MembroColumn.Name = "MembroColumn";
            MembroColumn.ReadOnly = true;
            // 
            // ListarMembrosButton
            // 
            ListarMembrosButton.Location = new Point(183, 110);
            ListarMembrosButton.Name = "ListarMembrosButton";
            ListarMembrosButton.Size = new Size(167, 23);
            ListarMembrosButton.TabIndex = 2;
            ListarMembrosButton.Text = "Listar Membros";
            ListarMembrosButton.UseVisualStyleBackColor = true;
            ListarMembrosButton.Click += ListarMembrosButton_Click;
            // 
            // Principal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(677, 487);
            Controls.Add(ListarMembrosButton);
            Controls.Add(MembrosDataGridView);
            Controls.Add(BuscarClanButton);
            Name = "Principal";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)MembrosDataGridView).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button BuscarClanButton;
        private DataGridView MembrosDataGridView;
        private Button ListarMembrosButton;
        private DataGridViewTextBoxColumn MembroColumn;
    }
}
