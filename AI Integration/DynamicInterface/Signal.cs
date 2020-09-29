using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicInterface
{
    public class Signal:Object
    {
        public Signal(DateTime date, string signalName, int value)
        {
            Date = date;
            SignalName = signalName;
            Value = value;
        }

        public Signal()
        {
            Date = DateTime.Now;
            SignalName = "signalName";
            Value = 0;
        }
        public DateTime Date { get; set; }
        public string SignalName { get; set; }
        public int Value { get; set; }
    }
}
