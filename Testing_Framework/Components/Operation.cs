using System;
using System.Text.RegularExpressions;
using System.Threading;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Testing_Framework.Components {

    public class Operation {

        [JsonProperty]
        private String operationName, url;
        [JsonProperty]
        private object expected, received;
        [JsonProperty]
        private bool write;

        private bool passed;

        public Operation(String operationName, String url, object expected) {
            this.operationName = operationName;
            this.url = url.Replace("%20", " ");
            this.expected = expected;
        }

        [JsonConstructor]
        public Operation(String operationName, String url, object expected, bool write) {
            this.operationName = operationName;
            this.url = url.Replace("%20", " ");
            this.expected = expected;
            this.write = write;
        }

        //Handle Vigo
        public virtual bool RunOperation() {
            if (write) {
                return DataHandling.VigoHandling.SetValue(url, this.expected);
            }
            this.received = DataHandling.VigoHandling.GetValueAsString(url);
            if (this.expected == null || received == null) {
                return false;
            }
            if (this.expected.ToString().Contains("-") && this.expected.ToString().IndexOf("-") > 0) {
                if (IsInRange(this.expected.ToString(), this.received.ToString())) {
                    this.passed = true;
                }
            } else if (expected.ToString().Equals(this.received.ToString())) {
                //Console.WriteLine("Test Passed, Expected value == Received");
                this.passed = true;
            } else if (this.expected.ToString().Contains("<") && this.expected.ToString().IndexOf("<") == 0) {
                String expectedVal = this.expected.ToString().Replace("<", "");
                double expectedDouble, receivedDouble;
                try {
                    expectedDouble = CustomDoubleParse(expectedVal);
                    receivedDouble = CustomDoubleParse(this.received.ToString());
                } catch (Exception e) {
                    throw e;
                }
                this.passed = receivedDouble < expectedDouble;
            } else if (this.expected.ToString().Contains(">") && this.expected.ToString().IndexOf(">") == 0) {
                String expectedVal = this.expected.ToString().Replace(">", "");
                double expectedDouble, receivedDouble;
                try {
                    expectedDouble = CustomDoubleParse(expectedVal);
                    receivedDouble = CustomDoubleParse(this.received.ToString());
                } catch (Exception e) {
                    throw e;
                }
                this.passed = receivedDouble > expectedDouble;
            }
            //Console.WriteLine("Test Failed, Expected value: {0} {1} != Received: {2} {3}", this.expected, this.expected.GetType(), received, received.GetType());
            return this.passed;
        }
        
        private double CustomDoubleParse(String s) {
            String decimalSetter = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            String seperatorTReplace = decimalSetter.Equals(".") ? "," : ".";
            return Double.Parse(s.Replace(seperatorTReplace, decimalSetter));
        }

        private Boolean IsInRange(String expected, String received) {
            String decimalSetter = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            String seperatorTReplace = decimalSetter.Equals(".") ? "," : ".";

            double min = Double.Parse(Regex.Replace(expected.Substring(0, expected.IndexOf("-")).Replace(seperatorTReplace, decimalSetter), @"\s+", ""));
            double max = Double.Parse(Regex.Replace(expected.Substring(expected.IndexOf("-") + 1).Replace(seperatorTReplace, decimalSetter), @"\s+", ""));
            double receiveNum = Double.Parse(received.ToString().Replace(seperatorTReplace, decimalSetter));
            //Console.WriteLine("[Operation.IsInRange] min: {0}, max: {1}, receiveNum: {2}, receiveNumParse: {3}", min, max, received.ToString().Replace(seperatorTReplace, decimalSetter), receiveNum);
            return receiveNum >= min && receiveNum <= max;
        }

        public String GetName() {
            return this.operationName;
        }

        public object GetExpected() {
            return this.expected;
        }

        public String GetURL() {
            return this.url;
        }

        public object GetReceived() {
            return this.received;
        }

        public bool GetWrite() {
            return this.write;
        }

        public bool GetPassed() {
            return this.passed;
        }

        public void SetName(String name) {
            this.operationName = name;
        }

        public JObject GetReportJSON() {
            JObject obj = new JObject();
            obj.Add(new JProperty("Operation Name", this.operationName));
            obj.Add(new JProperty("Operation URL", this.url));
            obj.Add(new JProperty("Expected", this.expected));
            obj.Add(new JProperty("Received", this.received));
            obj.Add(new JProperty("Passed", this.passed));
            Console.WriteLine("Operation Debug GetReportJSON(): " + obj.ToString());

            return obj;
        }

    }

}