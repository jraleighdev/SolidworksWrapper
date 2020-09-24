using SolidWorks.Interop.sldworks;
using SolidworksWrapper.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.EquationManager
{
    public class SolidworksEquation
    {
        public int Id { get; private set; }

        public string Equation { get; private set; }

        public bool IsGlobal { get; private set; }

        public string Name { get; private set; }

        public UnitTypes UnitType { get; private set; }

        public double Value { get; set; }

        public SolidworksEquation(int id, string equation, double value, bool isGlobal)
        {
            Id = id;
            Equation = equation;
            Value = value;
            IsGlobal = isGlobal;

            if (Equation.Contains("="))
            {
                var splitEquation = Equation.Split('=');

                Name = splitEquation[0].Trim().Replace("\"", "");
            }
        }
    }
}
