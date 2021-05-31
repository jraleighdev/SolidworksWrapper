using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidworksWrapper.Components;
using SolidworksWrapper.Drawing.Points.Enums;

namespace SolidworksWrapper.Drawing.Points.Interfaces
{
    public interface ISolidworksPoint
    {
        PointTypeEnum Type { get; }

        double X { get; }

        double Y { get; }

        SolidWorksComponent Component { get; }

        void Select();
    }
}
