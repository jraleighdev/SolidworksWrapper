using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Enums
{
    /// <summary>
    /// Configuration options for a document
    /// </summary>
    public enum ConfigurationOptions
    {
        ConfigPropertySuppressFeatures = 0,
        ThisConfiguration = 1,
        AllConfiguration = 2,
        SpecifyConfiguration = 3,
        LinkedToParent = 4,
        SpeedpakConfiguration = 5
    }
}
