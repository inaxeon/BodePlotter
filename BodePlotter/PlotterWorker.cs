using BodePlotter.Models;
using Gpib.InstrumentInterface.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BodePlotter
{
    public class PlotterWorker : IDisposable
    {
        public delegate void OperationCompletedCallback();
        public event OperationCompletedCallback OperationComplete;

        public delegate void OperationFailedCallback(Exception ex);
        public event OperationFailedCallback OperationFailed;

        public delegate void AddPointCallback(double point);
        public event AddPointCallback AddPoint;

        private readonly ISourceInstrument _sourceInstrument;
        private readonly IMeasurementInstrument _measurementInstrument;
        private Thread _workerThread;
        private ChartScale _scale;

        public PlotterWorker(ISourceInstrument sourceInstrument, IMeasurementInstrument measurementInstrument)
        {
            _sourceInstrument = sourceInstrument;
            _measurementInstrument = measurementInstrument;
        }

        public ISourceInstrument SourceInstrument
        {
            get
            {
                return _sourceInstrument;
            }
        }

        public IMeasurementInstrument MeasurementInstrument
        {
            get
            {
                return _measurementInstrument;
            }
        }

        public bool IsRunning { get; private set; }

        public void Dispose()
        {
            _sourceInstrument.Dispose();
            _measurementInstrument.Dispose();
        }

        public ChartScale SetupSweep(int range, double testVoltage, double offset, uint startFrequency, uint stopFrequency, uint numPoints)
        {
            _scale = new ChartScale
            {
                StartFrequency = startFrequency,
                StopFrequency = stopFrequency,
                Offset = offset,
                MeasurementRange = range,
                ReferenceVoltage = testVoltage,
                Frequencies = GeneratePoints(startFrequency, stopFrequency, numPoints)
            };

            return _scale;
        }

        public void StartSweep()
        {
            Connect();

            _measurementInstrument.SetMeasurement(_scale.MeasurementRange);
            _sourceInstrument.SetSineOutput(_scale.StartFrequency, _scale.ReferenceVoltage, 0);

            _workerThread = new Thread(WorkerThread);
            _workerThread.Start();
        }

        public OffsetMeasurementResult CalculateOffset(int range, double testVoltage, uint testFrequency)
        {
            Connect();

            _measurementInstrument.SetMeasurement(range);
            _sourceInstrument.SetSineOutput(testFrequency, testVoltage, 0);

            var measurement = _measurementInstrument.ReadVoltageNow();

            return new OffsetMeasurementResult
            {
                OffsetDb = VoltageGainToDb(measurement, testVoltage),
                MeasuredVoltage = measurement
            };
        }

        private void Connect()
        {
            _measurementInstrument.Init();
            _sourceInstrument.Init();
        }

        public void Stop()
        {
            IsRunning = false;
        }

        private List<uint> GeneratePoints(uint startFrequency, uint stopFrequency, uint numPoints)
        {
            var frequencies = new List<uint>();
            var step = (Math.Log(stopFrequency) - Math.Log(startFrequency)) / (numPoints - 1);

            for (var i = 0; i < numPoints; i++)
                frequencies.Add((uint)Math.Exp(Math.Log(startFrequency) + i * step));

            return frequencies;
        }

        private double VoltageGainToDb(double measuredVoltage, double referenceVoltage)
        {
            var dB = 20 * (Math.Log(measuredVoltage / referenceVoltage) / (Math.Log(10)));
            return Math.Round(dB, 4);
        }

        private void WorkerThread()
        {
            IsRunning = true;

            try
            {
                foreach (var freq in _scale.Frequencies)
                {
                    if (!IsRunning)
                        break;

                    _sourceInstrument.SetFrequency(freq);

                    var measurement = _measurementInstrument.ReadVoltageNow();
                    var vg = VoltageGainToDb(measurement, _scale.ReferenceVoltage);

                    vg -= _scale.Offset;

                    AddPoint?.Invoke(vg);
                }
            }
            catch (Exception ex)
            {
                OperationFailed?.Invoke(ex);
                return;
            }
            finally
            {
                IsRunning = false;
            }

            OperationComplete?.Invoke();
        }
    }
}
