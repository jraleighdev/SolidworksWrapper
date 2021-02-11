using SolidWorks.Interop.sldworks;
using SolidworksWrapper.Documents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Components
{
    /// <summary>
    /// Solidworks component
    /// </summary>
    public class SolidWorksComponent : IDisposable
    {
        private SolidworksDocument _document;

        private SolidWorksComponent _parent;

        private SolidworksComponents _children;

        /// <summary>
        /// Reference to the component interop
        /// </summary>
        public IComponent2 _component;

        /// <summary>
        /// Unique id for the component
        /// </summary>
        public Guid Id { get; private set; }

        public SolidWorksComponent(IComponent2 component)
        {
            _component = component;
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets and sets the value of the component
        /// </summary>
        public string Name
        {
            get => _component.Name2;
            set => _component.Name2 = value;
        }

        /// <summary>
        /// Gets sets the configuration for the component
        /// </summary>
        public string ReferencedConfiguration
        {
            get => _component.ReferencedConfiguration;
            set => _component.ReferencedConfiguration = value;
        }

        /// <summary>
        /// Gets and sets the suppression status of the component
        /// </summary>
        public bool Suppressed
        {
            get => _component.GetSuppression2() == 0;
            set => _component.SetSuppression2(value ? 0 : 3);
        }

        /// <summary>
        /// Document for the referenced component
        /// </summary>
        public SolidworksDocument SolidworksDocument
        {
            get
            {
                if (_document == null)
                {
                    IModelDoc2 modelDoc = _component.GetModelDoc2();

                    if (modelDoc == null) return null;

                    _document = new SolidworksDocument(modelDoc);
                }

                return _document;
            }
        }

        /// <summary>
        /// Parent component of the this
        /// </summary>
        public SolidWorksComponent Parent
        {
            get
            {
                if (_parent == null)
                {
                    IComponent2 parent = _component.GetParent();

                    if (parent == null) return null;

                    _parent = new SolidWorksComponent(parent);
                }

                return _parent;
            }
        }

        /// <summary>
        /// Children of this component
        /// </summary>
        public List<SolidWorksComponent> Children
        {
            get
            {
                if (_children == null)
                {
                    _children = new SolidworksComponents(_component.GetChildren());
                }

                return _children;
            }
        }

        /// <summary>
        /// Selects the component
        /// </summary>
        /// <returns></returns>
        public bool Select()
        {
            return _component.Select4(true, null, false);
        }

        public void Dispose()
        {
            if (_component != null)
            {
                Marshal.ReleaseComObject(_component);
            }
        }
    }
}
