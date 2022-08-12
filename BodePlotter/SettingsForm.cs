using NationalInstruments.Visa;
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
    public partial class SettingsForm : Form
    {
        ResourceManager _manager;

        public SettingsForm()
        {
            _manager = new ResourceManager();
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            var devices = _manager.Find("(ASRL|GPIB|TCPIP|USB)?*");

            ddlSourceDevice.Items.AddRange(devices.Cast<object>().ToArray());
            ddlMeasurementDevice.Items.AddRange(devices.Cast<object>().ToArray());

            if (!string.IsNullOrEmpty(Properties.Settings.Default.MeasurementDevice))
                ddlMeasurementDevice.SelectedItem = Properties.Settings.Default.MeasurementDevice;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.SourceDevice))
                ddlSourceDevice.SelectedItem = Properties.Settings.Default.SourceDevice;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            SaveSettings();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.MeasurementDevice = (string)ddlMeasurementDevice.SelectedItem;
            Properties.Settings.Default.SourceDevice = (string)ddlSourceDevice.SelectedItem;
            Properties.Settings.Default.Save();
        }
    }
}
