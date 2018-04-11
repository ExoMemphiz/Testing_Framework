namespace Testing_Framework.GUI {
    partial class SequenceForm {
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
            this.operationView = new System.Windows.Forms.ListView();
            this.operationIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.operationName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.expectedValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.readWrite = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonAddOperation = new System.Windows.Forms.Button();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonFinish = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSequenceName = new System.Windows.Forms.TextBox();
            this.buttonAddSleep = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.sliderLabel = new System.Windows.Forms.Label();
            this.checkBoxAbortTest = new System.Windows.Forms.CheckBox();
            this.buttonEdit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(281, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Create Sequence";
            // 
            // operationView
            // 
            this.operationView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.operationIndex,
            this.operationName,
            this.expectedValue,
            this.readWrite});
            this.operationView.FullRowSelect = true;
            this.operationView.GridLines = true;
            this.operationView.HideSelection = false;
            this.operationView.Location = new System.Drawing.Point(12, 155);
            this.operationView.MultiSelect = false;
            this.operationView.Name = "operationView";
            this.operationView.Size = new System.Drawing.Size(572, 366);
            this.operationView.TabIndex = 3;
            this.operationView.UseCompatibleStateImageBehavior = false;
            this.operationView.View = System.Windows.Forms.View.Details;
            this.operationView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OperationView_KeyDown);
            this.operationView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OperationView_MouseDoubleClick);
            // 
            // operationIndex
            // 
            this.operationIndex.Text = "#";
            // 
            // operationName
            // 
            this.operationName.Text = "Operation Name";
            this.operationName.Width = 110;
            // 
            // expectedValue
            // 
            this.expectedValue.Text = "Expected Value";
            this.expectedValue.Width = 110;
            // 
            // readWrite
            // 
            this.readWrite.Text = "Read/Write";
            this.readWrite.Width = 159;
            // 
            // buttonAddOperation
            // 
            this.buttonAddOperation.Location = new System.Drawing.Point(590, 290);
            this.buttonAddOperation.Name = "buttonAddOperation";
            this.buttonAddOperation.Size = new System.Drawing.Size(140, 43);
            this.buttonAddOperation.TabIndex = 6;
            this.buttonAddOperation.Text = "Add Operation";
            this.buttonAddOperation.UseVisualStyleBackColor = true;
            this.buttonAddOperation.Click += new System.EventHandler(this.ButtonAddOperation_Click);
            // 
            // buttonMoveUp
            // 
            this.buttonMoveUp.Location = new System.Drawing.Point(591, 155);
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Size = new System.Drawing.Size(140, 43);
            this.buttonMoveUp.TabIndex = 4;
            this.buttonMoveUp.Text = "Move Up";
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.ButtonMoveUp_Click);
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.Location = new System.Drawing.Point(591, 204);
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Size = new System.Drawing.Size(140, 43);
            this.buttonMoveDown.TabIndex = 5;
            this.buttonMoveDown.Text = "Move Down";
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.ButtonMoveDown_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(591, 478);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(140, 43);
            this.buttonDelete.TabIndex = 8;
            this.buttonDelete.Text = "Delete Selected";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonFinish
            // 
            this.buttonFinish.Location = new System.Drawing.Point(538, 541);
            this.buttonFinish.Name = "buttonFinish";
            this.buttonFinish.Size = new System.Drawing.Size(193, 48);
            this.buttonFinish.TabIndex = 10;
            this.buttonFinish.Text = "Finish";
            this.buttonFinish.UseVisualStyleBackColor = true;
            this.buttonFinish.Click += new System.EventHandler(this.ButtonFinish_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(12, 541);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(193, 48);
            this.buttonBack.TabIndex = 9;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.ButtonBack_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "Sequence name";
            // 
            // textBoxSequenceName
            // 
            this.textBoxSequenceName.Location = new System.Drawing.Point(229, 82);
            this.textBoxSequenceName.Name = "textBoxSequenceName";
            this.textBoxSequenceName.Size = new System.Drawing.Size(355, 26);
            this.textBoxSequenceName.TabIndex = 1;
            // 
            // buttonAddSleep
            // 
            this.buttonAddSleep.Location = new System.Drawing.Point(590, 339);
            this.buttonAddSleep.Name = "buttonAddSleep";
            this.buttonAddSleep.Size = new System.Drawing.Size(140, 43);
            this.buttonAddSleep.TabIndex = 7;
            this.buttonAddSleep.Text = "Add Sleep";
            this.buttonAddSleep.UseVisualStyleBackColor = true;
            this.buttonAddSleep.Click += new System.EventHandler(this.ButtonAddSleep_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Delay between operations";
            // 
            // trackBar1
            // 
            this.trackBar1.LargeChange = 100;
            this.trackBar1.Location = new System.Drawing.Point(229, 114);
            this.trackBar1.Maximum = 1000;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(355, 69);
            this.trackBar1.SmallChange = 100;
            this.trackBar1.TabIndex = 2;
            this.trackBar1.TickFrequency = 200;
            this.trackBar1.ValueChanged += new System.EventHandler(this.TrackBar1_ValueChanged);
            // 
            // sliderLabel
            // 
            this.sliderLabel.AutoSize = true;
            this.sliderLabel.Location = new System.Drawing.Point(593, 125);
            this.sliderLabel.Name = "sliderLabel";
            this.sliderLabel.Size = new System.Drawing.Size(39, 20);
            this.sliderLabel.TabIndex = 14;
            this.sliderLabel.Text = "0ms";
            // 
            // checkBoxAbortTest
            // 
            this.checkBoxAbortTest.AutoSize = true;
            this.checkBoxAbortTest.Location = new System.Drawing.Point(286, 554);
            this.checkBoxAbortTest.Name = "checkBoxAbortTest";
            this.checkBoxAbortTest.Size = new System.Drawing.Size(173, 24);
            this.checkBoxAbortTest.TabIndex = 15;
            this.checkBoxAbortTest.Text = "Abort Test On Error";
            this.checkBoxAbortTest.UseVisualStyleBackColor = true;
            this.checkBoxAbortTest.CheckedChanged += new System.EventHandler(this.checkBoxAbortTest_CheckedChanged);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(591, 429);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(140, 43);
            this.buttonEdit.TabIndex = 16;
            this.buttonEdit.Text = "Edit Selected";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // SequenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(738, 601);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.checkBoxAbortTest);
            this.Controls.Add(this.sliderLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonAddSleep);
            this.Controls.Add(this.textBoxSequenceName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonFinish);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonMoveDown);
            this.Controls.Add(this.buttonMoveUp);
            this.Controls.Add(this.buttonAddOperation);
            this.Controls.Add(this.operationView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "SequenceForm";
            this.Text = "SequenceForm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SequenceForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView operationView;
        private System.Windows.Forms.ColumnHeader operationName;
        private System.Windows.Forms.ColumnHeader expectedValue;
        private System.Windows.Forms.ColumnHeader readWrite;
        private System.Windows.Forms.Button buttonAddOperation;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.Button buttonMoveDown;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonFinish;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSequenceName;
        private System.Windows.Forms.Button buttonAddSleep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label sliderLabel;
        private System.Windows.Forms.ColumnHeader operationIndex;
        private System.Windows.Forms.CheckBox checkBoxAbortTest;
        private System.Windows.Forms.Button buttonEdit;
    }
}

