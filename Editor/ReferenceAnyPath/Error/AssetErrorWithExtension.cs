namespace ReferenceAnyPath {
    public class AssetErrorWithExtension<TValidator> : Error<TValidator> where TValidator : WithExtensionValidator {
        protected override bool IsInvalidPath(string absolutePath, string assetPath) {
            var extensions = Validator.Extensions;
            if (!Validator.IsValidExtension(extensions, absolutePath))
                return true;

            return !Validator.IsValidAsset(extensions, assetPath);
        }
    }
}