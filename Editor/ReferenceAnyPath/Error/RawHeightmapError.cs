namespace ReferenceAnyPath {
    public class RawHeightmapError<TValidator> :
        Error<TValidator> where TValidator : WithExtensionValidator, IRawHeightmapValidator  {
        protected override bool IsInvalidPath(string absolutePath, string assetPath) {
            var extensions = Validator.Extensions;
            if (!Validator.IsValidExtension(extensions, absolutePath))
                return true;

            var width = Property.GetInt(PropertyName._width);
            if (width is null or <= 0)
                return true;

            var height = Property.GetInt(PropertyName._height);
            if (height is null or <= 0)
                return true;

            var bits = Property.GetInt(PropertyName._bits);
            if (bits is null or <= 0 || bits % 8 != 0)
                return true;

#if REFERENCE_ANY_PATH_NO_PARALLEL_CHECK_IN_INSPECTOR
            return false;
#else
            var validationResult =
                ParallelValidator.Validate(Property.Root, extensions, absolutePath,
                    width.Value, height.Value, bits.Value, Validator.IsValidHeightmapFile);

            return validationResult.HasValue && !validationResult.Value;
#endif
        }
    }
}