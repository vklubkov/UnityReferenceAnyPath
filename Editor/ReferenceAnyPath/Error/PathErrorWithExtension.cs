namespace ReferenceAnyPath {
    public class PathErrorWithExtension<TValidator> : Error<TValidator> where TValidator : WithExtensionValidator {
        protected override bool IsInvalidPath(string absolutePath, string assetPath) {
            var extensions = Validator.Extensions;
            if (!Validator.IsValidExtension(extensions, absolutePath))
                return true;

#if REFERENCE_ANY_PATH_NO_PARALLEL_CHECK_IN_INSPECTOR
            return false;
#else
            var validationResult = ParallelValidator.Validate(
                Property.Root, extensions, absolutePath, Validator.IsValidPath);

            return validationResult.HasValue && !validationResult.Value;
#endif
        }
    }
}