namespace ReferenceAnyPath {
    public class PathChangesWithoutExtension<TValidator> :
        PathChanges<TValidator> where TValidator : WithoutExtensionValidator {
        protected override bool IsValidPath(string absolutePath) =>
            Validator.IsValidPath(Validator.Extensions, absolutePath);
    }
}