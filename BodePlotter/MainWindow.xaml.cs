using BodePlotter.Helpers;
using BodePlotter.Models;
using Gpib.InstrumentInterface;
using Gpib.InstrumentInterface.Exceptions;
using Gpib.InstrumentInterface.Interfaces;
using Ivi.Visa;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BodePlotter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PlotterWorker _worker;
        private ChartDataSource _chartDataSource;
        private UiState _lastUiState;

        private enum UiState
        {
            Disconnected,
            Ready,
            Busy
        }

        public MainWindow()
        {
            InitializeComponent();

            _chartDataSource = new ChartDataSource(GetChartConfig());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetUiState(UiState.Disconnected);
            ChtBodeChart.DataContext = _chartDataSource;
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (_worker == null)
            {
                Connect();

                if (_worker != null)
                {
                    SetUiState(UiState.Ready);
                    BindParameters();
                }
            }
            else
            {
                Disconnect();
                SetUiState(UiState.Disconnected);
            }
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (_worker.IsRunning)
                _worker.Stop();
            else
            {
                Start();
            }
        }

        private void BtnAddToRef_Click(object sender, RoutedEventArgs e)
        {
            _chartDataSource.CurrentToReference();
            SetUiState(_lastUiState);
        }

        private void BtnClearRef_Click(object sender, RoutedEventArgs e)
        {
            _chartDataSource.ClearReference();
            SetUiState(_lastUiState);
        }

        private void CmdExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CmdOptions_Click(object sender, RoutedEventArgs e)
        {
            var settings = new SettingsForm();

            if (settings.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                _chartDataSource.UpdateConfig(GetChartConfig());
        }

        private void CmdOffsetCalculator_Click(object sender, RoutedEventArgs e)
        {
            if (!ParseAndSaveInputs())
                return;

            var dlg = new OffsetMeasurement(_worker);
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (dlg.OffsetResult != null)
                {
                    Properties.Settings.Default.Offset = (double)dlg.OffsetResult;
                    Properties.Settings.Default.Save();
                    BindParameters();
                }
            }
        }

        private void CmdOpenRef_Click(object sender, RoutedEventArgs e)
        {
            OpenPlot(false);
        }

        private void CmdOpenActual_Click(object sender, RoutedEventArgs e)
        {
            OpenPlot(true);
        }

        private void OpenPlot(bool useActual)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Saved plots (.xml)|*.xml";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;

                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(SavedPlot));
                System.IO.StreamReader file = new System.IO.StreamReader(filename);
                SavedPlot plot = (SavedPlot)reader.Deserialize(file);
                file.Close();

                if (!_chartDataSource.ExistingScaleMatches(plot.Scale))
                {
                    if (MessageBox.Show("Loaded plot was not measured with the same measurement parameters presently configured. Continue?" +
                        "\r\n\r\n" + _chartDataSource.ExistingScaleDifference(plot.Scale), "Scale warning",
                        MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        return;

                    Properties.Settings.Default.StartFrequency = plot.Scale.StartFrequency;
                    Properties.Settings.Default.StopFrequency = plot.Scale.StopFrequency;
                    Properties.Settings.Default.ReferenceVoltage = plot.Scale.ReferenceVoltage;
                    Properties.Settings.Default.Offset = plot.Scale.Offset;
                    Properties.Settings.Default.MeasurementRange = plot.Scale.MeasurementRange;

                    BindParameters();
                }

                if (useActual)
                    _chartDataSource.LoadPlotToActual(plot);
                else
                    _chartDataSource.LoadPlotToReference(plot);

                SetUiState(_lastUiState);
            }
        }

        private void CmdSaveRef_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "SavedPlot";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Saved plots (.xml)|*.xml";

            var result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                var plot = _chartDataSource.GetCurrentPlot();
                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(SavedPlot));
                System.IO.FileStream file = System.IO.File.Create(filename);
                writer.Serialize(file, plot);
                file.Close();
            }
        }

        private void Connect()
        {
            var sourceAddr = Properties.Settings.Default.SourceDevice;
            IMeasurementInstrument measurementInstrument = null;
            ISourceInstrument sourceInstrument = null;

            if (string.IsNullOrEmpty(sourceAddr))
            {
                MessageBox.Show("Source device not configured", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var measurementAddr = Properties.Settings.Default.MeasurementDevice;

            if (string.IsNullOrEmpty(measurementAddr))
            {
                MessageBox.Show("Measurement device not configured", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!DialogHelper.TryInstrumentOperation(() =>
            {
                var sourceSession = GlobalResourceManager.Open(sourceAddr) as IMessageBasedSession;
                sourceInstrument = SourceInstrumentFactory.GetSource(sourceSession, sourceAddr);
            }))
            {
                return;
            }

            if (!DialogHelper.TryInstrumentOperation(() =>
            {
                var measurementSession = GlobalResourceManager.Open(measurementAddr) as IMessageBasedSession;
                measurementInstrument = MeasurementInstrumentFactory.GetMeasurer(measurementSession, measurementAddr);
            }))
            {
                return;
            }

            _worker = new PlotterWorker(sourceInstrument, measurementInstrument);
            _worker.OperationComplete += Worker_OperationComplete;
            _worker.OperationFailed += Worker_OperationFailed;
            _worker.AddPoint += Worker_AddPoint;
        }

        private void Disconnect()
        {
            _worker.Dispose();
            _worker = null;
        }


        delegate void Worker_OperationFailedDelegate(Exception ex);
        private void Worker_OperationFailed(Exception ex)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new Worker_OperationFailedDelegate(Worker_OperationFailed), ex);
                return;
            }

            DialogHelper.PromptOperationException(ex);
            SetUiState(UiState.Ready);
        }

        delegate void Worker_OperationCompleteDelegate();
        private void Worker_OperationComplete()
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new Worker_OperationCompleteDelegate(Worker_OperationComplete));
                return;
            }

            SetUiState(UiState.Ready);
        }

        private void Worker_AddPoint(double point)
        {
            _chartDataSource.AddPoint(point);
        }

        private void SetUiState(UiState state)
        {
            switch (state)
            {
                case UiState.Disconnected:
                    BtnConnect.Content = "Connect";
                    TxtNumPoints.Text = "";
                    TxtStartFrequency.Text = "";
                    TxtStopFrequency.Text = "";
                    TxtTestVoltage.Text = "";
                    GrdMeasurementParameters.IsEnabled = false;
                    BtnStart.IsEnabled = false;
                    BtnAddToRef.IsEnabled = false;
                    CmdOffsetCalculator.IsEnabled = false;
                    CmdOptions.IsEnabled = true;
                    break;
                case UiState.Ready:
                    BtnStart.Content = "Start";
                    GrdMeasurementParameters.IsEnabled = true;
                    BtnConnect.IsEnabled = true;
                    BtnStart.IsEnabled = true;
                    CmdOffsetCalculator.IsEnabled = true;
                    CmdOptions.IsEnabled = true;
                    break;
                case UiState.Busy:
                    BtnStart.Content = "Stop";
                    GrdMeasurementParameters.IsEnabled = false;
                    BtnConnect.IsEnabled = false;
                    BtnAddToRef.IsEnabled = false;
                    CmdOffsetCalculator.IsEnabled = false;
                    CmdOptions.IsEnabled = false;
                    break;
            }

            BtnAddToRef.IsEnabled = _chartDataSource.HasFullDataSet;
            BtnClearRef.IsEnabled = _chartDataSource.HasReference;
            CmdSaveRef.IsEnabled = _chartDataSource.HasFullDataSet;
            _lastUiState = state;
        }

        private void BindParameters()
        {
            BtnConnect.Content = "Disconnect";
            DdlMeasurementRange.DisplayMemberPath = "Item1";
            DdlMeasurementRange.SelectedValuePath = "Item2";
            DdlMeasurementRange.ItemsSource = _worker.MeasurementInstrument.GetVoltageRanges(Gpib.InstrumentInterface.Models.MeasurementType.AC);
            DdlMeasurementRange.SelectedValue = Properties.Settings.Default.MeasurementRange;
            TxtNumPoints.Text = Properties.Settings.Default.NumPoints.ToString();
            TxtStartFrequency.Text = Properties.Settings.Default.StartFrequency.ToString();
            TxtStopFrequency.Text = Properties.Settings.Default.StopFrequency.ToString();
            TxtTestVoltage.Text = Properties.Settings.Default.ReferenceVoltage.ToString();
            TxtOffset.Text = Properties.Settings.Default.Offset.ToString();
        }

        private void Start()
        {
            ParseAndSaveInputs();

            _chartDataSource.ClearActualReadingPoints();

            var scale = _worker.SetupSweep(Properties.Settings.Default.MeasurementRange, Properties.Settings.Default.ReferenceVoltage, Properties.Settings.Default.Offset,
                    Properties.Settings.Default.StartFrequency, Properties.Settings.Default.StopFrequency, Properties.Settings.Default.NumPoints);

            if (_chartDataSource.HasReference && _chartDataSource.ExistingScaleMatches(scale) == false)
            {
                if (MessageBox.Show("Reference plot was not measured with the same measurement parameters presently configured. Continue?" +
                    "\r\n\r\n" + _chartDataSource.ExistingScaleDifference(scale), "Scale warning",
                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    return;
            }

            _chartDataSource.SetScale(scale);

            if (DialogHelper.TryInstrumentOperation(() =>
            {
                _worker.StartSweep();
            }))
            {
                SetUiState(UiState.Busy);
            }
        }

        private ChartConfiguration GetChartConfig()
        {
            return new ChartConfiguration
            {
                ActualPlotLabel = Properties.Settings.Default.ActualPlotLabel,
                RefPlotLabel = Properties.Settings.Default.RefPlotLabel
            };
        }

        private bool ParseAndSaveInputs()
        {
            uint numPoints;
            uint startFrequency;
            uint stopFrequency;
            double testVoltage;
            double offset;

            if (!uint.TryParse(TxtNumPoints.Text, out numPoints))
            {
                MessageBox.Show("Invalid number of points", "Invalid input",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            Properties.Settings.Default.NumPoints = numPoints;

            if (!uint.TryParse(TxtStartFrequency.Text, out startFrequency))
            {
                MessageBox.Show("Invalid start frequency", "Invalid input",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            Properties.Settings.Default.StartFrequency = startFrequency;

            if (!uint.TryParse(TxtStopFrequency.Text, out stopFrequency))
            {
                MessageBox.Show("Invalid stop frequency", "Invalid input",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            Properties.Settings.Default.StopFrequency = stopFrequency;

            if (!double.TryParse(TxtTestVoltage.Text, out testVoltage))
            {
                MessageBox.Show("Invalid test voltage", "Invalid input",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            Properties.Settings.Default.ReferenceVoltage = testVoltage;

            if (!double.TryParse(TxtOffset.Text, out offset))
            {
                MessageBox.Show("Invalid offset", "Invalid input",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            Properties.Settings.Default.Offset = offset;

            if (DdlMeasurementRange.SelectedIndex < 0)
            {
                MessageBox.Show("No range selected", "Invalid input",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            Properties.Settings.Default.MeasurementRange = (int)DdlMeasurementRange.SelectedValue;

            Properties.Settings.Default.Save();

            return true;
        }
    }
}
