using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.EquationManager
{
    /// <summary>
    /// Manages get and setting equations
    /// </summary>
    public class SolidworksEquationManager : List<SolidworksEquation>, IDisposable
    {
        /// <summary>
        /// Reference to the source solidworks equation manager
        /// </summary>
        public IEquationMgr _equationMgr;

        /// <summary>
        /// Sets the source interop object and creates a list of equations
        /// </summary>
        /// <param name="equationMgr"></param>
        public SolidworksEquationManager(IEquationMgr equationMgr)
        {
            _equationMgr = equationMgr;

            var count = _equationMgr.GetCount();

            for (var i = 0; i < count; i++)
            {
                this.Add(new SolidworksEquation(i, _equationMgr.Equation[i], _equationMgr.Value[i], _equationMgr.GlobalVariable[i]));
            }
        }

        /// <summary>
        /// Gets the equation by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SolidworksEquation GetEquation(string name)
        {
            return this.FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper());
        }

        /// <summary>
        /// Sets the value of the equation
        /// </summary>
        /// <param name="name">Name of the equation</param>
        /// <param name="value">New value for the equation</param>
        /// <param name="isGlobal">If the equation should be global</param>
        public void SetEquation(string name, double value, bool isGlobal = true)
        {
            var equation = this.FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper() && x.IsGlobal == isGlobal);

            if (equation != null)
            {
                _equationMgr.Equation[equation.Id] = $"\"{name}\" = {value}";
            }
        }

        public void Dispose()
        {
            if (_equationMgr != null)
            {
                Marshal.ReleaseComObject(_equationMgr);
            }
        }
    }
}
