namespace ReferenceAnyPath {
    public class SceneValidator : FileValidatorWithExtension {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            base.IsValidAsset(extensions, assetPath) &&
            assetPath.GetScenePathFromAssetPath() != null;
    }
}