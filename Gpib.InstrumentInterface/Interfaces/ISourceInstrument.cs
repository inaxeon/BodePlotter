using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Gpib.InstrumentInterface.Instruments.HpArb;

namespace Gpib.InstrumentInterface.Interfaces
{
    public interface ISourceInstrument : IInstrument
    {
        void SetSineOutput(uint hz, double vrms, double offset);
        void SetFrequency(uint hz);
    }
}
