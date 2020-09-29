from System.Collections.Generic import *
from System import DateTime
import sys
sys.path.append(r"F:\WorkSpace\71 Andrew AI\AI Integration\DynamicInterface\bin\Debug")

import clr
clr.AddReference(r"DynamicInterface.dll")
from DynamicInterface import Signal
from DynamicInterface import ProcessSignals
class OperationSignal:
    def processSignal(self, signals):
        for signal in signals:
            signal.Value += 1
        return signals

    def increaseSignal(self, signals):
        increase_object = ProcessSignals()
        new_signals = []
        for signal in signals:
            new_signals.append(signal)
        increase_signals = increase_object.IncreaseSignals(new_signals)
        return increase_signals

    def getListOfSignals(self):
        signals = []
        for i in range(0, 10):
            signal = Signal(DateTime.Now, "signal" + str(i), i)
            signals.append(signal)
        return signals