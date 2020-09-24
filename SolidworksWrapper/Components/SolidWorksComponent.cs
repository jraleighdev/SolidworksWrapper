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
    public class SolidWorksComponent : IDisposable
    {
        private SolidworksDocument _document;

        private SolidWorksComponent _parent;

        private SolidworksComponents _children;

        public IComponent2 _component;

        public Guid Id { get; private set; }

        public SolidWorksComponent(IComponent2 component)
        {
            _component = component;
            Id = Guid.NewGuid();
        }

        public string Name
        {
            get => _component.Name2;
            set => _component.Name2 = value;
        }

        public string ReferencedConfiguration
        {
            get => _component.ReferencedConfiguration;
            set => _component.ReferencedConfiguration = value;
        }

        public bool Suppressed
        {
            get => _component.GetSuppression2() == 0;
            set => _component.SetSuppression2(value ? 0 : 3);
        }

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
