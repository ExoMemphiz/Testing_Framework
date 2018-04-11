using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Testing_Framework.DataHandling;

namespace Testing_Framework.GUI {

    public partial class OperationPythonForm : Form {

        public OperationPythonForm() {
            InitializeComponent();
            CenterToScreen();
        }

        private void buttonSave_Click(object sender, EventArgs e) {
            /*
            "Click (Relative)",
            "Click (Absolute)",
            "Read Mouse X",
            "Read Mouse Y",
            "Read Menu ID",
            "Keypad Input"
            */

            switch (operationAction.SelectedIndex) {
                case 0:
                    Console.WriteLine("Click Relative");
                    try {
                        int x = int.Parse(operationParam1.Text);
                        int y = int.Parse(operationParam2.Text);
                        WebHandling.ClickMouseRelative(x, y);
                    } catch {
                        Console.WriteLine("Parse Error!");
                    }
                    break;
                case 1:
                    Console.WriteLine("Click Absolute");
                    try {
                        int x = int.Parse(operationParam1.Text);
                        int y = int.Parse(operationParam2.Text);
                        WebHandling.ResetCursor();
                        WebHandling.ClickMouse(x, y);
                    } catch {
                        Console.WriteLine("Parse Error!");
                    }
                    break;
                case 2:
                    Console.WriteLine("Read Mouse X");
                    break;
                case 4:
                    Console.WriteLine("Read Mouse Y");
                    break;
                case 5:
                    Console.WriteLine("Read Menu ID");
                    break;
                case 6:
                    Console.WriteLine("Keypad Input");
                    break;
                default:
                    Console.WriteLine("Please Select an action");
                    break;
            }
        }
    }
}
