using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swmotionstudy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Features
{
    /// <summary>
    /// Solidworks feature 
    /// </summary>
    public class SolidworksFeature : IDisposable
    {
        /// <summary>
        /// Source solidworks interop feature
        /// </summary>
        public IFeature _feature;

        public SolidworksFeature(IFeature feature)
        {
            _feature = feature;
        }

        /// <summary>
        /// Name of the feature
        /// </summary>
        public string Name => _feature.Name;

        /// <summary>
        /// Description of the feature
        /// </summary>
        public string Description => _feature.Description;

        /// <summary>
        /// Type of the feature
        /// </summary>
        public string TypeName => _feature.GetTypeName2();

        /// <summary>
        /// Select the feature
        /// </summary>
        /// <returns>If the feature was successfully selected will return true</returns>
        public bool Select() => _feature.Select2(true, 0);

        /// <summary>
        /// Gets and sets the status of the feature
        /// </summary>
        public bool Suppressed
        {
            get => _feature.IsSuppressed2(1, null)[0];

            set => _feature.SetSuppression2(value ? 0 : 1, 0, null);
        }

        /// <summary>
        /// Sets the weldment configuration of the feature
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="Exception"></exception>
        public void SetWeldmentConfiguration(string name)
        {
            IStructuralMemberFeatureData featureData = _feature.GetDefinition() as IStructuralMemberFeatureData;

            if (featureData == null) throw new Exception("Feature is not valid weldment member");

            var a = featureData.AccessSelections((ModelDoc2)SolidworksApplication.ActiveDocument.modelDoc, null);

            if (string.IsNullOrEmpty(featureData.ConfigurationName))
            {
                var featurePath = featureData.WeldmentProfilePath;

                var featureName = System.IO.Path.GetFileNameWithoutExtension(featurePath);

                var newPath = featurePath.Replace(featureName, name);

                if (System.IO.File.Exists(newPath))
                {
                    featureData.WeldmentProfilePath = newPath;
                }
            }
            else
            {
                featureData.ConfigurationName = name;
            }

            _feature.ModifyDefinition(featureData, (ModelDoc2)SolidworksApplication.ActiveDocument.modelDoc, null);
        }

        public void Dispose()
        {
            if (_feature != null)
            {
                Marshal.ReleaseComObject(_feature);
            }
        }
    }
}
