using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

using Testing_Framework.Components;
using Testing_Framework.DataHandling;
using Testing_Framework.Exceptions;
using Testing_Framework.FileHandling;
using Testing_Framework.GUI;

namespace Testing_Framework {
    static class Program {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            /*
            for (int i = 0; i < 200; i++) {
                ShowcaseSetIP("192.168.80.216");
            }
            */
            /*
            bool testVal = true;
            for (int i = 0; i < 999; i++) {
                //ShowcaseSetIP("192.168.80.241");
                ShowcaseAlarm(1, "26." + i, testVal, "24." + i, !testVal);
                testVal = !testVal;
                Console.WriteLine("Finished Iteration #{0}", (i + 1));
            }
            */
            //RunApplication();
            //TestReport();
            PacificDisplayHandling.ClickTaskbarItem(1);
        }

        public static void RunApplication() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TestForm("Test"));
        }

        public static void TestReport() {

            //Set up Operations
            Operation operation = new Operation("Test Operation", "Test.com", "123");
            operation.RunOperation();
            Operation operation2 = new Operation("OpTest2", "ASd", "False");
            operation2.RunOperation();
            Operation operation3 = new Operation("Last Operation", "Commander6:Master_Box_1.Pacific.Pacific_127.MIB_05", "1");
            operation3.RunOperation();

            //Set up sequence
            Sequence seq = new Sequence("Test Sequence", 500, true);
            seq.AddToList(operation);
            seq.AddToList(operation2);

            Sequence seq2 = new Sequence("Second Sequence", -500, false);
            seq2.AddToList(operation);
            seq2.AddToList(operation3);

            Sequence seq3 = new Sequence("Lasst Sequence", 0, false);
            seq3.AddToList(operation2);
            seq3.AddToList(operation3);


            //Set up test
            Test test = new Test("Experimental Test 1");
            test.AddToList(seq);
            test.AddToList(seq2);

            Test test2 = new Test("Temperature Test");
            test2.AddToList(seq3);

            //Set up Test List
            TestList testList = new TestList("First List");
            testList.AddToList(test);
            testList.AddToList(test2);

            //Create Report
            Report report = new Report(testList);
            JSONHandler.Reports.SaveReport(report);
        }

        public static void ShowcaseSetIP(String ip) {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            PacificDisplayHandling.ClickTaskbarItem(4);
            PacificDisplayHandling.ClickMenuItem(4);
            PacificDisplayHandling.ClickMenuItem(2);
            PacificDisplayHandling.ClickMenuItem(1);
            PacificDisplayHandling.ClickMenuItem(1);
            PacificDisplayHandling.KeypadHandler.SelectKeypadInput((4 - ip.Split('.').Length) + 1);
            PacificDisplayHandling.KeypadHandler.ClickKeypad("Clear");
            PacificDisplayHandling.KeypadHandler.ClickKeypad(ip);
            PacificDisplayHandling.ClickTaskbarItem(3);
            PacificDisplayHandling.ClickTaskbarItem(4);
            watch.Stop();
            Console.WriteLine("Took {0} seconds to set the ip.", ((double)watch.ElapsedMilliseconds) / 1000);
        }

        public static void ShowcaseAlarm(int probeNumber, String highAlarmSetpoint, bool enableHighAlarm, String lowAlarmSetpoint, bool enableLowAlarm) {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            PacificDisplayHandling.ClickProbe(probeNumber);
            PacificDisplayHandling.ProbeHandler.ClickMenuItem(3);       //Clicks "Config"
            PacificDisplayHandling.ConfigurationHandler.SetTab(2);      //Clicks "Alarms/Warnings"
            PacificDisplayHandling.ConfigurationHandler.ClickMenuItem(1);   //Clicks "Setpoint" for the Alarms/Warnings Menu

            Console.WriteLine("Setting Alarm High");
            SetAlarmEnabled(probeNumber, enableHighAlarm, 1);    //<-- Complete Action, will switch tabs
            SetAlarmSetpoint(probeNumber, highAlarmSetpoint, "High");    //<-- Complete Action, will switch tabs

            Console.WriteLine("Setting Alarm High");
            SetAlarmEnabled(probeNumber, enableLowAlarm, 2);     //<-- Complete Action, will switch tabs
            SetAlarmSetpoint(probeNumber, lowAlarmSetpoint, "Low");      //<-- Complete Action, will switch tabs

            Thread.Sleep(500);

            PacificDisplayHandling.ClickTaskbarItem(4);
            Thread.Sleep(300);
            watch.Stop();
            Console.WriteLine("Took {0} seconds to set the high & low alarms.", ((double)watch.ElapsedMilliseconds) / 1000);
        }

        public static void SetAlarmEnabled(int probeNumber, bool enabled, int tabNumber) {   //1st tab = 3, 2nd = 4, 3rd = 5 and 4th = 6
            CompleteAction("SetAlarmEnabled #" + tabNumber + ": " + enabled, () => {
                Console.WriteLine("Running AlarmEnabled Action");
                PacificDisplayHandling.AlarmHandler.ClickMenuItem(1);
                Thread.Sleep(500);
            }, () => {
                //Is the value set correctly?
                String vigoString = "Commander6:Master_Box_1.Pacific.Pacific_127.Probe_0" + probeNumber + ".Temperature.Measurement.Temperature.Setpoints.Enable alarms[" + (tabNumber + 2) + "]";
                Console.WriteLine("Retrieving value from: {0}", vigoString);
                String alarmEnabled = VigoHandling.GetValueAsString(vigoString);
                Console.WriteLine("Found Vigo Value: {0}", alarmEnabled);
                bool result = alarmEnabled.Equals(enabled.ToString());
                Console.WriteLine("Running AlarmEnabled Confirmation: {0}", result);
                return result;
            }, () => {
                //Fail-safe
                Console.WriteLine("Running AlarmEnabled Fail-Safe");
                PacificDisplayHandling.AlarmHandler.SetTab(tabNumber);
                Thread.Sleep(200);
            });
        }

        public static void SetAlarmSetpoint(int probeNumber, String setpoint, String alarmType) {
            CompleteAction("SetAlarmSetpoint " + alarmType + ": " + setpoint, () => {  //High alarm Setpoint
                PacificDisplayHandling.AlarmHandler.ClickMenuItem(2);
                Thread.Sleep(300);
                PacificDisplayHandling.KeypadHandler.ClickKeypad(PacificDisplayHandling.KeypadHandler.KeypadKeys.Clr);
                PacificDisplayHandling.KeypadHandler.ClickKeypad(setpoint);
                Thread.Sleep(300);
                PacificDisplayHandling.ClickTaskbarItem(3);
                Thread.Sleep(600);
            }, () => {
                //Is the value set correctly?
                String vigoSetPoint = VigoHandling.GetValueAsString("Commander6:Master_Box_1.Pacific.Pacific_127.Probe_0" + probeNumber + ".Temperature.Measurement.Temperature.Setpoints." + alarmType + " alarm.Setpoint");
                //Console.WriteLine("High alarm Setpoint: ", highAlarm.Replace(",", ".").Equals(vigoSetPoint.Replace(",", ".")));
                return parseDoubleCustom(setpoint).Equals(parseDoubleCustom(vigoSetPoint));
            }, () => {
                //Fail-safe
                int index = alarmType.Equals("High") ? 1 : 2;
                PacificDisplayHandling.AlarmHandler.SetTab(index);
                Thread.Sleep(200);
            }, 1, 2000);
        }

        public static bool CompleteAction(String actionName, Action action, Func<bool> confirmCompletion, Action failSafe) {
            return CompleteAction(actionName, action, confirmCompletion, failSafe, 2, 1000);
        }

        public static bool CompleteAction(String actionName, Action action, Func<bool> confirmCompletion, Action failSafe, int currentAttempts, int timeout) {
            if (currentAttempts > 5) {
                throw new TooManyAttemptsException("Too many attempts, " + actionName);
            }
            if (!confirmCompletion()) {
                failSafe?.Invoke();
                Console.WriteLine("Attempt #{0} for {1}", currentAttempts, actionName);
                action();
                for (int i = 0; i < timeout / 50; i++) {
                    if (confirmCompletion()) {
                        return true;
                    }
                    Thread.Sleep(50);
                }
                CompleteAction(actionName, action, confirmCompletion, failSafe, currentAttempts + 1, timeout);
            }
            return false;
        }

        public static double parseDoubleCustom(String n) {
            String decimalSetter = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            String seperatorToReplace = decimalSetter.Equals(".") ? "," : ".";
            return Double.Parse(n.Replace(seperatorToReplace, decimalSetter));
        }

    }

}