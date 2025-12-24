namespace ClashOfClans
{
    partial class ResumoDuasUltimasGuerrasView
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
            NomeColumn = new DataGridViewTextBoxColumn();
            GuerrasParticipadasSeqColumn = new DataGridViewTextBoxColumn();
            QuantidadeAtaquesColumn = new DataGridViewTextBoxColumn();
            TagColumn = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)MembrosDataGridView).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // BuscarClanButton
            // 
            BuscarClanButton.Location = new Point(36, 33);
            BuscarClanButton.Name = "BuscarClanButton";
            BuscarClanButton.Size = new Size(203, 23);
            BuscarClanButton.TabIndex = 0;
            BuscarClanButton.Text = "Listar membros";
            BuscarClanButton.UseVisualStyleBackColor = true;
            // 
            // MembrosDataGridView
            // 
            MembrosDataGridView.AllowUserToAddRows = false;
            MembrosDataGridView.AllowUserToDeleteRows = false;
            MembrosDataGridView.AllowUserToOrderColumns = true;
            MembrosDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            MembrosDataGridView.Columns.AddRange(new DataGridViewColumn[] { NomeColumn, GuerrasParticipadasSeqColumn, QuantidadeAtaquesColumn, TagColumn });
            MembrosDataGridView.Dock = DockStyle.Fill;
            MembrosDataGridView.Location = new Point(0, 0);
            MembrosDataGridView.Name = "MembrosDataGridView";
            MembrosDataGridView.ReadOnly = true;
            MembrosDataGridView.Size = new Size(677, 406);
            MembrosDataGridView.TabIndex = 1;
            // 
            // NomeColumn
            // 
            NomeColumn.DataPropertyName = "Nome";
            NomeColumn.HeaderText = "Nome";
            NomeColumn.Name = "NomeColumn";
            NomeColumn.ReadOnly = true;
            // 
            // GuerrasParticipadasSeqColumn
            // 
            GuerrasParticipadasSeqColumn.DataPropertyName = "GuerrasParticipadasSeq";
            GuerrasParticipadasSeqColumn.FillWeight = 150F;
            GuerrasParticipadasSeqColumn.HeaderText = "Sequencia de guerras";
            GuerrasParticipadasSeqColumn.Name = "GuerrasParticipadasSeqColumn";
            GuerrasParticipadasSeqColumn.ReadOnly = true;
            GuerrasParticipadasSeqColumn.Width = 150;
            // 
            // QuantidadeAtaquesColumn
            // 
            QuantidadeAtaquesColumn.DataPropertyName = "QuantidadeAtaques";
            QuantidadeAtaquesColumn.HeaderText = "Quantidade de ataques";
            QuantidadeAtaquesColumn.Name = "QuantidadeAtaquesColumn";
            QuantidadeAtaquesColumn.ReadOnly = true;
            QuantidadeAtaquesColumn.Width = 200;
            // 
            // TagColumn
            // 
            TagColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            TagColumn.DataPropertyName = "Tag";
            TagColumn.HeaderText = "Tag";
            TagColumn.Name = "TagColumn";
            TagColumn.ReadOnly = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(BuscarClanButton);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(677, 81);
            panel1.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.Controls.Add(MembrosDataGridView);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 81);
            panel2.Name = "panel2";
            panel2.Size = new Size(677, 406);
            panel2.TabIndex = 4;
            // 
            // Principal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(677, 487);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "Principal";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)MembrosDataGridView).EndInit();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button BuscarClanButton;
        private DataGridView MembrosDataGridView;
        private Panel panel1;
        private Panel panel2;
        private DataGridViewTextBoxColumn NomeColumn;
        private DataGridViewTextBoxColumn GuerrasParticipadasSeqColumn;
        private DataGridViewTextBoxColumn QuantidadeAtaquesColumn;
        private DataGridViewTextBoxColumn TagColumn;
    }
}
