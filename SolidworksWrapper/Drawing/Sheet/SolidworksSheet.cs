using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidworksWrapper.Base;
using SolidworksWrapper.Documents;
using SolidworksWrapper.Drawing.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Drawing.Sheet
{
    public class SolidworksSheet : SolidworksBaseObject<ISheet>
    {
        private double[] _properties;

        public SolidworksDrawingDocument Parent { get; private set; }
       
        public PaperSizes PaperSize => (PaperSizes)Convert.ToInt32(_properties[0]);

        public DrawingTemplates DrawingTemplate => (DrawingTemplates)Convert.ToInt32(_properties[1]);

        public double ScaleOne => _properties[2];

        public double ScaleTwo => _properties[3];

        public bool FirstAngle => Convert.ToBoolean(_properties[4]);

        public double Width => _properties[5];

        public double Height => _properties[6];

        public bool Loaded => BaseObject.IsLoaded();

        public bool SameCustProp => Convert.ToBoolean(_properties[7]);

        public string Name
        {
            get => BaseObject.GetName();
            set => BaseObject.SetName(value);
        }

        public SolidworksSheet(ISheet sheet, SolidworksDrawingDocument parent) : base(sheet)
        {
            Parent = parent;
            _properties = BaseObject.GetProperties2();
        }

        public bool SetScale(double num, double denom)
        {
            return BaseObject.SetScale(num, denom, false, false);
        }

        public void SetProperties(PaperSizes paperSize, DrawingTemplates template, double scale1, double scale2, bool firstAngle = true, double width = 0, double height = 0, bool sameCustomPropAsSheet = false)
        {
            BaseObject.SetProperties2((int)paperSize, (int)template, scale1, scale2, firstAngle, width, height, sameCustomPropAsSheet);

            // refresh the cached array
            _properties = BaseObject.GetProperties2();
        }
    }
}
