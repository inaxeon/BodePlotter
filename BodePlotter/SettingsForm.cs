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
        private string _currentFont;
        private uint _currentOpacity;

        public SettingsForm()
        {
            _manager = new ResourceManager();
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            ScanDevices();
            BindSettings();
            btnOK.Focus();
        }

        private void ScanDevices()
        {
            var devices = _manager.Find("(ASRL|GPIB|TCPIP|USB)?*");

            ddlSourceDevice.Items.Clear();
            ddlMeasurementDevice.Items.Clear();

            ddlSourceDevice.Items.AddRange(devices.Cast<object>().ToArray());
            ddlMeasurementDevice.Items.AddRange(devices.Cast<object>().ToArray());
        }

        private void BindSettings()
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.MeasurementDevice))
                ddlMeasurementDevice.SelectedItem = Properties.Settings.Default.MeasurementDevice;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.SourceDevice))
                ddlSourceDevice.SelectedItem = Properties.Settings.Default.SourceDevice;

            txtActualPlot.Text = Properties.Settings.Default.ActualPlotLabel;
            txtRefPlot.Text = Properties.Settings.Default.RefPlotLabel;

            btnActualColor.BackColor = ColorTranslator.FromHtml(Properties.Settings.Default.ActualPlotColor);
            btnRefColor.BackColor = ColorTranslator.FromHtml(Properties.Settings.Default.RefPlotColor);
            btnFontColor.BackColor = ColorTranslator.FromHtml(Properties.Settings.Default.ChartFontColor);
            btnGridColor.BackColor = ColorTranslator.FromHtml(Properties.Settings.Default.ChartGridColor);
            btnBackgroundColor.BackColor = ColorTranslator.FromHtml(Properties.Settings.Default.ChartBackgroundColor);
            _currentFont = Properties.Settings.Default.ChartFont;
            txtFillOpacity.Text = Properties.Settings.Default.ChartFillOpacity.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            uint opacity;
            if (!uint.TryParse(txtFillOpacity.Text, out opacity) || opacity > 100)
            {
                MessageBox.Show("Please enter a valid opacity value", "Invalid opacity value", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            _currentOpacity = opacity;

            if (string.IsNullOrEmpty(txtActualPlot.Text))
            {
                MessageBox.Show("Please enter actual plot text", "Missing text", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            if (string.IsNullOrEmpty(txtRefPlot.Text))
            {
                MessageBox.Show("Please enter reference plot text", "Missing text", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.None;
                return;
            }

            SaveSettings();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.MeasurementDevice = (string)ddlMeasurementDevice.SelectedItem;
            Properties.Settings.Default.SourceDevice = (string)ddlSourceDevice.SelectedItem;
            Properties.Settings.Default.ActualPlotLabel = txtActualPlot.Text;
            Properties.Settings.Default.RefPlotLabel = txtRefPlot.Text;
            Properties.Settings.Default.ActualPlotColor = ColorTranslator.ToHtml(btnActualColor.BackColor);
            Properties.Settings.Default.RefPlotColor = ColorTranslator.ToHtml(btnRefColor.BackColor);
            Properties.Settings.Default.ChartFont = _currentFont;
            Properties.Settings.Default.ChartFontColor = ColorTranslator.ToHtml(btnFontColor.BackColor);
            Properties.Settings.Default.ChartGridColor = ColorTranslator.ToHtml(btnGridColor.BackColor);
            Properties.Settings.Default.ChartBackgroundColor = ColorTranslator.ToHtml(btnBackgroundColor.BackColor);
            Properties.Settings.Default.ChartFillOpacity = _currentOpacity;

            Properties.Settings.Default.Save();
        }

        private void btnActualColor_Click(object sender, EventArgs e)
        {
            PickColor(btnActualColor);
        }

        private void btnRefColor_Click(object sender, EventArgs e)
        {
            PickColor(btnRefColor);
        }

        private void btnAxisFont_Click(object sender, EventArgs e)
        {
            var fontDialog = new FontDialog();
            var fontConverter = new FontConverter();

            fontDialog.ShowColor = false;
            fontDialog.Font = (Font)fontConverter.ConvertFromString(_currentFont);

            if (fontDialog.ShowDialog() != DialogResult.Cancel)
            {
                _currentFont = fontConverter.ConvertToString(fontDialog.Font);
            }
        }

        private void btnFontColor_Click(object sender, EventArgs e)
        {
            PickColor(btnFontColor);
        }

        private void btnGridColor_Click(object sender, EventArgs e)
        {
            PickColor(btnGridColor);
        }

        private void btnBackgroundColor_Click(object sender, EventArgs e)
        {
            PickColor(btnBackgroundColor);
        }

        private void PickColor(Button colorLabel)
        {
            var colorDlg = new ColorDialog();
            colorDlg.AllowFullOpen = true;
            colorDlg.ShowHelp = true;
            colorDlg.Color = colorLabel.BackColor;

            if (colorDlg.ShowDialog() == DialogResult.OK)
                colorLabel.BackColor = colorDlg.Color;
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            BindSettings();
        }

        private void btnRescan_Click(object sender, EventArgs e)
        {
            ScanDevices();
            BindSettings();
        }
    }
}
