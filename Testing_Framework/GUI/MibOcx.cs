using System;
using System.Windows.Forms;
using Testing_Framework.DataHandling;

namespace Testing_Framework.GUI {

    public partial class FormMibOcx : Form {

        private const String CONN_ERROR_TEXT = "Vigo Server not running!";

        private Action<String, String, String> callback;
        private bool connectionError;

        public FormMibOcx() {
            this.connectionError = false;
            InitializeComponent();
        }

        public FormMibOcx(Action<String, String, String> callback) {
            this.callback = callback;
            this.connectionError = false;
            InitializeComponent();
        }

        public FormMibOcx(Action<String, String, String> callback, String startValue) {
            this.callback = callback;
            this.connectionError = false;
            InitializeComponent();
            this.axMibocx2.PhysId = startValue;
        }

        private void ButtonCancel_Click(object sender, EventArgs e) {
            Close();
        }
        
        private void FormMibOcx_Load(object sender, EventArgs e) {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void ButtonOK_Click_1(object sender, EventArgs e) {
            String value = /* textBoxValue.Text == CONN_ERROR_TEXT ? "" : */ textBoxValue.Text;
            int index = textBoxPhysicalID.Text.IndexOf(":") + 1;
            String name = textBoxPhysicalID.Text.Substring(index).Replace(".", " ");
            this.callback(textBoxPhysicalID.Text, value, name);
            this.Close();
        }

        private void AxMibocx2_SelChange(object sender, EventArgs e) {
            String physID = axMibocx2.PhysId.ToString();
            textBoxPhysicalID.Text = physID;
            try {
                if (!connectionError) {
                    textBoxValue.Text = VigoHandling.GetValueAsString(physID);
                }
            } catch {
                connectionError = true;
                MessageBox.Show("You must run the Vigo Server to retrieve the values!");
                textBoxValue.Text = CONN_ERROR_TEXT;
            }
        }

    }

}