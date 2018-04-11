using System;

using Newtonsoft.Json.Linq;

namespace Testing_Framework.Components {

    class Report {

        String reportName;
        TestList testList;

        public Report(TestList testList) {
            this.reportName = testList.GetName();
            this.testList = testList;
        }

        public JObject CreateJSON() {
            JObject report = new JObject();
            //report.Add(new JProperty("Report Name", this.reportName));

            JArray arr = new JArray();

            testList.GetTests().ForEach((Test) => {
                arr.Add(Test.GetReportJSON());
            });

            report[this.reportName] = arr;

            return report;
        }

    }

}