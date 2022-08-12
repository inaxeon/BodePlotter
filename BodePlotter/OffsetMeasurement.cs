using BodePlotter.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BodePlotter
{
    public partial class OffsetMeasurement : Form
    {
        private readonly PlotterWorker _worker;
        public double? OffsetResult { get; private set; }

        public OffsetMeasurement(PlotterWorker worker)
        {
            _worker = worker;
            InitializeComponent();
        }

        private void btnMeasure_Click(object sender, EventArgs e)
        {
            uint testFrequency;
            if (!uint.TryParse(txtTestFrequency.Text, out testFrequency))
            {
                MessageBox.Show("Invalid test frequency format", "Invalid input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogHelper.TryInstrumentOperation(() =>
            {
                var result = _worker.CalculateOffset(Properties.Settings.Default.MeasurementRange,
                                Properties.Settings.Default.ReferenceVoltage, testFrequency);

                txtResult.Text = result.OffsetDb.ToString("0.##") + " dB";
                txtMeasuredVoltage.Text = result.MeasuredVoltage.ToString("0.###");

                OffsetResult = result.OffsetDb;

                Properties.Settings.Default.OffsetCalculationTestFrequency = testFrequency;
                Properties.Settings.Default.Save();
            });
        }

        private void OffsetCalculator_Load(object sender, EventArgs e)
        {
            txtTestFrequency.Text = Properties.Settings.Default.OffsetCalculationTestFrequency.ToString();
        }
    }
}
