using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Testing_Framework.Components;
using Testing_Framework.Components.Operations;
using Testing_Framework.FileHandling;

namespace Testing_Framework.GUI {

    public partial class SequenceForm : Form {

        private String testName;
        private Sequence sequence;
        private Action<int, Sequence> callback;     //Callback for the updated sequence, and index of the sequence (Can be used to check if name has updated, etc)
        private int index;

        public SequenceForm(String testName, Sequence sequence, int index, Action<int, Sequence> callback) {
            this.testName = testName;
            this.callback = callback;
            this.sequence = sequence;
            this.index = index;
            InitializeComponent();
            RefreshList();
            CenterToScreen();
            textBoxSequenceName.Text = this.sequence.GetName();
            trackBar1.Value = this.sequence.GetDelay();
            checkBoxAbortTest.Checked = this.sequence.AbortOnError();
        }

        private void RefreshButton_Click(object sender, EventArgs e) {
            Sequence seq;
            try {
                seq = JSONHandler.Tests.GetTest(testName).GetSequences()[this.index];
                //Console.WriteLine("Refreshing SequenceForm, found Sequence {0}, {1}", seq.GetName(), this.index);
            } catch {
                seq = new Sequence(sequence.GetName(), 0, checkBoxAbortTest.Checked);
                //callback(this.index, seq);
            }
            List<Operation> operations = seq.GetOperations();
            operationView.Items.Clear();
            int index = 1;
            foreach (Operation o in operations) {
                String readWrite = o.GetWrite() ? "Write" : "Read";
                ListViewItem item = new ListViewItem(new[] {index.ToString(), o.GetName(), o.GetExpected().ToString(), readWrite});
                operationView.Items.Add(item);
                index++;
            }
            operationView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }
        
        private void OperationView_MouseDoubleClick(object sender, MouseEventArgs e) {
            ListViewHitTestInfo hit = operationView.HitTest(e.Location);
            if (hit.Item != null && hit.Item.Text != null && hit.Item.Text != "") {
                String operationName = hit.Item.Text;
                ListView.SelectedIndexCollection selected = operationView.SelectedIndices;
                if (selected.Count > 0) {
                    int index = selected[0];
                    new OperationVigoForm(sequence.GetOperations()[index], (prevName, newOperation) => {
                        if (prevName != newOperation.GetName()) {
                            JSONHandler.Operations.RenameOperation(prevName, newOperation.GetName());
                        }
                        sequence.RemoveFromList(index);
                        sequence.AddToList(newOperation, index);
                        JSONHandler.Sequences.SaveSequence(sequence);
                        callback(this.index, sequence);
                        RefreshList();
                    }).ShowDialog();
                }
            }
        }

        private void ButtonMoveUp_Click(object sender, EventArgs e) {
            ListView.SelectedIndexCollection selected = operationView.SelectedIndices;
            if (selected.Count > 0) {
                int index = selected[0];
                if (index > 0) {
                    sequence.SwapIndex(index, index - 1);
                    JSONHandler.Sequences.SaveSequence(sequence);
                    callback(this.index, sequence);
                    RefreshList();
                    operationView.Items[index - 1].Selected = true;
                }
            }
        }

        private void ButtonMoveDown_Click(object sender, EventArgs e) {
            ListView.SelectedIndexCollection selected = operationView.SelectedIndices;
            if (selected.Count > 0) {
                int index = selected[0];
                if (index < sequence.GetOperations().Count - 1) {
                    sequence.SwapIndex(index, index + 1);
                    JSONHandler.Sequences.SaveSequence(sequence);
                    callback(this.index, sequence);
                    RefreshList();
                    operationView.Items[index + 1].Selected = true;
                }
            }
        }

        private void RefreshList() {
            RefreshButton_Click(null, null);
        }

        private void ButtonAddOperation_Click(object sender, EventArgs e) {
            new OperationMenu(testName, sequence.GetName(), (newOperation, addToSequence) => {
                //Console.WriteLine("Received {0} as callback", newOperation);
                //Console.WriteLine("[SequenceForm.AddOperation] OperationMenu Callback return! Operation name: {0}, expected: {1}", newOperation.getName(), newOperation.getExpected().ToString());
                sequence.SetName(textBoxSequenceName.Text);
                if (addToSequence) {
                    sequence.AddToList(newOperation);
                }
                callback(index, sequence);
                JSONHandler.Sequences.SaveSequence(sequence);
                RefreshList();
            }).ShowDialog();
        }

        private void ButtonDelete_Click(object sender, EventArgs e) {
            ListView.SelectedIndexCollection selected = operationView.SelectedIndices;
            if (selected.Count > 0) {
                int index = selected[0];
                DialogResult res = MessageBox.Show("Are you sure you wish to delete operation " + operationView.Items[index].Text + " from this sequence?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes) {
                    sequence.RemoveFromList(index);
                    callback(this.index, sequence);
                    JSONHandler.Sequences.SaveSequence(sequence);
                    RefreshList();
                }
            }
        }

        private void OperationView_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Delete) {
                ButtonDelete_Click(null, null);
            }
        }

        private void ButtonFinish_Click(object sender, EventArgs e) {
            if (textBoxSequenceName.Text == "") {
                MessageBox.Show("Sequence name cannot be empty!");
                return;
            }
            if (sequence.GetName() != textBoxSequenceName.Text) {
                DialogResult res = MessageBox.Show("Are you sure you wish to rename this sequence to " + textBoxSequenceName.Text + "?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.No) {
                    return;
                }
            }
            if (callback != null) {
                sequence.SetName(textBoxSequenceName.Text);
                callback(this.index, sequence);
            }
            this.Close();
        }
        
        private void ButtonAddSleep_Click(object sender, EventArgs e) {
            Sleep sleep = new Sleep(0);
            sequence.AddToList(sleep);
            //Console.WriteLine("[SequenceForm.AddSleep] Added Sleep to Sequence #{0}", this.index);
            JSONHandler.Sequences.SaveSequence(sequence);
            callback(this.index, sequence);
            RefreshList();
        }

        private void ButtonBack_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void TrackBar1_ValueChanged(object sender, EventArgs e) {
            double nearest = RoundToNearest(trackBar1.Value, 100);
            trackBar1.Value = (int) nearest;
            sliderLabel.Text = trackBar1.Value.ToString() + "ms";
            sequence.SetDelay(trackBar1.Value);
            JSONHandler.Sequences.SaveSequence(sequence);
            callback?.Invoke(this.index, sequence);
        }

        private double RoundToNearest(double number, int n) {
            return Math.Round(number / n, 0) * n;
        }

        private void SequenceForm_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode.Equals(Keys.Escape)) {
                this.Close();
            }
        }

        private void checkBoxAbortTest_CheckedChanged(object sender, EventArgs e) {
            this.sequence.SetAbortOnError(checkBoxAbortTest.Checked);
            callback?.Invoke(this.index, this.sequence);
        }

        private void buttonEdit_Click(object sender, EventArgs e) {
            ListView.SelectedIndexCollection selected = operationView.SelectedIndices;
            if (selected.Count > 0) {
                int index = selected[0];
                new OperationVigoForm(sequence.GetOperations()[index], (prevName, newOperation) => {
                    if (prevName != newOperation.GetName()) {
                        JSONHandler.Operations.RenameOperation(prevName, newOperation.GetName());
                    }
                    sequence.RemoveFromList(index);
                    sequence.AddToList(newOperation, index);
                    JSONHandler.Sequences.SaveSequence(sequence);
                    callback(this.index, sequence);
                    RefreshList();
                }).ShowDialog();
            }
        }
    }

}