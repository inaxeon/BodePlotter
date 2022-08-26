using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace BodePlotter.Models
{
    public class ChartConfiguration
    {
        public static ChartConfiguration FromConfig()
        {
            var fontConverter = new FontConverter();
            var wfFont = (Font)fontConverter.ConvertFromString(Properties.Settings.Default.ChartFont);

            FontFamilyConverter fontFamilyConverter = new FontFamilyConverter();
            var axisFontFamily = (System.Windows.Media.FontFamily)fontFamilyConverter.ConvertFromString(GetFontName(wfFont));

            return new ChartConfiguration
            {
                ActualPlotLabel = Properties.Settings.Default.ActualPlotLabel,
                RefPlotLabel = Properties.Settings.Default.RefPlotLabel,
                ActualPlotColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(Properties.Settings.Default.ActualPlotColor),
                RefPlotColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(Properties.Settings.Default.RefPlotColor),
                ChartFontColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(Properties.Settings.Default.ChartFontColor),
                ChartGridColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(Properties.Settings.Default.ChartGridColor),
                ChartBackgroundColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString(Properties.Settings.Default.ChartBackgroundColor),
                Font = axisFontFamily,
                FontSize = wfFont.Size,
                FontStyle = new Func<System.Windows.FontStyle>(() =>
                {
                    switch (wfFont.Style)
                    {
                        case System.Drawing.FontStyle.Regular:
                            return System.Windows.FontStyles.Normal;
                        case System.Drawing.FontStyle.Italic:
                            return System.Windows.FontStyles.Italic;
                        case System.Drawing.FontStyle.Bold:
                            return System.Windows.FontStyles.Oblique;
                        default:
                            return System.Windows.FontStyles.Normal;
                    }
                })(),
            };
        }

        public string ActualPlotLabel { get; set; }
        public string RefPlotLabel { get; set; }
        public Color ActualPlotColor { get; set; }
        public Color RefPlotColor { get; set; }
        public Color ChartFontColor { get; set; }
        public Color ChartGridColor { get; set; }
        public Color ChartBackgroundColor { get; set; }
        public byte StrokeAlpha {  get { return 255; } }
        public byte FillAlpha { get { return 32; } }
        public System.Windows.Media.FontFamily Font { get; set; }
        public double FontSize { get; set; }
        public System.Windows.FontStyle FontStyle { get; set; }
        public Color FontColor { get; set; }

        private static List<string> LimitFontList(List<string> fontList, string word)
        {
            var newFontList = new List<string>();

            foreach (var fontFamily in fontList)
            {
                if (fontFamily.ToLower().Contains(word.ToLower()))
                    newFontList.Add(fontFamily);
            }

            return newFontList.Count > 0 ? newFontList : fontList;
        }

        private static string GetFontName(Font font)
        {
            string fontWanted = font.FontFamily.Name;
            var family = new System.Windows.Media.FontFamily(fontWanted);
            string baseFont = "";

            foreach (var baseF in family.FamilyNames.Values)
                baseFont = baseF;

            if (baseFont == fontWanted)
                return fontWanted;

            string fontTypeface = fontWanted.Substring(baseFont.Length).Trim();

            var fontNames = new List<string>();

            foreach (var typeface in family.FamilyTypefaces)
            {
                foreach (var fn in typeface.AdjustedFaceNames)
                    fontNames.Add(baseFont + " " + fn.Value);
            }

            fontNames = LimitFontList(fontNames, fontTypeface);

            if (!baseFont.ToLower().Contains("bold") && font.Bold)
                fontNames = LimitFontList(fontNames, "bold");

            if (!baseFont.ToLower().Contains("italic") && font.Italic)
                fontNames = LimitFontList(fontNames, "italic");

            if (fontNames.Count == 1)
                return fontNames[0];

            return fontWanted;
        }
    }
}
