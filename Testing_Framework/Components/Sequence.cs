using System;
using System.Collections.Generic;
using System.Threading;

using Testing_Framework.Exceptions;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Testing_Framework.Components {

    public class Sequence {

        [JsonProperty]
        private String sequenceName;
        [JsonProperty]
        private List<Operation> operations;
        [JsonProperty]
        private int delay;
        [JsonProperty]
        private bool abort;

        public Sequence(String sequenceName, int delay, bool abort) {
            this.sequenceName = sequenceName;
            this.operations = new List<Operation>();
            this.delay = delay;
            this.abort = abort;
        }

        public void AddToList(Operation operation, int index) {
            this.operations.Insert(index, operation);
        }

        public void AddToList(Operation operation) {
            this.operations.Add(operation);
        }

        public void UpdateInList(Operation operation) {
            for (int i = 0; i < this.operations.Count; i++) {
                if (this.operations[i].GetName() == operation.GetName()) {
                    this.operations[i] = operation;
                    break;
                }
            }
        }

        public void AddToList(List<Operation> operations) {
            foreach (Operation o in this.operations) {
                AddToList(o);
            }
        }

        public void RemoveFromList(int index) {
            Operation op = this.operations[index];
            RemoveFromList(op);
        }

        public void RemoveFromList(Operation operation) {
            this.operations.Remove(operation);
        }

        public void RemoveFromList(String operationName) {
            for (int i = 0; i < this.operations.Count; i++) {
                Operation o = this.operations[i];
                if (o.GetName().Equals(operationName)) {
                    RemoveFromList(i);
                    break;
                }
            }
        }

        public void SwapIndex(int n1, int n2) {
            Operation tempOp = this.operations[n1];
            this.operations[n1] = this.operations[n2];
            this.operations[n2] = tempOp;
        }
        
        public void ExecuteOperations(Action<int> callback) {   //Callback for how many operations have been completed in this sequence
            for (int i = 0; i < this.operations.Count; i++) {
                Thread.Sleep(this.delay);
                Operation o = this.operations[i];
                if (!o.RunOperation()) {
                    String s = this.sequenceName + " failed running " + o.GetName() + " (Operation #" + (i + 1) + "), expected " + o.GetExpected() + ", but received: " + o.GetReceived();
                    String noSeq = "Failed running " + o.GetName() + " (Operation #" + (i + 1) + "), expected " + o.GetExpected() + ", but received: " + o.GetReceived();
                    String minifiedError = "Failed! Expected: " + o.GetExpected() + ", Received: " + o.GetReceived();
                    throw new ComparisonFailException(noSeq, this);
                }
                callback?.Invoke(i + 1);
            }
        }

        public void ExecuteOperations(Action<Sequence, int, ComparisonFailException> callback) {   //Callback for the name of the sequence, how many operations have been completed, and if it has completed.
            for (int i = 0; i < this.operations.Count; i++) {
                Thread.Sleep(this.delay);
                Operation o = this.operations[i];
                if (!o.RunOperation()) {
                    String s = this.sequenceName + " failed running " + o.GetName() + " (Operation #" + (i + 1) + "), expected " + o.GetExpected() + ", but received: " + o.GetReceived();
                    String noSeq = "Failed running " + o.GetName() + " (Operation #" + (i + 1) + "), expected " + o.GetExpected() + ", but received: " + o.GetReceived();
                    String minifiedError = "Failed! Expected: " + o.GetExpected() + ", Received: " + o.GetReceived();
                    if (this.abort) {
                        throw new ComparisonFailException(s, this);
                    }
                    callback?.Invoke(this, i + 1, new ComparisonFailException(noSeq, this));
                    return;
                } else {
                    callback?.Invoke(this, i + 1, null);
                }
            }
        }

        public String GetName() {
            return this.sequenceName;
        }

        public void SetName(String name) {
            this.sequenceName = name;
        }

        public List<Operation> GetOperations() {
            return this.operations;
        }

        public String GetOperationNames() {
            String s = "";
            foreach (Operation o in this.operations) {
                s += o.GetName() + ", ";
            }
            return s;
        }

        public Operation GetOperation(String name) {
            foreach (Operation o in this.operations) {
                if (o.GetName().Equals(name)) {
                    return o;
                }
            }
            throw new Exception("No operation with name: " + name + " in sequence: " + this.sequenceName);
        }

        public int GetDelay() {
            return this.delay;
        }

        public void SetDelay(int delay) {
            this.delay = delay;
        }

        public bool AbortOnError() {
            return this.abort;
        }

        public void SetAbortOnError(bool abort) {
            this.abort = abort;
        }

        public override string ToString() {
            String s = this.sequenceName + ": ";
            foreach (Operation o in this.operations) {
                s += o.GetName() + ", ";
            }
            return s;
        }

        public JObject GetReportJSON() {
            JObject obj = new JObject();
            JArray arr = new JArray();

            this.operations.ForEach((operation) => {
                arr.Add(operation.GetReportJSON());
            });
            
            obj[this.GetName()] = arr;

            return obj;
        }

    }

}