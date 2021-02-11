using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.General
{
    /// <summary>
    /// Manages unit interaction with solidworks
    /// 
    /// </summary>
    public static class UnitManager
    {
        /// <summary>
        /// Lenght units the wrapper is sending to solidworks
        /// </summary>
        public static UnitTypes UnitTypes { get; set; } = UnitTypes.In;

        /// <summary>
        /// Gets the measurement units
        /// </summary>
        public static List<UnitTypes> MeasurementUnits
        {
            get
            {
                return new List<UnitTypes> { UnitTypes.In, UnitTypes.M, UnitTypes.MM };
            }
        }

        /// <summary>
        /// Takes given value and sends to solidworks
        /// The length then the value is converted to meters
        /// Other units types are sent as is
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double UnitsToSolidworks(double value)
        {
            switch (UnitTypes)
            {
                case UnitTypes.In:
                    return value / 39.37;
                case UnitTypes.M:
                    return value;
                case UnitTypes.MM:
                    return value / 1000;
                default:
                    return value;
            }
        }

        /// <summary>
        /// Takes given value from solidworks and converts to user specified
        /// If the unit type is length then the value is converted to user specified length units
        /// Other unit types are retrieved as is
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double UnitsFromSolidworks(double value)
        {
            switch (UnitTypes)
            {
                case UnitTypes.In:
                    return value * 39.37;
                case UnitTypes.M:
                    return value;
                case UnitTypes.MM:
                    return value * 1000;
                default:
                    return value;
            }
        }

        /// <summary>
        /// Converts degrees to radians
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ConvertDegrees(double value)
        {
            double constant = Math.PI / 180;

            return value * constant;
        }

        /// <summary>
        /// Convert radians to degrees
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ConvertRadians(double value)
        {
            double constant = 180 / Math.PI;

            return value * constant;
        }
    }
}
