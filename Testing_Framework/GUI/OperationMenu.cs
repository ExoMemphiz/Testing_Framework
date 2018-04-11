using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Testing_Framework.FileHandling;
using Testing_Framework.Components;

namespace Testing_Framework.GUI {

    public partial class OperationMenu : Form {

        private String testName, sequenceName;
        private Action<Operation, bool> callback;   //Callback for the updated operation, and if the sequence should add the operation to its list.

        public OperationMenu(String testName, String sequenceName, Action<Operation, bool> callback) {
            this.testName = testName;
            this.sequenceName = sequenceName;
            this.callback = callback;
            InitializeComponent();
            PopulateOperations();
            CenterToScreen();
            interfaceSelector.SelectedItem = "VigoInterface";
        }

        public void PopulateOperations() {
            selectedOperation.Items.Clear();
            //selectedOperation.Items.AddRange(JSONHandler.readOperations().OrderBy(o => o.getName()).ToArray());
            foreach (Operation o in JSONHandler.Operations.ReadOperations()) {
                selectedOperation.Items.Add(o.GetName());
                selectedOperation.AutoCompleteCustomSource.Add(o.GetName());
            }
            selectedOperation.Sorted = true;
            if (selectedOperation.Items.Count > 0) {
                selectedOperation.SelectedIndex = 0;
            }
        }

        private void ButtonSelect_Click(object sender, EventArgs e) {
            var item = selectedOperation.SelectedItem;
            List<Operation> ops = JSONHandler.Operations.ReadOperations();
            Operation op = null;
            foreach (Operation o in ops) {
                if (o.GetName().Equals(item)) {
                    op = o;
                }
            }
            if (op != null) {
                callback(op, true);
                this.Close();
            }
        }

        private void interfaceSelector_SelectedIndexChanged(object sender, EventArgs e) {
            //Change dropdown list here, to the one that supports whichever interface has been selected!
        }

        private void ButtonCreate_Click(object sender, EventArgs e) {
            //TODO: Make dependant upon which Interface was selected based on interfaceSelector combobox ---------
            //switch ()
            String selection = interfaceSelector.Items[interfaceSelector.SelectedIndex].ToString();
            
            switch (selection) {

                case "VigoInterface":
                    new OperationVigoForm((prevName, operation) => {
                        if (prevName != operation.GetName()) {
                            JSONHandler.Operations.RenameOperation(prevName, operation.GetName());
                        }
                        callback(operation, false);
                        PopulateOperations();
                        selectedOperation.SelectedItem = operation.GetName();
                    }).ShowDialog();
                    break;

                case "PythonInterface":
                    Console.WriteLine("Not yet supported");
                    new OperationPythonForm().ShowDialog();
                    break;

                default:
                    throw new NotImplementedException("No selection was made");
            }

            
        }
        
    }

}