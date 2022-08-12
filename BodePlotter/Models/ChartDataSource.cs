using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BodePlotter.Models
{
    /// <summary>
    /// Data source the chart is bound to
    /// </summary>
    public class ChartDataSource : INotifyPropertyChanged
    {
        private ChartScale _currentScale;

        public ChartDataSource()
        {
            YFormatter = value => value.ToString() + " dB";
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Actual",
                    Values = new ChartValues<double>(),
                    PointGeometry = null,
                    Stroke = new SolidColorBrush(Color.FromArgb(255, 33, 149, 242)),
                    Fill = new SolidColorBrush(Color.FromArgb(32, 33, 149, 242))
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public bool HasFrequencies {  get { return Labels.Any(); } }
        public bool HasReference {  get { return SeriesCollection.Count == 2; } }
        public bool HasFullDataSet
        {
            get
            {
                if (Labels == null)
                    return false;

                return SeriesCollection[0].Values.Count == Labels.Count();
            }
        }

        public bool ExistingScaleMatches(ChartScale scale)
        {
            return scale.Equals(_currentScale);
        }

        public string ExistingScaleDifference(ChartScale scale)
        {
            return scale.Difference(_currentScale);
        }

        public void SetScale(ChartScale scale)
        {
            Labels = scale.Frequencies.Select((f) =>
            {
                if (f < 1000)
                    return f.ToString() + " Hz";
                return ((double)f / 1000).ToString("0.###") + " KHz";
            }).ToArray();

            NotifyPropertyChanged("Labels");

            _currentScale = scale;
        }

        public void AddPoint(double point)
        {
            SeriesCollection[0].Values.Add(point);
            NotifyPropertyChanged("SeriesCollection");
        }

        public SavedPlot GetCurrentPlot()
        {
            return new SavedPlot
            {
                Scale = _currentScale,
                Points = SeriesCollection[0].Values.Cast<double>().ToList()
            };
        }

        public void LoadPlotToReference(SavedPlot plot)
        {
            ResetReference();

            foreach (var point in plot.Points)
                SeriesCollection[1].Values.Add(point);

            if (_currentScale == null)
                SetScale(plot.Scale);
        }

        public void LoadPlotToActual(SavedPlot plot)
        {
            ClearActualReadingPoints();

            foreach (var point in plot.Points)
                SeriesCollection[0].Values.Add(point);

            if (_currentScale == null)
                SetScale(plot.Scale);
        }

        public void ClearActualReadingPoints()
        {
            SeriesCollection[0].Values.Clear();
        }

        private void ResetReference()
        {
            if (SeriesCollection.Count == 1)
            {
                SeriesCollection.Add(
                    new LineSeries
                    {
                        Title = "Ref",
                        Values = new ChartValues<double>(),
                        PointGeometry = null,
                        Stroke = new SolidColorBrush(Color.FromArgb(255, 255, 67, 54)),
                        Fill = new SolidColorBrush(Color.FromArgb(32, 255, 67, 54))
                    }
                );
            }
            else if (SeriesCollection.Count == 2)
            {
                SeriesCollection[1].Values.Clear();
            }
        }

        public void CurrentToReference()
        {
            ResetReference();

            foreach (var point in SeriesCollection[0].Values)
                SeriesCollection[1].Values.Add(point);
        }

        public void ClearReference()
        {
            if (SeriesCollection.Count == 2)
                SeriesCollection.RemoveAt(1);
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
