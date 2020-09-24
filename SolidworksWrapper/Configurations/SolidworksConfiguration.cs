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
    public class SolidworksConfiguration : IDisposable
    {
        private SolidWorksCustomPropertyManager _properties;

        private SolidWorksComponent _rootComponent;

        public IConfiguration _configuration;

        public string Name => _configuration.Name;

        public string Description => _configuration.Description;

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
