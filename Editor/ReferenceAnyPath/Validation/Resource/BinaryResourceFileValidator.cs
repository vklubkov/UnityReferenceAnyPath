namespace ReferenceAnyPath {
    public class BinaryResourceFileValidator : AssetFileValidatorWithExtension {
        public override bool IsValidAsset(string[] extensions, string assetPath) =>
            base.IsValidAsset(extensions, assetPath) &&
            assetPath.GetBinaryResourcePathFromAssetPath() != null;
    }
}