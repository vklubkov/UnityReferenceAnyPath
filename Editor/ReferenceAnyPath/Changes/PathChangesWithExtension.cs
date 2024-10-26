namespace ReferenceAnyPath {
    public class PathChangesWithExtension<TValidator> :
        PathChanges<TValidator> where TValidator : WithExtensionValidator {
        protected override bool IsValidPath(string absolutePath) {
            var extensions = Validator.Extensions;
            var path = Validator.IsValidPath(extensions, absolutePath);
            var extension = Validator.IsValidExtension(extensions, absolutePath);
            return path && extension;
        }
    }
}