namespace ReferenceAnyPath {
    public class RuntimeFileValidatorWithExtension : AssetFileValidatorWithExtension {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            base.IsValidAsset(extensions, assetPath) &&
            assetPath.GetRuntimePathFromAssetPath() != null;
    }
}