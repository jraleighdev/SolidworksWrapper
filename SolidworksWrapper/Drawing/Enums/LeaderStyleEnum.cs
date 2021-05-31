using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Drawing.Enums
{
    public enum LeaderStyleEnum
    {
        No = 0,
        Straight = 1,
        Bent = 2,
        Underlined = 3,
        Spline = 4,
        AttachLeaderTop = 256,
        AttachLeaderCenter = 512,
        AttachLeaderBottom = 1024,
        AttachLeaderNearest = 2048,
        AlwaysAttachToBalloon = 4100
    }
}
