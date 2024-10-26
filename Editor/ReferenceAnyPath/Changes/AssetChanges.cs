using UnityEditor;
using UnityObject = UnityEngine.Object;

namespace ReferenceAnyPath {
    public abstract class AssetChanges<TValidator> : Changes<TValidator> where TValidator : Validator {
        public override void Apply() {
            var inputProperty = Property.MainProperty;
            var newObject = inputProperty.objectReferenceValue;
            if (newObject == null) {
                SetProperties(null, null, null, null, null);
                return;
            }

            var assetPath = AssetDatabase.GetAssetPath(newObject);
            if (!Validator.IsValidAsset(Validator.Extensions, assetPath))
                return;

            var relativePath = assetPath.GetRelativePathFromAssetPath();
            var absolutePath = relativePath.GetAbsolutePathFromRelativePath();
            if (!IsValidPath(absolutePath))
                return;

            var runtimePath = assetPath.GetRuntimePathFromAssetPath();
            SetProperties(newObject, relativePath, absolutePath, assetPath, runtimePath);
        }

        void SetProperties(
            UnityObject @object,
            string relativePath,
            string absolutePath,
            string assetPath,
            string runtimePath) {
            Property.SetObject(PropertyName._object, @object);
            var path = relativePath.PackPathComplex();
            Property.SetString(PropertyName._path, path);
            Property.SetString(PropertyName._relativePath, path);
            Property.SetString(PropertyName._absolutePath, absolutePath.PackPathSimple());
            Property.SetString(PropertyName._assetPath, assetPath.PackPathSimple());
            Property.SetString(PropertyName._runtimePath, runtimePath.PackPathComplex());
        }

        protected abstract bool IsValidPath(string absolutePath);
    }
}