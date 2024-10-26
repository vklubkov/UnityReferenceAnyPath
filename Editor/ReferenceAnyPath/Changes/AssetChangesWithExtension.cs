namespace ReferenceAnyPath {
    public class AssetChangesWithExtension<TValidator> :
        AssetChanges<TValidator> where TValidator : WithExtensionValidator {
        protected override bool IsValidPath(string absolutePath) {
            var extensions = Validator.Extensions;
            var path = Validator.IsValidPath(extensions, absolutePath);
            var extension = Validator.IsValidExtension(extensions, absolutePath);
            return path && extension;
        }
    }
}