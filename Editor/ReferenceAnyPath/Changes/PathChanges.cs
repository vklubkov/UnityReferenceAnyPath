namespace ReferenceAnyPath {
    public abstract class PathChanges<TValidator> : Changes<TValidator> where TValidator : Validator {
        public override void Apply() {
            var inputProperty = Property.MainProperty;
            var input = inputProperty.stringValue.ApplyUnitySeparators();
            Property.SetString(PropertyName._path, input);

            var unpackedInputPath = input.UnpackPathComplex();
            var unpackedAbsolutePath = unpackedInputPath.GetAbsolutePathFromRelativePath();
            var unpackedRelativePath = unpackedAbsolutePath.GetRelativePathFromAbsolutePath();
            var unpackedAssetPath = unpackedAbsolutePath.GetAssetPathFromAbsolutePath();
            if (!IsValidPath(unpackedAbsolutePath) ||
                !Validator.IsValidAsset(Validator.Extensions, unpackedAssetPath) ) {
                SetProperties(unpackedRelativePath, unpackedAssetPath, null);
                return;
            }

            var unpackedRuntimePath = unpackedAssetPath.GetRuntimePathFromAssetPath();
            SetProperties(unpackedRelativePath, unpackedAssetPath, unpackedRuntimePath);
        }

        void SetProperties(string relativePath, string assetPath, string runtimePath) {
            Property.SetString(PropertyName._relativePath, relativePath.PackPathComplex());
            Property.SetString(PropertyName._assetPath, assetPath.PackPathSimple());
            Property.SetString(PropertyName._runtimePath, runtimePath.PackPathComplex());
        }

        protected abstract bool IsValidPath(string absolutePath);
    }
}