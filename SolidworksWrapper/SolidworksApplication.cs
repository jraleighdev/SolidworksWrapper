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
    /// <summary>
    /// Solidworks application management
    /// </summary>
    public static class SolidworksApplication
    {
        #region Interops

        /// <summary>
        /// Reference to the solidworks application interop
        /// </summary>
        public static SolidWorks.Interop.sldworks.SldWorks _solidWorks;
        
        #endregion

        #region Application Events

        /// <summary>
        /// Handler for document changed
        /// </summary>
        public delegate void DocumentChangedHandler();

        /// <summary>
        /// Event for the document changed
        /// </summary>
        public static event DocumentChangedHandler DocumentChanged;

        /// <summary>
        /// Listen to changes in solidworks active document
        /// </summary>
        public static void Listen()
        {
            _solidWorks.ActiveDocChangeNotify += NotifyDocumentChanged;
        }

        /// <summary>
        /// Stops listening to change in active documents in sollidworks
        /// </summary>
        public static void StopListening()
        {
            ((SldWorks)_solidWorks).ActiveDocChangeNotify -= NotifyDocumentChanged;
        }
         
        /// <summary>
        /// Emit the document has changed
        /// </summary>
        /// <returns></returns>
        public static int NotifyDocumentChanged()
        {
            DocumentChanged();

            return 0;
        }

        #endregion

        #region Application methods and properties

        /// <summary>
        /// Attach the wrapper to solidworks
        /// </summary>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// If the wrapper is attached to solidworks
        /// </summary>
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

        #endregion

        #region Document Methods and properties

        /// <summary>
        /// Gets the active document
        /// </summary>
        public static SolidworksDocument ActiveDocument
        {
            get
            {
                IModelDoc2 doc = _solidWorks.ActiveDoc;

                if (doc == null) return null;

                return new SolidworksDocument(doc);
            }
        }

        /// <summary>
        /// Activate the given document the document should be a reference in the active assembly
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static SolidworksDocument ActivateDocument(string name)
        {
            IModelDoc2 doc = _solidWorks.ActivateDoc(name);

            if (doc == null) return null;

            return new SolidworksDocument(doc);
        }

        /// <summary>
        /// Opens the with the matching name
        /// </summary>
        /// <param name="name">Full file name of the document</param>
        /// <param name="documentTypes">Document type</param>
        /// <param name="configuration">Configuration to activate in the document</param>
        /// <param name="openDocumentOptions">Options for the opening the document</param>
        /// <returns></returns>
        public static SolidworksDocument Open(string name, DocumentTypes documentTypes, string configuration = "", OpenDocumentOptions openDocumentOptions = OpenDocumentOptions.Silent)
        {
            int errors = 0;
            int warnings = 0;

            IModelDoc2 doc = _solidWorks.OpenDoc6(name, (int)documentTypes, (int)openDocumentOptions, configuration, ref errors, ref warnings);

            if (doc == null) return null;

            return new SolidworksDocument(doc);
        }

        /// <summary>
        /// Closes all the open documents
        /// </summary>
        public static void CloseAll() => _solidWorks.CloseAllDocuments(true);

        /// <summary>
        /// Closes the document that matches the given name
        /// </summary>
        /// <param name="name"></param>
        public static void Close(string name)
        {
            _solidWorks.CloseDoc(name);
        }
        
        #endregion
        
        public static void Dispose()
        {
            if (_solidWorks != null)
            {
                Marshal.ReleaseComObject(_solidWorks);
            }
        }
    }
}
