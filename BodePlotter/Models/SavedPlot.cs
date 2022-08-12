using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodePlotter.Models
{
    [Serializable]
    public class SavedPlot
    {
        public ChartScale Scale { get; set; }
        public List<double> Points { get; set; }
    }
}
