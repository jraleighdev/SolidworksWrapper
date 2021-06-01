using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidworksWrapper.Components;
using SolidworksWrapper.Drawing.Points.Classes;
using SolidworksWrapper.Drawing.Points.Extensions;
using SolidworksWrapper.Drawing.Points.Interfaces;
using SolidworksWrapper.Drawing.Views;
using SolidworksWrapper.General;

namespace SolidworksWrapper.Drawing.Edge
{
    public class SolidworksDrawingEdge : IDisposable
    {
        #region Fields

        private ICurve _curve;

        private IEdge _edge;

        private MathUtility _mathUtils;

        private SolidworksDrawingView _view;

        private SolidWorksComponent _comp;

        private ICurveParamData _curveData;

        public double UMax { get; private set; }

        public double Umin { get; private set; }

        #endregion

        public SolidworksDrawingEdge(SolidworksDrawingView view, IEntity entity, SolidWorksComponent comp)
        {
            Tolerance = 0.005;

            _edge = (IEdge) entity;
            _view = view;
            _curve = _edge.GetCurve();
            _curveData = _edge.GetCurveParams3();
            _mathUtils = SolidworksApplication._solidWorks.GetMathUtility();
            UMax = _curveData.UMaxValue;
            Umin = _curveData.UMinValue;
            _comp = comp;
        }

        #region Properties

        public bool IsLine => _curve.IsLine();

        public bool IsCircle
        {
            get
            {
                var value = false;

                if (_curve.IsCircle())
                {
                    if (StartPoint == null && EndPoint == null)
                    {
                        value = CurveStart().PointsMatch(CurveEnd(), .001);
                    }
                }

                return value;
            }
        }

        public bool IsArc
        {
            get
            {
                var value = false;

                if (_curve.IsCircle())
                {
                    value = CurveStart().PointsMatch(CurveEnd());
                }

                return value != true;
            }
        }

        public double Length =>
            IsLine ? Math.Sqrt(Math.Pow(StartPoint.X - EndPoint.X, 2) + Math.Pow(StartPoint.Y - EndPoint.Y, 2)) : 0;

        public bool IsTrimmed => _curve.IsTrimmedCurve();

        public ISolidworksPoint StartPoint => GetStartPoint();

        public ISolidworksPoint EndPoint => GetEndPoint();

        public ISolidworksPoint CenterPoint => GetCenterPoint();

        public ISolidworksPoint MidPoint => GetMidPoint();

        public bool IsHorizontal => IsLine && Math.Abs(StartPoint.Y - EndPoint.Y) < Tolerance;

        public bool IsVertical => IsLine && Math.Abs(StartPoint.X - EndPoint.X) < Tolerance;

        public double Slope => IsLine ? (EndPoint.Y - StartPoint.Y) / (EndPoint.Y - EndPoint.X) : 0;

        public double Angle => IsLine ? UnitManager.ConvertRadians(Math.Atan(Slope)) : 0;

        public double Tolerance { get; set; }

        #endregion

        #region Public Methods

        public ISolidworksPoint CurveStart()
        {
            var point = new double[3];

            point[0] = _curveData.StartPoint[0];
            point[1] = _curveData.StartPoint[1];
            point[2] = _curveData.StartPoint[2];

            return _view.TransformedPoint((IEntity)_edge, point, true, _comp);
        }

        public ISolidworksPoint CurveEnd()
        {

            var point = new double[3];

            point[0] = _curveData.EndPoint[0];
            point[1] = _curveData.EndPoint[1];
            point[2] = _curveData.EndPoint[2];

            return _view.TransformedPoint((IEntity)_edge, point, true, _comp);
        }

        public void Select()
        {
            var entity = (IEntity)_edge;

            entity.Select4(true, null);
        }

        public void SelectMidPoint()
        {
            Select();

            SolidworksApplication.ActiveDocument.SelectMidPoint();
        }

        #endregion

        #region Private Methods

        private ISolidworksPoint GetStartPoint()
        {
            IVertex vertex = _edge.GetStartVertex();

            if (vertex == null) return null;

            double[] point = vertex.GetPoint();

            return _view.TransformedPoint((IEntity) vertex, point, false, _comp);
        }

        private ISolidworksPoint GetEndPoint()
        {
            IVertex vertex = _edge.GetEndVertex();

            if (vertex == null) return null;

            double[] point = vertex.GetPoint();

            return _view.TransformedPoint((IEntity) vertex, point, false, _comp);
        }

        private ISolidworksPoint GetMidPoint()
        {
            if (!IsLine) throw new ArgumentException("Curve must be a linear curve");

            double x = (StartPoint.X + EndPoint.X) / 2;
            double y = (StartPoint.Y + EndPoint.Y) / 2;

            return new SolidworksDelegatePoint(SelectMidPoint, x, y);
        }

        private ISolidworksPoint GetCenterPoint()
        {
            if (!IsCircle) throw new ArgumentException("Curve must be a circular curve");

            double[] circleParams = _curve.CircleParams;

            var point = new double[3];

            point[0] = circleParams[0];

            point[1] = circleParams[1];

            point[2] = circleParams[2];

            return _view.TransformedPoint((IEntity) _edge, point, true, _comp);
        }

        #endregion

        public void Dispose()
        {
            Marshal.ReleaseComObject(_curveData);
            Marshal.ReleaseComObject(_curve);
            Marshal.ReleaseComObject(_edge);
        }
    }
}
