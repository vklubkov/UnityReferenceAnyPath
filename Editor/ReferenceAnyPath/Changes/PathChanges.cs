namespace ReferenceAnyPath {
    public abstract class PathChanges<TValidator> : Changes<TValidator> where TValidator : Validator {
        public override void Apply() {
            var inputProperty = Property.MainProperty;
            var input = inputProperty.stringValue.ApplyUnitySeparators();
            Property.SetString(PropertyName._path, input);

            var absolutePath = input.GetAbsolutePathFromRelativePath();
            if (!IsValidPath(absolutePath)) {
                SetProperties(null, null, null, null);
                return;
            }

            var relativePath = absolutePath.GetRelativePathFromAbsolutePath();
            var assetPath = absolutePath.GetAssetPathFromAbsolutePath();
            if (!Validator.IsValidAsset(Validator.Extensions, assetPath)) {
                SetProperties(relativePath, absolutePath, null, null);
                return;
            }

            var runtimePath = assetPath.GetRuntimePathFromAssetPath();
            SetProperties(relativePath, absolutePath, assetPath, runtimePath);
        }

        void SetProperties(string relativePath, string absolutePath, string assetPath, string runtimePath) {
            Property.SetString(PropertyName._relativePath, relativePath.PackPathComplex());
            Property.SetString(PropertyName._assetPath, assetPath.PackPathSimple());
            Property.SetString(PropertyName._runtimePath, runtimePath.PackPathComplex());
        }

        protected abstract bool IsValidPath(string absolutePath);
    }
}