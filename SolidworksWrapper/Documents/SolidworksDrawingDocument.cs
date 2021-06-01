using SolidWorks.Interop.sldworks;
using SolidworksWrapper.Drawing.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.swconst;
using SolidworksWrapper.Drawing.Enums;
using SolidworksWrapper.Drawing.Points.Extensions;
using SolidworksWrapper.Drawing.Points.Interfaces;
using SolidworksWrapper.Drawing.Sheet;
using SolidworksWrapper.General;

namespace SolidworksWrapper.Documents
{
    /// <summary>
    /// Solidworks Drawing document if created from the Solidworks document use dispose from the parent
    /// </summary>
    public class SolidworksDrawingDocument
    {
        public IDrawingDoc drawingDoc;

        /// <summary>
        /// Still testing out if this is a good work flow :)
        /// Idea is drawings can have a lot sheets, views, and curve collections so have a single place to dispose of all might make things easier
        /// </summary>
        private List<IDisposable> _disposables;

        public SolidworksDocument DocumentReference { get; private set; }

        /// <summary>
        /// Activate a sheet by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ActivateSheet(string name) => drawingDoc.ActivateSheet(name);

        /// <summary>
        /// Activate a viewRef by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ActivateView(string name) => drawingDoc.ActivateView(name);

        /// <summary>
        /// Get the current sheet count
        /// </summary>
        /// <returns></returns>
        public int SheetCount() => drawingDoc.GetSheetCount();

        /// <summary>
        /// Get the sheet names
        /// </summary>
        public string[] StringNames => drawingDoc.GetSheetNames();

        /// <summary>
        /// 
        /// </summary>
        public int ViewCount => drawingDoc.GetViewCount();

        public SolidworksDrawingDocument(SolidworksDocument documentRef, IDrawingDoc doc)
        {
            this.drawingDoc = doc;
            DocumentReference = documentRef;
            _disposables = new List<IDisposable>();
        }

        #region Build

        /// <summary>
        /// Rebuild the document
        /// </summary>
        public void Rebuild() => drawingDoc.EditRebuild();

        #endregion

        #region Sheets

        /// <summary>
        /// Gets the active sheet from the drawing
        /// </summary>
        public SolidworksSheet ActiveSheet
        {
            get
            {
                var sheet = new SolidworksSheet(drawingDoc.GetCurrentSheet(), this);

                _disposables.Add(sheet);

                return sheet;
            }
        }

        /// <summary>
        /// Adds a new sheet to the active drawing document
        /// </summary>
        /// <param name="name"></param>
        /// <param name="paperSize"></param>
        /// <param name="template"></param>
        /// <param name="scaleOne"></param>
        /// <param name="scaleTwo"></param>
        /// <param name="firstAngle"></param>
        /// <param name="templateName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="propertyViewName"></param>
        /// <param name="zoneLeftMargin"></param>
        /// <param name="zoneRightMargin"></param>
        /// <param name="zoneTopMargin"></param>
        /// <param name="zoneBottomMargin"></param>
        /// <param name="zoneRow"></param>
        /// <param name="zoneCol"></param>
        /// <returns></returns>
        public SolidworksSheet AddSheet(string name, PaperSizeEnum paperSize, DwgTemplateSize template, double scaleOne, double scaleTwo, bool firstAngle, string templateName, double width, double height, string propertyViewName, double zoneLeftMargin, double zoneRightMargin, double zoneTopMargin, double zoneBottomMargin, int zoneRow, int zoneCol)
        {
            var success = drawingDoc.NewSheet4(name, (int) paperSize, (int) template, scaleOne, scaleTwo,
                firstAngle, templateName, width, height, propertyViewName, zoneLeftMargin, zoneRightMargin,
                zoneTopMargin, zoneBottomMargin, zoneRow, zoneCol);

            if (!success) throw new Exception("Could create a new sheet");

            // make sure the sheet is active
            ActivateSheet(name);

            return ActiveSheet;
        }


        #endregion


        #region Views

        /// <summary>
        /// Create detail viewRef from a parent viewRef
        /// </summary>
        /// <param name="x">X position for the detail viewRef</param>
        /// <param name="y">Y Position for the detail viewRef</param>
        /// <param name="style">Style of viewRef based on <see cref="DetailViewStyleEnum"/> </param>
        /// <param name="scaleOne">Numerator value of the scale</param>
        /// <param name="scaleTwo">Denominator value of the scale</param>
        /// <param name="label">View for the detail viewRef</param>
        /// <param name="showType">Type of the sketch for the detail viewRef <see cref="DetailViewCircleShow"/></param>
        /// <param name="fullOutline">True to show a full outline, false to not; valid only if NoOutline is false</param>
        /// <param name="jaggedOutline">True to show a jagged outline, false to not; only valid if NoOutline is false</param>
        /// <param name="noOutline">True to not show an outline, false to show an outline</param>
        /// <param name="z">Z position for the default value of 0</param>
        /// <param name="jaggedOutLine">Intensity of jagged outline; valid range is 1 (most) to 5 (least) and only valid if JaggedOutline is true and NoOutline is false</param>
        /// <returns></returns>
        public SolidworksDrawingView CreateDetailView(double x, double y, DetailViewStyleEnum style, double scaleOne, double scaleTwo, string label, DetailViewCircleShow showType, bool fullOutline, bool jaggedOutline, bool noOutline,  double z = 0, int jaggedOutLine = 1)
        {
            return drawingDoc.CreateDetailViewAt4(x, y, z, (int) style, scaleOne, scaleTwo, label, (int) showType, fullOutline,
                jaggedOutline, noOutline, (int) jaggedOutLine);

        }

        public void Rotate(SolidworksDrawingView view, double degrees)
        {
            view.ClearSelection();

            view.Select();

            drawingDoc.DrawingViewRotate(UnitManager.ConvertRadians(degrees));

            view.ClearSelection();
        }

        public void RemoveHiddenLines(SolidworksDrawingView view)
        {
            view.ClearSelection();

            view.Select();

            view.Activate();

            drawingDoc.ViewDisplayHidden();
        }

        public void InsertCenterMark(SolidworksDrawingView view, IEnumerable<ISolidworksPoint> points, CenterMarkStyleEnum style, bool applyToPattern, bool forSlot)
        {
            view.ClearSelection();

            points.SelectAll();

            var centerMarks = drawingDoc.InsertCenterMark3((int)style, applyToPattern, forSlot);

            if (style == CenterMarkStyleEnum.LinearGroup)
            {
                centerMarks.ConnectionLines = (int)CenterMarkConnectionLineEnum.ShowLinear;
            }

            view.ClearSelection();
        }

        #endregion

        #region Memory Managament

        /// <summary>
        /// Dispose of all drawings child objects
        /// </summary>
        public void DisposeOfChildren()
        {
            foreach (var d in _disposables)
            {
                try
                {
                    d.Dispose();
                }
                catch (Exception e)
                {
                    throw new Exception("Ran into an error disposing a drawing child object", e);
                }
            }
        }

        #endregion
    }
}
