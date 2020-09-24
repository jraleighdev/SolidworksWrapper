using SolidWorks.Interop.sldworks;
using SolidworksWrapper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Dimensions
{
    public class SolidworksDimension
    {
        public IDimension _dimension;

        public string FullName => _dimension.FullName;

        public string Name => _dimension.Name;

        public DimensionTypes Type => (DimensionTypes)_dimension.GetType();

        public double GetValue(ConfigurationOptions configurationOptions = ConfigurationOptions.ThisConfiguration, string config = "")
        {
            return _dimension.GetValue3((int)configurationOptions, config);
        }

        public bool SetValue(double value, ConfigurationOptions configurationOptions = ConfigurationOptions.ThisConfiguration, string config = "")
        {
            var success = _dimension.SetSystemValue3(value, (int)configurationOptions, config);

            return success == 0;
        }

        public SolidworksDimension(IDimension dimension)
        {
            _dimension = dimension;
        }
    }
}
