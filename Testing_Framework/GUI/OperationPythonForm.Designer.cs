namespace Testing_Framework.GUI {
    partial class OperationPythonForm {
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
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.operationName = new System.Windows.Forms.TextBox();
            this.operationAction = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.operationParam1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.operationParam2 = new System.Windows.Forms.TextBox();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(170, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(180, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Save Operation";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(293, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "Operation action";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Operation name";
            // 
            // operationName
            // 
            this.operationName.Location = new System.Drawing.Point(22, 118);
            this.operationName.Name = "operationName";
            this.operationName.Size = new System.Drawing.Size(193, 26);
            this.operationName.TabIndex = 8;
            // 
            // operationAction
            // 
            this.operationAction.FormattingEnabled = true;
            this.operationAction.Items.AddRange(new object[] {
            "Click (Relative)",
            "Click (Absolute)",
            "Read Mouse X",
            "Read Mouse Y",
            "Read Menu ID",
            "Keypad Input"});
            this.operationAction.Location = new System.Drawing.Point(297, 118);
            this.operationAction.MaxDropDownItems = 4;
            this.operationAction.Name = "operationAction";
            this.operationAction.Size = new System.Drawing.Size(193, 28);
            this.operationAction.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 20);
            this.label3.TabIndex = 13;
            this.label3.Text = "Operation Parameter 1";
            // 
            // operationParam1
            // 
            this.operationParam1.Location = new System.Drawing.Point(22, 200);
            this.operationParam1.Name = "operationParam1";
            this.operationParam1.Size = new System.Drawing.Size(193, 26);
            this.operationParam1.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(297, 174);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(170, 20);
            this.label5.TabIndex = 15;
            this.label5.Text = "Operation Parameter 2";
            // 
            // operationParam2
            // 
            this.operationParam2.Location = new System.Drawing.Point(297, 200);
            this.operationParam2.Name = "operationParam2";
            this.operationParam2.Size = new System.Drawing.Size(193, 26);
            this.operationParam2.TabIndex = 14;
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(157, 256);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(193, 38);
            this.buttonSave.TabIndex = 16;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // OperationPythonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 319);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.operationParam2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.operationParam1);
            this.Controls.Add(this.operationAction);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.operationName);
            this.Controls.Add(this.label1);
            this.Name = "OperationPythonForm";
            this.Text = "OperationPythonForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox operationName;
        private System.Windows.Forms.ComboBox operationAction;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox operationParam1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox operationParam2;
        private System.Windows.Forms.Button buttonSave;
    }
}