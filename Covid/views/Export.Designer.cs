
namespace Covid
{
    partial class Export
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
            this.g2b_import = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // g2b_import
            // 
            this.g2b_import.AutoRoundedCorners = true;
            this.g2b_import.BorderRadius = 30;
            this.g2b_import.CheckedState.Parent = this.g2b_import;
            this.g2b_import.CustomImages.Parent = this.g2b_import;
            this.g2b_import.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.g2b_import.ForeColor = System.Drawing.Color.White;
            this.g2b_import.HoverState.Parent = this.g2b_import;
            this.g2b_import.Location = new System.Drawing.Point(368, 213);
            this.g2b_import.Margin = new System.Windows.Forms.Padding(4);
            this.g2b_import.Name = "g2b_import";
            this.g2b_import.ShadowDecoration.Parent = this.g2b_import;
            this.g2b_import.Size = new System.Drawing.Size(267, 62);
            this.g2b_import.TabIndex = 8;
            this.g2b_import.Text = "Zoznam pozitívnych";
            this.g2b_import.Click += new System.EventHandler(this.g2b_import_Click);
            // 
            // guna2Button1
            // 
            this.guna2Button1.AutoRoundedCorners = true;
            this.guna2Button1.BorderRadius = 30;
            this.guna2Button1.CheckedState.Parent = this.guna2Button1;
            this.guna2Button1.CustomImages.Parent = this.guna2Button1;
            this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.guna2Button1.ForeColor = System.Drawing.Color.White;
            this.guna2Button1.HoverState.Parent = this.guna2Button1;
            this.guna2Button1.Location = new System.Drawing.Point(368, 353);
            this.guna2Button1.Margin = new System.Windows.Forms.Padding(4);
            this.guna2Button1.Name = "guna2Button1";
            this.guna2Button1.ShadowDecoration.Parent = this.guna2Button1;
            this.guna2Button1.Size = new System.Drawing.Size(267, 62);
            this.guna2Button1.TabIndex = 9;
            this.guna2Button1.Text = "Zoznam testovaných";
            this.guna2Button1.Click += new System.EventHandler(this.guna2Button1_Click);
            // 
            // Export
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 642);
            this.Controls.Add(this.guna2Button1);
            this.Controls.Add(this.g2b_import);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Export";
            this.Text = "Export";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button g2b_import;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}