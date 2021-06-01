using SolidWorks.Interop.sldworks;
using SolidworksWrapper.Documents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SolidworksWrapper.Base;

namespace SolidworksWrapper.Components
{
    /// <summary>
    /// Solidworks component
    /// </summary>
    public class SolidWorksComponent : SolidworksBaseObject<IComponent2>
    {
        private SolidworksDocument _document;

        private SolidWorksComponent _parent;

        private SolidworksComponents _children;

        /// <summary>
        /// Unique id for the component
        /// </summary>
        public Guid Id { get; private set; }

        public SolidWorksComponent(IComponent2 component) : base(component)
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets and sets the value of the component
        /// </summary>
        public string Name
        {
            get => BaseObject.Name2;
            set => BaseObject.Name2 = value;
        }

        /// <summary>
        /// Gets sets the configuration for the component
        /// </summary>
        public string ReferencedConfiguration
        {
            get => BaseObject.ReferencedConfiguration;
            set => BaseObject.ReferencedConfiguration = value;
        }

        /// <summary>
        /// Gets and sets the suppression status of the component
        /// </summary>
        public bool Suppressed
        {
            get => BaseObject.GetSuppression2() == 0;
            set => BaseObject.SetSuppression2(value ? 0 : 3);
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
                    IModelDoc2 modelDoc = BaseObject.GetModelDoc2();

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
                    IComponent2 parent = BaseObject.GetParent();

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
                    _children = new SolidworksComponents(BaseObject.GetChildren());
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
            return BaseObject.Select4(true, null, false);
        }
    }
}
