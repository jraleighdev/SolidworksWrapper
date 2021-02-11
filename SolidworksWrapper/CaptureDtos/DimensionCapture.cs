using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.CaptureDtos
{
    /// <summary>
    /// Captures dimension information
    /// </summary>
    public class DimensionCapture
    {
        /// <summary>
        /// Name of the dimension
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of the dimension
        /// </summary>
        public double Value { get; set; }

        public DimensionCapture(string name, double value)
        {
            Name = name;
            Value = value;
        }
    }
}
