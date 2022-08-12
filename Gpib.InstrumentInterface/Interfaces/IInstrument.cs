using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gpib.InstrumentInterface.Interfaces
{
    public interface IInstrument : IDisposable
    {
        void Init();
    }
}
