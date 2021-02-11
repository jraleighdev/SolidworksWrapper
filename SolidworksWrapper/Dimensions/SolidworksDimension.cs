using SolidWorks.Interop.sldworks;
using SolidworksWrapper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Dimensions
{
    /// <summary>
    /// Solidworks dimension
    /// </summary>
    public class SolidworksDimension
    {
        public IDimension _dimension;

        /// <summary>
        /// Full name of the dimension
        /// </summary>
        public string FullName => _dimension.FullName;

        /// <summary>
        /// Name of the dimension
        /// </summary>
        public string Name => _dimension.Name;

        /// <summary>
        /// Type of dimension
        /// </summary>
        public DimensionTypes Type => (DimensionTypes)_dimension.GetType();

        /// <summary>
        /// Get the value of the dimension
        /// </summary>
        /// <param name="configurationOptions">Options for the configuration default value is the current configuration</param>
        /// <param name="config">The configuration to pull the dimension from</param>
        /// <returns></returns>
        public double GetValue(ConfigurationOptions configurationOptions = ConfigurationOptions.ThisConfiguration, string config = "")
        {
            return _dimension.GetValue3((int)configurationOptions, config);
        }

        /// <summary>
        /// Sets the value of the dimension
        /// </summary>
        /// <param name="value">The value to set the dimension</param>
        /// <param name="configurationOptions">Options for the configuration default value is the current configuration</param>
        /// <param name="config">The configuration to set the dimension in</param>
        /// <returns></returns>
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
