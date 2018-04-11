using System;
using System.Windows.Forms;

using Testing_Framework.Components;
using Testing_Framework.Components.Operations;
using Testing_Framework.FileHandling;

namespace Testing_Framework.GUI {

    public partial class OperationVigoForm : Form {

        private Action<String, Operation> callback;         //Callback with previous name (Used if it has changed), and the current operation
        private Operation op;

        public OperationVigoForm(Action<String, Operation> callback) {
            this.callback = callback;
            InitializeComponent();
            CenterToScreen();
        }
        
        public OperationVigoForm(Operation operation, Action<String, Operation> callback) {
            this.callback = callback;
            this.op = operation;
            InitializeComponent();
            operationName.Text = this.op.GetName();
            operationURL.Text = this.op.GetURL();
            operationValue.Text = this.op.GetExpected().ToString();
            CenterToScreen();
            if (this.op.GetType().Name.Equals("Sleep")) {
                operationURL.ReadOnly = true;
                radioButtonRead.Checked = false;
                radioButtonRead.Enabled = false;
                radioButtonWrite.Checked = false;
                radioButtonWrite.Enabled = false;
            } else {
                radioButtonRead.Checked = !operation.GetWrite();
                radioButtonWrite.Checked = operation.GetWrite();
            }
        }
        
        private void ButtonSave_Click(object sender, EventArgs e) {
            String name = operationName.Text;
            String url = operationURL.Text;
            object expected = operationValue.Text;
            bool write = radioButtonWrite.Checked;
            if (name.Equals("") || url.Equals("") && (op == null || !op.GetType().Name.Equals("Sleep")) || expected.Equals("")) {
                MessageBox.Show("Please fill out all fields before continuing.");
                return;
            }
            if (name.Equals("Sleep") && !op.GetType().Name.Equals("Sleep")) {
                MessageBox.Show("Sleep is an Operation keyword. Please use a different name!");
                return;
            }
            if (op != null && op.GetType().Name.Equals("Sleep")) {
                try {
                    expected = Convert.ToInt32(expected.ToString());
                } catch {
                    MessageBox.Show("Sleep must have a numeric timeout.");
                    return;
                }
            }
            Operation operation = new Operation(name, Uri.EscapeUriString(url), expected, write);
            if (op != null && op.GetType().Name.Equals("Sleep")) {
                operation = new Sleep(Convert.ToInt32(expected));
            }
            JSONHandler.Operations.SaveOperation(operation);
            /*
            if (!JSONHandler.OperationExists(name)) {
                JSONHandler.SaveOperation(operation);
            } else {
                MessageBox.Show("An operation with the name: " + name + " already exists!");
                return;
            }
            */
            String thisName = op != null ? op.GetName() : operation.GetName();
            callback?.Invoke(thisName, operation);
            this.Close();
        }

        public void LoadOperation(String testName, String sequenceName, String name) {
            op = JSONHandler.Tests.GetTest(testName).GetSequence(sequenceName).GetOperation(name);
            operationName.Text = name;
            operationURL.Text = op.GetURL();
            operationValue.Text = op.GetExpected().ToString();
            Console.WriteLine(op.GetType().Name);
            if (op.GetType().Name.Equals("Sleep")) {
                operationURL.ReadOnly = true;
            }
        }

        private void OperationURL_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                ButtonSave_Click(null, null);
            } else if (e.KeyCode == Keys.Escape) {
                this.Close();
            }
        }

        private void OperationValue_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                ButtonSave_Click(null, null);
            } else if (e.KeyCode == Keys.Escape) {
                this.Close();
            }
        }

        private void OperationName_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                ButtonSave_Click(null, null);
            } else if (e.KeyCode == Keys.Escape) {
                this.Close();
            }
        }

        private void OperationURL_Click(object sender, EventArgs e) {
            if (!operationURL.ReadOnly) {
                if (operationURL.Text.Equals("")) {
                    new FormMibOcx((partialID, value, name) => {
                        operationURL.Text = partialID;
                        if (operationValue.Text == "") {
                            operationValue.Text = value;
                        }
                        if (operationName.Text == "") {
                            operationName.Text = name;
                        }
                    }).ShowDialog();
                } else {
                    new FormMibOcx((partialID, value, name) => {
                        operationURL.Text = partialID;
                        if (operationValue.Text == "") {
                            operationValue.Text = value;
                        }
                        if (operationName.Text == "") {
                            operationName.Text = name;
                        }
                    }, operationURL.Text).ShowDialog();
                }
            }
        }

        private void RadioButtonRead_CheckedChanged(object sender, EventArgs e) {
            labelValue.Text = "Expected Value";
        }

        private void RadioButtonWrite_CheckedChanged(object sender, EventArgs e) {
            labelValue.Text = "Written Value";
        }

    }

}