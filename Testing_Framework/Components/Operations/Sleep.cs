using System.Threading;

using Newtonsoft.Json;

namespace Testing_Framework.Components.Operations {

    public class Sleep : Operation {

        [JsonProperty]
        private int timeout;
        
        public Sleep(int expected) : base("Sleep", "", expected) {
            this.timeout = expected;
        }

        public override bool RunOperation() {
            //Console.WriteLine("Sleeping!");
            Thread.Sleep(this.timeout);
            //return base.runOperation();
            return true;
        }

        public int GetTimeout() {
            return this.timeout;
        }

    }

}