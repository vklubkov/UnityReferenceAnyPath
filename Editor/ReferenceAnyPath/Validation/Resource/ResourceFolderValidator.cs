namespace ReferenceAnyPath {
    public class ResourceFolderValidator : AssetFolderValidator {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            base.IsValidAsset(extensions, assetPath) &&
            assetPath.GetResourcePathFromAssetPath() != null;
    }
}