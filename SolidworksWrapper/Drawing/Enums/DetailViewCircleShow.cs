using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Drawing.Enums
{
    public enum DetailViewCircleShow
    {
        /// <summary>
        /// Use Sketch profile to create detail viewRef
        /// </summary>
        Profile = 0,

        /// <summary>
        /// Use Sketch Circle to create detail viewRef
        /// </summary>
        Circle = 1,

        /// <summary>
        /// Do not show a sketch profile
        /// </summary>
        DoNotShow = 2
    }
}
