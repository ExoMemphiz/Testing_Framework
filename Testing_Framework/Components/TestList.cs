using System;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace Testing_Framework.Components {

    class TestList {

        [JsonProperty]
        private String listName;

        [JsonProperty]
        private List<Test> tests;

        public TestList(String listName) {
            this.listName = listName;
            this.tests = new List<Test>();
        }

        public void AddToList(List<Test> tests) {
            foreach (Test t in tests) {
                this.tests.Add(t);
            } 
        }

        public void AddToList(Test t) {
            this.tests.Add(t);
        }

        public void RemoveFromlist(Test t) {
            this.tests.Remove(t);
        }

        public void RemoveFromlist(int index) {
            this.tests.RemoveAt(index);
        }

        public void SwapIndex(int n1, int n2) {
            Test temp = this.tests[n1];
            this.tests[n1] = this.tests[n2];
            this.tests[n2] = temp;
        }

        public String GetName() {
            return this.listName;
        }

        public Test GetTest(int index) {
            return this.tests[index];
        }

        public List<Test> GetTests() {
            return this.tests;
        }

        public void RunTests() {
            foreach (Test t in this.tests) {
                t.RunTest();
            }
        }

    }

}