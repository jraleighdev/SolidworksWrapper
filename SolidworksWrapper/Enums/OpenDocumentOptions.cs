using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Enums
{
    public enum OpenDocumentOptions
    {
        Silent = 1,
        ReadOnly = 2,
        ViewOnly = 4,
        RapidDraft = 8,
        LoadModel = 16,
        AutoMissingConfig = 32,
        OverrideDefaultLoadLightweight = 64,
        LoadLightweight = 128,
        DontLoadHiddenComponents = 256,
        LoadExternalReferencesInMemory = 512,
        OpenDetailingMode = 1024
    }
}
