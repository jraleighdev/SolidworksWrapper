using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Drawing.Dimensions.Enums
{
    public enum DimensionTypeEnum
    {
        Unknown = 0,
        Ordinate = 1,
        Linear = 2,
        Angular = 3,
        ArcLength = 4,
        Radial = 5,
        Diameter = 6,
        HorOrdinate = 7,
        VertOrdinate = 8,
        ZAxis = 9, 
        Chamfer = 10,
        HorLinear = 11,
        VertLinear = 12,
        Scalar = 13
    }
}
