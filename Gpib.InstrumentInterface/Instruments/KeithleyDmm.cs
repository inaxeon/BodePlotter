using Gpib.InstrumentInterface.Exceptions;
using Gpib.InstrumentInterface.Extensions;
using Gpib.InstrumentInterface.Interfaces;
using Gpib.InstrumentInterface.Models;
using Ivi.Visa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gpib.InstrumentInterface.Instruments
{
    public class KeithleyDmm : IMeasurementInstrument
    {
        private enum TypeAndRange
        {
            [Range(MeasurementType.DC, "0.2", "200mV")]
            DC_200mV,
            [Range(MeasurementType.DC, "2", "2V")]
            DC_2V,
            [Range(MeasurementType.DC, "20", "20V")]
            DC_20V,
            [Range(MeasurementType.DC, "200", "20V")]
            DC_200V,
            [Range(MeasurementType.DC, "1000", "1000V")]
            DC_1000V,
            [Range(MeasurementType.AC, "0.2", "200mV")]
            AC_200mV,
            [Range(MeasurementType.AC, "2", "2V")]
            AC_2V,
            [Range(MeasurementType.AC, "20", "20V")]
            AC_20V,
            [Range(MeasurementType.AC, "200", "200V")]
            AC_200V,
            [Range(MeasurementType.AC, "750", "750V")]
            AC_750V,
        }

        private readonly IMessageBasedSession _session;

        public KeithleyDmm(IMessageBasedSession session)
        {
            _session = session;
        }

        public void Init()
        {
            _session.RawIO.Write("*RST\n");
            _session.RawIO.Write("*IDN?\n");

            var response = _session.RawIO.ReadString();

            if (!response.StartsWith("KEITHLEY INSTRUMENTS INC.,MODEL 200"))
                throw new InvalidInstrumentException($"Incorrect instrument on: {_session.HardwareInterfaceName}. Instrument response: {response}");
        }

        public void Dispose()
        {
            _session.Dispose();
        }

        public void SetMeasurement(int type)
        {
            var range = EnumExtensions.GetRange((TypeAndRange)type);

            if (range.Type == MeasurementType.DC)
            {
                _session.RawIO.Write("SENS:FUNC \"VOLT:DC\"\n");
                _session.RawIO.Write($"SENS:VOLT:DC:RANGE {range.ScpiText}\n");
            }
            else if (range.Type == MeasurementType.AC)
            {
                _session.RawIO.Write("SENS:FUNC \"VOLT:AC\"\n");
                _session.RawIO.Write($"SENS:VOLT:AC:RANGE {range.ScpiText}\n");
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public double ReadVoltageNow()
        {
            _session.RawIO.Write("READ?\n");
            string result = _session.RawIO.ReadString();
            var parts = result.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            var exponentString = parts[0].Substring(0, parts[0].Length - 4);
            var inRangeFlag = parts[0].Substring(parts[0].Length - 4, 1);
            if (inRangeFlag == "O")
                throw new InstrumentOverloadException();
            return double.Parse(exponentString, System.Globalization.NumberStyles.Float);
        }

        public List<Tuple<string, int>> GetVoltageRanges(MeasurementType type)
        {
            var ret = new List<Tuple<string, int>>();
            var typeAndRangeList = Enum.GetValues(typeof(TypeAndRange)).Cast<TypeAndRange>().ToList();

            foreach (var tr in typeAndRangeList)
            {
                var range = EnumExtensions.GetRange(tr);
                if (range.Type == type)
                    ret.Add(new Tuple<string, int>(range.Description, (int)tr));
            }

            return ret;
        }
    }
}
