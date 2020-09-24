using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.CustomProperties
{
    public class SolidWorksCustomPropertyManager : List<string>, IDisposable
    {
        public ICustomPropertyManager _propertyManager;

        public SolidWorksCustomPropertyManager(ICustomPropertyManager propertyManager)
        {
            _propertyManager = propertyManager;

            var names = _propertyManager.GetNames();

            if (names == null) return;

            foreach (var n in names)
            {
                this.Add(n);
            }
        }

        public void SetValue(string name, string value)
        {
            var prop = this.FirstOrDefault(x => x == name);

            if (prop == null)
            {
                _propertyManager.Add3(name, 30, value, 2);
            }

            var success = _propertyManager.Set2(name, value);

            if (success != 0)
            {
                var message = success == 1 ? "Custom property does not exist" : "Specied value has incorrect type";

                throw new Exception(message);
            }
        }

        public string GetValue(string name)
        {
            var prop = this.FirstOrDefault(x => x == name);

            if (prop == null) throw new Exception("Property does not exist");

            _propertyManager.Get6(name, false, out string value, out string eval, out bool resolved, out bool linkProperty);

            return value;
        }

        public void Dispose()
        {
            if (_propertyManager != null)
            {
                Marshal.ReleaseComObject(_propertyManager);
            }
        }
    }
}
