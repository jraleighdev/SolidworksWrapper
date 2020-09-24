using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.CaptureDtos
{
    public class FeatureCapture
    {
        public string Name { get; set; }

        public string FeatureType { get; set; }

        public bool Suppressed { get; set; }

        public FeatureCapture(string name, string featureType, bool suppressed)
        {
            Name = name;
            FeatureType = featureType;
            Suppressed = suppressed;
        }
    }
}
