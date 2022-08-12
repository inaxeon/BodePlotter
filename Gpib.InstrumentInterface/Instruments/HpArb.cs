using Gpib.InstrumentInterface.Exceptions;
using Gpib.InstrumentInterface.Interfaces;
using Ivi.Visa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gpib.InstrumentInterface.Instruments
{

    public class HpArb : ISourceInstrument
    {
        public enum WaveformType
        {
            Sine,
        }

        private readonly IMessageBasedSession _session;

        public HpArb(IMessageBasedSession session)
        {
            _session = session;
        }

        public void Init()
        {
            _session.RawIO.Write("*RST\n");
            _session.RawIO.Write("*IDN?\n");

            var response = _session.RawIO.ReadString();

            if (!response.StartsWith("HEWLETT-PACKARD,33120A"))
                throw new InvalidInstrumentException($"Incorrect instrument on: {_session.HardwareInterfaceName}. Instrument response: {response}");
        }

        public void Dispose()
        {
            _session.Dispose();
        }

        public void SetSineOutput(uint hz, double vrms, double offset)
        {
            var vpkStr = ((double)vrms * Math.Sqrt(2)).ToString("0.000");
            var hzStr = hz.ToString("0.#####E+0");
            var offsetStr = offset.ToString("0.000");

            _session.RawIO.Write($"APPL:SIN {hzStr}, {vpkStr}, {offsetStr}\n");
        }

        public void SetFrequency(uint hz)
        {
            var hzStr = hz.ToString("0.#####E+0");

            _session.RawIO.Write($"FREQ {hzStr}\n");
        }
    }
}
