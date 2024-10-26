using UnityEditor;

namespace ReferenceAnyPath {
    [CustomPropertyDrawer(typeof(Resource), useForChildren: true)]
    public class ResourceDrawer :
        AssetDrawer<
            AssetLayout,
            ResourceValidator,
            AssetErrorWithExtension<ResourceValidator>,
            FoldoutWithExtensionInput<AssetLayout>,
            AssetChangesWithExtension<ResourceValidator>> {
        protected override AssetLayout Layout { get; } = new();
        protected override ResourceValidator Validator { get; } = new();
        protected override AssetErrorWithExtension<ResourceValidator> Error { get; } = new();
        protected override FoldoutWithExtensionInput<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<ResourceValidator> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(TextResource), useForChildren: true)]
    public class TextResourceDrawer :
        AssetDrawer<
            AssetLayout,
            ResourceFileValidatorWithExtension,
            AssetErrorWithExtension<ResourceFileValidatorWithExtension>,
            FoldoutWithExtensionInput<AssetLayout>,
            AssetChangesWithExtension<ResourceFileValidatorWithExtension>> {
        protected override AssetLayout Layout { get; } = new();
        protected override ResourceFileValidatorWithExtension Validator { get; } = new();
        protected override AssetErrorWithExtension<ResourceFileValidatorWithExtension> Error { get; } = new();
        protected override FoldoutWithExtensionInput<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<ResourceFileValidatorWithExtension> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(BinaryResource), useForChildren: true)]
    public class BinaryResourceDrawer :
        AssetDrawer<
            AssetLayout,
            BinaryResourceFileValidator,
            AssetErrorWithExtension<BinaryResourceFileValidator>,
            FoldoutWithExtensionInfo<AssetLayout>,
            AssetChangesWithExtension<BinaryResourceFileValidator>> {
        protected override AssetLayout Layout { get; } = new();
        protected override BinaryResourceFileValidator Validator { get; } = new();
        protected override AssetErrorWithExtension<BinaryResourceFileValidator> Error { get; } = new();
        protected override FoldoutWithExtensionInfo<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<BinaryResourceFileValidator> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(Resource<>), useForChildren: true)]
    public class GenericResourceDrawer :
        AssetDrawer<
            AssetLayout,
            ResourceFileValidatorWithoutExtension,
            AssetErrorWithoutExtension<ResourceFileValidatorWithoutExtension>,
            FoldoutWithoutExtension<AssetLayout>,
            AssetChangesWithoutExtension<ResourceFileValidatorWithoutExtension>> {
        protected override AssetLayout Layout { get; } = new();
        protected override ResourceFileValidatorWithoutExtension Validator { get; } = new();
        protected override AssetErrorWithoutExtension<ResourceFileValidatorWithoutExtension> Error { get; } = new();
        protected override FoldoutWithoutExtension<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithoutExtension<ResourceFileValidatorWithoutExtension> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(ResourceFile), useForChildren: true)]
    public class ResourceFileDrawer :
        AssetDrawer<
            AssetLayout,
            ResourceFileValidatorWithExtension,
            AssetErrorWithExtension<ResourceFileValidatorWithExtension>,
            FoldoutWithExtensionInput<AssetLayout>,
            AssetChangesWithExtension<ResourceFileValidatorWithExtension>> {
        protected override AssetLayout Layout { get; } = new();
        protected override ResourceFileValidatorWithExtension Validator { get; } = new();
        protected override AssetErrorWithExtension<ResourceFileValidatorWithExtension> Error { get; } = new();
        protected override FoldoutWithExtensionInput<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<ResourceFileValidatorWithExtension> Changes { get; } = new();
    }

    [CustomPropertyDrawer(typeof(ResourceFolder), useForChildren: true)]
    public class ResourceFolderDrawer :
        AssetDrawer<
            AssetLayout,
            ResourceFolderValidator,
            AssetErrorWithoutExtension<ResourceFolderValidator>,
            FoldoutWithoutExtension<AssetLayout>,
            AssetChangesWithoutExtension<ResourceFolderValidator>> {
        protected override AssetLayout Layout { get; } = new();
        protected override ResourceFolderValidator Validator { get; } = new();
        protected override AssetErrorWithoutExtension<ResourceFolderValidator> Error { get; } = new();
        protected override FoldoutWithoutExtension<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithoutExtension<ResourceFolderValidator> Changes { get; } = new();
    }
}