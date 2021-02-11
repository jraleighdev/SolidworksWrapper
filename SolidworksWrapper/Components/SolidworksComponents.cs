using SolidWorks.Interop.sldworks;
using SolidworksWrapper.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Components
{
    /// <summary>
    /// Solidworks component collection
    /// </summary>
    public class SolidworksComponents : List<SolidWorksComponent>, IDisposable
    {
        private List<Tuple<SolidWorksComponent, bool>> _capturedState;

        /// <summary>
        /// Gets the components from an array of objects
        /// </summary>
        /// <param name="comps"></param>
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

        /// <summary>
        /// Captures the suppression state of all components in the model
        /// Some operations in solidworks un-suppressing all makes the operation simple
        /// </summary>
        public void CaptureSuppressionState()
        {
            _capturedState = new List<Tuple<SolidWorksComponent, bool>>();

            ForEach(x => _capturedState.Add(new Tuple<SolidWorksComponent, bool>(x, x.Suppressed)));;
        }

        /// <summary>
        /// Restores a previous capture state
        /// </summary>
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

        /// <summary>
        /// Unsupress all the components
        /// </summary>
        public void UnsuppressAll() => ForEach(x => x.Suppressed = false);

        /// <summary>
        /// Gets all the referenced document names from the active document
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                        throw new Exception("Could not retrieve document name " + ex.ToString());
                    }

                    tempList.Add(c.SolidworksDocument.FullFileName);
                }

                index++;
            }

            return tempList;
        }

        /// <summary>
        /// Gets the referenced documents from the active assembly
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
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
                        throw new Exception("Could not retrieve document name " + ex.ToString());
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
