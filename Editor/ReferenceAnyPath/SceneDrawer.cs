using UnityEditor;

namespace ReferenceAnyPath {
    [CustomPropertyDrawer(typeof(Scene), useForChildren: true)]
    public class SceneDrawer :
        AssetDrawer<
            AssetLayout,
            RuntimeFileValidatorWithExtension,
            AssetErrorWithExtension<RuntimeFileValidatorWithExtension>,
            FoldoutWithSceneName<AssetLayout>,
            AssetChangesWithExtension<RuntimeFileValidatorWithExtension>> {
        protected override AssetLayout Layout { get; } = new();
        protected override RuntimeFileValidatorWithExtension Validator { get; } = new();
        protected override AssetErrorWithExtension<RuntimeFileValidatorWithExtension> Error { get; } = new();
        protected override FoldoutWithSceneName<AssetLayout> Foldout { get; } = new();
        protected override AssetChangesWithExtension<RuntimeFileValidatorWithExtension> Changes { get; } = new();
    }
}