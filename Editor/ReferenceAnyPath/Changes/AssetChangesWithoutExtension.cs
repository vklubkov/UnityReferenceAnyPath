namespace ReferenceAnyPath {
    public class AssetChangesWithoutExtension<TValidator> :
        AssetChanges<TValidator> where TValidator : WithoutExtensionValidator {
        protected override bool IsValidPath(string absolutePath) =>
            Validator.IsValidPath(Validator.Extensions, absolutePath);
    }
}