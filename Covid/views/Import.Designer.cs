
namespace Covid
{
    partial class Import
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
            this.SuspendLayout();
            // 
            // g2b_import
            // 
            this.g2b_import.AutoRoundedCorners = true;
            this.g2b_import.BorderRadius = 13;
            this.g2b_import.CheckedState.Parent = this.g2b_import;
            this.g2b_import.CustomImages.Parent = this.g2b_import;
            this.g2b_import.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.g2b_import.ForeColor = System.Drawing.Color.White;
            this.g2b_import.HoverState.Parent = this.g2b_import;
            this.g2b_import.Location = new System.Drawing.Point(288, 193);
            this.g2b_import.Name = "g2b_import";
            this.g2b_import.ShadowDecoration.Parent = this.g2b_import;
            this.g2b_import.Size = new System.Drawing.Size(123, 29);
            this.g2b_import.TabIndex = 7;
            this.g2b_import.Text = "Načítanie údajov";
            this.g2b_import.Click += new System.EventHandler(this.g2b_import_Click);
            // 
            // Import
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 522);
            this.Controls.Add(this.g2b_import);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Import";
            this.Text = "Import";
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button g2b_import;
    }
}