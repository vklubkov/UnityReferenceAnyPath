namespace ReferenceAnyPath {
    public class StreamingAssetValidator : AssetValidator {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            base.IsValidAsset(extensions, assetPath) &&
            assetPath.GetStreamingAssetPathFromAssetPath() != null;
    }
}