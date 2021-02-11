using SolidWorks.Interop.sldworks;
using SolidworksWrapper.Components;
using SolidworksWrapper.CustomProperties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Configurations
{
    /// <summary>
    /// Solidworks configuration
    /// </summary>
    public class SolidworksConfiguration : IDisposable
    {
        private SolidWorksCustomPropertyManager _properties;

        private SolidWorksComponent _rootComponent;

        /// <summary>
        /// Reference to the interop configuration
        /// </summary>
        public IConfiguration _configuration;

        /// <summary>
        /// Name of the configuration
        /// </summary>
        public string Name => _configuration.Name;

        /// <summary>
        /// Description of the configuration
        /// </summary>
        public string Description => _configuration.Description;
        
        /// <summary>
        /// Gets the root component of the configuration
        /// </summary>
        public SolidWorksComponent RootComponent
        {
            get
            {

                if (_rootComponent == null)
                {
                    IComponent2 comp = _configuration.GetRootComponent3(true);

                    if (comp != null)
                    {
                        _rootComponent = new SolidWorksComponent(comp);
                    }
                }

                return _rootComponent;
            }
        }

        /// <summary>
        /// Gets the custom properties configuration
        /// </summary>
        public SolidWorksCustomPropertyManager Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = new SolidWorksCustomPropertyManager(_configuration.CustomPropertyManager);
                }

                return _properties;
            }
        }

        public SolidworksConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Dispose()
        {
            if (_configuration != null)
            {
                Marshal.ReleaseComObject(_configuration);
            }
        }
    }
}
