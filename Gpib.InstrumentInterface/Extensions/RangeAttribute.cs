using Gpib.InstrumentInterface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gpib.InstrumentInterface.Extensions
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class RangeAttribute : Attribute
    {
        public static readonly RangeAttribute Default = new RangeAttribute();

        public RangeAttribute() : this(MeasurementType.DC, "", "")
        {
        }

        public RangeAttribute(MeasurementType type, string scpiText, string description)
        {
            this.TypeValue = type;
            this.RangeValue = scpiText;
            this.DescriptionValue = description;
        }

        public virtual string ScpiText
        {
            get { return RangeValue; }
        }

        public virtual string Description
        {
            get { return DescriptionValue; }
        }

        public virtual MeasurementType Type
        {
            get { return TypeValue; }
        }

        protected string RangeValue { get; set; }
        protected string DescriptionValue { get; set; }
        protected MeasurementType TypeValue { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == this)
            {
                return true;
            }

            RangeAttribute other = obj as RangeAttribute;

            return (other != null) && other.ScpiText == ScpiText && other.Description == Description && other.Type == Type;
        }

        public override int GetHashCode()
        {
            return ScpiText.GetHashCode() + Description.GetHashCode();
        }

        public override bool IsDefaultAttribute()
        {
            return (this.Equals(Default));
        }
    }
}