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
    public class SolidworksDocument : IDisposable
    {
        private SolidworksEquationManager _equationManger;

        private SolidWorksCustomPropertyManager _properties;

        private SolidworksComponents _children;

        public IModelDoc2 _doc;

        public IModelDocExtension _extension;

        public ISelectionMgr _selectionMgr;

        public string Name => System.IO.Path.GetFileNameWithoutExtension(Title);

        public string Title => _doc.GetTitle();

        public string FullFileName => _doc.GetPathName();

        public string FileName => System.IO.Path.GetFileName(FullFileName);

        public List<string> ConfigurationNames
        {
            get
            {
                string[] configNames = _doc.GetConfigurationNames();

                return configNames.ToList();
            }
        }

        public DocumentTypes DocumentType => (DocumentTypes)_doc.GetType();

        public bool IsAssemblyDoc => DocumentType == DocumentTypes.ASSEMBLY;

        public bool IsPartDoc => DocumentType == DocumentTypes.PART;

        public bool IsDrawingDoc => DocumentType == DocumentTypes.DRAWING;

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

        public SolidworksConfiguration ActiveConfiguration => new SolidworksConfiguration(_doc.ConfigurationManager.ActiveConfiguration);

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

        public SolidworksDocument(IModelDoc2 doc)
        {
            _doc = doc;
            _extension = doc.Extension;
            _selectionMgr = doc.SelectionManager;
        }

        public void Close(bool save = false)
        {
            if (save) Save();

            SolidworksApplication.Close(Title);
        }

        public void Save()
        {
            int errors = 0;
            int warnings = 0;


            _doc.Save3(1, ref errors, ref warnings);
        }

        public void SaveAs(string name)
        {
            int errors = 0;
            int warnings = 0;

            _extension.SaveAs3(name, 0, 1, null, null, ref errors, ref warnings);
        }

        public void ShowConfiguration(string name) => _doc.ShowConfiguration2(name);

        public bool ConfigurationExists(string name) => ConfigurationNames.Exists(x => x == name);

        public void ClearSelection() => _doc.ClearSelection2(true);

        public SolidworksFeature GetSelectedFeature()
        {
            var feature = _selectionMgr.GetSelectedObject6(1, 0) as IFeature;

            if (feature != null)
            {
                return new SolidworksFeature(feature);
            }

            return null;
        }

        public SolidWorksComponent GetSelectedComponent()
        {
            var component = _selectionMgr.GetSelectedObject6(1, 0) as IComponent2;

            if (component != null)
            {
                return new SolidWorksComponent(component);
            }

            return null;
        }

        public void HideSelected()
        {
            _doc.HideComponent2();
        }

        public void ShowSelected()
        {
            _doc.ShowComponent2();
        }

        public void UnsuppressSelected()
        {
            _doc.EditUnsuppress2();
        }

        public void SuppressSelected()
        {
            _doc.EditSuppress2();
        }

        public int SelectedCount() => _selectionMgr.GetSelectedObjectCount2(0);

        public bool Select(string name, string type) => _extension.SelectByID2(name, type, 0, 0, 0, false, -1, null, (int)swSelectOption_e.swSelectOptionDefault);

        public void Rebuild()
        {
            _doc.Rebuild(1);
        }

        public void ForceRebuildAll()
        {
            _doc.Extension.ForceRebuildAll();
        }

        public void SetSelectedWeldmentConfiguration(string name)
        {

        }

        public SolidworksDimension GetSolidworksDimension(string name)
        {
            IDimension dim = _doc.Parameter(name) as IDimension;

            if (dim == null) return null;

            return new SolidworksDimension(dim);
        }

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

        public void Dispose()
        {
            if (_doc != null)
            {
                Marshal.ReleaseComObject(_doc);
            }
        }
    }
}
