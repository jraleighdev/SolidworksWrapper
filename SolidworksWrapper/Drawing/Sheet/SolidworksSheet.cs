using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidworksWrapper.Documents;
using SolidworksWrapper.Drawing.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SolidworksWrapper.Drawing.Views;

namespace SolidworksWrapper.Drawing.Sheet
{
    public class SolidworksSheet : IDisposable
    {
        public ISheet sheet;

        private double[] _properties;

        public SolidworksDrawingDocument Parent { get; private set; }
       
        public PaperSizes PaperSize => (PaperSizes)Convert.ToInt32(_properties[0]);

        public DrawingTemplates DrawingTemplate => (DrawingTemplates)Convert.ToInt32(_properties[1]);

        public List<SolidworksDrawingView> Views { get; private set; }

        public double ScaleOne => _properties[2];

        public double ScaleTwo => _properties[3];

        public bool FirstAngle => Convert.ToBoolean(_properties[4]);

        public double Width => _properties[5];

        public double Height => _properties[6];

        public bool Loaded => sheet.IsLoaded();

        public bool SameCustProp => Convert.ToBoolean(_properties[7]);

        public string Name
        {
            get => sheet.GetName();
            set => sheet.SetName(value);
        }

        public SolidworksSheet(ISheet sheet, SolidworksDrawingDocument parent)
        {
            this.sheet = sheet;
            Parent = parent;
            _properties = sheet.GetProperties2();

            Views = new List<SolidworksDrawingView>();
            GetViews();
        }

        public bool SetScale(double num, double denom)
        {
            return sheet.SetScale(num, denom, false, false);
        }

        public void SetProperties(PaperSizes paperSize, DrawingTemplates template, double scale1, double scale2, bool firstAngle = true, double width = 0, double height = 0, bool sameCustomPropAsSheet = false)
        {
            sheet.SetProperties2((int)paperSize, (int)template, scale1, scale2, firstAngle, width, height, sameCustomPropAsSheet);

            // refresh the cached array
            _properties = sheet.GetProperties2();
        }

        public void ZoomToFit()
        {
            Select();

            Parent.DocumentReference.modelDoc.ViewZoomToSelection();

            Parent.DocumentReference.modelDoc.ClearSelection2(true);
        }

        public void Select() => Parent.DocumentReference.Select(Name, "SHEET");

        #region Drawing Views

        private void GetViews()
        {
            object[] views = sheet.GetViews();

            if (views == null || !views.Any()) return;

            foreach (IView view in views)
            {
                Views.Add(new SolidworksDrawingView(view, this));
            }
        }

        public SolidworksDrawingView GetView(string name) => this.Views.FirstOrDefault(x => x.Name == name);

        #endregion

        public void Dispose()
        {
            if (sheet != null)
            {
                Marshal.ReleaseComObject(sheet);
                sheet = null;
            }
        }
    }
}
