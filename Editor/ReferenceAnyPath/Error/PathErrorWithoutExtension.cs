namespace ReferenceAnyPath {
    public class PathErrorWithoutExtension<TValidator> :
        Error<TValidator> where TValidator : WithoutExtensionValidator {
        protected override bool IsInvalidPath(string absolutePath, string assetPath) {
#if REFERENCE_ANY_PATH_NO_PARALLEL_CHECK_IN_INSPECTOR
            return false;
#else
            var validationResult = ParallelValidator.Validate(
                Property.Root, Validator.Extensions, absolutePath, Validator.IsValidPath);

            return validationResult.HasValue && !validationResult.Value;
#endif
        }
    }
}