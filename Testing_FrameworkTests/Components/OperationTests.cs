using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Testing_Framework.Components.Tests {

    [TestClass()]
    public class OperationTests {

        private String physID;

        [TestInitialize()]
        public void Initialize() {
            this.physID = "Commander6:Master_Box_1.Pacific.Pacific_127.";
        }

        [TestMethod()]
        public void RunFailTest() {
            String operationName = "TestOperationFail";
            String url = "Probe_01.Temperature.Measurement.Temperature.Alarms[0]";
            object expected = false;
            Operation op = new Operation(operationName, url, expected);
            Assert.IsFalse(op.RunOperation(), "Expected " + expected.ToString() + ", but received: " + op.GetReceived().ToString());
        }

        [TestMethod()]
        public void RunPassTest() {
            String operationName = "TestOperationPass";
            String url = "MIB_05";
            object expected = 1;
            Operation op = new Operation(operationName, physID + url, expected);
            Assert.IsTrue(op.RunOperation(), "Expected " + expected.ToString() + ", but received: " + op.GetReceived().ToString());
        }

        [TestMethod()]
        public void RunPassRangeTest() {
            String operationName = "TestOperationRangePass";
            String url = "Probe_01.Temperature.Measurement.Temperature.Value";
            object expected = "22-28";
            Operation op = new Operation(operationName, physID + url, expected);
            Assert.IsTrue(op.RunOperation(), "Expected " + expected.ToString() + ", but received: " + op.GetReceived().ToString());
        }

        [TestMethod()]
        public void RunFailRangeTest() {
            String operationName = "TestOperationRangeFail";
            String url = "Probe_01.Temperature.Measurement.Temperature.Value";
            object expected = "18-22";
            Operation op = new Operation(operationName, physID + url, expected);
            Assert.IsFalse(op.RunOperation(), "Expected " + expected.ToString() + ", but received: " + op.GetReceived().ToString());
        }

        [TestMethod()]
        public void RunCommaRangeTest() {
            String operationName = "TestOperationRangeFail";
            String url = "Probe_01.Temperature.Measurement.Temperature.Value";
            object expected = "22,4-28,3";
            Operation op = new Operation(operationName, physID + url, expected);
            Assert.IsTrue(op.RunOperation(), "Expected " + expected.ToString() + ", but received: " + op.GetReceived().ToString());
        }

        [TestMethod()]
        public void RunDotRangeTest() {
            String operationName = "TestOperationRangeFail";
            String url = "Probe_01.Temperature.Measurement.Temperature.Value";
            object expected = "22.4-28.3";
            Operation op = new Operation(operationName, physID + url, expected);
            Assert.IsTrue(op.RunOperation(), "Expected " + expected.ToString() + ", but received: " + op.GetReceived().ToString());
        }

    }

}