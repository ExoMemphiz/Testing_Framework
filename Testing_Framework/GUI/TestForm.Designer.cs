namespace Testing_Framework.GUI {
    partial class TestForm {
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.sequenceView = new System.Windows.Forms.ListView();
            this.sequenceIndex = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sequenceName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sequenceCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sequenceStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonRunTest = new System.Windows.Forms.Button();
            this.buttonBack = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonAddSequence = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTestName = new System.Windows.Forms.TextBox();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.progressBar.Cursor = System.Windows.Forms.Cursors.Default;
            this.progressBar.ForeColor = System.Drawing.Color.Blue;
            this.progressBar.Location = new System.Drawing.Point(159, 694);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(684, 45);
            this.progressBar.TabIndex = 0;
            // 
            // sequenceView
            // 
            this.sequenceView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.sequenceIndex,
            this.sequenceName,
            this.sequenceCount,
            this.sequenceStatus});
            this.sequenceView.FullRowSelect = true;
            this.sequenceView.GridLines = true;
            this.sequenceView.HideSelection = false;
            this.sequenceView.Location = new System.Drawing.Point(12, 158);
            this.sequenceView.MultiSelect = false;
            this.sequenceView.Name = "sequenceView";
            this.sequenceView.Size = new System.Drawing.Size(831, 515);
            this.sequenceView.TabIndex = 2;
            this.sequenceView.UseCompatibleStateImageBehavior = false;
            this.sequenceView.View = System.Windows.Forms.View.Details;
            this.sequenceView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SequenceView_KeyDown);
            this.sequenceView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SequenceView_MouseDoubleClick);
            // 
            // sequenceIndex
            // 
            this.sequenceIndex.Text = "#";
            // 
            // sequenceName
            // 
            this.sequenceName.Text = "Sequence Name";
            this.sequenceName.Width = 150;
            // 
            // sequenceCount
            // 
            this.sequenceCount.Text = "# of Operations";
            this.sequenceCount.Width = 120;
            // 
            // sequenceStatus
            // 
            this.sequenceStatus.Text = "Sequence Status";
            this.sequenceStatus.Width = 280;
            // 
            // buttonRunTest
            // 
            this.buttonRunTest.Location = new System.Drawing.Point(12, 692);
            this.buttonRunTest.Name = "buttonRunTest";
            this.buttonRunTest.Size = new System.Drawing.Size(133, 47);
            this.buttonRunTest.TabIndex = 8;
            this.buttonRunTest.Text = "Run Test";
            this.buttonRunTest.UseVisualStyleBackColor = true;
            this.buttonRunTest.Click += new System.EventHandler(this.ButtonRunTest_Click);
            // 
            // buttonBack
            // 
            this.buttonBack.Location = new System.Drawing.Point(857, 691);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(140, 48);
            this.buttonBack.TabIndex = 9;
            this.buttonBack.Text = "Back";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(404, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "Create Test";
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(857, 630);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(140, 43);
            this.buttonDelete.TabIndex = 7;
            this.buttonDelete.Text = "Delete Selected";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.ButtonDelete_Click);
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.Location = new System.Drawing.Point(857, 207);
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Size = new System.Drawing.Size(140, 43);
            this.buttonMoveDown.TabIndex = 4;
            this.buttonMoveDown.Text = "Move Down";
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.ButtonMoveDown_Click);
            // 
            // buttonMoveUp
            // 
            this.buttonMoveUp.Location = new System.Drawing.Point(857, 158);
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Size = new System.Drawing.Size(140, 43);
            this.buttonMoveUp.TabIndex = 3;
            this.buttonMoveUp.Text = "Move Up";
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.ButtonMoveUp_Click);
            // 
            // buttonAddSequence
            // 
            this.buttonAddSequence.Location = new System.Drawing.Point(857, 417);
            this.buttonAddSequence.Name = "buttonAddSequence";
            this.buttonAddSequence.Size = new System.Drawing.Size(140, 43);
            this.buttonAddSequence.TabIndex = 5;
            this.buttonAddSequence.Text = "Add Sequence";
            this.buttonAddSequence.UseVisualStyleBackColor = true;
            this.buttonAddSequence.Click += new System.EventHandler(this.ButtonAddSequence_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 11;
            this.label2.Text = "Test name";
            // 
            // textBoxTestName
            // 
            this.textBoxTestName.Location = new System.Drawing.Point(378, 115);
            this.textBoxTestName.Name = "textBoxTestName";
            this.textBoxTestName.Size = new System.Drawing.Size(465, 26);
            this.textBoxTestName.TabIndex = 1;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(857, 476);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(140, 43);
            this.buttonEdit.TabIndex = 6;
            this.buttonEdit.Text = "Edit Sequence";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.ButtonEdit_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 753);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.textBoxTestName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonMoveDown);
            this.Controls.Add(this.buttonMoveUp);
            this.Controls.Add(this.buttonAddSequence);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonRunTest);
            this.Controls.Add(this.sequenceView);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.ListView sequenceView;
        private System.Windows.Forms.Button buttonRunTest;
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonMoveDown;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.Button buttonAddSequence;
        private System.Windows.Forms.ColumnHeader sequenceName;
        private System.Windows.Forms.ColumnHeader sequenceCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTestName;
        private System.Windows.Forms.ColumnHeader sequenceStatus;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.ColumnHeader sequenceIndex;
    }
}