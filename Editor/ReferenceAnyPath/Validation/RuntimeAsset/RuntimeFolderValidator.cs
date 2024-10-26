namespace ReferenceAnyPath {
    public class RuntimeFolderValidator : AssetFolderValidator {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            base.IsValidAsset(extensions, assetPath) &&
            assetPath.GetRuntimePathFromAssetPath() != null;
    }
}