using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

using Testing_Framework.Components;
using Testing_Framework.Exceptions;
using Testing_Framework.FileHandling;
using Testing_Framework.GUI.Utility;

namespace Testing_Framework.GUI {

    public partial class TestForm : Form {

        private Action<Sequence> callback;
        private String testName;
        private Test test;
        private bool runningTest = false;

        public TestForm(Action<Sequence> callback) {
            this.testName = "";
            this.callback = callback;
            test = new Test(this.testName);
            InitializeComponent();
            CenterToScreen();
            RefreshList();
        }

        public TestForm(String testName) {
            this.testName = testName;
            try {
                this.test = JSONHandler.Tests.GetTest(testName);
            } catch {
                this.test = new Test(this.testName);
            }
            InitializeComponent();
            CenterToScreen();
            textBoxTestName.Text = this.testName;
            RefreshList();
        }

        public TestForm(String testName, Action<Sequence> callback) {
            this.testName = testName;
            this.callback = callback;
            try {
                this.test = JSONHandler.Tests.GetTest(testName);
            } catch {
                this.test = new Test(this.testName);
            }
            InitializeComponent();
            CenterToScreen();
            textBoxTestName.Text = this.testName;
            RefreshList();
        }

        /** Logic / Events **/

        private void RunTest(int totalOperations, List<Sequence> sequences) {
            int multiplier = 100 / totalOperations;
            int completed = 0;

            int index = 0;

            bool failed = false;

            List<Sequence> failedSequences = new List<Sequence>();
            try {
                test.RunTest((sequence, operationsDone, exception) => {
                    if (exception == null) {
                        completed++;
                        if (runningTest) {
                            UpdateBar(multiplier * completed);
                            Console.WriteLine("[RunTest] [exception == null] UpdateBar: {0}", multiplier * completed);
                            if (operationsDone == sequence.GetOperations().Count && !failedSequences.Contains(sequence)) {
                                UpdateStatus(index, "Success!");
                                index++;
                            }
                        }
                    } else {
                        UpdateStatus(index, exception.Message);
                        int remainingOperations = sequence.GetOperations().Count - operationsDone;
                        completed += remainingOperations;
                        UpdateBar(multiplier * completed);
                        Console.WriteLine("[RunTest] [exception != null] UpdateBar: {0}", multiplier * completed);
                        failed = true;
                        index++;
                        if (!failedSequences.Contains(sequence)) {
                            failedSequences.Add(sequence);
                        }
                    }
                    if (!runningTest) {
                        /*
                        UpdateBar(100);
                        Console.WriteLine("[RunTest] [!runningTest] UpdateBar: {0}", 100);
                        if (failed) {
                            UpdateBarColour(Color.Red);
                            Console.WriteLine("[RunTest] [!runningTest] UpdateBarColour: {0}", "Red");
                        }
                        
                        */
                        throw new ThreadInterruptedException("Test interrupted!");
                    }
                });
            } catch (ComparisonFailException e) {
                Console.WriteLine("Error happened on Sequence #{0}, message: {1}", index, e.Message);
                UpdateStatus(index, e.Message);
                Sequence seq = test.GetSequences()[index];
                if (!failedSequences.Contains(seq)) {
                    failedSequences.Add(seq);
                }
                failed = true;
            } catch {
                UpdateBar(100, (didSetValue) => {
                    Console.WriteLine("[RunTest] [ThreadInterrupted] UpdateBar: {0}", didSetValue);
                    if (!failed) {
                        UpdateBarColour(Color.Yellow);
                        Console.WriteLine("[RunTest] [ThreadInterrupted] UpdateBarColour: {0}", "Yellow");
                    } else {
                        UpdateBarColour(Color.Red);
                        Console.WriteLine("[RunTest] [ThreadInterrupted] UpdateBarColour: {0}", "Red");
                    }
                });
                return;
            }
            UpdateBar(100);
            Console.WriteLine("[RunTest] [failed == {0}] UpdateBar: {1}", failed, 100);
            if (failed) {
                UpdateBarColour(Color.Red);
                Console.WriteLine("[RunTest] UpdateBarColour: {0}", "Red");
            }
        }

        public int GetTotalOperations(List<Sequence> sequences) {
            int total = 0;
            for (int i = 0; i < sequences.Count; i++) {
                total += sequences[i].GetOperations().Count;
            }
            return total;
        }

        private void ButtonRunTest_Click(object sender, EventArgs e) {
            RefreshList();
            List<Sequence> sequences;
            try {
                sequences = JSONHandler.Sequences.ReadSequences(testName);
            } catch {
                return;
            }
            int totalOperations = GetTotalOperations(sequences);
            DisableButtons();
            buttonBack.Text = "Stop";
            Thread myThread = new Thread(() => {
                /*
                mockProgress();
                enableButtons();
                */
                try {
                    RunTest(totalOperations, sequences);
                    EnableButtons();
                    MethodInvoker invoke = new MethodInvoker(() => {
                        buttonBack.Text = "Back";
                    });
                    buttonBack.Invoke(invoke);
                } catch (ThreadInterruptedException ex) {
                    Console.WriteLine(ex.StackTrace);
                    EnableButtons();
                    MethodInvoker invoke = new MethodInvoker(() => {
                        buttonBack.Text = "Back";
                    });
                    buttonBack.Invoke(invoke);
                } catch (Exception ex) {    //Handle communication-thrown errors
                    Console.WriteLine(ex.StackTrace);
                    MessageBox.Show(ex.Message);
                    UpdateBarColour(Color.Red);
                    EnableButtons();
                }
            });
            myThread.IsBackground = true;
            myThread.Start();
        }

        private void ButtonMoveUp_Click(object sender, EventArgs e) {
            ListView.SelectedIndexCollection selected = sequenceView.SelectedIndices;
            if (selected.Count > 0) {
                int index = selected[0];
                if (index > 0) {
                    test.SwapIndex(index, index - 1);
                    JSONHandler.Tests.SaveTest(test);
                    RefreshList();
                    sequenceView.Items[index - 1].Focused = true;
                    sequenceView.Items[index - 1].Selected = true;
                }
            }
        }

        private void ButtonAddSequence_Click(object sender, EventArgs e) {
            new SequenceMenu(testName, (sequenceObj) => {
                test.AddToList(sequenceObj);
                JSONHandler.Tests.SaveTest(test);
                RefreshList();
            }).ShowDialog();
        }

        private void UpdateStatus(int index, String status) {
            MethodInvoker m = new MethodInvoker(() => {
                Sequence seq = test.GetSequences()[index];
                sequenceView.Items[index] = new ListViewItem(new[] {(index + 1).ToString(), seq.GetName(), seq.GetOperations().Count.ToString(), status});
                sequenceView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            });
            try {
                sequenceView.Invoke(m);
            } catch { }
            
        }

        private void ClearStatus() {
            RefreshList();
        }

        private void RefreshList() {
            List<Sequence> sequences = test.GetSequences();
            sequenceView.Items.Clear();
            int index = 1;
            foreach (Sequence s in sequences) {
                ListViewItem item = new ListViewItem(new[] {index.ToString(), s.GetName(), s.GetOperations().Count.ToString()});
                sequenceView.Items.Add(item);
                index++;
            }
            sequenceView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            UpdateBar(0, (setValue) => {
                UpdateBarColour(Color.Green);
                Console.WriteLine("[RefreshList] UpdateBarColour: {0}", "Green");
            });
            Console.WriteLine("[RefreshList] UpdateBar: {0}", 0);
        }

        private void SequenceView_MouseDoubleClick(object sender, MouseEventArgs e) {
            if (runningTest || !buttonEdit.Enabled) {
                return;
            }
            ListViewHitTestInfo hit = sequenceView.HitTest(e.Location);
            if (hit.Item != null && hit.Item.Text != null && hit.Item.Text != "") {
                String sequenceName = hit.Item.Text;
                ListView.SelectedIndexCollection selected = sequenceView.SelectedIndices;
                if (selected.Count > 0) {
                    int index = selected[0];
                    Sequence seq = JSONHandler.Tests.GetTest(testName).GetSequences()[index];
                    new SequenceForm(testName, seq, index, (seqIndex, newSequence) => {
                        test.RemoveFromList(index);
                        test.AddToList(newSequence, index);
                        JSONHandler.Tests.SaveTest(test);
                        RefreshList();
                    }).ShowDialog();
                }
            }
        }

        private void ButtonDelete_Click(object sender, EventArgs e) {
            ListView.SelectedIndexCollection selected = sequenceView.SelectedIndices;
            if (selected.Count > 0) {
                int index = selected[0];
                DialogResult res = MessageBox.Show("Are you sure you wish to delete sequence " + sequenceView.Items[index].Text + " from this test?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes) {
                    test.RemoveFromList(index);
                    JSONHandler.Tests.SaveTest(test);
                    RefreshList();
                }
            }
        }

        private void ButtonMoveDown_Click(object sender, EventArgs e) {
            ListView.SelectedIndexCollection selected = sequenceView.SelectedIndices;
            if (selected.Count > 0) {
                int index = selected[0];
                if (index < test.GetSequences().Count - 1) {
                    test.SwapIndex(index, index + 1);
                    JSONHandler.Tests.SaveTest(test);
                    RefreshList();
                    sequenceView.Items[index + 1].Focused = true;
                    sequenceView.Items[index + 1].Selected = true;
                }
            }
        }

        private void SequenceView_KeyDown(object sender, KeyEventArgs e) {
            if (!runningTest && e.KeyCode == Keys.Delete) {
                ButtonDelete_Click(null, null);
            }
        }

        private void ButtonEdit_Click(object sender, EventArgs e) {
            ListView.SelectedIndexCollection selected = sequenceView.SelectedIndices;
            if (selected.Count > 0) {
                int index = selected[0];
                String sequenceName = sequenceView.Items[index].SubItems[0].Text;
                Sequence seq = JSONHandler.Tests.GetTest(testName).GetSequences()[index];
                new SequenceForm(testName, seq, index, (seqIndex, newSequence) => {
                    //Console.WriteLine("[TestForm.Edit_Click] NewSequence Count {0}, prevCount {1}", newSequence.GetOperations().Count, JSONHandler.GetTest(testName).GetSequence(sequenceName).GetOperations().Count);
                    test.RemoveFromList(seqIndex);
                    test.AddToList(newSequence, index);
                    JSONHandler.Tests.SaveTest(test);
                    RefreshList();
                }).ShowDialog();
            }
        }

        private void HandleFormClosing(Object sender, FormClosingEventArgs e) {
            if (runningTest && e.CloseReason != CloseReason.WindowsShutDown) {
                switch (MessageBox.Show(this, "A test is still running, are you sure you wish to close the application before it has finished?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) {
                    case DialogResult.No:
                        e.Cancel = true;
                        return;
                }
            }
            Environment.Exit(Environment.ExitCode);
        }
        
        /** Draw / GUI **/

        private void DisableButtons() {
            buttonMoveUp.Enabled = false;
            buttonMoveDown.Enabled = false;
            buttonAddSequence.Enabled = false;
            buttonDelete.Enabled = false;
            buttonRunTest.Enabled = false;
            //buttonBack.Enabled = false;
            buttonEdit.Enabled = false;
            this.runningTest = true;
        }

        private void EnableButtons() {
            MethodInvoker m = new MethodInvoker(() => {
                buttonMoveUp.Enabled = true;
                buttonMoveDown.Enabled = true;
                buttonAddSequence.Enabled = true;
                buttonDelete.Enabled = true;
                buttonRunTest.Enabled = true;
                buttonBack.Enabled = true;
                buttonEdit.Enabled = true;
                this.runningTest = false;
            });
            try {
                buttonMoveUp.Invoke(m);
                buttonMoveDown.Invoke(m);
                buttonAddSequence.Invoke(m);
                buttonDelete.Invoke(m);
            } catch { }
            
        }

        public void UpdateBar(int i) {
            MethodInvoker m = new MethodInvoker(() => {
                progressBar.Value = i;
            });
            try {
                progressBar.Invoke(m);
            } catch { }
        }

        public void UpdateBar(int i, Action<bool> callback) {
            MethodInvoker m = new MethodInvoker(() => {
                progressBar.Value = i;
                callback(progressBar.Value == i);
            });
            try {
                progressBar.Invoke(m);
            } catch { }
        }

        public void UpdateBarColour(Color c) {
            try {
                MethodInvoker m = new MethodInvoker(() => {
                    progressBar.ForeColor = c;

                    int state = 1;
                    if (c.Equals(Color.Red)) {
                        state = 2;
                    } else if (c.Equals(Color.Yellow)) {
                        state = 3;
                    }
                    GUI_Utilities.SetState(progressBar, state);
                });
                progressBar.Invoke(m);
            } catch { }
        }

        /** Testing / Mock / Utility / Other **/

        private void MockProgress() {
            Random random = new Random();
            bool set1 = false;
            bool set2 = false;
            bool set3 = false;
            for (int i = 0; i <= 100; i++) {
                UpdateBar(i);
                if (i >= 30 && !set1 && JSONHandler.Sequences.ReadSequences(testName).Count > 0) {
                    Console.WriteLine("Trying to set " + JSONHandler.Sequences.ReadSequences(testName)[0].GetName());
                    try {
                        UpdateStatus(0, "Success!");
                        set1 = true;
                    } catch { }
                }
                if (i >= 80 && !set2 && JSONHandler.Sequences.ReadSequences(testName).Count > 1) {
                    Console.WriteLine("Trying to set " + JSONHandler.Sequences.ReadSequences(testName)[1].GetName());
                    UpdateStatus(1, "Success!");
                    set2 = true;
                }
                if (i >= 90 && !set3 && JSONHandler.Sequences.ReadSequences(testName).Count > 2) {
                    Sequence seq = JSONHandler.Sequences.ReadSequences(testName)[2];
                    Console.WriteLine("Trying to set " + seq.GetName());
                    String stat = seq.GetName() + " failed running " + seq.GetOperations()[0].GetName() + ", expected " + seq.GetOperations()[0].GetExpected() + ", but received: " + "xyz";
                    UpdateStatus(2, stat);
                    set3 = true;
                }
                int rand = random.Next(0, 100);
                if (rand >= 80) {
                    i += 2;
                } else if (rand >= 50) {
                    i++;
                }
                Thread.Sleep(random.Next(50, 150));
            }
            UpdateBarColour(Color.Red);
        }

        private void buttonBack_Click(object sender, EventArgs e) {
            if (buttonBack.Text.Equals("Stop")) {
                MethodInvoker invoker = new MethodInvoker(() => {
                    buttonBack.Enabled = false;
                    runningTest = false;
                });
                buttonBack.Invoke(invoker);
            }
        }
    }

}