using UnityEditor;

namespace ReferenceAnyPath {
    [CustomPropertyDrawer(typeof(StreamingAsset), useForChildren: true)]
    public class StreamingAssetDrawer :
        AssetDrawer<
            AssetLayout,
            StreamingAssetValidator,
            AssetErrorWithExtension<StreamingAssetValidator>,
            FoldoutWithExtensionInput<AssetLayout>,
            AssetChangesWithExtension<StreamingAssetValidator>> {
        protected override AssetLayout Layout { get; } = new();
        protected override StreamingAssetValidator Validator { get; } = new();
        protected override AssetErrorWithExtension<StreamingAssetValidator> Error { get; } = new();
        protected override FoldoutWithExtensionInput<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<StreamingAssetValidator> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(TextStreamingAsset), useForChildren: true)]
    public class TextStreamingAssetFileDrawer :
        AssetDrawer<
            AssetLayout,
            StreamingAssetFileValidator,
            AssetErrorWithExtension<StreamingAssetFileValidator>,
            FoldoutWithExtensionInput<AssetLayout>,
            AssetChangesWithExtension<StreamingAssetFileValidator>> {
        protected override AssetLayout Layout { get; } = new();
        protected override StreamingAssetFileValidator Validator { get; } = new();
        protected override AssetErrorWithExtension<StreamingAssetFileValidator> Error { get; } = new();
        protected override FoldoutWithExtensionInput<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<StreamingAssetFileValidator> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(BinaryStreamingAsset), useForChildren: true)]
    public class BinaryStreamingAssetFileDrawer :
        AssetDrawer<
            AssetLayout,
            StreamingAssetFileValidator,
            AssetErrorWithExtension<StreamingAssetFileValidator>,
            FoldoutWithExtensionInput<AssetLayout>,
            AssetChangesWithExtension<StreamingAssetFileValidator>> {
        protected override AssetLayout Layout { get; } = new();
        protected override StreamingAssetFileValidator Validator { get; } = new();
        protected override AssetErrorWithExtension<StreamingAssetFileValidator> Error { get; } = new();
        protected override FoldoutWithExtensionInput<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<StreamingAssetFileValidator> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(StreamingAssetFile), useForChildren: true)]
    public class StreamingAssetFileDrawer :
        AssetDrawer<
            AssetLayout,
            StreamingAssetFileValidator,
            AssetErrorWithExtension<StreamingAssetFileValidator>,
            FoldoutWithExtensionInput<AssetLayout>,
            AssetChangesWithExtension<StreamingAssetFileValidator>> {
        protected override AssetLayout Layout { get; } = new();
        protected override StreamingAssetFileValidator Validator { get; } = new();
        protected override AssetErrorWithExtension<StreamingAssetFileValidator> Error { get; } = new();
        protected override FoldoutWithExtensionInput<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<StreamingAssetFileValidator> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(StreamingAssetFolder), useForChildren: true)]
    public class StreamingAssetFolderDrawer :
        AssetDrawer<
            AssetLayout,
            StreamingAssetFolderValidator,
            AssetErrorWithoutExtension<StreamingAssetFolderValidator>,
            FoldoutWithoutExtension<AssetLayout>,
            AssetChangesWithoutExtension<StreamingAssetFolderValidator>> {
        protected override AssetLayout Layout { get; } = new();
        protected override StreamingAssetFolderValidator Validator { get; } = new();
        protected override AssetErrorWithoutExtension<StreamingAssetFolderValidator> Error { get; } = new();
        protected override FoldoutWithoutExtension<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithoutExtension<StreamingAssetFolderValidator> Changes { get; } = new();
    }
}