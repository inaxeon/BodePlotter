using Gpib.InstrumentInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Gpib.InstrumentInterface.Extensions
{
    public static class EnumExtensions
    {
        public static RangeAttribute GetRange(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            RangeAttribute[] attributes =
                fi.GetCustomAttributes(typeof(RangeAttribute), false) as RangeAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First();
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}
