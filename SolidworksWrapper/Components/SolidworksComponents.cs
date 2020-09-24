using SolidWorks.Interop.sldworks;
using SolidworksWrapper.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Components
{
    public class SolidworksComponents : List<SolidWorksComponent>, IDisposable
    {
        private List<Tuple<SolidWorksComponent, bool>> _capturedState;

        public SolidworksComponents(object[] comps)
        {
            if (comps != null && comps.Count() > 0)
            {
                foreach (IComponent2 comp in comps)
                {
                    if (comp == null) continue;

                    Add(new SolidWorksComponent(comp));
                }
            }
        }

        public void CaptureSuppressionState()
        {
            _capturedState = new List<Tuple<SolidWorksComponent, bool>>();

            ForEach(x => _capturedState.Add(new Tuple<SolidWorksComponent, bool>(x, x.Suppressed)));;
        }

        public void RestoreSuppressionState()
        {
            if (_capturedState != null && _capturedState.Count > 0)
            {
                foreach (var c in this)
                {
                    var captureItem = _capturedState.FirstOrDefault(x => x.Item1.Id == c.Id);
                       
                    if (captureItem != null)
                    {
                        c.Suppressed = captureItem.Item2;
                    }
                }
            }
        }

        public void UnsuppressAll() => ForEach(x => x.Suppressed = false);

        public List<string> GetReferencedDocumentsNames()
        {
            var tempList = new List<string>();

            var index = 0;

            foreach (var c in this)
            {
                if (c.Suppressed) continue;

                if (index == 0)
                {
                    tempList.Add(c.SolidworksDocument.FullFileName);
                }
                else
                {
                    try
                    {
                        if (tempList.Any(x => x.ToUpper() == c.SolidworksDocument.FullFileName.ToUpper())) continue;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Could not retreive document name " + ex.ToString());
                    }

                    tempList.Add(c.SolidworksDocument.FullFileName);
                }

                index++;
            }

            return tempList;
        }

        public List<SolidworksDocument> GetReferencedDocuments()
        {
            var tempList = new List<SolidworksDocument>();

            var index = 0;

            foreach (var c in this)
            {
                if (c.Suppressed) continue;

                if (index == 0)
                {
                    tempList.Add(c.SolidworksDocument);
                }
                else
                {
                    try
                    {
                        if (tempList.Any(x => x.FullFileName.ToUpper() == c.SolidworksDocument.FullFileName.ToUpper())) continue;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Could not retreive document name " + ex.ToString());
                    }

                    tempList.Add(c.SolidworksDocument);
                }

                index++;
            }

            return tempList;
        }

        public void Dispose()
        {
            foreach (var c in this)
            {
                c.Dispose();
            }

            this.Clear();
        }
    }
}
