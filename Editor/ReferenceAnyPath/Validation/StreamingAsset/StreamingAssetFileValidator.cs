namespace ReferenceAnyPath {
    public class StreamingAssetFileValidator : AssetFileValidatorWithExtension {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            base.IsValidAsset(extensions, assetPath) &&
            assetPath.GetStreamingAssetPathFromAssetPath() != null;
    }
}