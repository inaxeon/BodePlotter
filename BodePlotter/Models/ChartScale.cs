using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodePlotter.Models
{
    /// <summary>
    /// All of the properties relating to a plot
    /// </summary>
    [Serializable]
    public class ChartScale : IEquatable<ChartScale>
    {
        public List<uint> Frequencies { get; set; }
        public double ReferenceVoltage { get; set; }
        public double Offset { get; set; }
        public uint StartFrequency { get; set; }
        public uint StopFrequency { get; set; }
        public int MeasurementRange { get; set; }

        public bool Equals(ChartScale other)
        {
            if (other == null)
                return true;

            return string.IsNullOrEmpty(Difference(other));
        }

        public string Difference(ChartScale other)
        {
            var diffs = new List<string>();

            if (other.StartFrequency != StartFrequency)
                diffs.Add($"StartFrequency1: {StartFrequency} != StartFrequency2: {other.StartFrequency}");

            if (other.StopFrequency != StopFrequency)
                diffs.Add($"StopFrequency1: {StopFrequency} != StopFrequency2: {other.StopFrequency}");

            if (other.Offset != Offset)
                diffs.Add($"Offset1: {Offset} != Offset2: {other.Offset}");

            if (other.MeasurementRange != MeasurementRange)
                diffs.Add($"Range1: {MeasurementRange} != Range2: {other.MeasurementRange}");

            if (other.ReferenceVoltage != ReferenceVoltage)
                diffs.Add($"ReferenceVoltage1: {ReferenceVoltage} != ReferenceVoltage2: {other.ReferenceVoltage}");

            if (other.Frequencies.Count != Frequencies.Count)
                diffs.Add($"FrequenciesList1 != FrequenciesList2");

            if (!Enumerable.SequenceEqual(other.Frequencies, Frequencies))
                diffs.Add($"FrequenciesList1 != FrequenciesList2");

            return string.Join("\r\n", diffs);
        }
    }
}
