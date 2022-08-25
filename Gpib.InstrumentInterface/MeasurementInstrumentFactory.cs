using Gpib.InstrumentInterface.Exceptions;
using Gpib.InstrumentInterface.Instruments;
using Gpib.InstrumentInterface.Interfaces;
using Ivi.Visa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gpib.InstrumentInterface
{
    public static class MeasurementInstrumentFactory
    {
        public static IMeasurementInstrument GetMeasurer(IMessageBasedSession session, string addr)
        {
            session.RawIO.Write("*IDN?\n");

            var response = session.RawIO.ReadString();

            if (response.StartsWith("KEITHLEY INSTRUMENTS INC.,MODEL 200"))
                return new KeithleyDmm(session);

            throw new InvalidInstrumentException($"Unknown measurement instrument on: {addr}. Instrument response: {response}");
        }
    }
}
