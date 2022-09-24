namespace WindowsFormsApp
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonImage = new System.Windows.Forms.Button();
            this.buttonTable = new System.Windows.Forms.Button();
            this.buttonDiagram = new System.Windows.Forms.Button();
            this.inputUserControl = new VisualComponentLibrary.InputUserControl();
            this.outputUserControl = new VisualComponentLibrary.OutputUserControl();
            this.choiceUserControl = new VisualComponentLibrary.ChoiceUserControl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Select the weather:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Select the date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(516, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Select the place:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(386, 247);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonImage
            // 
            this.buttonImage.Location = new System.Drawing.Point(12, 238);
            this.buttonImage.Name = "buttonImage";
            this.buttonImage.Size = new System.Drawing.Size(75, 23);
            this.buttonImage.TabIndex = 8;
            this.buttonImage.Text = "Image";
            this.buttonImage.UseVisualStyleBackColor = true;
            this.buttonImage.Click += new System.EventHandler(this.buttonImage_Click);
            // 
            // buttonTable
            // 
            this.buttonTable.Location = new System.Drawing.Point(113, 237);
            this.buttonTable.Name = "buttonTable";
            this.buttonTable.Size = new System.Drawing.Size(75, 23);
            this.buttonTable.TabIndex = 9;
            this.buttonTable.Text = "Table";
            this.buttonTable.UseVisualStyleBackColor = true;
            // 
            // buttonDiagram
            // 
            this.buttonDiagram.Location = new System.Drawing.Point(212, 237);
            this.buttonDiagram.Name = "buttonDiagram";
            this.buttonDiagram.Size = new System.Drawing.Size(75, 23);
            this.buttonDiagram.TabIndex = 10;
            this.buttonDiagram.Text = "Diagram";
            this.buttonDiagram.UseVisualStyleBackColor = true;
            this.buttonDiagram.Click += new System.EventHandler(this.buttonDiagram_Click);
            // 
            // inputUserControl
            // 
            this.inputUserControl.Location = new System.Drawing.Point(223, 23);
            this.inputUserControl.MaxDate = null;
            this.inputUserControl.MinDate = null;
            this.inputUserControl.Name = "inputUserControl";
            this.inputUserControl.SelectItemProperty = null;
            this.inputUserControl.Size = new System.Drawing.Size(206, 54);
            this.inputUserControl.TabIndex = 6;
            // 
            // outputUserControl
            // 
            this.outputUserControl.Location = new System.Drawing.Point(444, 23);
            this.outputUserControl.Name = "outputUserControl";
            this.outputUserControl.SelectItemProperty = -1;
            this.outputUserControl.Size = new System.Drawing.Size(229, 228);
            this.outputUserControl.TabIndex = 2;
            // 
            // choiceUserControl
            // 
            this.choiceUserControl.Location = new System.Drawing.Point(12, 23);
            this.choiceUserControl.Name = "choiceUserControl";
            this.choiceUserControl.SelectItemProperty = "";
            this.choiceUserControl.Size = new System.Drawing.Size(196, 199);
            this.choiceUserControl.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 282);
            this.Controls.Add(this.buttonDiagram);
            this.Controls.Add(this.buttonTable);
            this.Controls.Add(this.buttonImage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.inputUserControl);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputUserControl);
            this.Controls.Add(this.choiceUserControl);
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VisualComponentLibrary.ChoiceUserControl choiceUserControl;
        private VisualComponentLibrary.OutputUserControl outputUserControl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private VisualComponentLibrary.InputUserControl inputUserControl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonImage;
        private System.Windows.Forms.Button buttonTable;
        private System.Windows.Forms.Button buttonDiagram;
    }
}

