using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.CaptureDtos
{
    public class DimensionCapture
    {
        public string Name { get; set; }

        public double Value { get; set; }

        public DimensionCapture(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}
