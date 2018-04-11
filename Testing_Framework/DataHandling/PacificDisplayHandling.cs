using System;
using System.Threading;

namespace Testing_Framework.DataHandling {

    public class PacificDisplayHandling {

        private static CustomPoint prevPoint = null;

        /// <summary>
        /// Clicks the n'th menu item.
        /// </summary>
        /// <param name="n">Which n'th item to click</param>
        /// <exception cref="ArgumentOutOfRangeException">Menu items' index start at 1</exception>
        public static void ClickMenuItem(int n) {
            if (n < 1) {
                throw new ArgumentOutOfRangeException("Menu items' index start at 1");
            }
            SecureClick(120, 70 + 65 * (n - 1));
            Thread.Sleep(70);
        }

        /// <summary>
        /// Clicks the taskbar item. (The persistant menu on the right-hand side)
        /// </summary>
        /// <param name="n">The n.</param>
        /// <exception cref="ArgumentOutOfRangeException">Menu items' index start at 1</exception>
        /// <exception cref="Exception">Menu has not changed!</exception>
        public static void ClickTaskbarItem(int n) {
            if (n < 1) {
                throw new ArgumentOutOfRangeException("Menu items' index start at 1");
            }
            String menu = WebHandling.GetMenuNumber();
            SecureClick(750, 50 + 90 * (n - 1));
            for (int i = 0; i < 100; i++) {
                Thread.Sleep(50);
                if (!menu.Equals(WebHandling.GetMenuNumber())) {
                    break;
                }
            }
            if (menu.Equals(WebHandling.GetMenuNumber())) {
                throw new Exception("Menu has not changed!");
            }
        }

        public static void ClickProbe(int n) {
            if (n < 1) {
                throw new ArgumentOutOfRangeException("Menu items' index start at 1");
            }
            int xMul = (n - 1) % 3;
            bool yAdd = (int)((n - 1) / 3) >= 1;
            int x = 100 + 250 * xMul;
            int y = 100 + (yAdd ? 150 : 0);
            String menu = WebHandling.GetMenuNumber();
            SecureClick(x, y);
            for (int i = 0; i < 100; i++) {
                Thread.Sleep(50);
                if (!menu.Equals(WebHandling.GetMenuNumber())) {
                    break;
                }
            }
            if (menu.Equals(WebHandling.GetMenuNumber())) {
                throw new Exception("Menu has not changed!");
            }
        }

        public static void SecureClick(int x, int y) {
            bool correct = false;
            if (prevPoint == null) {
                Console.WriteLine("PrevPoint == null, trying to click at: {0}, {1}", x, y);
                for (int i = 0; i < 3; i++) {
                    WebHandling.ResetCursor();
                    bool resetCursor = false;
                    for (int j = 0; j < 1000; j++) { //Max 1 second timeout
                        Thread.Sleep(1);
                        if (IsMouseCorrectPosition(0, 0)) {
                            resetCursor = true;
                            break;
                        }
                    }
                    if (resetCursor) {
                        WebHandling.MoveMouseRelative(x, y);
                        int count = 0;
                        for (int j = 0; j < 1000; j++) {     //Max 1 second timeout
                            Thread.Sleep(10);
                            if (IsMouseCorrectPosition(x, y)) {
                                if (count >= 3) {
                                    correct = true;
                                    break;
                                }
                                count++;
                            }
                        }
                    }
                    if (correct) {
                        break;
                    }
                }
                if (!correct) {
                    throw new Exception("Could not move mouse to absolute Position (" + x + ", " + y + ")");
                }
                WebHandling.ClickAtCursor();
                prevPoint = new CustomPoint(x, y);
            } else {
                int newX = x - prevPoint.GetX();
                int newY = y - prevPoint.GetY();
                //Console.WriteLine("newX: {0}, newY: {1}, x: {2}, y: {3}, prevX: {4}, prevY: {5}", newX, newY, x, y, prevPoint.GetX(), prevPoint.GetY());
                WebHandling.MoveMouseRelative(newX, newY);
                for (int j = 0; j < 1000; j++) {     //Max 1 second timeout
                    //Thread.Sleep(1);
                    if (IsMouseCorrectPosition(x, y)) {
                        correct = true;
                        break;
                    } else {
                        //Console.WriteLine("Mouse is not in correct Position, should be: {0}, {1}  is: {2}, {3}", x, y, readX, readY);
                    }
                }
                if (!correct) {
                    throw new Exception("Could not move mouse to absolute Position (" + newX + ", " + newY + "), relative Position (" + x + ", " + y + ")");
                }
                WebHandling.ClickAtCursor();
                prevPoint = new CustomPoint(x, y);
            }
        }

        private static bool IsMouseCorrectPosition(int x, int y) {
            return x.ToString().Equals(WebHandling.GetMouseX()) && y.ToString().Equals(WebHandling.GetMouseY());
        }

        private class CustomPoint {

            private int x, y;

            public CustomPoint() {

            }

            public CustomPoint(int x, int y) {
                this.x = x;
                this.y = y;
            }

            public int GetX() {
                return x;
            }

            public int GetY() {
                return y;
            }

        }

        public class KeypadHandler {

            public static void ClickKeypad(String s) {
                KeypadKeys[] keys;
                try {
                    keys = new KeypadKeys[] {
                        GetKey(s)
                    };
                } catch {
                    char[] chars = s.ToCharArray();
                    keys = new KeypadKeys[chars.Length];
                    for (int i = 0; i < chars.Length; i++) {
                        keys[i] = GetKey(chars[i].ToString());
                    }
                }
                ClickKeypad(keys);
            }

            public static void ClickKeypad(params KeypadKeys[] keys) {
                foreach (KeypadKeys key in keys) {
                    ClickKeypad(key);
                }
            }

            public static void ClickKeypad(KeypadKeys key) {
                String[,] arr2D = new String[4, 4] {
                    {"Del", "One", "Two", "Three"},
                    {"Clr", "Four", "Five", "Six"},
                    {null, "Seven", "Eight", "Nine"},
                    {null, "Dot", "Zero", "Dash"}
                };
                CustomPoint p = new CustomPoint(0, 0);
                for (int x = 0; x < arr2D.GetLength(0); x++) {
                    for (int y = 0; y < arr2D.GetLength(1); y++) {
                        String s = arr2D[y, x];
                        if (s != null && s.Equals(key.ToString())) {
                            p = new CustomPoint(x, y);
                            goto breakLoops;
                        }
                    }
                }
                breakLoops:
                SecureClick(150 + (p.GetX() * 140), 110 + (p.GetY() * 75));
                Thread.Sleep(100);
            }

            /// <summary>
            /// Select which input field should be selected (1 -> 4, left -> right)
            /// </summary>
            /// <param name="n"></param>
            public static void SelectKeypadInput(int n) {
                if (n < 1) {
                    throw new ArgumentOutOfRangeException("Menu items' index start at 1");
                }
                String currentlySelected = WebHandling.GetPropertyFromURL("http://127.0.0.1:5000/pacific/symbols/enter_ip_selected_box", WebHandling.METHOD_GET, "value");
                if (!currentlySelected.Equals((n - 1).ToString())) {
                    Console.WriteLine("Changing selected input from {0} to {1}", currentlySelected, (n - 1));
                    SecureClick(210 + 100 * (n - 1), 50);
                    Thread.Sleep(100);
                } else {

                }
            }

            /// <summary>
            /// 
            /// </summary>
            public enum KeypadKeys {

                One,
                Two,
                Three,
                Four,
                Five,
                Six,
                Seven,
                Eight,
                Nine,
                Zero,
                Dot,
                Del,
                Clr,
                Dash

            }

            public static KeypadKeys GetKey(int i) {
                return GetKey("" + i);
            }

            public static KeypadKeys GetKey(String s) {
                switch (s.ToLower()) {
                    case "1":
                        return KeypadKeys.One;
                    case "2":
                        return KeypadKeys.Two;
                    case "3":
                        return KeypadKeys.Three;
                    case "4":
                        return KeypadKeys.Four;
                    case "5":
                        return KeypadKeys.Five;
                    case "6":
                        return KeypadKeys.Six;
                    case "7":
                        return KeypadKeys.Seven;
                    case "8":
                        return KeypadKeys.Eight;
                    case "9":
                        return KeypadKeys.Nine;
                    case "0":
                        return KeypadKeys.Zero;
                    case ".":
                        return KeypadKeys.Dot;
                    case "dot":
                        return KeypadKeys.Dot;
                    case "del":
                        return KeypadKeys.Del;
                    case "delete":
                        return KeypadKeys.Del;
                    case "clr":
                        return KeypadKeys.Clr;
                    case "clear":
                        return KeypadKeys.Clr;
                    case "dash":
                        return KeypadKeys.Dash;
                    case "-":
                        return KeypadKeys.Dash;

                }
                throw new ArgumentException(s + " is not a valid Keypad key!");
            }

        }

        public class ScrollbarHandler {
            
            /// <summary>
            /// Calculates the amount of scrolls needed, scrolls, and then clicks the item numbered 'n'
            /// </summary>
            public static int ClickMenuItem(int n) {
                if (n < 1) {
                    throw new ArgumentOutOfRangeException("Menu items' index start at 1");
                }
                int scrolls = (int)Math.Floor((double)(n - 1) / 5);
                for (int i = 0; i < scrolls; i++) {
                    Scroll(true);
                }
                int index = (n - 1) % 5;
                int x = 300;
                int y = 75 + 80 * index;
                SecureClick(x, y);
                Console.WriteLine("Scrolls needed: {0}, index: {1}, x: {2}, y: {3}", scrolls, index, x, y);
                Thread.Sleep(70);
                return scrolls;
            }

            public static void Scroll(bool down) {
                int x = 680;
                int y = down ? 380 : 100;
                SecureClick(x, y);
                Thread.Sleep(70);
            }
            
            /// <summary>
            /// Scrolls up n times
            /// </summary>
            /// <param name="n"></param>
            public static void ResetScroll(int n) {
                for (int i = 0; i < n; i++) {
                    Scroll(false);
                }
            }

        }

        public class ConfigurationHandler {

            public static void SetTab(int n) {
                if (n < 1) {
                    throw new ArgumentOutOfRangeException("Tab items' index start at 1");
                }
                int x = 150 + 200 * (n - 1);
                int y = 75;
                SecureClick(x, y);
                Thread.Sleep(250);
            }

            public static void ClickMenuItem(int n) {
                if (n < 1) {
                    throw new ArgumentOutOfRangeException("Menu items' index start at 1");
                }
                int x = 200;
                int y = 140 + 65 * (n - 1);
                SecureClick(x, y);
                String menu = WebHandling.GetMenuNumber();
                for (int i = 0; i < 100; i++) {
                    Thread.Sleep(50);
                    if (!menu.Equals(WebHandling.GetMenuNumber())) {
                        break;
                    }
                }
                if (menu.Equals(WebHandling.GetMenuNumber())) {
                    throw new Exception("Menu has not changed!");
                }
            }

        }

        public class ProbeHandler {

            public static void SetTab(int n) {
                if (n < 1) {
                    throw new ArgumentOutOfRangeException("Tab items' index start at 1");
                }
                int x = 320 * (n - 1);
                int y = 70;
                SecureClick(x, y);
                Thread.Sleep(250);
            }

            public static void ClickMenuItem(int n) {
                if (n < 1) {
                    throw new ArgumentOutOfRangeException("Tab items' index start at 1");
                }
                int x = 120;
                int y = 300 + 70 * (n - 1);
                SecureClick(x, y);
                Thread.Sleep(300);
            }
                 
        }

        public class AlarmHandler {

            public static void SetTab(int n) {
                if (n < 1) {
                    throw new ArgumentOutOfRangeException("Tab items' index start at 1");
                }
                int x = 100 + 150 * (n - 1);
                int y = 140;
                SecureClick(x, y);
                Thread.Sleep(300);
            }

            public static void ClickMenuItem(int n) {
                if (n < 1) {
                    throw new ArgumentOutOfRangeException("Tab items' index start at 1");
                }
                int x = 120;
                int y = 200 + 60 * (n - 1);
                SecureClick(x, y);
                Thread.Sleep(500);
            }

        }

    }

}