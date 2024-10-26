namespace ReferenceAnyPath {
    public class StreamingAssetFolderValidator : AssetFolderValidator {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            base.IsValidAsset(extensions, assetPath) &&
            assetPath.GetStreamingAssetPathFromAssetPath() != null;
    }
}