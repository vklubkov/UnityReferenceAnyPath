namespace ReferenceAnyPath {
    public class RuntimeFileValidatorWithoutExtension : AssetFileValidatorWithoutExtension {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            base.IsValidAsset(extensions, assetPath) &&
            assetPath.GetRuntimePathFromAssetPath() != null;
    }
}