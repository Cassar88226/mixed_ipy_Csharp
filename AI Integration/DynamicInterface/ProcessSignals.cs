using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicInterface
{
    public class ProcessSignals
    {
        public List<Signal> IncreaseSignals(IList<object> signals)
        {
            Console.WriteLine(signals);
            List<Signal> result = new List<Signal>();
            foreach(dynamic element in signals)
            {
                element.Value++;
                result.Add(new Signal(element.Date, element.SignalName, element.Value));
            }
            return result;
        }
    }
}
