using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.General
{
    public static class UnitManager
    {
        public static UnitTypes UnitTypes { get; set; } = UnitTypes.In;

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

        public static double ConvertDegrees(double value)
        {
            double constant = Math.PI / 180;

            return value * constant;
        }

        public static double ConvertRadians(double value)
        {
            double constant = 180 / Math.PI;

            return value * constant;
        }
    }
}
