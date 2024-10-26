namespace ReferenceAnyPath {
    public class ResourceFileValidatorWithoutExtension : AssetFileValidatorWithoutExtension {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            base.IsValidAsset(extensions, assetPath) &&
            assetPath.GetResourcePathFromAssetPath() != null;
    }
}