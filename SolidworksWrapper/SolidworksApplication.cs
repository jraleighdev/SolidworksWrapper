using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidworksWrapper.Documents;
using SolidworksWrapper.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper
{
    public static class SolidworksApplication
    {
        public static SolidWorks.Interop.sldworks.SldWorks _solidWorks;

        public delegate void DocumentChangedHandler();

        public static event DocumentChangedHandler DocumentChanged;

        public static void Listen()
        {
            _solidWorks.ActiveDocChangeNotify += NotifyDocumentChanged;
        }

        public static void StopListening()
        {
            ((SldWorks)_solidWorks).ActiveDocChangeNotify -= NotifyDocumentChanged;
        }
         
        public static int NotifyDocumentChanged()
        {
            DocumentChanged();

            return 0;
        }

        public static void Attach()
        {
            try
            {
                _solidWorks = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            }
            catch (Exception)
            {
                try
                {
                    Type invAppType = Type.GetTypeFromProgID("SldWorks.Application");

                    _solidWorks = (SldWorks)System.Activator.CreateInstance(invAppType);
                    _solidWorks.Visible = true;
                }
                catch (Exception exception)
                {
                    throw new Exception("Unable to get or start Solidworks");
                }
            }
        }

        public static bool Attached
        {
            get
            {
                if (_solidWorks == null)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        var doc = _solidWorks.GetDocumentCount();

                        return true;
                    }
                    catch (Exception ex)
                    {
                        _solidWorks = null;
                        return false;
                    }
                }
            }
        }

        public static SolidworksDocument ActiveDocument
        {
            get
            {
                IModelDoc2 doc = _solidWorks.ActiveDoc;

                if (doc == null) return null;

                return new SolidworksDocument(doc);
            }
        }

        public static SolidworksDocument ActivateDocument(string name)
        {
           IModelDoc2 doc = _solidWorks.ActivateDoc(name);

            if (doc == null) return null;

            return new SolidworksDocument(doc);
        }

        public static SolidworksDocument Open(string name, DocumentTypes documentTypes, string configuration = "", OpenDocumentOptions openDocumentOptions = OpenDocumentOptions.Silent)
        {
            int errors = 0;
            int warnings = 0;

            IModelDoc2 doc = _solidWorks.OpenDoc6(name, (int)documentTypes, (int)openDocumentOptions, configuration, ref errors, ref warnings);

            if (doc == null) return null;

            return new SolidworksDocument(doc);
        }

        public static void CloseAll() => _solidWorks.CloseAllDocuments(true);

        public static void Close(string name)
        {
            _solidWorks.CloseDoc(name);
        }

        public static void Dispose()
        {
            if (_solidWorks != null)
            {
                Marshal.ReleaseComObject(_solidWorks);
            }
        }
    }
}
