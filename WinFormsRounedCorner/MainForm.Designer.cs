namespace WinFormsTest
{
    partial class MainForm
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
            border1 = new RoundedRectangle();
            SuspendLayout();
            // 
            // border1
            // 
            border1.BackColor = SystemColors.ActiveCaption;
            border1.BorderColor = Color.Gray;
            border1.BorderThickness = new Thickness(15, 20, 25, 30);
            border1.CornerRadius = new CornerRadius(20, 30, 40, 50);
            border1.Location = new Point(241, 107);
            border1.Name = "border1";
            border1.Size = new Size(333, 211);
            border1.TabIndex = 0;
            border1.Text = "border1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(border1);
            Name = "MainForm";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private RoundedRectangle border1;
    }
}
