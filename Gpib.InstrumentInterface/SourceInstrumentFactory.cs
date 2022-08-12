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
    public static class SourceInstrumentFactory
    {
        public static ISourceInstrument GetSource(IMessageBasedSession session, string addr)
        {
            session.RawIO.Write("*IDN?\n");

            var response = session.RawIO.ReadString();

            if (response.StartsWith("HEWLETT-PACKARD,33120A"))
                return new HpArb(session);

            throw new InvalidInstrumentException($"Unknown source instrument on: {addr}. Instrument response: {response}");
        }
    }
}
