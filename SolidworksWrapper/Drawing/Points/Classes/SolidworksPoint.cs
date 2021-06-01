using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidworksWrapper.Base;
using SolidworksWrapper.Components;
using SolidworksWrapper.Drawing.Points.Enums;
using SolidworksWrapper.Drawing.Points.Interfaces;

namespace SolidworksWrapper.Drawing.Points.Classes
{
    public class SolidworksPoint : ISolidworksPoint
    {
        public IEntity _entity;

        public IVertex _vertex;

        public SolidworksPoint(IEntity entity, double x, double y, SolidWorksComponent component)
        {
            _entity = entity;
            X = x;
            Y = y;
            Type = PointTypeEnum.Entity;
            Component = component;
            _vertex = entity as IVertex;
        }

        public PointTypeEnum Type { get; private set; }
        public double X { get; private set; }
        public double Y { get; private set; }
        
        public SolidWorksComponent Component { get; private set; }
        
        public void Select()
        {
            _entity.Select4(true, null);
        }

        public void Dispose()
        {
            if (_vertex != null)
            {
                Marshal.ReleaseComObject(_vertex);
            }

            if (_entity != null)
            {
                Marshal.ReleaseComObject(_entity);
            }

            _vertex = null;
            _entity = null;
        }
    }
}
