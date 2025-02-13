using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SPS01CalibrateAndTestNewModeApp.SubForm
{
    partial class FormDisplaySpsdata
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.TextBoxSpsdata = new ReaLTaiizor.Controls.DungeonRichTextBox();
            this.SuspendLayout();
            // 
            // TextBoxSpsdata
            // 
            this.TextBoxSpsdata.AutoWordSelection = false;
            this.TextBoxSpsdata.BackColor = System.Drawing.Color.Transparent;
            this.TextBoxSpsdata.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.TextBoxSpsdata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxSpsdata.EdgeColor = System.Drawing.Color.White;
            this.TextBoxSpsdata.Font = new System.Drawing.Font("Tahoma", 10F);
            this.TextBoxSpsdata.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.TextBoxSpsdata.Location = new System.Drawing.Point(0, 0);
            this.TextBoxSpsdata.Name = "TextBoxSpsdata";
            this.TextBoxSpsdata.ReadOnly = false;
            this.TextBoxSpsdata.Size = new System.Drawing.Size(800, 450);
            this.TextBoxSpsdata.TabIndex = 1;
            this.TextBoxSpsdata.TextBackColor = System.Drawing.Color.White;
            this.TextBoxSpsdata.WordWrap = true;
            // 
            // FormDisplaySpsdata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TextBoxSpsdata);
            this.Name = "FormDisplaySpsdata";
            this.Text = "FormDisplaySpsdata";
            this.ResumeLayout(false);
        }

        private ReaLTaiizor.Controls.DungeonRichTextBox TextBoxSpsdata;

        #endregion
    }
}