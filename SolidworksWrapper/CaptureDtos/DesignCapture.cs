using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.CaptureDtos
{
    public class DesignCapture
    {
        public List<DimensionCapture> Dimensions { get; set; }

        public List<FeatureCapture> Features { get; set; }

        public void AddDim(DimensionCapture dimension)
        {
            var dimExists = Dimensions.Exists(x => x.Name == dimension.Name);

            if (dimExists) return;

            Dimensions.Add(dimension);
        }

        public DesignCapture()
        {
            Dimensions = new List<DimensionCapture>();
            Features = new List<FeatureCapture>();
        }
    }
}
