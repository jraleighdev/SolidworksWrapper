using SolidWorks.Interop.sldworks;
using SolidworksWrapper.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.CopyTools
{
    public class SolidworksCopyHelpers
    {
        private SolidworksDocument _topDocument;

        public void References(SolidworksDocument document, List<Tuple<string, string>> paths)
        {
            _topDocument = document;

            Recurse(document, paths);
        }

        private void Recurse(SolidworksDocument document, List<Tuple<string, string>> paths)
        {
            foreach (var c in document.Children(true))
            {
                var path = c.SolidworksDocument.FullFileName;

                var refPath = paths.FirstOrDefault(x => x.Item1.ToUpper() == path.ToUpper());

                if (refPath == null) continue;

                document.ClearSelection();

                c.Select();

                var adoc = document._doc as AssemblyDoc;

                if (adoc == null) continue;

                adoc.ReplaceComponents(refPath.Item2, c.ReferencedConfiguration, false, true);

                if (c.SolidworksDocument.IsAssemblyDoc)
                {
                    SolidworksApplication.ActivateDocument(c.SolidworksDocument.FileName);

                    var doc = SolidworksApplication.ActiveDocument;

                    Recurse(doc, paths);

                    doc.Close();
                }

            }
        }
    }
}
