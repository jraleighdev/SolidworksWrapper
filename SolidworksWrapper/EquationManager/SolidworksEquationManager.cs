using SolidWorks.Interop.sldworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.EquationManager
{
    public class SolidworksEquationManager : List<SolidworksEquation>, IDisposable
    {
        public IEquationMgr _equationMgr;

        public SolidworksEquationManager(IEquationMgr equationMgr)
        {
            _equationMgr = equationMgr;

            var count = _equationMgr.GetCount();

            for (var i = 0; i < count; i++)
            {
                this.Add(new SolidworksEquation(i, _equationMgr.Equation[i], _equationMgr.Value[i], _equationMgr.GlobalVariable[i]));
            }
        }

        public SolidworksEquation GetEquation(string name)
        {
            return this.FirstOrDefault(x => x.Name.ToUpper() == name.ToUpper());
        }

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
