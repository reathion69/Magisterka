namespace PlanZajecProsteUI
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.SelectInstanceComboBox = new System.Windows.Forms.ComboBox();
            this.ResolveButton = new System.Windows.Forms.Button();
            this.InstancesSource = new System.Windows.Forms.BindingSource(this.components);
            this.LoadToDatabaseButton = new System.Windows.Forms.Button();
            this.ConsoleRichTextBox = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ClassComboBox = new System.Windows.Forms.ComboBox();
            this.ClassSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.IterationTextBox = new System.Windows.Forms.TextBox();
            this.TabuDurationTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.NeighborhoodSizeTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.godzinaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.poniedziałekDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.wtorekDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.środaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.czwartekDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.piątekDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.planViewModelBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.planViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.InstancesSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClassSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.planViewModelBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.planViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectInstanceComboBox
            // 
            this.SelectInstanceComboBox.FormattingEnabled = true;
            this.SelectInstanceComboBox.Location = new System.Drawing.Point(13, 13);
            this.SelectInstanceComboBox.Name = "SelectInstanceComboBox";
            this.SelectInstanceComboBox.Size = new System.Drawing.Size(157, 21);
            this.SelectInstanceComboBox.TabIndex = 0;
            this.SelectInstanceComboBox.SelectedIndexChanged += new System.EventHandler(this.SelectInstanceComboBox_SelectedIndexChanged);
            // 
            // ResolveButton
            // 
            this.ResolveButton.Location = new System.Drawing.Point(239, 12);
            this.ResolveButton.Name = "ResolveButton";
            this.ResolveButton.Size = new System.Drawing.Size(174, 23);
            this.ResolveButton.TabIndex = 1;
            this.ResolveButton.Text = "Resolve";
            this.ResolveButton.UseVisualStyleBackColor = true;
            this.ResolveButton.Click += new System.EventHandler(this.ResolveButton_Click);
            // 
            // LoadToDatabaseButton
            // 
            this.LoadToDatabaseButton.Location = new System.Drawing.Point(477, 13);
            this.LoadToDatabaseButton.Name = "LoadToDatabaseButton";
            this.LoadToDatabaseButton.Size = new System.Drawing.Size(177, 23);
            this.LoadToDatabaseButton.TabIndex = 2;
            this.LoadToDatabaseButton.Text = "Load instance from file";
            this.LoadToDatabaseButton.UseVisualStyleBackColor = true;
            this.LoadToDatabaseButton.Click += new System.EventHandler(this.LoadToDatabaseButton_Click);
            // 
            // ConsoleRichTextBox
            // 
            this.ConsoleRichTextBox.Location = new System.Drawing.Point(12, 418);
            this.ConsoleRichTextBox.Name = "ConsoleRichTextBox";
            this.ConsoleRichTextBox.Size = new System.Drawing.Size(645, 96);
            this.ConsoleRichTextBox.TabIndex = 4;
            this.ConsoleRichTextBox.Text = "";
            this.ConsoleRichTextBox.TextChanged += new System.EventHandler(this.ConsoleRichTextBox_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.godzinaDataGridViewTextBoxColumn,
            this.poniedziałekDataGridViewTextBoxColumn,
            this.wtorekDataGridViewTextBoxColumn,
            this.środaDataGridViewTextBoxColumn,
            this.czwartekDataGridViewTextBoxColumn,
            this.piątekDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.planViewModelBindingSource1;
            this.dataGridView1.Location = new System.Drawing.Point(12, 95);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(645, 317);
            this.dataGridView1.TabIndex = 6;
            // 
            // ClassComboBox
            // 
            this.ClassComboBox.FormattingEnabled = true;
            this.ClassComboBox.Location = new System.Drawing.Point(13, 68);
            this.ClassComboBox.Name = "ClassComboBox";
            this.ClassComboBox.Size = new System.Drawing.Size(157, 21);
            this.ClassComboBox.TabIndex = 7;
            this.ClassComboBox.SelectedIndexChanged += new System.EventHandler(this.ClassComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Iterations";
            // 
            // IterationTextBox
            // 
            this.IterationTextBox.Location = new System.Drawing.Point(70, 42);
            this.IterationTextBox.Name = "IterationTextBox";
            this.IterationTextBox.Size = new System.Drawing.Size(100, 20);
            this.IterationTextBox.TabIndex = 10;
            this.IterationTextBox.Text = "1000";
            // 
            // TabuDurationTextBox
            // 
            this.TabuDurationTextBox.Location = new System.Drawing.Point(313, 42);
            this.TabuDurationTextBox.Name = "TabuDurationTextBox";
            this.TabuDurationTextBox.Size = new System.Drawing.Size(100, 20);
            this.TabuDurationTextBox.TabIndex = 12;
            this.TabuDurationTextBox.Text = "500";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(236, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Tabu duration";
            // 
            // NeighborhoodSizeTextBox
            // 
            this.NeighborhoodSizeTextBox.Location = new System.Drawing.Point(554, 42);
            this.NeighborhoodSizeTextBox.Name = "NeighborhoodSizeTextBox";
            this.NeighborhoodSizeTextBox.Size = new System.Drawing.Size(100, 20);
            this.NeighborhoodSizeTextBox.TabIndex = 14;
            this.NeighborhoodSizeTextBox.Text = "300";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(474, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Neighborhood";
            // 
            // godzinaDataGridViewTextBoxColumn
            // 
            this.godzinaDataGridViewTextBoxColumn.DataPropertyName = "Godzina";
            this.godzinaDataGridViewTextBoxColumn.HeaderText = "Godzina";
            this.godzinaDataGridViewTextBoxColumn.Name = "godzinaDataGridViewTextBoxColumn";
            // 
            // poniedziałekDataGridViewTextBoxColumn
            // 
            this.poniedziałekDataGridViewTextBoxColumn.DataPropertyName = "Poniedziałek";
            this.poniedziałekDataGridViewTextBoxColumn.HeaderText = "Poniedziałek";
            this.poniedziałekDataGridViewTextBoxColumn.Name = "poniedziałekDataGridViewTextBoxColumn";
            // 
            // wtorekDataGridViewTextBoxColumn
            // 
            this.wtorekDataGridViewTextBoxColumn.DataPropertyName = "Wtorek";
            this.wtorekDataGridViewTextBoxColumn.HeaderText = "Wtorek";
            this.wtorekDataGridViewTextBoxColumn.Name = "wtorekDataGridViewTextBoxColumn";
            // 
            // środaDataGridViewTextBoxColumn
            // 
            this.środaDataGridViewTextBoxColumn.DataPropertyName = "Środa";
            this.środaDataGridViewTextBoxColumn.HeaderText = "Środa";
            this.środaDataGridViewTextBoxColumn.Name = "środaDataGridViewTextBoxColumn";
            // 
            // czwartekDataGridViewTextBoxColumn
            // 
            this.czwartekDataGridViewTextBoxColumn.DataPropertyName = "Czwartek";
            this.czwartekDataGridViewTextBoxColumn.HeaderText = "Czwartek";
            this.czwartekDataGridViewTextBoxColumn.Name = "czwartekDataGridViewTextBoxColumn";
            // 
            // piątekDataGridViewTextBoxColumn
            // 
            this.piątekDataGridViewTextBoxColumn.DataPropertyName = "Piątek";
            this.piątekDataGridViewTextBoxColumn.HeaderText = "Piątek";
            this.piątekDataGridViewTextBoxColumn.Name = "piątekDataGridViewTextBoxColumn";
            // 
            // planViewModelBindingSource1
            // 
            this.planViewModelBindingSource1.DataSource = typeof(PlanZajecProsteUI.ViewModels.PlanViewModel);
            // 
            // planViewModelBindingSource
            // 
            this.planViewModelBindingSource.DataSource = typeof(PlanZajecProsteUI.ViewModels.PlanViewModel);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 526);
            this.Controls.Add(this.NeighborhoodSizeTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TabuDurationTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.IterationTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClassComboBox);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.ConsoleRichTextBox);
            this.Controls.Add(this.LoadToDatabaseButton);
            this.Controls.Add(this.ResolveButton);
            this.Controls.Add(this.SelectInstanceComboBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.InstancesSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ClassSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.planViewModelBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.planViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox SelectInstanceComboBox;
        private System.Windows.Forms.Button ResolveButton;
        private System.Windows.Forms.BindingSource InstancesSource;
        private System.Windows.Forms.Button LoadToDatabaseButton;
        private System.Windows.Forms.RichTextBox ConsoleRichTextBox;
        private System.Windows.Forms.BindingSource planViewModelBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource planViewModelBindingSource1;
        private System.Windows.Forms.DataGridViewTextBoxColumn godzinaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn poniedziałekDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn wtorekDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn środaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn czwartekDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn piątekDataGridViewTextBoxColumn;
        private System.Windows.Forms.ComboBox ClassComboBox;
        private System.Windows.Forms.BindingSource ClassSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IterationTextBox;
        private System.Windows.Forms.TextBox TabuDurationTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox NeighborhoodSizeTextBox;
        private System.Windows.Forms.Label label3;
    }
}

