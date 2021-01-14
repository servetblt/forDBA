namespace IntegrationService
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
            this.serverNameText = new MetroFramework.Controls.MetroTextBox();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.databaseCombo = new MetroFramework.Controls.MetroComboBox();
            this.tableCombo = new MetroFramework.Controls.MetroComboBox();
            this.columnCombo = new MetroFramework.Controls.MetroComboBox();
            this.SuspendLayout();
            // 
            // serverNameText
            // 
            this.serverNameText.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.serverNameText.Location = new System.Drawing.Point(151, 129);
            this.serverNameText.Multiline = true;
            this.serverNameText.Name = "serverNameText";
            this.serverNameText.Size = new System.Drawing.Size(524, 34);
            this.serverNameText.TabIndex = 0;
            this.serverNameText.Text = ".";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.Location = new System.Drawing.Point(23, 129);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(99, 20);
            this.metroLabel1.TabIndex = 1;
            this.metroLabel1.Text = "ServerName : ";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.Location = new System.Drawing.Point(7, 229);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(115, 20);
            this.metroLabel2.TabIndex = 1;
            this.metroLabel2.Text = "DatabaseName : ";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.Location = new System.Drawing.Point(32, 326);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(90, 20);
            this.metroLabel3.TabIndex = 1;
            this.metroLabel3.Text = "TabloName : ";
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.Location = new System.Drawing.Point(16, 417);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(106, 20);
            this.metroLabel4.TabIndex = 1;
            this.metroLabel4.Text = "ColumnName : ";
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(289, 494);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(139, 49);
            this.metroButton1.TabIndex = 3;
            this.metroButton1.Text = "Kaydet";
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // databaseCombo
            // 
            this.databaseCombo.FormattingEnabled = true;
            this.databaseCombo.ItemHeight = 24;
            this.databaseCombo.Location = new System.Drawing.Point(151, 219);
            this.databaseCombo.Name = "databaseCombo";
            this.databaseCombo.Size = new System.Drawing.Size(234, 30);
            this.databaseCombo.TabIndex = 4;
            this.databaseCombo.Enter += new System.EventHandler(this.databaseCombo_Enter);
            // 
            // tableCombo
            // 
            this.tableCombo.FormattingEnabled = true;
            this.tableCombo.ItemHeight = 24;
            this.tableCombo.Location = new System.Drawing.Point(151, 326);
            this.tableCombo.Name = "tableCombo";
            this.tableCombo.Size = new System.Drawing.Size(234, 30);
            this.tableCombo.TabIndex = 4;
            this.tableCombo.Enter += new System.EventHandler(this.tableCombo_Enter);
            // 
            // columnCombo
            // 
            this.columnCombo.FormattingEnabled = true;
            this.columnCombo.ItemHeight = 24;
            this.columnCombo.Location = new System.Drawing.Point(151, 417);
            this.columnCombo.Name = "columnCombo";
            this.columnCombo.Size = new System.Drawing.Size(234, 30);
            this.columnCombo.TabIndex = 4;
            this.columnCombo.Enter += new System.EventHandler(this.columnCombo_Enter);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 559);
            this.ControlBox = false;
            this.Controls.Add(this.columnCombo);
            this.Controls.Add(this.tableCombo);
            this.Controls.Add(this.databaseCombo);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.metroLabel4);
            this.Controls.Add(this.metroLabel3);
            this.Controls.Add(this.metroLabel2);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.serverNameText);
            this.Name = "Form1";
            this.Text = "Connect DB ";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox serverNameText;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroComboBox databaseCombo;
        private MetroFramework.Controls.MetroComboBox tableCombo;
        private MetroFramework.Controls.MetroComboBox columnCombo;
    }
}

