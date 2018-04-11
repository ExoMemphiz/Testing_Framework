using System;
using System.Net;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Testing_Framework.DataHandling {

    public static class WebHandling {

        public const String METHOD_GET = "GET";
        public const String METHOD_POST = "POST";

        private static WebClient client;

        public static String GetJSON(String url, String method) {
            return GetJSON(url, null, method);
        }

        public static String GetJSON(String url, String data, String method) {
            if (client == null) {
                client = new WebClient();
            }
            try {
                switch (method) {
                    case METHOD_GET:
                        return client.DownloadString(url);
                    case METHOD_POST:
                        return client.UploadString(url, data == null ? "" : data);
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw e;        //System.Net.WebException: 'The request was aborted: The operation has timed out.'
            }
            throw new Exception("Unsupported method, please use a valid HTTP method from the WebHandling class.");
        }

        //Mock returned JSON String
        public static String GetJSON(String url, String method, bool mock) {
            if (!mock) {
                return GetJSON(url, method);
            }
            String mockJSON = "{value: 23}";
            return mockJSON;
        }
        
        public static String GetPropertyFromURL(String url, String method, String property) {
            String json = GetJSON(url, method);
            return GetValueRecursively(DeserializeToJObject(json), property);
        }

        public static String GetMouseX() {
            String json = GetJSON("http://127.0.0.1:5000/pacific/symbols/mouse_x", METHOD_GET);
            //Console.WriteLine(json);
            return GetValueRecursively(DeserializeToJObject(json), "value");
        }

        public static String GetMouseY() {
            String json = GetJSON("http://127.0.0.1:5000/pacific/symbols/mouse_y", METHOD_GET);
            //Console.WriteLine(json);
            return GetValueRecursively(DeserializeToJObject(json), "value");
        }

        public static String ClickMouse(int x, int y) {
            return GetJSON("http://127.0.0.1:5000/pacific/click?x=" + x + "&y=" + y, METHOD_POST);
        }

        public static String ClickMouseX(int x) {
            JObject obj = DeserializeToJObject(GetJSON("http://127.0.0.1:5000/pacific/symbols/mouse_y", METHOD_GET));
            int y = Convert.ToInt32(GetValueRecursively(obj, "value"));
            return ClickMouse(x, y);
        }

        public static String ClickMouseY(int y) {
            JObject obj = DeserializeToJObject(GetJSON("http://127.0.0.1:5000/pacific/symbols/mouse_x", METHOD_GET));
            int x = Convert.ToInt32(GetValueRecursively(obj, "value"));
            return ClickMouse(x, y);
        }

        public static String ClickMouseRelative(int x, int y) {
            return GetJSON("http://127.0.0.1:5000/pacific/click/relative?x=" + x + "&y=" + y, METHOD_POST);
        }

        public static String ClickMouseRelativeX(int x) {
            JObject obj = DeserializeToJObject(GetJSON("http://127.0.0.1:5000/pacific/symbols/mouse_y", METHOD_GET));
            int y = Convert.ToInt32(GetValueRecursively(obj, "value"));
            return ClickMouseRelative(x, y);
        }

        public static String ClickMouseRelativeY(int y) {
            JObject obj = DeserializeToJObject(GetJSON("http://127.0.0.1:5000/pacific/symbols/mouse_x", METHOD_GET));
            int x = Convert.ToInt32(GetValueRecursively(obj, "value"));
            return ClickMouseRelative(x, y);
        }

        public static String ClickAtCursor() {
            return ClickMouseRelative(0, 0);
        }

        public static void ResetCursor() {
            GetJSON("http://127.0.0.1:5000/pacific/click/reset", METHOD_GET);
        }

        public static void MoveMouseRelative(int x, int y) {
            GetJSON("http://127.0.0.1:5000/pacific/move?x=" + x + "&y=" + y, METHOD_POST);
        }

        public static String GetMenuNumber() {
            return GetJSON("http://127.0.0.1:5000/pacific/current_menu", METHOD_GET);
        }

        public static JObject DeserializeToJObject(String json) {
            return (JObject)JsonConvert.DeserializeObject(json);
        }

        public static String ExecuteBash(String command) {
            String escaped = System.Uri.EscapeUriString(command);
            escaped = escaped.Replace("+", "%2B");
            Console.WriteLine("Executing command: {0}", escaped);
            return GetJSON("http://127.0.0.1:5000/pacific/bash?cmd=" + escaped, METHOD_POST);
        }

        public static String GetValueRecursively(JObject obj, String key) {
            foreach (JProperty prop in obj.Properties()) {
                if (prop.Name.Equals(key)) {
                    return prop.Value.ToString();
                } else if (prop.Value.GetType().Name.Equals("JObject")) {
                    return GetValueRecursively((JObject)prop.Value, key);
                }
            }
            throw new Exception("Could not find property " + key + " in obj: " + obj.ToString());
        }

    }

}