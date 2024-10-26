namespace ReferenceAnyPath {
    public class RuntimeAssetValidator : AssetValidator {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            base.IsValidAsset(extensions, assetPath) &&
            assetPath.GetRuntimePathFromAssetPath() != null;
    }
}