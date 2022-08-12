using Gpib.InstrumentInterface.Exceptions;
using Ivi.Visa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BodePlotter.Helpers
{
    public class DialogHelper
    {
        public static void PromptOperationException(Exception ex)
        {
            if (ex is InvalidInstrumentException)
            {
                MessageBox.Show(ex.Message, $"Invalid instrument", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ex is NativeVisaException)
            {
                MessageBox.Show(ex.Message, $"Communication error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ex is Ivi.Visa.IOTimeoutException)
            {
                MessageBox.Show(ex.Message, "Timeout error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ex is InstrumentOverloadException)
            {
                MessageBox.Show("Reading out of range. Please increase measurement voltage range.", "Out of range",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show(ex.Message, $"An unknown error occurred attempting to comminicate with an instrument: "
                + ex.Message, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static bool TryInstrumentOperation(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                DialogHelper.PromptOperationException(ex);
                return false;
            }

            return true;
        }
    }
}
