using UnityEditor;

namespace ReferenceAnyPath {
    [CustomPropertyDrawer(typeof(RawHeightmapFile), useForChildren: true)]
    public class RawHeightmapFileDrawer :
        PathDrawer<
            RawHeightmapFileLayout,
            RawHeightmapFileValidator,
            RawHeightmapError<RawHeightmapFileValidator>,
            MainFieldWithFileSelector<RawHeightmapFileLayout> ,
            FoldoutWithExtensionInputAndDimensions<RawHeightmapFileLayout>,
            PathChangesWithExtension<RawHeightmapFileValidator>> {
        protected override RawHeightmapFileLayout Layout { get; } = new();
        protected override RawHeightmapFileValidator Validator { get; } = new();
        protected override RawHeightmapError<RawHeightmapFileValidator> Error { get; } = new();
        protected override MainFieldWithFileSelector<RawHeightmapFileLayout> MainField { get; } = new ();
        protected override FoldoutWithExtensionInputAndDimensions<RawHeightmapFileLayout> Foldout { get; } = new();
        protected override PathChangesWithExtension<RawHeightmapFileValidator> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(RawHeightmapAsset), useForChildren: true)]
    public class RawHeightmapAssetDrawer :
        AssetDrawer<
            RawHeightmapAssetLayout,
            RawHeightmapAssetValidator,
            RawHeightmapError<RawHeightmapAssetValidator>,
            FoldoutWithExtensionInputAndDimensions<RawHeightmapAssetLayout>,
            AssetChangesWithExtension<RawHeightmapAssetValidator>> {
        protected override RawHeightmapAssetLayout Layout { get; } = new();
        protected override RawHeightmapAssetValidator Validator { get; } = new();
        protected override RawHeightmapError<RawHeightmapAssetValidator> Error { get; } = new();
        protected override FoldoutWithExtensionInputAndDimensions<RawHeightmapAssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<RawHeightmapAssetValidator> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(RawHeightmapRuntimeAsset), useForChildren: true)]
    public class RawHeightmapRuntimeAssetDrawer :
        AssetDrawer<
            RawHeightmapAssetLayout,
            RawHeightmapRuntimeAssetFileValidator,
            RawHeightmapError<RawHeightmapRuntimeAssetFileValidator>,
            FoldoutWithExtensionInputAndDimensions<RawHeightmapAssetLayout>,
            AssetChangesWithExtension<RawHeightmapRuntimeAssetFileValidator>> {
        protected override RawHeightmapAssetLayout Layout { get; } = new();
        protected override RawHeightmapRuntimeAssetFileValidator Validator { get; } = new();
        protected override RawHeightmapError<RawHeightmapRuntimeAssetFileValidator> Error { get; } = new();
        protected override FoldoutWithExtensionInputAndDimensions<RawHeightmapAssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<RawHeightmapRuntimeAssetFileValidator> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(RawHeightmapResource), useForChildren: true)]
    public class RawHeightmapResourceDrawer :
        AssetDrawer<
            RawHeightmapAssetLayout,
            RawHeightmapBinaryResourceFileValidator,
            RawHeightmapError<RawHeightmapBinaryResourceFileValidator>,
            FoldoutWithExtensionInfoAndDimensions<RawHeightmapAssetLayout>,
            AssetChangesWithExtension<RawHeightmapBinaryResourceFileValidator>> {
        protected override RawHeightmapAssetLayout Layout { get; } = new();
        protected override RawHeightmapBinaryResourceFileValidator Validator { get; } = new();
        protected override RawHeightmapError<RawHeightmapBinaryResourceFileValidator> Error { get; } = new();
        protected override FoldoutWithExtensionInfoAndDimensions<RawHeightmapAssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<RawHeightmapBinaryResourceFileValidator> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(RawHeightmapStreamingAsset), useForChildren: true)]
    public class RawHeightmapStreamingAssetDrawer :
        AssetDrawer<
            RawHeightmapAssetLayout,
            RawHeightmapStreamingAssetFileValidator,
            RawHeightmapError<RawHeightmapStreamingAssetFileValidator>,
            FoldoutWithExtensionInputAndDimensions<RawHeightmapAssetLayout>,
            AssetChangesWithExtension<RawHeightmapStreamingAssetFileValidator>> {
        protected override RawHeightmapAssetLayout Layout { get; } = new();
        protected override RawHeightmapStreamingAssetFileValidator Validator { get; } = new();
        protected override RawHeightmapError<RawHeightmapStreamingAssetFileValidator> Error { get; } = new();
        protected override FoldoutWithExtensionInputAndDimensions<RawHeightmapAssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<RawHeightmapStreamingAssetFileValidator> Changes { get; } = new();
    }
}