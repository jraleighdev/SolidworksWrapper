using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using SolidworksWrapper.CaptureDtos;
using SolidworksWrapper.Components;
using SolidworksWrapper.Configurations;
using SolidworksWrapper.Constants;
using SolidworksWrapper.CustomProperties;
using SolidworksWrapper.Dimensions;
using SolidworksWrapper.Enums;
using SolidworksWrapper.EquationManager;
using SolidworksWrapper.Features;
using SolidworksWrapper.Helpers;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Documents
{
    /// <summary>
    /// Solidworks document base object
    /// </summary>
    public class SolidworksDocument : IDisposable
    {
        #region Fields

        private SolidworksEquationManager _equationManger;

        private SolidWorksCustomPropertyManager _properties;

        private SolidworksComponents _children;

        #endregion

        #region Interops

        /// <summary>
        /// Source interop for the document
        /// </summary>
        public IModelDoc2 _doc;

        /// <summary>
        /// Source interop for the document extensions
        /// </summary>
        public IModelDocExtension _extension;

        /// <summary>
        /// Source interop for the selecting
        /// </summary>
        public ISelectionMgr _selectionMgr;

        #endregion

        #region Properties

        /// <summary>                                                                                                                         
        /// Name of the document without the extension                                                                                        
        /// </summary>                                                                                                                        
        public string Name => System.IO.Path.GetFileNameWithoutExtension(Title);

        /// <summary>                                                                                                                         
        /// Full name of the document                                                                                                         
        /// </summary>                                                                                                                        
        public string Title => _doc.GetTitle();

        /// <summary>                                                                                                                         
        /// Full file path of the document                                                                                                    
        /// </summary>                                                                                                                        
        public string FullFileName => _doc.GetPathName();

        /// <summary>                                                                                                                         
        /// File name of the document                                                                                                         
        /// </summary>                                                                                                                        
        public string FileName => System.IO.Path.GetFileName(FullFileName);

        /// <summary>                                                                                                                         
        /// Get the configuration name in the document                                                                                        
        /// </summary>                                                                                                                        
        public List<string> ConfigurationNames
        {
            get
            {
                string[] configNames = _doc.GetConfigurationNames();

                return configNames.ToList();
            }
        }

        /// <summary>                                                                                                                         
        /// Type of the document                                                                                                              
        /// </summary>                                                                                                                        
        public DocumentTypes DocumentType => (DocumentTypes) _doc.GetType();

        /// <summary>                                                                                                                         
        /// If the document is an assembly document                                                                                           
        /// </summary>                                                                                                                        
        public bool IsAssemblyDoc => DocumentType == DocumentTypes.ASSEMBLY;

        /// <summary>                                                                                                                         
        /// If the document is a part document                                                                                                
        /// </summary>                                                                                                                        
        public bool IsPartDoc => DocumentType == DocumentTypes.PART;

        /// <summary>                                                                                                                         
        /// If the document is a drawing document                                                                                             
        /// </summary>                                                                                                                        
        public bool IsDrawingDoc => DocumentType == DocumentTypes.DRAWING;

        /// <summary>                                                                                                                         
        /// Equations for the document                                                                                                        
        /// </summary>                                                                                                                        
        public SolidworksEquationManager Equations
        {
            get
            {
                if (_equationManger == null)
                {
                    _equationManger = new SolidworksEquationManager(_doc.GetEquationMgr());
                }

                return _equationManger;
            }
        }

        /// <summary>                                                                                                                         
        /// Properties for the document                                                                                                       
        /// </summary>                                                                                                                        
        public SolidWorksCustomPropertyManager Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = new SolidWorksCustomPropertyManager(_doc.Extension.CustomPropertyManager[""]);
                }

                return _properties;
            }
        }

        /// <summary>                                                                                                                         
        /// Active configuration for the document                                                                                             
        /// </summary>                                                                                                                        
        public SolidworksConfiguration ActiveConfiguration =>
            new SolidworksConfiguration(_doc.ConfigurationManager.ActiveConfiguration);

        /// <summary>                                                                                                                         
        /// Gets the children for the document                                                                                                
        /// Document must be an assembly document                                                                                             
        /// </summary>                                                                                                                        
        /// <param name="toplevelOnly">If true only the direct children of the document</param>                                               
        /// <param name="refresh">If collection should be refreshed</param>                                                                   
        /// <returns></returns>                                                                                                               
        public SolidworksComponents Children(bool toplevelOnly = false, bool refresh = false)
        {
            if (!IsAssemblyDoc) return null;

            if (_children == null || refresh)
            {
                AssemblyDoc adoc = _doc as AssemblyDoc;

                if (adoc == null) return null;

                _children = new SolidworksComponents(adoc.GetComponents(toplevelOnly));
            }

            return _children;
        }

        #endregion
        
        /// <summary>
        /// Sets the source interops
        /// </summary>
        /// <param name="doc"></param>
        public SolidworksDocument(IModelDoc2 doc)
        {
            _doc = doc;
            _extension = doc.Extension;
            _selectionMgr = doc.SelectionManager;
        }

        #region Methods

        /// <summary>
        /// Closes the document
        /// </summary>
        /// <param name="save">If true the document will be saved</param>
        public void Close(bool save = false)
        {
            if (save) Save();

            SolidworksApplication.Close(Title);
        }

        /// <summary>
        /// Save the document
        /// </summary>
        public void Save()
        {
            int errors = 0;
            int warnings = 0;


            _doc.Save3(1, ref errors, ref warnings);
        }

        /// <summary>
        /// Saves the document as a new file
        /// </summary>
        /// <param name="name">New file name of the document</param>
        public void SaveAs(string name)
        {
            int errors = 0;
            int warnings = 0;

            _extension.SaveAs3(name, 0, 1, null, null, ref errors, ref warnings);
        }

        /// <summary>
        /// Show the given configuration
        /// </summary>
        /// <param name="name">Name of the configuration to show</param>
        public void ShowConfiguration(string name) => _doc.ShowConfiguration2(name);

        /// <summary>
        /// Checks if the configuration exists in the active document
        /// </summary>
        /// <param name="name">Name of the configuration to check for</param>
        /// <returns></returns>
        public bool ConfigurationExists(string name) => ConfigurationNames.Exists(x => x == name);

        /// <summary>
        /// Clear the selection the manager
        /// </summary>
        public void ClearSelection() => _doc.ClearSelection2(true);

        /// <summary>
        /// Gets the selected and returns it
        /// </summary>
        /// <returns></returns>
        public SolidworksFeature GetSelectedFeature()
        {
            var feature = _selectionMgr.GetSelectedObject6(1, 0) as IFeature;

            if (feature != null)
            {
                return new SolidworksFeature(feature);
            }

            return null;
        }

        /// <summary>
        /// Gets the selected component and returns it 
        /// </summary>
        /// <returns></returns>
        public SolidWorksComponent GetSelectedComponent()
        {
            var component = _selectionMgr.GetSelectedObject6(1, 0) as IComponent2;

            if (component != null)
            {
                return new SolidWorksComponent(component);
            }

            return null;
        }

        /// <summary>
        /// Hides the selected component
        /// </summary>
        public void HideSelected()
        {
            _doc.HideComponent2();
        }

        /// <summary>
        /// Shows the selected component
        /// </summary>
        public void ShowSelected()
        {
            _doc.ShowComponent2();
        }

        /// <summary>
        /// Unsupress the selected object
        /// </summary>
        public void UnsuppressSelected()
        {
            _doc.EditUnsuppress2();
        }

        /// <summary>
        /// Supress the selected object
        /// </summary>
        public void SuppressSelected()
        {
            _doc.EditSuppress2();
        }

        /// <summary>
        /// Get the count of the selected objects
        /// </summary>
        /// <returns></returns>
        public int SelectedCount() => _selectionMgr.GetSelectedObjectCount2(0);

        /// <summary>
        /// Select the a object in the document by name and type
        /// </summary>
        /// <param name="name">Name of the feature to select</param>
        /// <param name="type">Type of feature in the document</param>
        /// <returns></returns>
        public bool Select(string name, string type) => _extension.SelectByID2(name, type, 0, 0, 0, false, -1, null,
            (int) swSelectOption_e.swSelectOptionDefault);

        /// <summary>
        /// Rebuild the document
        /// </summary>
        public void Rebuild()
        {
            _doc.Rebuild(1);
        }

        /// <summary>
        /// Force the rebuild of the document
        /// </summary>
        public void ForceRebuildAll()
        {
            _doc.Extension.ForceRebuildAll();
        }

        public void SetSelectedWeldmentConfiguration(string name)
        {
        }

        /// <summary>
        /// Get the dimension in the document by the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SolidworksDimension GetSolidworksDimension(string name)
        {
            IDimension dim = _doc.Parameter(name) as IDimension;

            if (dim == null) return null;

            return new SolidworksDimension(dim);
        }

        /// <summary>
        /// Gets the dimensions in the active model
        /// </summary>
        /// <returns></returns>
        public DesignCapture GetDimensions()
        {
            var designCapture = new DesignCapture();

            var featureSubTypes = typeof(FeatureSubTypes).GetConstants()
                .Where(x =>
                    x.GetCategoryAttribute() == FeatureSubTypeCategories.Body
                    || x.GetCategoryAttribute() == FeatureSubTypeCategories.SheetMetal
                    || x.GetCategoryAttribute() == FeatureSubTypeCategories.Weldment
                    || x.GetCategoryAttribute() == FeatureSubTypeCategories.Component)
                .Select(x => x.GetValue(x).ToString());

            Feature feature = _doc.FirstFeature();

            while (feature != null)
            {
                Feature subFeature = feature.GetFirstSubFeature();

                while (subFeature != null)
                {
                    DisplayDimension disDimSubFeature = subFeature.GetFirstDisplayDimension();

                    while (disDimSubFeature != null)
                    {
                        Dimension dimSubFeature = disDimSubFeature.GetDimension();

                        designCapture.AddDim(new DimensionCapture(dimSubFeature.FullName, dimSubFeature.Value));

                        disDimSubFeature = subFeature.GetNextDisplayDimension(disDimSubFeature);
                    }

                    subFeature = subFeature.GetNextSubFeature();
                }

                DisplayDimension disdim = feature.GetFirstDisplayDimension();

                while (disdim != null)
                {
                    Dimension dim = disdim.GetDimension();

                    designCapture.AddDim(new DimensionCapture(dim.FullName, dim.Value));

                    disdim = feature.GetNextDisplayDimension(disdim);
                }

                var featureType = feature.GetTypeName();

                var name = feature.Name;

                if (featureSubTypes.Any(x => x == featureType))
                {
                    designCapture.Features.Add(new FeatureCapture(name, featureType, feature.IsSuppressed()));
                }

                feature = feature.GetNextFeature();
            }

            return designCapture;
        }

        #endregion
        
        public void Dispose()
        {
            if (_doc != null)
            {
                Marshal.ReleaseComObject(_doc);
            }
        }
    }
}