using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.CaptureDtos
{
    /// <summary>
    /// Captures dimension and features for the active document
    /// </summary>
    public class DesignCapture
    {
        /// <summary>
        /// Dimensions in the active document
        /// </summary>
        public List<DimensionCapture> Dimensions { get; set; }

        /// <summary>
        /// Features for the active document
        /// </summary>
        public List<FeatureCapture> Features { get; set; }

        /// <summary>
        /// Adds the dimension to the colection
        /// </summary>
        /// <param name="dimension"></param>
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
