namespace ReferenceAnyPath {
    public class AssetErrorWithoutExtension<TValidator> : Error<TValidator> where TValidator : Validator {
        protected override bool IsInvalidPath(string absolutePath, string assetPath) =>
            !Validator.IsValidAsset(Validator.Extensions, assetPath);
    }
}