using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksWrapper.Constants
{
    /// <summary>
    /// Sub types of features
    /// Category Attribute stores the sub category
    /// </summary>
    public class FeatureSubTypes
    {
        [Category(FeatureSubTypeCategories.Assembly)]
        public const string AsmExploder = "AsmExploder";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string CompExplodeStep = "CompExplodeStep";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string ExplodeLineProfileFeature = "ExplodeLineProfileFeature";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string InContextFeatHolder = "InContextFeatHolder";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MagneticGroundPlane = "MagneticGroundPlane";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateCamTangent = "MateCamTangent";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateCoincident = "MateCoincident";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateConcentric = "MateConcentric";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateDistanceDim = "MateDistanceDim";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateGearDim = "MateGearDim";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateHinge = "MateHinge";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateInPlace = "MateInPlace";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateLinearCoupler = "MateLinearCoupler";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateLock = "MateLock";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateParallel = "MateParallel";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MatePerpendicular = "MatePerpendicular";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MatePlanarAngleDim = "MatePlanarAngleDim";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateProfileCenter = "MateProfileCenter";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateRackPinionDim = "MateRackPinionDim";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateScrew = "MateScrew";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateSlot = "MateSlot";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateSymmetric = "MateSymmetric";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateTangent = "MateTangent";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateUniversalJoint = "MateUniversalJoint";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string MateWidth = "MateWidth";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string PosGroupFolder = "PosGroupFolder";

        [Category(FeatureSubTypeCategories.Assembly)]
        public const string SmartComponentFeature = "SmartComponentFeature";

        [Category(FeatureSubTypeCategories.Body)]
        public const string AdvHoleWzd = "AdvHoleWzd";

        [Category(FeatureSubTypeCategories.Body)]
        public const string APattern = "APattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string BaseBody = "BaseBody";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Bending = "Bending";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Blend = "Blend";

        [Category(FeatureSubTypeCategories.Body)]
        public const string BlendCut = "BlendCut";

        [Category(FeatureSubTypeCategories.Body)]
        public const string BodyExplodeStep = "BodyExplodeStep";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Boss = "Boss";

        [Category(FeatureSubTypeCategories.Body)]
        public const string BossThin = "BossThin";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Chamfer = "Chamfer";

        [Category(FeatureSubTypeCategories.Body)]
        public const string CirPattern = "CirPattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string CombineBodies = "CombineBodies";

        [Category(FeatureSubTypeCategories.Body)]
        public const string CosmeticThread = "CosmeticThread";

        [Category(FeatureSubTypeCategories.Body)]
        public const string CosmeticWeldBead = "CosmeticWeldBead";

        [Category(FeatureSubTypeCategories.Body)]
        public const string CreateAssemFeat = "CreateAssemFeat";

        [Category(FeatureSubTypeCategories.Body)]
        public const string CurvePattern = "CurvePattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Cut = "Cut";

        [Category(FeatureSubTypeCategories.Body)]
        public const string CutThin = "CutThin";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Deform = "Deform";

        [Category(FeatureSubTypeCategories.Body)]
        public const string DeleteBody = "DeleteBody";

        [Category(FeatureSubTypeCategories.Body)]
        public const string DelFace = "DelFace";

        [Category(FeatureSubTypeCategories.Body)]
        public const string DerivedCirPattern = "DerivedCirPattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string DerivedLPattern = "DerivedLPattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string DimPattern = "DimPattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Dome = "Dome";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Draft = "Draft";

        [Category(FeatureSubTypeCategories.Body)]
        public const string EdgeMerge = "EdgeMerge";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Emboss = "Emboss";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Extrusion = "Extrusion";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Fillet = "Fillet";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Helix = "Helix";

        [Category(FeatureSubTypeCategories.Body)]
        public const string HoleSeries = "HoleSeries";

        [Category(FeatureSubTypeCategories.Body)]
        public const string HoleWzd = "HoleWzd";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Imported = "Imported";

        [Category(FeatureSubTypeCategories.Body)]
        public const string LocalChainPattern = "LocalChainPattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string LocalCirPattern = "LocalCirPattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string LocalCurvePattern = "LocalCurvePattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string LocalLPattern = "LocalLPattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string LocalSketchPattern = "LocalSketchPattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string LPattern = "LPattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string MacroFeature = "MacroFeature";

        [Category(FeatureSubTypeCategories.Body)]
        public const string MirrorCompFeat = "MirrorCompFeat";

        [Category(FeatureSubTypeCategories.Body)]
        public const string MirrorPattern = "MirrorPattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string MirrorSolid = "MirrorSolid";

        [Category(FeatureSubTypeCategories.Body)]
        public const string MirrorStock = "MirrorStock";

        [Category(FeatureSubTypeCategories.Body)]
        public const string MoveCopyBody = "MoveCopyBody";

        [Category(FeatureSubTypeCategories.Body)]
        public const string NetBlend = "NetBlend";

        [Category(FeatureSubTypeCategories.Body)]
        public const string PrtExploder = "PrtExploder";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Punch = "Punch";

        [Category(FeatureSubTypeCategories.Body)]
        public const string ReplaceFace = "ReplaceFace";

        [Category(FeatureSubTypeCategories.Body)]
        public const string RevCut = "RevCut";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Roundfilletcorner = "Round fillet corner";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Revolution = "Revolution";

        [Category(FeatureSubTypeCategories.Body)]
        public const string RevolutionThin = "RevolutionThin";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Rib = "Rib";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Rip = "Rip";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Sculpt = "Sculpt";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Shape = "Shape";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Shell = "Shell";

        [Category(FeatureSubTypeCategories.Body)]
        public const string SketchHole = "SketchHole";

        [Category(FeatureSubTypeCategories.Body)]
        public const string SketchPattern = "SketchPattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Split = "Split";

        [Category(FeatureSubTypeCategories.Body)]
        public const string SplitBody = "SplitBody";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Stock = "Stock";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Sweep = "Sweep";

        [Category(FeatureSubTypeCategories.Body)]
        public const string SweepCut = "SweepCut";

        [Category(FeatureSubTypeCategories.Body)]
        public const string SweepThread = "SweepThread";

        [Category(FeatureSubTypeCategories.Body)]
        public const string TablePattern = "TablePattern";

        [Category(FeatureSubTypeCategories.Body)]
        public const string Thicken = "Thicken";

        [Category(FeatureSubTypeCategories.Body)]
        public const string ThickenCut = "ThickenCut";

        [Category(FeatureSubTypeCategories.Body)]
        public const string VarFillet = "VarFillet";

        [Category(FeatureSubTypeCategories.Drawing)]
        public const string BendTableAchor = "BendTableAchor";

        [Category(FeatureSubTypeCategories.Drawing)]
        public const string BomFeat = "BomFeat";

        [Category(FeatureSubTypeCategories.Drawing)]
        public const string BomTemplate = "BomTemplate";

        [Category(FeatureSubTypeCategories.Drawing)]
        public const string DetailCircle = "DetailCircle";

        [Category(FeatureSubTypeCategories.Drawing)]
        public const string DrBreakoutSectionLine = "DrBreakoutSectionLine";

        [Category(FeatureSubTypeCategories.Drawing)]
        public const string DrSectionLine = "DrSectionLine";

        [Category(FeatureSubTypeCategories.Drawing)]
        public const string GeneralTableAnchor = "GeneralTableAnchor";

        [Category(FeatureSubTypeCategories.Drawing)]
        public const string HoleTableAnchor = "HoleTableAnchor";

        [Category(FeatureSubTypeCategories.Drawing)]
        public const string LiveSection = "LiveSection";

        [Category(FeatureSubTypeCategories.Drawing)]
        public const string PunchTableAnchor = "PunchTableAnchor";

        [Category(FeatureSubTypeCategories.Drawing)]
        public const string RevisionTableAnchor = "RevisionTableAnchor";

        [Category(FeatureSubTypeCategories.Drawing)]
        public const string WeldmentTableAnchor = "WeldmentTableAnchor";

        [Category(FeatureSubTypeCategories.Drawing)]
        public const string WeldTableAnchor = "WeldTableAnchor";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string BlockFolder = "BlockFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string CommentsFolder = "CommentsFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string CosmeticWeldSubFolder = "CosmeticWeldSubFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string CutListFolder = "CutListFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string FeatSolidBodyFolder = "FeatSolidBodyFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string FeatSurfaceBodyFolder = "FeatSurfaceBodyFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string FtrFolder = "FtrFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string InsertedFeatureFolder = "InsertedFeatureFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string MateReferenceGroupFolder = "MateReferenceGroupFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string ProfileFtrFolder = "ProfileFtrFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string RefAxisFtrFolder = "RefAxisFtrFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string RefPlaneFtrFolder = "RefPlaneFtrFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string SketchSliceFolder = "SketchSliceFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string SolidBodyFolder = "SolidBodyFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string SubAtomFolder = "SubAtomFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string SubWeldFolder = "SubWeldFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string SurfaceBodyFolder = "SurfaceBodyFolder";

        [Category(FeatureSubTypeCategories.Folder)]
        public const string TemplateFlatPattern = "TemplateFlatPattern";

        [Category(FeatureSubTypeCategories.ImportedFile)]
        public const string MBimport = "MBimport";

        [Category(FeatureSubTypeCategories.Miscellaneous)]
        public const string Attribute = "Attribute";

        [Category(FeatureSubTypeCategories.Miscellaneous)]
        public const string BlockDef = "BlockDef";

        [Category(FeatureSubTypeCategories.Miscellaneous)]
        public const string CurveInFile = "CurveInFile";

        [Category(FeatureSubTypeCategories.Miscellaneous)]
        public const string GridFeature = "GridFeature";

        [Category(FeatureSubTypeCategories.Miscellaneous)]
        public const string LibraryFeature = "LibraryFeature";

        [Category(FeatureSubTypeCategories.Miscellaneous)]
        public const string Scale = "Scale";

        [Category(FeatureSubTypeCategories.Miscellaneous)]
        public const string Sensor = "Sensor";

        [Category(FeatureSubTypeCategories.Miscellaneous)]
        public const string ViewBodyFeature = "ViewBodyFeature";

        [Category(FeatureSubTypeCategories.Mold)]
        public const string Cavity = "Cavity";

        [Category(FeatureSubTypeCategories.Mold)]
        public const string MoldCoreCavitySolids = "MoldCoreCavitySolids";

        [Category(FeatureSubTypeCategories.Mold)]
        public const string MoldPartingGeom = "MoldPartingGeom";

        [Category(FeatureSubTypeCategories.Mold)]
        public const string MoldPartLine = "MoldPartLine";

        [Category(FeatureSubTypeCategories.Mold)]
        public const string MoldShutOffSrf = "MoldShutOffSrf";

        [Category(FeatureSubTypeCategories.Mold)]
        public const string SideCore = "SideCore";

        [Category(FeatureSubTypeCategories.Mold)]
        public const string XformStock = "XformStock";

        [Category(FeatureSubTypeCategories.MotionAndSimulation)]
        public const string AEM3DContact = "AEM3DContact";

        [Category(FeatureSubTypeCategories.MotionAndSimulation)]
        public const string AEMGravity = "AEMGravity";

        [Category(FeatureSubTypeCategories.MotionAndSimulation)]
        public const string AEMLinearDamper = "AEMLinearDamper";

        [Category(FeatureSubTypeCategories.MotionAndSimulation)]
        public const string AEMLinearMotor = "AEMLinearMotor";

        [Category(FeatureSubTypeCategories.MotionAndSimulation)]
        public const string AEMLinearSpring = "AEMLinearSpring";

        [Category(FeatureSubTypeCategories.MotionAndSimulation)]
        public const string AEMRotationalMotor = "AEMRotationalMotor";

        [Category(FeatureSubTypeCategories.MotionAndSimulation)]
        public const string AEMTorque = "AEMTorque";

        [Category(FeatureSubTypeCategories.MotionAndSimulation)]
        public const string AEMTorsionalDamper = "AEMTorsionalDamper";

        [Category(FeatureSubTypeCategories.MotionAndSimulation)]
        public const string AEMTorsionalSpring = "AEMTorsionalSpring";

        [Category(FeatureSubTypeCategories.MotionAndSimulation)]
        public const string SimPlotFeature = "SimPlotFeature";

        [Category(FeatureSubTypeCategories.MotionAndSimulation)]
        public const string SimPlotXAxisFeature = "SimPlotXAxisFeature";

        [Category(FeatureSubTypeCategories.MotionAndSimulation)]
        public const string SimPlotYAxisFeature = "SimPlotYAxisFeature";

        [Category(FeatureSubTypeCategories.MotionAndSimulation)]
        public const string SimResultFolder = "SimResultFolder";

        [Category(FeatureSubTypeCategories.ReferenceGeometry)]
        public const string BoundingBox = "BoundingBox";

        [Category(FeatureSubTypeCategories.ReferenceGeometry)]
        public const string CoordSys = "CoordSys";

        [Category(FeatureSubTypeCategories.ReferenceGeometry)]
        public const string GroundPlane = "GroundPlane";

        [Category(FeatureSubTypeCategories.ReferenceGeometry)]
        public const string RefAxis = "RefAxis";

        [Category(FeatureSubTypeCategories.ReferenceGeometry)]
        public const string RefPlane = "RefPlane";

        [Category(FeatureSubTypeCategories.ScenesLightsCamera)]
        public const string AmbientLight = "AmbientLight";

        [Category(FeatureSubTypeCategories.ScenesLightsCamera)]
        public const string CameraFeature = "CameraFeature";

        [Category(FeatureSubTypeCategories.ScenesLightsCamera)]
        public const string DirectionLight = "DirectionLight";

        [Category(FeatureSubTypeCategories.ScenesLightsCamera)]
        public const string PointLight = "PointLight";

        [Category(FeatureSubTypeCategories.ScenesLightsCamera)]
        public const string SpotLight = "SpotLight";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string SMBaseFlange = "SMBaseFlange";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string BreakCorner = "BreakCorner";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string CornerTrim = "CornerTrim";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string CrossBreak = "CrossBreak";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string EdgeFlange = "EdgeFlange";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string FlatPattern = "FlatPattern";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string FlattenBends = "FlattenBends";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string Fold = "Fold";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string FormToolInstance = "FormToolInstance";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string Hem = "Hem";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string Jog = "Jog";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string LoftedBend = "LoftedBend";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string NormalCut = "NormalCut";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string OneBend = "OneBend";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string ProcessBends = "ProcessBends";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string SheetMetal = "SheetMetal";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string SketchBend = "SketchBend";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string SM3dBend = "SM3dBend";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string SMGusset = "SMGusset";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string SMMiteredFlange = "SMMiteredFlange";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string TemplateSheetMetal = "TemplateSheetMetal";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string ToroidalBend = "ToroidalBend";

        [Category(FeatureSubTypeCategories.SheetMetal)]
        public const string UnFold = "UnFold";

        [Category(FeatureSubTypeCategories.Sketch)]
        public const string ThreeDProfileFeature =  "3DProfileFeature";

        [Category(FeatureSubTypeCategories.Sketch)]
        public const string ThreeDSplineCurve =  "3DSplineCurve";

        [Category(FeatureSubTypeCategories.Sketch)]
        public const string CompositeCurve = "CompositeCurve";

        [Category(FeatureSubTypeCategories.Sketch)]
        public const string ImportedCurve = "ImportedCurve";

        [Category(FeatureSubTypeCategories.Sketch)]
        public const string PLine = "PLine";

        [Category(FeatureSubTypeCategories.Sketch)]
        public const string ProfileFeature = "ProfileFeature";

        [Category(FeatureSubTypeCategories.Sketch)]
        public const string RefCurve = "RefCurve";

        [Category(FeatureSubTypeCategories.Sketch)]
        public const string RefPoint = "RefPoint";

        [Category(FeatureSubTypeCategories.Sketch)]
        public const string SketchBlockDef = "SketchBlockDef";

        [Category(FeatureSubTypeCategories.Sketch)]
        public const string SketchBlockInst = "SketchBlockInst";

        [Category(FeatureSubTypeCategories.Sketch)]
        public const string SketchBitmap = "SketchBitmap";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string BlendRefSurface = "BlendRefSurface";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string ExtendRefSurface = "ExtendRefSurface";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string ExtruRefSurface = "ExtruRefSurface";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string FillRefSurface = "FillRefSurface";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string FlattenSurface = "FlattenSurface";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string MidRefSurface = "MidRefSurface";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string OffsetRefSuface = "OffsetRefSuface";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string PlanarSurface = "PlanarSurface";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string RadiateRefSurface = "RadiateRefSurface";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string RefSurface = "RefSurface";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string RevolvRefSurf = "RevolvRefSurf";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string RuledSrfFromEdge = "RuledSrfFromEdge";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string SewRefSurface = "SewRefSurface";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string SurfCut = "SurfCut";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string SweepRefSurface = "SweepRefSurface";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string TrimRefSurface = "TrimRefSurface";

        [Category(FeatureSubTypeCategories.Surface)]
        public const string UnTrimRefSurf = "UnTrimRefSurf";

        [Category(FeatureSubTypeCategories.Weldment)]
        public const string EndCap = "EndCap";

        [Category(FeatureSubTypeCategories.Weldment)]
        public const string Gusset = "Gusset";

        [Category("Weldment")]
        public const string WeldBeadFeat = "WeldBeadFeat";

        [Category(FeatureSubTypeCategories.Weldment)]
        public const string WeldCornerFeat = "WeldCornerFeat";

        [Category(FeatureSubTypeCategories.Weldment)]
        public const string WeldMemberFeat = "WeldMemberFeat";

        [Category(FeatureSubTypeCategories.Weldment)]
        public const string WeldmentFeature = "WeldmentFeature";

        [Category(FeatureSubTypeCategories.Weldment)]
        public const string WeldmentTableFeat = "WeldmentTableFeat";

        [Category(FeatureSubTypeCategories.Component)]
        public const string Reference = "Reference";
    }
}
