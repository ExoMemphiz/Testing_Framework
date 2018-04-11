using System;
using System.Collections.Generic;
using System.Linq;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Testing_Framework.Exceptions;

namespace Testing_Framework.Components {

    class Test {

        [JsonProperty]
        private String testName;
        [JsonProperty]
        private List<Sequence> sequences;
        
        public Test(String testName) {
            this.testName = testName;
            this.sequences = new List<Sequence>();
        }

        public void AddToList(Sequence sequence) {
            this.sequences.Add(sequence);
        }

        public void AddToList(Sequence sequence, int index) {
            this.sequences.Insert(index, sequence);
        }

        public void AddToList(List<Sequence> sequences) {
            foreach (Sequence s in this.sequences) {
                AddToList(s);
            }
        }

        public void RemoveFromList(Sequence sequence) {
            this.sequences.Remove(sequence);
        }

        public void RemoveFromList(int index) {
            this.sequences.RemoveAt(index);
        }

        public void RemoveFromList(String name) {
            var query = from s in sequences where s.GetName().Equals(name) select s;
            List<Sequence> removeList = query.ToList<Sequence>();
            this.sequences = this.sequences.Except(removeList).ToList();
        }

        public void UpdateSequence(int index, Sequence sequence) {
            this.sequences[index] = sequence;
        }

        public void SwapIndex(int n1, int n2) {
            Sequence tempSeq = this.sequences[n1];
            this.sequences[n1] = this.sequences[n2];
            this.sequences[n2] = tempSeq;
        }

        public void RunTest() {
            RunTest(null);
        }

        public void RunTest(Action<Sequence, int, ComparisonFailException> callback) {
            for (int i = 0; i < this.sequences.Count; i++) {
                Sequence seq = this.sequences[i];
                seq.ExecuteOperations(callback);
            }
        }

        public String GetName() {
            return this.testName;
        }

        public List<Sequence> GetSequences() {
            return this.sequences;
        }

        public Sequence GetSequence(String name) {
            foreach (Sequence s in this.sequences) {
                if (s.GetName().Equals(name)) {
                    return s;
                }
            }
            throw new Exception("No sequence with the name: " + name + " found in test: " + this.testName);
        }
        
        public JObject GetReportJSON() {

            JObject obj = new JObject();
            JArray arr = new JArray();

            this.sequences.ForEach((sequence) => {
                arr.Add(sequence.GetReportJSON());
            });
           
            obj[this.GetName()] = arr;

            return obj;
        }

    }

}