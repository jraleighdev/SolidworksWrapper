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
using SolidworksWrapper.Drawing.Enums;
using SolidworksWrapper.Drawing.Points.Classes;
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

        public bool Select() => SolidworksApplication.ActiveDocument.Select(Name, "DRAWINGVIEW");

        public void ClearSelection() => SolidworksApplication.ActiveDocument.ClearSelection();

        public void Delete()
        {
            ClearSelection();

            Select();

            SolidworksApplication.ActiveDocument.DeleteSelected();

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
