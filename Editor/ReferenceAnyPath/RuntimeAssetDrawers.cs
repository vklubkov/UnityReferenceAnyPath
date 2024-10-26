using UnityEditor;

namespace ReferenceAnyPath {
    [CustomPropertyDrawer(typeof(RuntimeAsset), useForChildren: true)]
    public class RuntimeAssetDrawer :
        AssetDrawer<
            AssetLayout,
            RuntimeAssetValidator,
            AssetErrorWithExtension<RuntimeAssetValidator>,
            FoldoutWithExtensionInput<AssetLayout>,
            AssetChangesWithExtension<RuntimeAssetValidator>> {
        protected override AssetLayout Layout { get; } = new();
        protected override RuntimeAssetValidator Validator { get; } = new();
        protected override AssetErrorWithExtension<RuntimeAssetValidator> Error { get; } = new();
        protected override FoldoutWithExtensionInput<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<RuntimeAssetValidator> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(Runtime<>), useForChildren: true)]
    public class GenericRuntimeAssetDrawer :
        AssetDrawer<
            AssetLayout,
            RuntimeFileValidatorWithoutExtension,
            AssetErrorWithoutExtension<RuntimeFileValidatorWithoutExtension>,
            FoldoutWithoutExtension<AssetLayout>,
            AssetChangesWithoutExtension<RuntimeFileValidatorWithoutExtension>> {
        protected override AssetLayout Layout { get; } = new();
        protected override RuntimeFileValidatorWithoutExtension Validator { get; } = new();
        protected override AssetErrorWithoutExtension<RuntimeFileValidatorWithoutExtension> Error { get; } = new();
        protected override FoldoutWithoutExtension<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithoutExtension<RuntimeFileValidatorWithoutExtension> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(RuntimeFile), useForChildren: true)]
    public class RuntimeFileDrawer :
        AssetDrawer<
            AssetLayout,
            RuntimeFileValidatorWithExtension,
            AssetErrorWithExtension<RuntimeFileValidatorWithExtension>,
            FoldoutWithExtensionInput<AssetLayout>,
            AssetChangesWithExtension<RuntimeFileValidatorWithExtension>> {
        protected override AssetLayout Layout { get; } = new();
        protected override RuntimeFileValidatorWithExtension Validator { get; } = new();
        protected override AssetErrorWithExtension<RuntimeFileValidatorWithExtension> Error { get; } = new();
        protected override FoldoutWithExtensionInput<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<RuntimeFileValidatorWithExtension> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(RuntimeFolder), useForChildren: true)]
    public class RuntimeFolderDrawer : 
        AssetDrawer<
            AssetLayout,
            RuntimeFolderValidator,
            AssetErrorWithoutExtension<RuntimeFolderValidator>,
            FoldoutWithoutExtension<AssetLayout>,
            AssetChangesWithoutExtension<RuntimeFolderValidator>> {
        protected override AssetLayout Layout { get; } = new();
        protected override RuntimeFolderValidator Validator { get; } = new();
        protected override AssetErrorWithoutExtension<RuntimeFolderValidator> Error { get; } = new();
        protected override FoldoutWithoutExtension<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithoutExtension<RuntimeFolderValidator> Changes { get; } = new();
    }
}