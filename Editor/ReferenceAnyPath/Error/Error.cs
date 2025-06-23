namespace ReferenceAnyPath {
    public abstract class Error<TValidator> where TValidator : Validator {
        protected Property Property { get; private set; }
        protected TValidator Validator { get; private set; }

        public void Init(Property property, TValidator validator) {
            Property = property;
            Validator = validator;
        }

        public bool IsError {
            get {
                if (Property.IsError)
                    return true;

                var unpackedRelativePath = Property.GetString(PropertyName._relativePath).UnpackPathSimple();
                var unpackedAbsolutePath = unpackedRelativePath.GetAbsolutePathFromRelativePath();
                if (string.IsNullOrEmpty(unpackedAbsolutePath)) {
                    var unpackedPath = Property.GetString(PropertyName._path).UnpackPathComplex();
                    return !string.IsNullOrEmpty(unpackedPath);
                }

                var unpackedAssetPath = Property.GetString(PropertyName._assetPath).UnpackPathSimple();
                return IsInvalidPath(unpackedAbsolutePath, unpackedAssetPath);
            }
        }

        protected abstract bool IsInvalidPath(string absolutePath, string assetPath);
    }
}