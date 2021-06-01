using SolidWorks.Interop.sldworks;
using SolidworksWrapper.Base;
using SolidworksWrapper.Drawing.Sheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.SWRoutingLib;
using SolidworksWrapper.Components;
using SolidworksWrapper.Documents;
using SolidworksWrapper.Drawing.Dimensions.Enums;
using SolidworksWrapper.Drawing.Edge;
using SolidworksWrapper.Drawing.Enums;
using SolidworksWrapper.Drawing.Points.Classes;
using SolidworksWrapper.Drawing.Points.Extensions;
using SolidworksWrapper.Drawing.Points.Interfaces;
using SolidworksWrapper.Enums;
using SolidworksWrapper.General;

namespace SolidworksWrapper.Drawing.Views
{
    public class SolidworksDrawingView : SolidworksBaseObject<IView>
    {
        private SolidworksDocument _referencedDocument;

        /// <summary>
        /// Id for the view used for tracking the views when creating and editing
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Math utils
        /// </summary>
        public MathUtility _mathUtils;

        #region Properties

        public SolidworksSheet Parent { get; private set; }

        public string Name
        {
            get => BaseObject.GetName2();
            set => BaseObject.SetName2(value);
        }

        public double X => UnitManager.UnitsFromSolidworks(BaseObject.Position[0]);

        public double Y => UnitManager.UnitsFromSolidworks(BaseObject.Position[1]);

        public double MinX => UnitManager.UnitsFromSolidworks(BaseObject.GetOutline()[0]);

        public double MinY => UnitManager.UnitsFromSolidworks(BaseObject.GetOutline()[1]);

        public double MaxX => UnitManager.UnitsFromSolidworks(BaseObject.GetOutline()[2]);

        public double MaxY => UnitManager.UnitsFromSolidworks(BaseObject.GetOutline()[3]);

        public double CenterX => MinX + (MaxX - MinX) / 2;

        public double CenterY => MinY + (MaxY - MinY) / 2;

        public double Width => MaxX - MinX;

        public double Height => MaxY - MinY;

        public double Scale
        {
            get => BaseObject.ScaleDecimal;
            set => BaseObject.ScaleDecimal = value;
        }

        public string RefrencedConfiguration
        {
            get => BaseObject.ReferencedConfiguration;
            set
            {
                BaseObject.ReferencedConfiguration = value;
                Parent.Parent.Rebuild();
            }
        }

        public SolidworksDocument ReferencedDocument
        {
            get
            {
                if (_referencedDocument == null)
                {
                    _referencedDocument = new SolidworksDocument(BaseObject.ReferencedDocument);
                }

                Disposables.Add(_referencedDocument);

                return _referencedDocument;
            }
        }

        public bool IsBroken => BaseObject.IsBroken();

        public bool BaseView { get; set; }

        public List<IDisposable> Disposables { get; private set; }

        #endregion

        public SolidworksDrawingView(IView comObject, SolidworksSheet parent) : base(comObject)
        {
            Id = Guid.NewGuid();
            Parent = parent;
            _mathUtils = SolidworksApplication._solidWorks.GetMathUtility();
            Disposables = new List<IDisposable>();
        }

        #region Methods

        public bool Activate() => Parent.Parent.ActivateView(Name);

        public bool Deactivate() => Parent.Parent.ActivateView("");

        public bool Select() => Parent.Parent.DocumentReference.Select(Name, "DRAWINGVIEW");

        public void ClearSelection() => Parent.Parent.DocumentReference.ClearSelection();

        public void Delete()
        {
            ClearSelection();

            Select();

            Parent.Parent.DocumentReference.DeleteSelected();

            ClearSelection();

            Dispose();
        }

        public void RemoveHiddenLines() => Parent.Parent.RemoveHiddenLines(this);

        public void DeleteAnnotations()
        {
            ClearSelection();

            object[] annonations = BaseObject.GetAnnotations();

            if (annonations == null) return;

            foreach (IAnnotation annotation in annonations)
            {
                annotation.Select3(true, null);
            }

            SolidworksApplication.ActiveDocument.DeleteSelected();

            ClearSelection();
        }

        public void Move(double x, double y)
        {
            double[] newPosition = {UnitManager.UnitsToSolidworks(x), UnitManager.UnitsToSolidworks(y)};

            BaseObject.Position = newPosition;

            Parent.Parent.Rebuild();
        }

        public bool AlignVertical(SolidworksDrawingView view) =>
            BaseObject.AlignWithView((int) AlignmentTypeEnum.AlignViewHorizontalCenter, view.BaseObject as View);

        public bool AlignHorizontal(SolidworksDrawingView view) =>
            BaseObject.AlignWithView((int)AlignmentTypeEnum.AlignViewVerticalCenter, view.BaseObject as View);

        public void Rotate(double degrees)
        {
            Parent.Parent.Rotate(this, degrees);
        }

        #endregion

        #region Entities

        public SolidworksComponents GetVisibleComponents() =>
            new SolidworksComponents(BaseObject.GetVisibleComponents());

        public List<SolidworksDrawingEdge> Edges(SolidWorksComponent comp)
        {
            var tempList = new List<SolidworksDrawingEdge>();

            Select();

            Activate();

            object[] edges =
                BaseObject.GetVisibleEntities2((Component2) comp.UnSafeObject, (int) ViewEntitiesEnum.Edge);

            if (edges != null)
            {
                foreach (IEntity e in edges)
                {
                    IEdge edge = e as IEdge;

                    if (e != null)
                    {
                        tempList.Add(new SolidworksDrawingEdge(this, e, comp));
                    }
                }
            }

            return tempList;
        }

        public List<SolidworksDrawingEdge> Edges(IEnumerable<SolidWorksComponent> comps)
        {
            var tempList = new List<SolidworksDrawingEdge>();

            foreach (var c in comps)
            {
                tempList.AddRange(Edges(c));
            }

            return tempList;
        }

        public List<ISolidworksPoint> Points(IEnumerable<SolidWorksComponent> comps)
        {
            var tempList = new List<ISolidworksPoint>();

            foreach (var comp in comps)
            {
                tempList.AddRange(Points(comp));
            }

            return tempList;
        }

        public List<ISolidworksPoint> Points(SolidWorksComponent comp)
        {
            Select();

            Activate();

            object[] vertex =
                BaseObject.GetVisibleEntities2((Component2) comp.UnSafeObject, (int) ViewEntitiesEnum.Vertex);

            var tempList = new List<ISolidworksPoint>();

            if (vertex == null) return tempList;

            if (ReferencedDocument.IsAssemblyDoc)
            {
                foreach (IVertex v in vertex)
                {
                    if (v == null) continue;

                    tempList.Add(TransformedPoint((IEntity)v, v.GetPoint(), false, comp));
                }
            }
            else if (ReferencedDocument.IsPartDoc)
            {
                foreach (IVertex v in vertex)
                {
                    if (v == null) continue;
                    
                    tempList.Add(TransformedPoint((IEntity)v, v.GetPoint(), false, comp));
                }
            }

            return tempList;
        }

        #endregion

        #region Dimensions

        public void CleanUpDims()
        {
            CenterAllLinear();
            AlignDimsWithView();
        }

        public void CenterAllLinear()
        {
            foreach (DisplayDimension d in BaseObject.GetDisplayDimensions())
            {
                d.CenterText = true;
            }
        }

        public void AlignDimsWithView(double offset = 0.25)
        {
            foreach (DisplayDimension d in BaseObject.GetDisplayDimensions())
            {
                Annotation annotation = d.GetAnnotation();

                var xLocation = UnitManager.UnitsFromSolidworks(annotation.GetPosition()[0]);
                var yLocation = UnitManager.UnitsFromSolidworks(annotation.GetPosition()[1]);

                if (xLocation > MaxX)
                {
                    xLocation = MaxX - offset;
                }
                else if (xLocation < MinX)
                {
                    xLocation = MinX - offset;
                }

                if (yLocation > MaxY)
                {
                    yLocation = MaxY + offset;
                }
                else if (yLocation < MinY)
                {
                    yLocation = MinY - offset;
                }

                annotation.SetPosition2(UnitManager.UnitsToSolidworks(xLocation),
                    UnitManager.UnitsToSolidworks(yLocation), 0.00);
            }
        }

        public void AddDim(ISolidworksPoint pointOne, ISolidworksPoint pointTwo, DimensionOrientationEnum type,
            double x, double y) => Parent.Parent.DocumentReference.AddDim(pointOne, pointTwo, type, x, y);

        public void AddOrdinateDim(List<ISolidworksPoint> points, DimensionOrientationEnum type, double offset) =>
            Parent.Parent.DocumentReference.AddOrdinateDim(this, points, type, offset);

        public void AddDiameterDim(ISolidworksPoint point, double x, double y) =>
            Parent.Parent.DocumentReference.AddDiameterDim(point, x, y);

        #endregion

        #region Center Marks and lines

        public void AddCenterMark(IEnumerable<ISolidworksPoint> points,
            CenterMarkStyleEnum style = CenterMarkStyleEnum.Single, bool applyToPattern = false, bool forSlot = false)
        {
            Parent.Parent.InsertCenterMark(this, points, style, applyToPattern, forSlot);
        }

        public void AddCenterLines(IEnumerable<ISolidworksPoint> points, bool vertical, bool horizontal,
            double tolerance = 0.005)
        {
            ClearSelection();

            if (vertical)
            {
                var verticalAddedPoint = new List<ISolidworksPoint>();

                var orderPointsFromBottom = points.OrderBy(x => x.Y).ToList();

                for (var i = 0; i < orderPointsFromBottom.Count - 1; i++)
                {
                    ClearSelection();

                    var p = orderPointsFromBottom[i];

                    if (verticalAddedPoint.Count > 0)
                    {
                        if (verticalAddedPoint.Any(x => x.PointsMatch(p))) continue;
                    }

                    var matchingYPoints = new List<ISolidworksPoint>();

                    matchingYPoints.Add(p);

                    for (var j = i + 1; j < orderPointsFromBottom.Count; j++)
                    {
                        var pointsToCompare = orderPointsFromBottom[j];

                        if (Math.Abs(pointsToCompare.X - p.X) < tolerance)
                        {
                            matchingYPoints.Add(pointsToCompare);
                        }
                    }

                    if (matchingYPoints.Count > 0)
                    {
                        verticalAddedPoint.AddRange(matchingYPoints);

                        AddCenterMark(matchingYPoints, CenterMarkStyleEnum.LinearGroup, true);
                    }
                }
            }

            if (vertical)
            {
                var horizontalAddedPoint = new List<ISolidworksPoint>();

                var orderPointsFromLeft = points.OrderBy(x => x.X).ToList();

                for (var i = 0; i < orderPointsFromLeft.Count - 1; i++)
                {
                    ClearSelection();

                    var p = orderPointsFromLeft[i];

                    if (horizontalAddedPoint.Count > 0)
                    {
                        if (horizontalAddedPoint.Any(x => x.PointsMatch(p))) continue;
                    }

                    var matchingYPoints = new List<ISolidworksPoint>();

                    matchingYPoints.Add(p);

                    for (var j = i + 1; j < orderPointsFromLeft.Count; j++)
                    {
                        var pointsToCompare = orderPointsFromLeft[j];

                        if (Math.Abs(pointsToCompare.X - p.Y) < tolerance)
                        {
                            matchingYPoints.Add(pointsToCompare);
                        }
                    }

                    if (matchingYPoints.Count > 0)
                    {
                        horizontalAddedPoint.AddRange(matchingYPoints);

                        AddCenterMark(matchingYPoints, CenterMarkStyleEnum.LinearGroup, true);
                    }
                }
            }
        }

        #endregion

        #region Transformations

        public SolidworksPoint TransformedPoint(IEntity entity, double[] tPoint, bool transFormPosition,
            SolidWorksComponent component)
        {
            double[] point = tPoint;

            if (ReferencedDocument.IsAssemblyDoc)
            {
                // Get a reference to the comp
                Component2 entComp = entity.GetComponent();

                if (entComp == null) return null;

                MathTransform compTransform = entComp.Transform2;

                MathTransform viewTransform = BaseObject.ModelToViewTransform;

                MathPoint mPoint = _mathUtils.CreatePoint(point);

                Sketch sketch = BaseObject.GetSketch();

                MathTransform sketchTransform = sketch.ModelToSketchTransform;

                // Transform the point by the component matrix
                mPoint = mPoint.MultiplyTransform(compTransform);

                mPoint = mPoint.MultiplyTransform(SheetScaleTransform(transFormPosition));

                mPoint = mPoint.MultiplyTransform(viewTransform);

                mPoint = mPoint.IMultiplyTransform(sketchTransform);

                point = mPoint.ArrayData;
            }
            else if (ReferencedDocument.IsPartDoc)
            {
                MathTransform viewTransform = BaseObject.ModelToViewTransform;

                MathPoint mPoint = _mathUtils.CreatePoint(point);

                Sketch sketch = BaseObject.GetSketch();

                MathTransform sketchTransform = sketch.ModelToSketchTransform;

                mPoint = mPoint.MultiplyTransform(sketchTransform);

                mPoint = mPoint.MultiplyTransform(SheetScaleTransform(transFormPosition));

                mPoint = mPoint.MultiplyTransform(viewTransform);

                point = mPoint.ArrayData;
            }

            return new SolidworksPoint(entity, UnitManager.UnitsFromSolidworks(point[0]),
                UnitManager.UnitsFromSolidworks(point[1]), component);
        }

        public SolidworksPoint TransformedPointView(IEntity entity, double[] tPoint,
            SolidWorksComponent component)
        {
            double[] point = tPoint;

            if (ReferencedDocument.IsAssemblyDoc)
            {
                // Get a reference to the comp
                Component2 entComp = entity.GetComponent();

                if (entComp == null) return null;

                MathTransform compTransform = entComp.Transform2;

                MathTransform viewTransform = BaseObject.ModelToViewTransform;

                MathPoint mPoint = _mathUtils.CreatePoint(point);

                Sketch sketch = BaseObject.GetSketch();

                MathTransform sketchTransform = sketch.ModelToSketchTransform;

                // Transform the point by the component matrix
                mPoint = mPoint.MultiplyTransform(compTransform);

                mPoint = mPoint.MultiplyTransform(SheetScaleTransform(true, false));

                mPoint = mPoint.MultiplyTransform(viewTransform);

                mPoint = mPoint.IMultiplyTransform(sketchTransform);

                point = mPoint.ArrayData;
            }
            else if (ReferencedDocument.IsPartDoc)
            {
                MathTransform viewTransform = BaseObject.ModelToViewTransform;

                MathPoint mPoint = _mathUtils.CreatePoint(point);

                Sketch sketch = BaseObject.GetSketch();

                MathTransform sketchTransform = sketch.ModelToSketchTransform;

                mPoint = mPoint.MultiplyTransform(viewTransform);

                mPoint = mPoint.MultiplyTransform(sketchTransform);

                point = mPoint.ArrayData;
            }

            return new SolidworksPoint(entity, UnitManager.UnitsFromSolidworks(point[0]),
                UnitManager.UnitsFromSolidworks(point[1]), component);
        }

        protected MathTransform SheetScaleTransform(bool transFormPosition = false, bool transformScale = true)
        {
            var sheetProps = BaseObject.Sheet.GetProperties2();

            var sheetScaleNom = sheetProps[2];
            var sheetScaleDenom = sheetProps[3];

            var sheetData = new double[16];

            sheetData[0] = Math.Cos(BaseObject.Angle);
            sheetData[1] = Math.Sin(BaseObject.Angle);
            sheetData[2] = 0;
            sheetData[3] = (-1) * Math.Sin(BaseObject.Angle);
            sheetData[4] = Math.Cos(BaseObject.Angle);
            sheetData[5] = 0;
            sheetData[6] = 0;
            sheetData[7] = 0;
            sheetData[8] = 1;
            sheetData[9] = transFormPosition ? BaseObject.Position[0] : 0;
            sheetData[10] = transFormPosition ? BaseObject.Position[1] : 0;
            sheetData[11] = 0;
            sheetData[12] = transformScale ? sheetScaleNom / sheetScaleDenom : 1;
            sheetData[13] = 0;
            sheetData[14] = 0;
            sheetData[15] = 0;

            return _mathUtils.CreateTransform(sheetData);
        }


        #endregion

        public override void Dispose()
        {
            foreach (var dis in Disposables)
            {
                try
                {
                    dis.Dispose();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }

            base.Dispose();
        }

    }
}
