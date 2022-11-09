namespace WindowsFormsApp
{
    partial class FormMain
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
            this.outputList1 = new VisualComponentLibrary.Visual.OutputList();
            this.SuspendLayout();
            // 
            // outputList1
            // 
            this.outputList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputList1.Location = new System.Drawing.Point(0, 0);
            this.outputList1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.outputList1.Name = "outputList1";
            this.outputList1.Size = new System.Drawing.Size(800, 450);
            this.outputList1.TabIndex = 0;
            this.outputList1.Load += new System.EventHandler(this.outputList1_Load);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.outputList1);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.ResumeLayout(false);

        }

        #endregion

        private VisualComponentLibrary.Visual.OutputList outputList1;
    }
}