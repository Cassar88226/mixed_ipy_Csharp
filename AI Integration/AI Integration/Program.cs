using DynamicInterface;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Integration
{
    class Program
    {
        static dynamic options = new Dictionary<string, object> { ["Debug"] = true };
        static ScriptEngine engine = Python.CreateEngine(options);
        static ScriptScope scope = engine.CreateScope();
        static dynamic pyScript = engine.ExecuteFile("../../../test_ipy/test_ipy/test_ipy.py", scope);
        static dynamic process = pyScript.OperationSignal();


        static void Main(string[] args)
        {
            // get Signals from IronPython
            List<Signal> signals = GetListOfSignals();

            // get Signals from Json Response of API
            List<Signal> signals_1 = GetListOfSignalsFromAPI(signals);

            // process Signals in IronPython script
            ProcessSignals(signals);

            // call IronPython method which calls C# method there
            // workflow : 
            // 1. call ironpython increaseSignal method
            // 2. call C# IncreaseSignals method of DynamicInterface DLL
            // 3. return Signals from IronPython to C#
            List<Signal> listOfSignals = new List<Signal>();
            var rets = process.increaseSignal(signals);
            foreach (dynamic element in rets)
            {
                Console.WriteLine(element.Value);
                Signal signal = new Signal(element.Date, element.SignalName, element.Value);
                listOfSignals.Add(signal);
            }
            // DisplaySignals(listOfSignals);

        }

        private static List<Signal> GetListOfSignals()
        {
            List<Signal> listOfSignals = new List<Signal>();

            IList<object> signals = process.getListOfSignals();

            Console.WriteLine("Original Values of Signal : ");
            foreach (dynamic element in signals)
            {                
                Console.WriteLine(element.Value);
                Signal signal = new Signal(element.Date, element.SignalName, element.Value);
                listOfSignals.Add(signal);
            }
            
            return listOfSignals;
        }

        private static List<Signal> GetListOfSignalsFromAPI(List<Signal> signals)
        {
            string json_string = JsonConvert.SerializeObject(signals);
            List<Signal> listOfSignals = new List<Signal>();
            /*
            call API with json string
            string response = Json response of API  
            example response
            string response = @"[
                {'Date':'9/26/2020 10:08:36 PM', 'SignalName':'signal1', 'Value':1},
                {'Date':'9/26/2020 10:08:37 PM', 'SignalName':'signal2', 'Value':2},
                {'Date':'9/26/2020 10:08:38 PM', 'SignalName':'signal3', 'Value':3},
            ]";
            */
            string response = json_string;
            listOfSignals = JsonConvert.DeserializeObject<List<Signal>>(response);
            return listOfSignals;
        }
        private static void ProcessSignals(List<Signal> signals)
        {
            var processed_signals = process.processSignal(signals);
            DisplaySignals(processed_signals);
        }

        private static void DisplaySignals(List<Signal> signals)
        {
            Console.WriteLine("Signals : ");
            foreach (Signal signal in signals)
            {
                Console.WriteLine(signal.Value);
            }
        }
    }
}
