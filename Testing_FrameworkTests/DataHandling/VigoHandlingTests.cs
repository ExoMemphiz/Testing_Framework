using Testing_Framework.DataHandling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Testing_Framework.DataHandling.Tests {

    [TestClass()]
    public class VigoHandlingTests {

        private String physID;

        [TestInitialize()]
        public void Initialize() {
            this.physID = "Commander6:Master_Box_1.Pacific.Pacific_127.";
            //Commander6:Master_Box_1.Pacific.Pacific_127.Probe_01.Temperature.Measurement.Temperature.Value
        }

        [TestMethod()]
        public void GetValueAsStringTest() {
            String partialID = "MIB_05";
            String expected = "1";
            String received = VigoHandling.GetValueAsString(physID, partialID);
            Assert.AreEqual(expected, received);
        }

        [TestMethod()]
        public void SetValueTest() {
            String partialID = "Probe_01.Temperature.Setup[0]";
            object settingValue = "True";
            bool success = VigoHandling.SetValue(physID + partialID, settingValue);
            if (!success) {
                Assert.Fail("Could not set {0} as value into the VIGO physID {1}", settingValue, (physID + partialID));
            }
            Assert.AreEqual(VigoHandling.GetValueAsString(physID + partialID), settingValue);
            VigoHandling.SetValue(physID + partialID, "False");
        }

    }

}