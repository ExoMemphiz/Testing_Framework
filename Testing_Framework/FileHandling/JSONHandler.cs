using System;
using System.Collections.Generic;
using System.IO;

using Testing_Framework.Components;

using Newtonsoft.Json;

namespace Testing_Framework.FileHandling {

    static class JSONHandler {

        private const String OPERATION_FILE_NAME = "operations.json";
        private const String SEQUENCE_FILE_NAME = "sequences.json";
        private const String TEST_FILE_NAME = "tests.json";

        private static JsonSerializerSettings GetSettings() {
            return new JsonSerializerSettings {
                TypeNameHandling = TypeNameHandling.Auto,
                Formatting = Formatting.Indented
            };
        }

        /** Saving and reading operations **/

        public static class Operations {

            public static void SaveOperation(Operation operation) {
                List<Operation> operations = ReadOperations();
                bool replaced = false;
                for (int i = 0; i < operations.Count; i++) {
                    if (operations[i].GetName().Equals(operation.GetName())) {
                        operations[i] = operation;
                        replaced = true;
                        break;
                    }
                }
                if (!replaced) {
                    operations.Add(operation);
                }
                String json = JsonConvert.SerializeObject(operations, GetSettings());
                File.WriteAllText(OPERATION_FILE_NAME, json);
            }

            public static void RenameOperation(String prevName, String newName) {
                List<Operation> operations = ReadOperations();
                foreach (Operation o in operations) {
                    if (o.GetName().Equals(prevName)) {
                        o.SetName(newName);
                        DeleteOperation(prevName);
                        SaveOperation(o);
                        return;
                    }
                }
            }

            public static void DeleteOperation(String name) {
                List<Operation> operations = new List<Operation>();
                foreach (Operation o in operations) {
                    if (!o.GetName().Equals(name)) {
                        operations.Add(o);
                    }
                }
                String json = JsonConvert.SerializeObject(operations, GetSettings());
                File.WriteAllText(OPERATION_FILE_NAME, json);
            }

            public static List<Operation> ReadOperations(String testName, String sequenceName) {
                List<Sequence> seqs = Tests.GetTest(testName).GetSequences();
                foreach (Sequence s in seqs) {
                    if (s.GetName().Equals(sequenceName)) {
                        return s.GetOperations();
                    }
                }
                throw new Exception("Could not find sequence " + sequenceName + " in test: " + testName + "!");
            }

            public static Operation GetOperation(String testName, String sequenceName, String name) {
                List<Operation> operations = ReadOperations(testName, sequenceName);
                foreach (Operation o in operations) {
                    if (o.GetName().Equals(name)) {
                        return o;
                    }
                }
                throw new Exception("Could not find any operation with the name " + name + " for the sequence " + sequenceName + " in test: " + testName + "!");
            }

            public static bool OperationExists(String opName) {
                List<Operation> operations = ReadOperations();
                foreach (Operation o in operations) {
                    if (o.GetName().Equals(opName)) {
                        return true;
                    }
                }
                return false;
            }

            public static List<Operation> ReadOperations() {
                if (!File.Exists(OPERATION_FILE_NAME)) {
                    File.Create(OPERATION_FILE_NAME).Close();
                }
                List<Operation> operations = JsonConvert.DeserializeObject<List<Operation>>(File.ReadAllText(OPERATION_FILE_NAME), GetSettings());
                if (operations == null) {
                    return new List<Operation>();
                }
                return operations;
            }

        }

        /** Saving and reading sequences **/
        public static class Sequences {

            public static void SaveSequence(Sequence sequence) {
                List<Sequence> sequences = ReadSequences();
                bool replaced = false;
                for (int i = 0; i < sequences.Count; i++) {
                    if (sequences[i].GetName().Equals(sequence.GetName())) {
                        sequences[i] = sequence;
                        replaced = true;
                        break;
                    }
                }
                if (!replaced) {
                    sequences.Add(sequence);
                }
                foreach (Operation o in sequence.GetOperations()) {
                    Operations.SaveOperation(o);
                }
                String json = JsonConvert.SerializeObject(sequences, GetSettings());
                File.WriteAllText(SEQUENCE_FILE_NAME, json);
            }

            public static List<Sequence> ReadSequences() {
                if (!File.Exists(SEQUENCE_FILE_NAME)) {
                    File.Create(SEQUENCE_FILE_NAME).Close();
                }
                List<Sequence> sequences = JsonConvert.DeserializeObject<List<Sequence>>(File.ReadAllText(SEQUENCE_FILE_NAME), GetSettings());
                if (sequences == null) {
                    //Console.WriteLine("SequenceList Read == null");
                    return new List<Sequence>();
                }
                return sequences;
            }

            public static List<Sequence> ReadSequences(String testName) {
                List<Test> tests = Tests.ReadTests();
                foreach (Test t in tests) {
                    if (t.GetName().Equals(testName)) {
                        return t.GetSequences();
                    }
                }
                throw new Exception("Could not find any sequences for test " + testName + "!");
            }

        }

        /** Saving and reading tests **/

        public static class Tests {

            public static List<Test> ReadTests() {
                if (!File.Exists(TEST_FILE_NAME)) {
                    File.Create(TEST_FILE_NAME).Close();
                }
                List<Test> tests = JsonConvert.DeserializeObject<List<Test>>(File.ReadAllText(TEST_FILE_NAME), GetSettings());
                if (tests == null) {
                    //Console.WriteLine("TestList Read == null");
                    return new List<Test>();
                }

                return tests;
            }

            public static void SaveTest(Test test) {
                List<Test> tests = ReadTests();
                bool replaced = false;
                for (int i = 0; i < tests.Count; i++) {
                    if (tests[i].GetName().Equals(test.GetName())) {
                        tests[i] = test;
                        replaced = true;
                        break;
                    }
                }
                if (!replaced) {
                    //Console.WriteLine("Writing a New Test! {0}", test.getName());
                    tests.Add(test);
                }
                String json = JsonConvert.SerializeObject(tests, GetSettings());
                File.WriteAllText(TEST_FILE_NAME, json);
            }

            public static Test GetTest(String testName) {
                List<Test> tests = ReadTests();
                for (int i = 0; i < tests.Count; i++) {
                    Test s = tests[i];
                    if (s.GetName().Equals(testName)) {
                        return s;
                    }
                }
                throw new Exception("Could not find Test with name: " + testName);
            }

        }

        public static class Reports {

            public static void SaveReport(Report report) {
                String json = JsonConvert.SerializeObject(report.CreateJSON(), GetSettings());
                File.WriteAllText("ReportTest1.json", json);
            }

        }

    }

}