using UnityEditor;

namespace ReferenceAnyPath {
    [CustomPropertyDrawer(typeof(Asset), useForChildren: true)]
    public class AssetDrawer :
        AssetDrawer<
            AssetLayout,
            AssetValidator,
            AssetErrorWithExtension<AssetValidator>,
            FoldoutWithExtensionInput<AssetLayout>,
            AssetChangesWithExtension<AssetValidator>> {
        protected override AssetLayout Layout { get; } = new();
        protected override AssetValidator Validator { get; } = new();
        protected override AssetErrorWithExtension<AssetValidator> Error { get; } = new();
        protected override FoldoutWithExtensionInput<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<AssetValidator> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(Asset<>), useForChildren: true)]
    public class GenericAssetDrawer :
        AssetDrawer<
            AssetLayout,
            AssetFileValidatorWithoutExtension,
            AssetErrorWithoutExtension<AssetFileValidatorWithoutExtension>,
            FoldoutWithoutExtension<AssetLayout>,
            AssetChangesWithoutExtension<AssetFileValidatorWithoutExtension>> {
        protected override AssetLayout Layout { get; } = new();
        protected override AssetFileValidatorWithoutExtension Validator { get; } = new();
        protected override AssetErrorWithoutExtension<AssetFileValidatorWithoutExtension> Error { get; } = new();
        protected override FoldoutWithoutExtension<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithoutExtension<AssetFileValidatorWithoutExtension> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(AssetFile), useForChildren: true)]
    public class AssetFileDrawer :
        AssetDrawer<
            AssetLayout,
            AssetFileValidatorWithExtension,
            AssetErrorWithExtension<AssetFileValidatorWithExtension>,
            FoldoutWithExtensionInput<AssetLayout>,
            AssetChangesWithExtension<AssetFileValidatorWithExtension>> {
        protected override AssetLayout Layout { get; } = new();
        protected override AssetFileValidatorWithExtension Validator { get; } = new();
        protected override AssetErrorWithExtension<AssetFileValidatorWithExtension> Error { get; } = new();
        protected override FoldoutWithExtensionInput<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<AssetFileValidatorWithExtension> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(AssetFolder), useForChildren: true)]
    public class AssetFolderDrawer :
        AssetDrawer<
            AssetLayout,
            AssetFolderValidator,
            AssetErrorWithoutExtension<AssetFolderValidator>,
            FoldoutWithoutExtension<AssetLayout>,
            AssetChangesWithoutExtension<AssetFolderValidator>> {
        protected override AssetLayout Layout { get; } = new();
        protected override AssetFolderValidator Validator { get; } = new();
        protected override AssetErrorWithoutExtension<AssetFolderValidator> Error { get; } = new();
        protected override FoldoutWithoutExtension<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithoutExtension<AssetFolderValidator> Changes { get; } = new();
    }
}