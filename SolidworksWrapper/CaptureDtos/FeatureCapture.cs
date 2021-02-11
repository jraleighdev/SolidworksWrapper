using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.CaptureDtos
{
    /// <summary>
    /// Captures Feature information
    /// </summary>
    public class FeatureCapture
    {
        /// <summary>
        /// Name of the feature
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of the feature
        /// </summary>
        public string FeatureType { get; set; }

        /// <summary>
        /// Status of the feature
        /// </summary>
        public bool Suppressed { get; set; }

        public FeatureCapture(string name, string featureType, bool suppressed)
        {
            Name = name;
            FeatureType = featureType;
            Suppressed = suppressed;
        }
    }
}
