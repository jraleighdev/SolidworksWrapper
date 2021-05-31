using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidworksWrapper.Components;
using SolidworksWrapper.Drawing.Points.Enums;
using SolidworksWrapper.Drawing.Points.Interfaces;

namespace SolidworksWrapper.Drawing.Points.Classes
{
    public class SolidworksDelegatePoint : ISolidworksPoint
    {
        private Action _action;

        public SolidworksDelegatePoint(Action action, double x, double y)
        {
            _action = action;
            Type = PointTypeEnum.Delegate;
            X = x;
            Y = y;
        }

        public PointTypeEnum Type { get; }
        public double X { get; }
        public double Y { get; }

        public SolidWorksComponent Component => throw new NotImplementedException();

        public void Select()
        {
            _action.Invoke();
        }
    }
}
