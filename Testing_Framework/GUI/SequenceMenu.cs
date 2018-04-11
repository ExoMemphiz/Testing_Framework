using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Testing_Framework.Components;
using Testing_Framework.FileHandling;

namespace Testing_Framework.GUI {

    public partial class SequenceMenu : Form {

        private String testName;
        private Action<Sequence> callback;

        public SequenceMenu(String testName, Action<Sequence> callback) {
            this.testName = testName;
            this.callback = callback;
            InitializeComponent();
            PopulateSequences();
            CenterToScreen();
        }

        public void PopulateSequences() {
            selectedSequence.Items.Clear();
            foreach (Sequence o in JSONHandler.Sequences.ReadSequences()) {
                selectedSequence.Items.Add(o.GetName());
                selectedSequence.AutoCompleteCustomSource.Add(o.GetName());
            }
            if (selectedSequence.Items.Count > 0) {
                selectedSequence.SelectedIndex = 0;
            }
        }

        private void ButtonCreate_Click(object sender, EventArgs e) {
            String sequenceNameDialog = Prompt.ShowDialog("Input New Sequence Name", "New Sequence Name");
            if (sequenceNameDialog == null) {
                return;
            }
            if (sequenceNameDialog == "") {
                MessageBox.Show("Sequence name cannot be empty!");
                return;
            }
            Console.WriteLine("Creating new Sequence with name: {0}", sequenceNameDialog);
            try {
                JSONHandler.Tests.GetTest(testName).GetSequence(sequenceNameDialog);
                MessageBox.Show("A sequence with the name " + sequenceNameDialog + " already exists!");
                return;
            } catch {
                callback(new Sequence(sequenceNameDialog, 0, false));
            }
            this.Close();
        }

        private void ButtonSelect_Click(object sender, EventArgs e) {
            var item = selectedSequence.SelectedItem;
            List<Sequence> seqs = JSONHandler.Sequences.ReadSequences();
            Sequence seq = null;
            foreach (Sequence s in seqs) {
                if (s.GetName().Equals(item)) {
                    seq = s;
                }
            }
            if (seq != null) {
                callback(seq);
                this.Close();
            }
        }
    }

    static class Prompt {

        public static string ShowDialog(string text, string caption) {
            Form prompt = new Form() {
                Width = 250,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 20, Top = 20, Width = 200, Text = text };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 200 };
            Button confirmation = new Button() { Text = "Ok", Left = 20, Width = 70, Top = 80, DialogResult = DialogResult.OK };
            Button cancel = new Button() { Text = "Cancel", Left = 151, Width = 70, Top = 80, DialogResult = DialogResult.Cancel };
            confirmation.Click += (sender, e) => {
                prompt.Close();
            };
            cancel.Click += (sender, e) => {
                prompt.Close();
                return;
            };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(cancel);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : null;
        }

    }

}