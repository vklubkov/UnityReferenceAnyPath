namespace ReferenceAnyPath {
    public abstract class PathChanges<TValidator> : Changes<TValidator> where TValidator : Validator {
        public override void Apply() {
            var inputProperty = Property.MainProperty;
            var input = inputProperty.stringValue.ApplyUnitySeparators();
            Property.SetString(PropertyName._path, input);

            var inputPath = input.UnpackPathComplex();
            var absolutePath = inputPath.GetAbsolutePathFromRelativePath();
            var relativePath = absolutePath.GetRelativePathFromAbsolutePath();
            var assetPath = absolutePath.GetAssetPathFromAbsolutePath();
            if (!IsValidPath(absolutePath) ||
                !Validator.IsValidAsset(Validator.Extensions, assetPath) ) {
                SetProperties(relativePath, assetPath, null);
                return;
            }

            var runtimePath = assetPath.GetRuntimePathFromAssetPath();
            SetProperties(relativePath, assetPath, runtimePath);
        }

        void SetProperties(string relativePath, string assetPath, string runtimePath) {
            Property.SetString(PropertyName._relativePath, relativePath.PackPathComplex());
            Property.SetString(PropertyName._assetPath, assetPath.PackPathSimple());
            Property.SetString(PropertyName._runtimePath, runtimePath.PackPathComplex());
        }

        protected abstract bool IsValidPath(string absolutePath);
    }
}