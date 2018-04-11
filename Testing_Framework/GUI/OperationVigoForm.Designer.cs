namespace Testing_Framework.GUI {

    partial class OperationVigoForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.operationName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelValue = new System.Windows.Forms.Label();
            this.operationValue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.operationURL = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonWrite = new System.Windows.Forms.RadioButton();
            this.radioButtonRead = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(165, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Save Operation";
            // 
            // operationName
            // 
            this.operationName.Location = new System.Drawing.Point(17, 117);
            this.operationName.Name = "operationName";
            this.operationName.Size = new System.Drawing.Size(193, 26);
            this.operationName.TabIndex = 1;
            this.operationName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OperationName_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Operation name";
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Location = new System.Drawing.Point(308, 169);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(117, 20);
            this.labelValue.TabIndex = 3;
            this.labelValue.Text = "Expected value";
            // 
            // operationValue
            // 
            this.operationValue.Location = new System.Drawing.Point(304, 193);
            this.operationValue.Name = "operationValue";
            this.operationValue.Size = new System.Drawing.Size(193, 26);
            this.operationValue.TabIndex = 4;
            this.operationValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OperationValue_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(300, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Operation url";
            // 
            // operationURL
            // 
            this.operationURL.Location = new System.Drawing.Point(304, 115);
            this.operationURL.Name = "operationURL";
            this.operationURL.Size = new System.Drawing.Size(193, 26);
            this.operationURL.TabIndex = 7;
            this.operationURL.Click += new System.EventHandler(this.OperationURL_Click);
            this.operationURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OperationURL_KeyDown);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(152, 262);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(193, 38);
            this.buttonSave.TabIndex = 8;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonWrite);
            this.panel1.Controls.Add(this.radioButtonRead);
            this.panel1.Location = new System.Drawing.Point(17, 163);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(192, 76);
            this.panel1.TabIndex = 9;
            // 
            // radioButtonWrite
            // 
            this.radioButtonWrite.AutoSize = true;
            this.radioButtonWrite.Location = new System.Drawing.Point(0, 45);
            this.radioButtonWrite.Name = "radioButtonWrite";
            this.radioButtonWrite.Size = new System.Drawing.Size(134, 24);
            this.radioButtonWrite.TabIndex = 1;
            this.radioButtonWrite.Text = "Write to VIGO";
            this.radioButtonWrite.UseVisualStyleBackColor = true;
            this.radioButtonWrite.CheckedChanged += new System.EventHandler(this.RadioButtonWrite_CheckedChanged);
            // 
            // radioButtonRead
            // 
            this.radioButtonRead.AutoSize = true;
            this.radioButtonRead.Checked = true;
            this.radioButtonRead.Location = new System.Drawing.Point(0, 10);
            this.radioButtonRead.Name = "radioButtonRead";
            this.radioButtonRead.Size = new System.Drawing.Size(154, 24);
            this.radioButtonRead.TabIndex = 0;
            this.radioButtonRead.TabStop = true;
            this.radioButtonRead.Text = "Read from VIGO";
            this.radioButtonRead.UseVisualStyleBackColor = true;
            this.radioButtonRead.CheckedChanged += new System.EventHandler(this.RadioButtonRead_CheckedChanged);
            // 
            // OperationVigoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 319);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.operationURL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.operationValue);
            this.Controls.Add(this.labelValue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.operationName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "OperationVigoForm";
            this.Text = "OperationVigoForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox operationName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelValue;
        private System.Windows.Forms.TextBox operationValue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox operationURL;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonWrite;
        private System.Windows.Forms.RadioButton radioButtonRead;
    }

}