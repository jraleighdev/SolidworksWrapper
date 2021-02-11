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
    /// <summary>
    /// Solidworks Equation
    /// </summary>
    public class SolidworksEquation
    {
        /// <summary>
        /// Id of the equation
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Equation are stored as a string
        /// </summary>
        public string Equation { get; private set; }

        /// <summary>
        /// If the equation is global
        /// </summary>
        public bool IsGlobal { get; private set; }

        /// <summary>
        /// Name of the equation
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Unit type of the equation
        /// </summary>
        public UnitTypes UnitType { get; private set; }

        /// <summary>
        /// Value of the equation
        /// </summary>
        public double Value { get; set; }

        /// <summary>
        /// Creates a new equation with an id, equation, value, and if global
        /// </summary>
        /// <param name="id"></param>
        /// <param name="equation"></param>
        /// <param name="value"></param>
        /// <param name="isGlobal"></param>
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
