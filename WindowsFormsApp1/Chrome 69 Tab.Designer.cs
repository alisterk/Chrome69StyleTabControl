namespace WindowsFormsApp1
{
    partial class Chrome_69_Tab
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
            this.chrome69Tabcontrol1 = new WindowsFormsApp1.Chrome69Tabcontrol();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chrome69Tabcontrol1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chrome69Tabcontrol1
            // 
            this.chrome69Tabcontrol1.Controls.Add(this.tabPage1);
            this.chrome69Tabcontrol1.Controls.Add(this.tabPage2);
            this.chrome69Tabcontrol1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chrome69Tabcontrol1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.chrome69Tabcontrol1.ItemSize = new System.Drawing.Size(245, 35);
            this.chrome69Tabcontrol1.Location = new System.Drawing.Point(0, 0);
            this.chrome69Tabcontrol1.Name = "chrome69Tabcontrol1";
            this.chrome69Tabcontrol1.SelectedIndex = 0;
            this.chrome69Tabcontrol1.Size = new System.Drawing.Size(800, 450);
            this.chrome69Tabcontrol1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.chrome69Tabcontrol1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(-4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(804, 415);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(-4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(804, 415);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Chrome_69_Tab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chrome69Tabcontrol1);
            this.Name = "Chrome_69_Tab";
            this.Text = "Chrome_69_Tab";
            this.Load += new System.EventHandler(this.Chrome_69_Tab_Load);
            this.chrome69Tabcontrol1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Chrome69Tabcontrol chrome69Tabcontrol1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}