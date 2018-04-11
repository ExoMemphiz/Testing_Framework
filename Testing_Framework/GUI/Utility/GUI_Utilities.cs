using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Testing_Framework.GUI.Utility {

    public static class GUI_Utilities {

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);

        private static int currentState = 1;

        public static void SetState(this ProgressBar pBar, int state) {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
            currentState = state;
        }

        public static int GetCurrentState() {
            return currentState;
        }

    }

}