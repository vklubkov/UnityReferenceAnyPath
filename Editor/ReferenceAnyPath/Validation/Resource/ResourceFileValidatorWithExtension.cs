namespace ReferenceAnyPath {
    public class ResourceFileValidatorWithExtension : AssetFileValidatorWithExtension {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            base.IsValidAsset(extensions, assetPath) &&
            assetPath.GetResourcePathFromAssetPath() != null;
    }
}