namespace ReferenceAnyPath {
    public class ResourceValidator : AssetValidator {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            base.IsValidAsset(extensions, assetPath) &&
            assetPath.GetResourcePathFromAssetPath() != null;
    }
}