using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.CustomProperties
{
    /// <summary>
    /// Manages interaction with the Solidworks document
    /// </summary>
    public class SolidWorksCustomPropertyManager : List<string>, IDisposable
    {
        /// <summary>
        /// Reference to the Custom Property Manager interop 
        /// </summary>
        public ICustomPropertyManager _propertyManager;

        /// <summary>
        /// Stores the reference to the interop and gets all the property names
        /// </summary>
        /// <param name="propertyManager"></param>
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

        /// <summary>
        /// Sets the value of a property
        /// </summary>
        /// <param name="name">Name of the property to set</param>
        /// <param name="value">New value for the property</param>
        /// <exception cref="Exception">If the property could not be set an error will be thrown</exception>
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
                var message = success == 1 ? "Custom property does not exist" : "Specified value has incorrect type";

                throw new Exception(message);
            }
        }

        /// <summary>
        /// Gets the value of the given property
        /// </summary>
        /// <param name="name">Name of the property to pull the value from</param>
        /// <returns></returns>
        /// <exception cref="Exception">If property does not exist it will throw an error</exception>
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
