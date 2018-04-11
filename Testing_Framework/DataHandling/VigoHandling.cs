using System;
using VigoInterfaceMTA;

namespace Testing_Framework.DataHandling {

    public class VigoHandling {

        private static VigoInterface vigo;

        public static String GetValueAsString(String fullID) {
            return GetValueAsString(fullID, "");
        }

        public static String GetValueAsString(String physID, String partialID) {
            VigoInterface vigo = GetVigo();
            String str = "";
            vigo.GetString(physID + partialID, ref str);
            return str;
        }

        public static bool SetValue(String fullID, object value) {
            return SetValue(fullID, "", value);
        }

        public static bool SetValue(String physID, String partialID, object value) {
            VigoInterface vigo = GetVigo();
            return vigo.setValue(physID + partialID, value);
        }
        
        private static VigoInterface GetVigo() {
            if (vigo == null) {
                vigo = new VigoInterface();
            }
            return vigo;
        }

    }

}