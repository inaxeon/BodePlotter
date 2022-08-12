using Gpib.InstrumentInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gpib.InstrumentInterface.Interfaces
{
    public interface IMeasurementInstrument : IInstrument
    {
        List<Tuple<string, int>> GetVoltageRanges(MeasurementType type);
        void SetMeasurement(int type);
        double ReadVoltageNow();
    }
}
